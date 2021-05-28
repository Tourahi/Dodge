using Godot;
using System;

public class Player : Area2D
{
    [Export]
    public int speed = 500; // Player speed

    private Vector2 _screenSize;//Size if window

    public override void _Ready()
    {
        this._screenSize = GetViewport().Size;
    }

    public void Animate(AnimatedSprite anim, Vector2 velocity)
    {
        if(velocity.x != 0)
        {
            anim.Animation = "walk";
            anim.FlipV = false;
            anim.FlipH = (velocity.x < 0);
        }
        else if(velocity.y != 0)
        {
            anim.Animation = "up";
            anim.FlipH = false;
            anim.FlipV = (velocity.y > 0);
        }
    }
    public override void _Process(float delta)
    {
        Vector2 velocity = new Vector2();
        if (Input.IsActionPressed("ui_right"))
        {
            velocity.x += 1;
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            velocity.x -= 1;
        }
        else if (Input.IsActionPressed("ui_up"))
        {
            velocity.y -= 1;
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            velocity.y += 1;
        }

        AnimatedSprite animation = GetNode<AnimatedSprite>("AnimatedSprite");
        
        if (velocity.Length() > 0)
        {
            //Here no need for normalizing because we only 
            //catch one PressAction per frame
            this.Animate(animation, velocity);
            velocity = velocity * this.speed;
            animation.Play();
        }
        else
        {
            animation.Stop();
        }
        Position += velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, this._screenSize.x),
            y: Mathf.Clamp(Position.y, 0, this._screenSize.y)   
        );
    }
}