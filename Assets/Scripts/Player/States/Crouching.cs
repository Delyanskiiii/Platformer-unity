using System;

class Crouching : State
{
    public override void trigger()
    {
        if (player.input.Item1 == 0 && player.input.Item2 == 0 && player.velocity.Item1 == 0 && player.velocity.Item2 == 0)
        {
            player.changeState(new Idle());
        }
        else if (player.velocity.Item2 != 0)
        {
            player.changeState(new Gliding());
        }
        else if (player.input.Item2 > 0)
        {
            jump(jumpForce);
            player.changeState(new Gliding());
        }
        else if (player.input.Item1 != 0 && player.input.Item2 == 0)
        {
            player.changeState(new Running());
        }
        else
        {
            increaseFrame(8);
            horizontalMovement(crouchingIncrement, crouchingMax);
            verticalMovement(gravityIncrement, gravityMax);
            translate();
        }
    }
}
