
using System;
using System.Runtime.CompilerServices;

namespace PlatformerGame
{
    public readonly record struct Vector2D(float X, float Y)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Vector2D a, Vector2D b)
            => new(a.X + b.X, a.Y + b.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Vector2D a, Vector2D b)
            => new(a.X - b.X, a.Y - b.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(Vector2D v, float scalar)
            => new(v.X * scalar, v.Y * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(float scalar, Vector2D v)
            => new(v.X * scalar, v.Y * scalar);
    }

    public readonly record struct Rect(
        float X,
        float Y,
        float Width,
        float Height)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Intersects(in Rect other)
        {
            return X < other.X + other.Width &&
                   X + Width > other.X &&
                   Y < other.Y + other.Height &&
                   Y + Height > other.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in Vector2D point)
        {
            return point.X >= X &&
                   point.X <= X + Width &&
                   point.Y >= Y &&
                   point.Y <= Y + Height;
        }
    }

    public class Player
    {
        private const float GroundY = 300.0f;
        private const float PlayerWidth = 16.0f;
        private const float PlayerHeight = 32.0f;
        private const float Gravity = 9.8f;
        private const float JumpVelocity = -12.0f;

        public Vector2D Position { get; private set; }
        public Vector2D Velocity { get; private set; }
        public bool IsGrounded { get; private set; }
        public int Score { get; private set; }

        public Rect BoundingBox =>
            new(Position.X, Position.Y, PlayerWidth, PlayerHeight);

        public Player(float startX, float startY)
        {
            Position = new Vector2D(startX, startY);
            Velocity = new Vector2D(0.0f, 0.0f);
            IsGrounded = startY + PlayerHeight >= GroundY;
            Score = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Jump()
        {
            if (!IsGrounded)
                return;

            Velocity = new Vector2D(Velocity.X, JumpVelocity);
            IsGrounded = false;
        }

        public void ApplyGravity(float delta)
        {
            if (delta <= 0.0f)
                return;

            Position += Velocity * delta;
            Velocity += new Vector2D(0.0f, Gravity * delta);

            if (Position.Y + PlayerHeight >= GroundY)
            {
                Position = new Vector2D(
                    Position.X,
                    GroundY - PlayerHeight);

                Velocity = new Vector2D(Velocity.X, 0.0f);
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }

        public void AddScore(int points)
        {
            Score += points;
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("2D Platformer C# Engine starting...");

            var player = new Player(100.0f, 268.0f);

            Console.WriteLine(
                $"Initial Position: ({player.Position.X}, {player.Position.Y})");

            var platform = new Rect(80.0f, 280.0f, 100.0f, 20.0f);

            for (int frame = 1; frame <= 5; frame++)
            {
                if (frame == 1)
                    player.Jump();

                player.ApplyGravity(0.1f);

                bool colliding = player.BoundingBox.Intersects(in platform);

                Console.WriteLine(
                    $"Frame {frame}: " +
                    $"Player Y = {player.Position.Y:F2}, " +
                    $"Grounded = {player.IsGrounded}, " +
                    $"Platform Collision = {colliding}");
            }

            var otherPlatform =
                new Rect(90.0f, 290.0f, 100.0f, 20.0f);

            Console.WriteLine(
                $"Collision with platform: " +
                $"{player.BoundingBox.Intersects(in otherPlatform)}");

            Console.WriteLine(
                $"Point inside platform: " +
                $"{platform.Contains(new Vector2D(100.0f, 290.0f))}");

            player.AddScore(10);

            Console.WriteLine($"Player Score: {player.Score}");
        }
    }
}
