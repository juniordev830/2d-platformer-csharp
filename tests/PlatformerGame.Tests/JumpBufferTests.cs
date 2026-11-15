using System;
using PlatformerGame;
using Xunit;

namespace PlatformerGame.Tests
{
    public sealed class JumpBufferTests
    {
        private static readonly Rect Platform = new(0f, 50f, 100f, 15f);

        [Fact]
        public void AirborneJumpShortlyBeforeLandingFiresAfterContact()
        {
            var player = new Player(10f, 0f);
            player.ApplyGravity(0.1f);
            Assert.False(player.IsGrounded);

            player.Jump();
            Assert.True(player.HasBufferedJump);

            var previousBottom = player.BoundingBox.Bottom;
            player.ApplyGravity(0.1f);
            Assert.True(player.TryLandOn(in Platform, previousBottom));

            Assert.False(player.IsGrounded);
            Assert.False(player.HasBufferedJump);
            Assert.True(player.Velocity.Y < 0f);
            Assert.Equal(Platform.Y, player.BoundingBox.Bottom, 3);
        }

        [Fact]
        public void BufferedJumpExpiresBeforeLanding()
        {
            var player = new Player(10f, 0f);
            player.Jump();
            Assert.True(player.HasBufferedJump);

            var previousBottom = player.BoundingBox.Bottom;
            player.ApplyGravity(0.2f);
            Assert.False(player.HasBufferedJump);
            Assert.True(player.TryLandOn(in Platform, previousBottom));

            Assert.True(player.IsGrounded);
            Assert.Equal(0f, player.Velocity.Y);
            Assert.Equal(Platform.Y, player.BoundingBox.Bottom, 3);
        }

        [Fact]
        public void JumpWhileGroundedStillWorksImmediately()
        {
            var player = new Player(10f, 0f);
            var previousBottom = player.BoundingBox.Bottom;
            player.ApplyGravity(0.2f);
            Assert.True(player.TryLandOn(in Platform, previousBottom));
            Assert.True(player.IsGrounded);

            player.Jump();

            Assert.False(player.IsGrounded);
            Assert.False(player.HasBufferedJump);
            Assert.True(player.Velocity.Y < 0f);
        }

        [Fact]
        public void FailedPhysicsUpdateDoesNotConsumePendingInput()
        {
            var player = new Player(10f, 0f);
            player.Jump();
            var originalPosition = player.Position;
            var originalVelocity = player.Velocity;

            Assert.Throws<OverflowException>(() => player.ApplyGravity(float.MaxValue));

            Assert.Equal(originalPosition, player.Position);
            Assert.Equal(originalVelocity, player.Velocity);
            Assert.True(player.HasBufferedJump);
        }

        [Fact]
        public void ZeroDeltaTimeDoesNotConsumeJumpBuffer()
        {
            var player = new Player(10f, 0f);
            player.Jump();
            player.ApplyGravity(0f);
            Assert.True(player.HasBufferedJump);
        }

        [Fact]
        public void BufferedInputDoesNotMovePlayerUntilPlatformContact()
        {
            var player = new Player(10f, 0f);
            var originalPosition = player.Position;
            player.Jump();
            Assert.Equal(originalPosition, player.Position);
            Assert.Equal(0f, player.Velocity.Y);
            Assert.False(player.IsGrounded);
        }
    }
}
