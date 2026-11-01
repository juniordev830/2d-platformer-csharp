using System;
using System.Collections.Generic;

namespace PlatformerGame
{
    public readonly record struct Vector2D(float X, float Y)
    {
        public static Vector2D operator +(in Vector2D a, in Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator *(in Vector2D a, float scalar) => new(a.X * scalar, a.Y * scalar);
        public static Vector2D operator *(float scalar, in Vector2D a) => new(a.X * scalar, a.Y * scalar);
    }

    public readonly record struct Rect(float X, float Y, float Width, float Height)
    {
        public bool Intersects(in Rect other)
        {
            return X < other.X + other.Width &&
                   X + Width > other.X &&
                   Y < other.Y + other.Height &&
                   Y + Height > other.Y;
        }

        public bool Contains(in Vector2D point)
        {
            return point.X >= X && point.X <= X + Width &&
                   point.Y >= Y && point.Y <= Y + Height;
        }
    }

    public class Player
    {
        private Vector2D _position;
        private Rect _boundingBox;

        public Vector2D Position
        {
            get => _position;
            private set
            {
                _position = value;
                _boundingBox = new Rect(_position.X, _position.Y, 16.0f, 32.0f);
            }
        }

        public Vector2D Velocity { get; private set; }
        public bool IsGrounded { get; private set; }
        public int Score { get; private set; }
        public ref readonly Rect BoundingBox => ref _boundingBox;

        public Player(float startX, float startY)
        {
            _position = new Vector2D(startX, startY);
            _boundingBox = new Rect(_position.X, _position.Y, 16.0f, 32.0f);
            Velocity = new Vector2D(0, 0);
            IsGrounded = false;
            Score = 0;
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                Velocity = new Vector2D(Velocity.X, -12.0f);
                IsGrounded = false;
            }
        }

        public void ApplyGravity(float delta)
        {
            Position += Velocity * delta;
            Velocity += new Vector2D(0, 9.8f * delta);

            if (Position.Y >= 300.0f)
            {
                Position = new Vector2D(Position.X, 300.0f);
                Velocity = new Vector2D(Velocity.X, 0.0f);
                IsGrounded = true;
            }
        }

        public void AddScore(int points) => Score += points;
    }

    public static class GameSimulation
    {
        public static void Run()
        {
            Console.WriteLine("🎮 2D Platformer C# Engine starting...");
            var player = new Player(100, 200);

            Console.WriteLine($"Initial Position: ({player.Position.X}, {player.Position.Y})");

            var platform = new Rect(80, 280, 100, 20);

            for (int frame = 1; frame <= 5; frame++)
            {
                if (frame == 1) player.Jump();
                player.ApplyGravity(0.1f);
                bool colliding = player.BoundingBox.Intersects(in platform);
                Console.WriteLine($"Frame {frame}: Player Y = {player.Position.Y:F2}, Grounded = {player.IsGrounded}, Platform Collision = {colliding}");
            }
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            GameSimulation.Run();
        }
    }
}
