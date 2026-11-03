# Bolt's Journal - Critical Learnings

## 2025-02-18 - Struct vs Reference Type Allocations for High-Frequency Math Types
**Learning:** In game engine physics loops, using `record class` (reference types) for math primitives like `Vector2D` and `Rect` creates heap allocations on every position/velocity update or frame update, causing GC pressure. Changing `record` / `record class` to `readonly record struct` (value types) eliminates heap allocations completely for vector/rect operations.
**Action:** Use `readonly record struct` or `readonly struct` for small math primitives in performance-critical physics / game loop code.

## 2025-02-18 - Redundant Defensive Validation in Frame-by-Frame Simulation Hot Paths
**Learning:** Reconstructing value types like `Rect` on every frame in physics loops triggers full defensive validations (e.g. `float.IsFinite` and non-negativity checks). When caller state (like `Player.Position` and compile-time dimensions) is already guaranteed valid, running these checks on every frame accounts for over 50% of the update loop overhead.
**Action:** Provide an internal unchecked constructor/factory (`CreateUnchecked`) for performance-critical inner simulation loops while keeping public constructors safe and validated.
