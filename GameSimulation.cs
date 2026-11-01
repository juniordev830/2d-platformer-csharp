using System;

namespace PlatformerGame
{
    public static class GameSimulation
    {
        public static void Run()
        {
            Console.WriteLine("🎮 2D Platformer C# Engine starting...");

            var player = new Player(100f, 200f);
            Console.WriteLine($"Initial Position: ({player.Position.X}, {player.Position.Y})");

            var platform = new Rect(80f, 280f, 100f, 20f);

            for (int frame = 1; frame <= 5; frame++)
            {
                if (frame == 1)
                {
                    player.Jump();
                }

                player.ApplyGravity(0.1f);

                bool colliding = player.BoundingBox.Intersects(in platform);
                Console.WriteLine(
                    $"Frame {frame}: Player Y = {player.Position.Y:F2}, Grounded = {player.IsGrounded}, Platform Collision = {colliding}");
            }
        }
    }
}
