using System.Collections.Generic;
using UnityEngine;
namespace Minigame
{
    public struct Hamster
    {
        public Color Color;
        public Gender Gender;
        public Age Age;
        public bool IsBreeding;
        public float Speed;
        public GameObject GO;
        public Collider2D Collider;
        public List<Collider2D> Collisions;
    }

    public struct Direction
    {
        public Vector2 Value;
    }

    public struct LifeTime
    {
        public float Value;
    }

    public struct Disposable
    {
        public bool Value;
    }

    public struct SpawnRequest
    {
        public Vector2 Position;
        public bool IsCompleted;
    }

    public struct CoolDown
    {
        public float Value;
    }

    public struct Obstacle
    {
        public Collider2D Collider;
    }

    public enum Gender { Male, Female }
    public enum Age { Child, Adult, Elder }
}