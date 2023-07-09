using System;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected State state;
    public Vector2 position;
    public Vector2 roundedPosition;
    public Tuple<int, int> velocity;
    public Tuple<int, int> velocityLeftOver;
    public Tuple<int, int> input;

    PlayerInput player_input = new PlayerInput();

    void Start()
    {
        velocity = Tuple.Create(0, 0);
        velocityLeftOver = Tuple.Create(0, 0);
        position = new Vector2(-50, 50);
        //Time.timeScale = 0.02f;
        input = player_input.input(Input.GetKey(KeyCode.LeftArrow), Input.GetKey(KeyCode.RightArrow), Input.GetKey(KeyCode.Space), Input.GetKey(KeyCode.DownArrow));
        changeState(new Idle());
    }

    private void FixedUpdate()
    {
        state.trigger();
    }

    void Update()
    {
        input = player_input.input(Input.GetKey(KeyCode.LeftArrow), Input.GetKey(KeyCode.RightArrow), Input.GetKey(KeyCode.Space), Input.GetKey(KeyCode.DownArrow));
    }

    public void changeState(State state)
    {
        Debug.Log(state);
        this.state = state;
        this.state.SetState(this);
        if (this.state is Running)
        {
            transform.localScale = new Vector3(1, 0.7f, 1);
        }
        else if (this.state is Idle)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (this.state is Crouching)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        }
        else if (this.state is Sliding)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else if (this.state is Gliding)
        {
            transform.localScale = new Vector3(0.7f, 1, 1);
        }
        this.state.trigger();
    }

    public void checkForCollision()
    {
        velocityLeftOver = Tuple.Create(0, 0);
        int initXVel = velocity.Item1;
        int initYVel = velocity.Item2;

        int x_change;
        int y_change;

        if (initXVel > 0)
        {
            initXVel += 1;
            x_change = 1;
        }
        else
        {
            initXVel -= 1;
            x_change = -1;
        }
        if (initYVel > 0)
        {
            initYVel += 1;
            y_change = 1;
        }
        else
        {
            initYVel -= 1;
            y_change = -1;
        }

        float ratio;
        
        if (initXVel == 0)
        {
            initXVel = 1;
        }
        if (initYVel == 0)
        {
            initYVel = 1;
        }
        ratio = initXVel / initYVel;

        Vector2 position_to_be = position + new Vector2(velocity.Item1, velocity.Item2);

        while (position != position_to_be)
        {
            var xEmpty = true;
            var yEmpty = true;

            if ((position.x + 50) % 100 == 0)
            {
                if ((position.y + 50) % 100 == 0)
                {
                    if (FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y))
                    {
                        xEmpty = false;
                    }
                }
                else
                {
                    if (position.y % 100 > 0)
                    {
                        if (position.y % 100 > 50)
                        {
                            if (FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y - position.y % 100 + 50) || FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y - position.y % 100 + 150))
                            {
                                xEmpty = false;
                            }
                        }
                        else
                        {
                            if (FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y - position.y % 100 + 50) || FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y - position.y % 100 - 50))
                            {
                                xEmpty = false;
                            }
                        }
                    }
                    else
                    {
                        if (position.y % 100 < -50)
                        {
                            if (FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y - position.y % 100 - 50) || FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y - position.y % 100 - 150))
                            {
                                xEmpty = false;
                            }
                        }
                        else
                        {
                            if (FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y - position.y % 100 + 50) || FindObjectOfType<World>().isEmpty(position.x + 100 * x_change, position.y - position.y % 100 - 50))
                            {
                                xEmpty = false;
                            }
                        }
                    }
                }

            }

            if ((position.y + 50) % 100 == 0)
            {
                if ((position.x + 50) % 100 == 0)
                {
                    if (FindObjectOfType<World>().isEmpty(position.x, position.y + 100 * y_change))
                    {
                        yEmpty = false;
                    }
                }
                else
                {
                    if (position.x % 100 > 0)
                    {
                        if (position.x % 100 > 50)
                        {
                            if (FindObjectOfType<World>().isEmpty(position.x - position.x % 100 + 50, position.y + 100 * y_change) || FindObjectOfType<World>().isEmpty(position.x - position.x % 100 + 150, position.y + 100 * y_change))
                            {
                                yEmpty = false;
                            }
                        }
                        else
                        {
                            if (FindObjectOfType<World>().isEmpty(position.x - position.x % 100 + 50, position.y + 100 * y_change) || FindObjectOfType<World>().isEmpty(position.x - position.x % 100 - 50, position.y + 100 * y_change))
                            {
                                yEmpty = false;
                            }
                        }
                    }
                    else
                    {
                        if (position.x % 100 < -50)
                        {
                            if (FindObjectOfType<World>().isEmpty(position.x - position.x % 100 - 50, position.y + 100 * y_change) || FindObjectOfType<World>().isEmpty(position.x - position.x % 100 - 150, position.y + 100 * y_change))
                            {
                                yEmpty = false;
                            }
                        }
                        else
                        {
                            if (FindObjectOfType<World>().isEmpty(position.x - position.x % 100 + 50, position.y + 100 * y_change) || FindObjectOfType<World>().isEmpty(position.x - position.x % 100 - 50, position.y + 100 * y_change))
                            {
                                yEmpty = false;
                            }
                        }
                    }
                }

            }

            if (xEmpty && yEmpty)
            {
                if (initXVel != x_change && initYVel != y_change)
                {
                    if (math.abs(ratio - (initXVel + x_change) / (initYVel + y_change)) < math.abs(ratio - (initXVel + x_change) / (initYVel)) && math.abs(ratio - (initXVel + x_change) / (initYVel + y_change)) < math.abs(ratio - (initXVel) / (initYVel + y_change)))
                    {
                        position.x += x_change;
                        initXVel -= x_change;
                        position.y += y_change;
                        initYVel -= y_change;
                    }
                    else if (math.abs(ratio - (initXVel + x_change) / (initYVel)) < math.abs(ratio - (initXVel + x_change) / (initYVel + y_change)) && math.abs(ratio - (initXVel + x_change) / (initYVel)) < math.abs(ratio - (initXVel) / (initYVel + y_change)))
                    {
                        position.x += x_change;
                        initXVel -= x_change;
                    }
                    else
                    {
                        position.y += y_change;
                        initYVel -= y_change;
                    }
                }
                else if (initXVel != x_change)
                {
                    position.x += x_change;
                    initXVel -= x_change;
                }
                else if (initYVel != y_change)
                {
                    position.y += y_change;
                    initYVel -= y_change;
                }
            }
            else if (xEmpty && initXVel != x_change)
            {
                position.x += x_change;
                initXVel -= x_change;
            }
            else if (yEmpty && initYVel != y_change)
            {
                position.y += y_change;
                initYVel -= y_change;
            }
            else
            {
                if (position.x != position_to_be.x)
                {
                    velocityLeftOver = Tuple.Create(velocity.Item1, velocityLeftOver.Item2);
                    velocity = Tuple.Create(0, velocity.Item2);
                }
                if (position.y != position_to_be.y)
                {
                    velocityLeftOver = Tuple.Create(velocityLeftOver.Item1, velocity.Item2);
                    velocity = Tuple.Create(velocity.Item1, 0);
                }
                break;
            }
        }
    }
}