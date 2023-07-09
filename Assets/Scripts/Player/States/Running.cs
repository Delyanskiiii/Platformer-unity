using System;
using System.Diagnostics;

class Running : State
{
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
        else if (player.input.Item2 < 0 && (player.velocity.Item1 > crouchingMax || player.velocity.Item1 < -crouchingMax))
        {
            player.changeState(new Sliding());
        }
        else if (player.input.Item2 < 0 && player.velocity.Item1 <= crouchingMax && player.velocity.Item1 >= -crouchingMax)
        {
            player.changeState(new Crouching());
        }
        else
        {
            increaseFrame(8);
            horizontalMovement(runningIncrement, runningMax);
            verticalMovement(gravityIncrement, gravityMax);
            translate();
        }
    }
}
