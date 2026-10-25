# Bolt's Journal - Critical Learnings

## 2025-02-18 - Struct vs Reference Type Allocations for High-Frequency Math Types
**Learning:** In game engine physics loops, using `record class` (reference types) for math primitives like `Vector2D` and `Rect` creates heap allocations on every position/velocity update or frame update, causing GC pressure. Changing `record` / `record class` to `readonly record struct` (value types) eliminates heap allocations completely for vector/rect operations.
**Action:** Use `readonly record struct` or `readonly struct` for small math primitives in performance-critical physics / game loop code.
