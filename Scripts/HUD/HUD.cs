using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    public void ShowMessage(string text)
    {
        Label message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();
        GetNode<Timer>("MessageTimer").Start();
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over!!!");
        Timer messageTimer = GetNode<Timer>("MessageTimer");
        await ToSignal(messageTimer,"timeout");
        Label message = GetNode<Label>("Message");
        message.Text = "Dodge !!";
        message.Show();
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        GetNode<Button>("StartButton").Show();
    }

    public void ScoreUpdate(int score)
    {
        GetNode<Label>("ScoreLabel").Text = score.ToString();
    }

    public override void _Ready()
    {
        
    }

    //Signal
    public void OnStartButtonPressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal("StartGame");
    }

    //Signal
    public void OnMessageTimerTimeout()
    {
        GetNode<Button>("StartButton").Hide();
    }

//  public override void _Process(float delta)
//  {
//      
//  }
}
