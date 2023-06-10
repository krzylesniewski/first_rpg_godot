using Godot;
using System;

public partial class interaction : Area2D
{
	// [Signal] public delegate void PlayerInside();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void OnBodyEntered(Node2D body)
	{
		if(body.Name == "Player")
		{
			GD.Print("Interact start");
		}
	}

		void OnBodyExited(Node2D body)
	{
		if(body.Name == "Player")
		{
			GD.Print("Interact end");
		}
	}

	
}
