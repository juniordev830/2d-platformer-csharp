using System;

namespace PlatformerGame
{
    public static class GameSimulation
    {
        public static void Run()
        {
            Console.WriteLine("2D Platformer C# simulation starting...");

            var player = new Player(100f, 200f);
            var platform = new Rect(80f, 280f, 100f, 20f);

            Console.WriteLine($"Initial Position: ({player.Position.X:F2}, {player.Position.Y:F2})");

            for (int frame = 1; frame <= 8; frame++)
            {
                if (frame == 4)
                {
                    player.Jump();
                }

                var previousBottom = player.BoundingBox.Bottom;
                player.ApplyGravity(0.1f);
                var landed = player.TryLandOn(in platform, previousBottom);

                Console.WriteLine(
                    $"Frame {frame}: Position=({player.Position.X:F2}, {player.Position.Y:F2}), " +
                    $"VerticalVelocity={player.Velocity.Y:F2}, Grounded={player.IsGrounded}, Landed={landed}");
            }
        }
    }
}
