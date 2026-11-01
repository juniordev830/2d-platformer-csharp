using System;
using PlatformerGame;
using Xunit;

namespace PlatformerGame.Tests
{
    public class PhysicsTests
    {
        [Fact]
        public void VectorAdditionAndMagnitudeAreCorrect()
        {
            var vector = new Vector2D(3f, 4f) + new Vector2D(1f, 2f);

            Assert.Equal(new Vector2D(4f, 6f), vector);
            Assert.Equal(MathF.Sqrt(52f), vector.Magnitude, 4);
        }

        [Fact]
        public void RectanglesIntersectOnlyWhenTheirAreasOverlap()
        {
            var first = new Rect(0f, 0f, 10f, 10f);
            var overlapping = new Rect(9f, 9f, 5f, 5f);
            var touching = new Rect(10f, 0f, 5f, 5f);

            Assert.True(first.Intersects(in overlapping));
            Assert.False(first.Intersects(in touching));
        }

        [Fact]
        public void RectangleRejectsNegativeDimensions()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rect(0f, 0f, -1f, 10f));
        }

        [Fact]
        public void JumpAppliesUpwardVelocityAndGravityChangesIt()
        {
            var player = new Player(10f, 20f);

            player.ApplyGravity(0.1f);
            Assert.True(player.Velocity.Y > 0f);

            var platform = new Rect(-10f, 80f, 100f, 20f);
            for (var i = 0; i < 10 && !player.IsGrounded; i++)
            {
                var previousBottom = player.BoundingBox.Bottom;
                player.ApplyGravity(0.1f);
                player.TryLandOn(in platform, previousBottom);
            }

            Assert.True(player.IsGrounded);
            player.Jump();
            Assert.True(player.Velocity.Y < 0f);
            Assert.False(player.IsGrounded);
        }

        [Fact]
        public void NegativeDeltaTimeIsRejected()
        {
            var player = new Player(0f, 0f);

            Assert.Throws<ArgumentOutOfRangeException>(() => player.ApplyGravity(-0.1f));
        }
        [Fact]
        public void PhysicsOverflowIsRejectedWithoutMutatingPlayerState()
        {
            var player = new Player(1f, 2f);
            var originalPosition = player.Position;
            var originalVelocity = player.Velocity;

            Assert.Throws<OverflowException>(() => player.ApplyGravity(float.MaxValue));
            Assert.Equal(originalPosition, player.Position);
            Assert.Equal(originalVelocity, player.Velocity);
        }

        [Fact]
        public void RectangleRejectsOverflowingBounds()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Rect(float.MaxValue, 0f, float.MaxValue, 1f));
        }

    }
}
