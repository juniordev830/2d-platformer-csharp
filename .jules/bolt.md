# Bolt's Journal - Critical Learnings

## 2025-02-19 - Caching Derived Bounding Box Structs with Ref Readonly Return
**Learning:** In 2D game loops, player collision checks against level geometry (e.g., platforms, hazards, items) query `player.BoundingBox` multiple times per frame. If `BoundingBox` is an expression-bodied property returning `new Rect(...)`, it repeatedly computes and copies a struct on every query (e.g. 30-100 queries/frame). Caching the `Rect` on `Position` mutation and returning `ref readonly Rect` eliminates repeated struct creation and avoids defensive struct copies during `Intersects(in Rect)` checks, yielding a ~2.5x speedup in collision loops.
**Action:** Cache derived bounding boxes/colliders upon entity position changes and expose them via `ref readonly` rather than recomputing them in property getters.

## 2025-02-18 - Struct vs Reference Type Allocations for High-Frequency Math Types
**Learning:** In game engine physics loops, using `record class` (reference types) for math primitives like `Vector2D` and `Rect` creates heap allocations on every position/velocity update or frame update, causing GC pressure. Changing `record` / `record class` to `readonly record struct` (value types) eliminates heap allocations completely for vector/rect operations.
**Action:** Use `readonly record struct` or `readonly struct` for small math primitives in performance-critical physics / game loop code.
