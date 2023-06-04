using Godot;
using System;

public partial class ork_enemy_1 : CharacterBody2D
{
	public float _speed = 300.0f;
	public Boolean _playerChase = false;
	public Node2D _player = null;
	public AnimatedSprite2D _animationTreeState;


	public override void _Ready()
	{
		_animationTreeState = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animationTreeState.Play("idle");
	}

	public override void _PhysicsProcess(double delta)
	{
		if(_playerChase)
		{
			Position += (_player.Position - Position)/_speed;
			_animationTreeState.Play("walk");

			if(_player.Position.X > Position.X)
			{
				_animationTreeState.FlipH = false;
			} else 
			{
				_animationTreeState.FlipH = true;
			}

		} else 
		{
			_animationTreeState.Play("idle");
		}
	}

	public void OnChaseDetectionAreaBodyEntered(Node2D _body)
	{
		_playerChase = true;
		_player = _body;

		GD.Print(_body);
	}

	private void OnChaseDetectionAreaBodyExited(Node2D _body)
	{
		GD.Print(_body);
		_playerChase = false;
		_player = null;

	}
}
