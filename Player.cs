using System;

namespace PlatformerGame
{
    /// <summary>Player state and simple frame-based physics for the console prototype.</summary>
    public sealed class Player
    {
        private const float Gravity = 980f;
        private const float JumpVelocity = 360f;
        private const float Width = 20f;
        private const float Height = 30f;

        private Rect _boundingBox;

        public Vector2D Position { get; private set; }
        public Vector2D Velocity { get; private set; }
        public bool IsGrounded { get; private set; }

        // Cache the bounding box and return by readonly reference to avoid redundant Rect construction
        // and validation overhead on every collision and physics check in the hot path.
        public ref readonly Rect BoundingBox => ref _boundingBox;

        public Player(float x, float y)
        {
            if (!float.IsFinite(x)) throw new ArgumentOutOfRangeException(nameof(x));
            if (!float.IsFinite(y)) throw new ArgumentOutOfRangeException(nameof(y));
            Position = new Vector2D(x, y);
            Velocity = Vector2D.Zero;
            _boundingBox = new Rect(x, y, Width, Height);
        }

        public void Jump()
        {
            if (!IsGrounded) return;
            Velocity = new Vector2D(Velocity.X, -JumpVelocity);
            IsGrounded = false;
        }

        public void ApplyGravity(float deltaTime)
        {
            if (!float.IsFinite(deltaTime) || deltaTime < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "Delta time must be finite and non-negative.");
            }

            if (deltaTime == 0f) return;

            var nextVelocityY = Velocity.Y + Gravity * deltaTime;
            var nextY = Position.Y + nextVelocityY * deltaTime;
            if (!float.IsFinite(nextVelocityY) || !float.IsFinite(nextY))
            {
                throw new OverflowException("Physics update produced a non-finite player state.");
            }

            Velocity = new Vector2D(Velocity.X, nextVelocityY);
            Position = new Vector2D(Position.X, nextY);
            _boundingBox = new Rect(Position.X, Position.Y, Width, Height);
        }

        /// <summary>Resolve a landing on a platform when the player's feet cross its top.</summary>
        public bool TryLandOn(in Rect platform, float previousBottom)
        {
            if (Velocity.Y < 0f || previousBottom > platform.Y || _boundingBox.Bottom < platform.Y)
            {
                return false;
            }

            if (_boundingBox.Right <= platform.X || _boundingBox.X >= platform.Right)
            {
                return false;
            }

            Position = new Vector2D(Position.X, platform.Y - Height);
            Velocity = new Vector2D(Velocity.X, 0f);
            _boundingBox = new Rect(Position.X, Position.Y, Width, Height);
            IsGrounded = true;
            return true;
        }
    }
}
