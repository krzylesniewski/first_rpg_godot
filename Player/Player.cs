using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = -400.0f;

	public AnimationPlayer _animationPlayer;
	public AnimationTree _animationTree;
	public Sprite2D _sprite2dIdle;
	public Sprite2D _sprite2dWalk;

	public AnimationNodeStateMachinePlayback _animationTreeState;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_sprite2dIdle = GetNode<Sprite2D>($"Sprite2D_idle");
		_sprite2dWalk = GetNode<Sprite2D>($"Sprite2D_walk");
		_animationTree = GetNode<AnimationTree>($"AnimationTree");
		_animationTreeState = (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/playback");
		_animationTreeState.Travel("idle");

		_sprite2dIdle.Visible = true;
		_sprite2dWalk.Visible = false;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();
		if (direction != Vector2.Zero)
		{
			GD.Print(direction);
			_animationTree.Set("parameters/idle/blend_position", direction);
			_animationTree.Set("parameters/walk/blend_position", direction);

			_animationTreeState.Travel("walk");
			
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			_animationTreeState.Travel("idle");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
