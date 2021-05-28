using Godot;
using System;

public class Mob : RigidBody2D
{
    [Export]
    public int MinSpeed = 150;

    [Export]
    public int MaxSpeed = 250;

    static private Random _random = new Random();

    //Signals
    public void _on_VisibilityNotifier2D_screen_exited()
    {
        QueueFree();
    }

    private AnimatedSprite randomAnim()
    {
        AnimatedSprite anim = GetNode<AnimatedSprite>("AnimatedSprite");
        string[] mobTypes = anim.Frames.GetAnimationNames();
        anim.Animation = mobTypes[_random.Next(0,mobTypes.Length)];
        return anim;
    }
    public override void _Ready()
    {
        
    }

//  public override void _Process(float delta)
//  {
//      
//  }
}
