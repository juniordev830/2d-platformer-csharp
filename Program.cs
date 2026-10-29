using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PlatformerGame
{
    // Optimized: Added operator overloading and AggressiveInlining with 'in' parameters for zero-copy high-frequency math operations.
    public readonly record struct Vector2D(float X, float Y)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(in Vector2D a, in Vector2D b) => new(a.X + b.X, a.Y + b.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(in Vector2D a, in Vector2D b) => new(a.X - b.X, a.Y - b.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(in Vector2D v, float scalar) => new(v.X * scalar, v.Y * scalar);
    }

    // Optimized: Added zero-allocation AABB collision detection with AggressiveInlining and 'in' parameter to avoid copying 16-byte structs.
    public readonly record struct Rect(float X, float Y, float Width, float Height)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Intersects(in Rect other)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Jump()
        {
            if (IsGrounded)
            {
                Velocity = new Vector2D(Velocity.X, -12.0f);
                IsGrounded = false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyGravity(float delta)
        {
            Vector2D newPos = Position + Velocity * delta;
            float newVy = Velocity.Y + 9.8f * delta;
            
            // Ground collision
            if (newPos.Y >= 300.0f)
            {
                newPos = new Vector2D(newPos.X, 300.0f);
                newVy = 0.0f;
                IsGrounded = true;
            }

            Position = newPos;
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

            // Demonstrate optimized AABB collision check
            var platform = new Rect(90, 290, 100, 20);
            var playerBounds = new Rect(player.Position.X, player.Position.Y, 16, 32);
            Console.WriteLine($"Collision with platform: {playerBounds.Intersects(platform)}");
        }
    }
}
