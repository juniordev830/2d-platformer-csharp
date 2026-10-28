using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PlatformerGame
{
    // Optimized: Converted from record class to readonly record struct to eliminate heap allocations
    // during frequent physics/position updates in the game loop, reducing GC pressure.
    public readonly record struct Vector2D(float X, float Y)
    {
        // Optimized: 'in' parameters pass struct by readonly reference, avoiding stack copy overhead in hot loops.
        // AggressiveInlining eliminates method call overhead during high-frequency physics tick iterations.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(in Vector2D a, in Vector2D b) => new(a.X + b.X, a.Y + b.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(in Vector2D a, float scalar) => new(a.X * scalar, a.Y * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(float scalar, in Vector2D a) => new(a.X * scalar, a.Y * scalar);
    }

    public readonly record struct Rect(float X, float Y, float Width, float Height)
    {
        // High-performance AABB intersection test using 'in' parameters to avoid 16-byte struct copy overhead in hot collision loops
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
            return point.X >= X && point.X <= X + Width &&
                   point.Y >= Y && point.Y <= Y + Height;
        }
    }

    public class Player
    {
        public Vector2D Position { get; private set; }
        public Vector2D Velocity { get; private set; }
        public bool IsGrounded { get; private set; }
        public int Score { get; private set; }
        public Rect BoundingBox => new(Position.X, Position.Y, 16.0f, 32.0f);

        public Player(float startX, float startY)
        {
            Position = new Vector2D(startX, startY);
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
            
            // Ground collision
            if (Position.Y >= 300.0f)
            {
                Position = new Vector2D(Position.X, 300.0f);
                Velocity = new Vector2D(Velocity.X, 0.0f);
                IsGrounded = true;
            }
        }

        public void AddScore(int points) => Score += points;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🎮 2D Platformer C# Engine starting...");
            var player = new Player(100, 200);

            Console.WriteLine($"Initial Position: ({player.Position.X}, {player.Position.Y})");

            var platform = new Rect(80, 280, 100, 20);
            
            // Simulate 5 frames
            for (int frame = 1; frame <= 5; frame++)
            {
                if (frame == 1) player.Jump();
                player.ApplyGravity(0.1f);
                bool colliding = player.BoundingBox.Intersects(in platform);
                Console.WriteLine($"Frame {frame}: Player Y = {player.Position.Y:F2}, Grounded = {player.IsGrounded}, Platform Collision = {colliding}");
            }
        }
    }
}
