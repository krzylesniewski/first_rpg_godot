using Godot;
using System;

public partial class GUI : CanvasLayer
{
	Control _gameMenu;
	Control _optionsMenu;

	public override void _Input(InputEvent @event)
	{
			base._Input(@event);
			if(@event.IsActionPressed("GameMenu"))
			{
				_gameMenu.Visible = !_gameMenu.Visible;
			}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_gameMenu =  GetNode<Control>("GameMenu");
		_optionsMenu =  GetNode<Control>("OptionsMenu");

		_optionsMenu.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnOptionsPressed()
	{
		_optionsMenu.Visible = true;
		_gameMenu.Visible = false;
		GD.Print("Options");
	}
	public void OnOptionsMenuBackPressed()
	{
		_optionsMenu.Visible = false;
		_gameMenu.Visible = true;
	}
}
