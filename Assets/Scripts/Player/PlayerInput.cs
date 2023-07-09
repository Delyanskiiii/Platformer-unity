using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput
{
    bool last_h_input;
    bool last_v_input;
    int horizontal_input;
    int vertical_input;

    public PlayerInput()
    {
        this.last_h_input = false;
        this.last_v_input = false;
        this.horizontal_input = 0;
        this.vertical_input = 0;
    }

    public Tuple<int, int> input(bool left_input, bool right_input, bool jump_input, bool crouch_input)
    {
        // Right and Left
        if (left_input && right_input)
        {
            if (this.last_h_input)
            {
                if (this.horizontal_input == 1)
                {
                    this.horizontal_input = -1;
                }
                else
                {
                    this.horizontal_input = 1;
                }
                this.last_h_input = false;
            }
        }
        // Left
        else if (left_input && !right_input)
        {
            this.horizontal_input = -1;
            this.last_h_input = true;
        }
        // Right
        else if (!left_input && right_input)
        {
            this.horizontal_input = 1;
            this.last_h_input = true;
        }
        // No horizontal input
        else
        {
            this.horizontal_input = 0;
            this.last_h_input = true;
        }

        // Jumping and crouching
        if (jump_input && crouch_input)
        {
            if (this.last_v_input)
            {
                if (this.vertical_input == 1)
                {
                    this.vertical_input = -1;
                }
                else
                {
                    this.vertical_input = 1;
                }
                this.last_v_input = false;
            }
        }
        // Jumping
        else if (jump_input && !crouch_input)
        {
            this.vertical_input = 1;
            this.last_v_input = true;
        }
        // Crouching
        else if (!jump_input && crouch_input)
        {
            this.vertical_input = -1;
            this.last_v_input = true;
        }
        // No vertical input
        else
        {
            this.vertical_input = 0;
            this.last_v_input = true;
        }

        return Tuple.Create(this.horizontal_input, this.vertical_input);
    }
}
