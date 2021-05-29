using Godot;
using System;

public class Main : Node
{
    [Export]
    public PackedScene Mob;

    private int _score;

    private Random _random = new Random();

    //Signals
    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
    }

    public void OnMobTimerTimeout()
    {
        PathFollow2D mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.Offset = _random.Next();
        RigidBody2D mob = (RigidBody2D)Mob.Instance();
        AddChild(mob);
        float dir = mobSpawnLocation.Rotation + Mathf.Pi / 2;
        mob.Position = mobSpawnLocation.Position;
        dir += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = dir;
        mob.LinearVelocity = new Vector2(RandRange(150f, 250f), 0).Rotated(dir);
    }


    public void OnScoreTimerTimeout()
    {
        _score++;
    }

    public void OnStartTimerTimeout()
    {
        Player player = GetNode<Player>("Player");
        Position2D startPos = GetNode<Position2D>("StartPosition");
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
        player.Start(startPos.Position);
    }

    public void NewGame()
    {
        this._score = 0;
        GetNode<Timer>("StartTimer").Start();
    }

    public override void _Ready()
    {
        NewGame();
    }

    public float RandRange(float min,float max)
    {
        return (float)this._random.NextDouble() * (max - min) + min;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
