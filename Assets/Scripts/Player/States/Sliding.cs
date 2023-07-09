using System;

class Sliding : State
{
    private int initDelay = 15;
    public override void trigger()
    {
        if (player.input.Item1 == 0 && player.input.Item2 == 0 && player.velocity.Item1 == 0 && player.velocity.Item2 == 0)
        {
            player.changeState(new Idle());
        }
        else if (player.input.Item2 > 0)
        {
            jump(jumpForce);
            player.changeState(new Gliding());
        }
        else if (player.velocity.Item2 != 0)
        {
            player.changeState(new Gliding());
        }
        else if ((player.input.Item1 != 0 && player.input.Item2 == 0) || (player.velocity.Item1 != 0 && player.input.Item2 == 0))
        {
            enterRunning();
        }
        else if (player.input.Item2 < 0 && player.velocity.Item1 <= crouchingMax && player.velocity.Item1 >= -crouchingMax)
        {
            player.changeState(new Crouching());
        }
        else
        {
            if (initDelay > 0)
            {
                initDelay--;
            }
            increaseFrame(8);
            if (player.velocity.Item1 > crouchingMax && initDelay == 0)
            {
                player.velocity = Tuple.Create(player.velocity.Item1 - slidingIncrement, player.velocity.Item2);
                initDelay = 5;
            }
            else if (player.velocity.Item1 < -crouchingMax && initDelay == 0)
            {
                player.velocity = Tuple.Create(player.velocity.Item1 + slidingIncrement, player.velocity.Item2);
                initDelay = 5;
            }
            verticalMovement(gravityIncrement, gravityMax);
            translate();
        }
    }
}
