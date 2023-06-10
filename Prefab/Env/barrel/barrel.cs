using Godot;
using System;

public partial class barrel : StaticBody2D
{
	
	public Sprite2D _idleState;
	public Sprite2D _openState;
	public Sprite2D _killState;
	public Control _interactLabel;

	public AudioStreamPlayer2D _openEffect;
	public AudioStreamPlayer2D _closeEffect;

	Boolean _canHaveInteraction = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_idleState = GetNode<Sprite2D>("Close");
		_openState = GetNode<Sprite2D>("Open");
		_killState = GetNode<Sprite2D>("Killed");
		_interactLabel = GetNode<Control>("Control");

		// audio load
		_openEffect = GetNode<AudioStreamPlayer2D>("OpenEffect");
		_closeEffect = GetNode<AudioStreamPlayer2D>("CloseEffect");

		_idleState.Visible = true;
		_openState.Visible = false;
		_killState.Visible = false;
		_interactLabel.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("InteractWithEnv") & _canHaveInteraction)
		{
			GD.Print("Zostałam zmacana przez playera");

			if(_idleState.Visible){
				handleOpen();
			} else 
			{
				handleClose();
			}


		}
	}

	public void OnAreaBodyEntered(Node2D _body)
	{
		if(_body.Name == "Player")
		{
			GD.Print("weszedł xD");
			_canHaveInteraction = true;
			_interactLabel.Visible = true;
		}
	}
	public void OnAreaBodyExited(Node2D _body)
	{
		if(_body.Name == "Player")
		{
			GD.Print("poszedł se xD");
			_canHaveInteraction = false;
			_interactLabel.Visible = false;

			if(_openState.Visible)
			{
				handleClose();
			}

		}
	}

	private void handleClose()
	{
		_idleState.Visible = true;
		_openState.Visible = false;

		_closeEffect.Play();
	}

	private void handleOpen()
	{
		_idleState.Visible = false;
		_openState.Visible = true;

		_openEffect.Play();
	}
}
