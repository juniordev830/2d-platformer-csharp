using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PlatformerGame
{
    // Optimized: Converted from record class to readonly record struct to eliminate heap allocations
    // during frequent physics/position updates in the game loop, reducing GC pressure.
    public readonly record struct Vector2D(float X, float Y)
    {
        // Optimized: Inlined vector addition passing by readonly reference (`in`) to avoid struct copying overhead.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(in Vector2D a, in Vector2D b) => new(a.X + b.X, a.Y + b.Y);

        // Optimized: Inlined vector scalar multiplication passing by readonly reference (`in`) to avoid struct copying overhead.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(in Vector2D v, float scalar) => new(v.X * scalar, v.Y * scalar);
    }

    public readonly record struct Rect(float X, float Y, float Width, float Height)
    {
        // Optimized: Uses `in` parameter passing (by readonly reference) to prevent 16-byte struct copy overhead
        // during high-frequency AABB collision checks in the game loop. AggressiveInlining eliminates method call overhead.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Intersects(in Rect other)
        {
            return X < other.X + other.Width &&
                   X + Width > other.X &&
                   Y < other.Y + other.Height &&
                   Y + Height > other.Y;
        }
    }

    public class Player
    {
        public Vector2D Position { get; private set; }
        public Vector2D Velocity { get; private set; }
        public bool IsGrounded { get; private set; }
        public int Score { get; private set; }

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
            float newY = Position.Y + Velocity.Y * delta;
            float newVy = Velocity.Y + 9.8f * delta;
            
            // Ground collision
            if (newY >= 300.0f)
            {
                newY = 300.0f;
                newVy = 0.0f;
                IsGrounded = true;
            }

            Position = new Vector2D(Position.X, newY);
            Velocity = new Vector2D(Velocity.X, newVy);
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
            
            // Simulate 5 frames
            for (int frame = 1; frame <= 5; frame++)
            {
                if (frame == 1) player.Jump();
                player.ApplyGravity(0.1f);
                Console.WriteLine($"Frame {frame}: Player Y = {player.Position.Y:F2}, Grounded = {player.IsGrounded}");
            }
        }
    }
}
