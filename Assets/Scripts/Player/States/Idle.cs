using System;

class Idle : State
{
    public override void trigger()
    {
        if (player.velocity.Item2 != 0)
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
            enterRunning();
        }
        else if (player.input.Item2 < 0)
        {
            player.changeState(new Crouching());
        }
        else
        {
            increaseFrame(8);
            verticalMovement(gravityIncrement, gravityMax);
            translate();
        }
    }
}
