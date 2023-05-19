using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = -400.0f;

	public AnimationPlayer _animationPlayer;
	public Sprite2D _sprite2dRun;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_sprite2dRun = GetNode<Sprite2D>($"Sprite2D_run");


		_animationPlayer.Play("idle_front");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();
		if (direction != Vector2.Zero)
		{
			_animationPlayer.Play("run_side");
			if(direction.X > 0)
			{
				_sprite2dRun.FlipH = true;
			} else
			{
				_sprite2dRun.FlipH = false;
			}
			
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			_animationPlayer.Play("idle_front");
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		if(direction == Vector2.Zero)
		{
			_animationPlayer.Play("idle_front");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
