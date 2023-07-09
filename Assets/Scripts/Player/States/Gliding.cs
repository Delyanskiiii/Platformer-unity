using System;

class Gliding : State
{
    public override void trigger()
    {
        if (player.input.Item1 == 0 && player.input.Item2 == 0 && player.velocity.Item1 == 0 && player.velocity.Item2 == 0 && player.velocityLeftOver.Item2 < 0)
        {
            player.changeState(new Idle());
        }
        else if (player.input.Item1 != 0 && player.input.Item2 == 0 && player.velocity.Item2 == 0 && player.velocityLeftOver.Item2 < 0)
        {
            enterRunning();
        }
        else if (player.input.Item2 < 0 && (player.velocity.Item1 > crouchingMax || player.velocity.Item1 < -crouchingMax) && player.velocity.Item2 == 0 && player.velocityLeftOver.Item2 < 0)
        {
            if ((Math.Abs(player.velocityLeftOver.Item2) / 2 > player.velocity.Item1) && player.velocity.Item1 > 0)
            {
                player.velocity = Tuple.Create(Math.Abs(player.velocityLeftOver.Item2) / 2, player.velocity.Item2);
            }
            else if ((Math.Abs(player.velocityLeftOver.Item2) / 2 < player.velocity.Item1) && player.velocity.Item1 > 0)
            {
                player.velocity = Tuple.Create(player.velocity.Item1 + Math.Abs(player.velocityLeftOver.Item2) / 2, player.velocity.Item2);
            }
            else if ((-Math.Abs(player.velocityLeftOver.Item2) / 2 < player.velocity.Item1) && player.velocity.Item1 < 0)
            {
                player.velocity = Tuple.Create(-Math.Abs(player.velocityLeftOver.Item2) / 2, player.velocity.Item2);
            }
            else if ((-Math.Abs(player.velocityLeftOver.Item2) / 2 > player.velocity.Item1) && player.velocity.Item1 < 0)
            {
                player.velocity = Tuple.Create(player.velocity.Item1 - Math.Abs(player.velocityLeftOver.Item2) / 2, player.velocity.Item2);
            }
            player.changeState(new Sliding());
        }
        else if (player.input.Item2 < 0 && player.velocity.Item1 <= crouchingMax && player.velocity.Item1 >= -crouchingMax && player.velocity.Item2 == 0 && player.velocityLeftOver.Item2 < 0)
        {
            player.changeState(new Crouching());
        }
        else
        {
            if (player.velocityLeftOver.Item1 != 0 && Math.Abs(player.velocityLeftOver.Item1) > jumpForce)
            {
                player.velocity = Tuple.Create(player.velocity.Item1, Math.Abs(player.velocityLeftOver.Item1));
            }
            else if ((player.input.Item2 > 0 && player.velocity.Item2 == 0 && player.velocityLeftOver.Item2 < 0) || (player.velocityLeftOver.Item1 != 0 && Math.Abs(player.velocityLeftOver.Item1) < jumpForce && player.input.Item2 > 0))
            {
                jump(jumpForce);
            }
            if (player.input.Item2 < 0)
            {
                if (player.velocity.Item2 % divingIncrement != 0)
                {
                    player.velocity = Tuple.Create(player.velocity.Item1, player.velocity.Item2 - player.velocity.Item2 % divingIncrement);
                }
                verticalMovement(divingIncrement, divingMax);
            }
            else
            {
                verticalMovement(gravityIncrement, gravityMax);
            }

            increaseFrame(8);
            horizontalMovement(glidingIncrement, glidingMax);
            translate();
        }
    }
}
