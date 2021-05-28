using Godot;
using System;

public class Player : Area2D
{
    [Export]
    public int speed = 500; // Player speed

    [Signal]
    public delegate void Hit();

    private Vector2 _screenSize;//Size if window

    //Signals
    public void _on_Player_body_entered(RigidBody2D enemie)
    {
        Hide();
        EmitSignal("Hit");
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }

    public override void _Ready()
    {
        Hide(); // Hide player at the start 
        this._screenSize = GetViewport().Size;
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
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
        anim.Play();
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
