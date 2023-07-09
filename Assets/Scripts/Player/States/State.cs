using System;

public abstract class State
{
    protected Player player;
    protected int frame = 0;
    protected int gravityMax = 28;
    protected int gravityIncrement = 2;
    protected int runningMax = 12;
    protected int runningIncrement = 2;
    protected int glidingMax = 12;
    protected int glidingIncrement = 2;
    protected int crouchingMax = 4;
    protected int crouchingIncrement = 1;
    protected int slidingIncrement = 2;
    protected int divingMax = 48;
    protected int divingIncrement = 4;
    protected int jumpForce = 30;

    public void SetState(Player player)
    {
        this.player = player;
        frame = 0;
    }
    public abstract void trigger();

    public void increaseFrame(int frames)
    {
        frame++;
        if (frame == frames + 1)
        {
            frame = 1;
        }
    }
    public void enterRunning()
    {
        if (player.velocity.Item1 < -runningMax)
        {
            player.velocity = Tuple.Create(-runningMax, player.velocity.Item2);
        }
        else if (player.velocity.Item1 > runningMax)
        {
            player.velocity = Tuple.Create(runningMax, player.velocity.Item2);
        }
        player.changeState(new Running());
    }
    public void jump(int initForce)
    {
        player.velocity = Tuple.Create(player.velocity.Item1, initForce);
    }
    public void verticalMovement(int increment, int maxSpeed)
    {
        if (player.velocity.Item2 > -maxSpeed)
        {
            player.velocity = Tuple.Create(player.velocity.Item1, player.velocity.Item2 - increment);
        }
    }
    public void horizontalMovement(int increment, int maxSpeed)
    {
        if (player.velocity.Item1 % increment != 0)
        {
            player.velocity = Tuple.Create(player.velocity.Item1 + 1, player.velocity.Item2);
        }
        if (player.input.Item1 > 0 && player.velocity.Item1 < maxSpeed)
        {
            player.velocity = Tuple.Create(player.velocity.Item1 + increment, player.velocity.Item2);
        }
        else if (player.input.Item1 < 0 && player.velocity.Item1 > -maxSpeed)
        {
            player.velocity = Tuple.Create(player.velocity.Item1 - increment, player.velocity.Item2);
        }
        else if (player.input.Item1 == 0)
        {
            if (player.velocity.Item1 > 0)
            {
                player.velocity = Tuple.Create(player.velocity.Item1 - increment, player.velocity.Item2);
            }
            else if (player.velocity.Item1 < 0)
            {
                player.velocity = Tuple.Create(player.velocity.Item1 + increment, player.velocity.Item2);
            }
        }
    }

    public void translate()
    {
        player.checkForCollision();

        if (player.position.x >= 0)
        {
            if (player.position.x % 10 < 5)
            {
                player.roundedPosition.x = player.position.x - player.position.x % 10;
            }
            else
            {
                player.roundedPosition.x = player.position.x - player.position.x % 10 + 10;
            }
        }
        else
        {
            if (player.position.x % 10 > -5)
            {
                player.roundedPosition.x = player.position.x - player.position.x % 10;
            }
            else
            {
                player.roundedPosition.x = player.position.x - player.position.x % 10 - 10;
            }
        }

        if (player.position.x >= 0)
        {
            if (player.position.y % 10 < 5)
            {
                player.roundedPosition.y = player.position.y - player.position.y % 10;
            }
            else
            {
                player.roundedPosition.y = player.position.y - player.position.y % 10 + 10;
            }
        }
        else
        {
            if (player.position.y % 10 > -5)
            {
                player.roundedPosition.y = player.position.y - player.position.y % 10;
            }
            else
            {
                player.roundedPosition.y = player.position.y - player.position.y % 10 - 10;
            }
        }

        player.transform.position = player.position;
    }
}