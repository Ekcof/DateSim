using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Minigame
{
    public class CollisionSystem : IEcsRunSystem
    {
        private EcsCustomInject<MiniGameConfiguration> _configuration = default;
        private EcsFilterInject<Inc<Hamster, Direction>> _filter = default;
        private EcsFilterInject<Inc<Obstacle>> _obstacleFilter = default;
        private EcsWorldInject _world;
        private EcsPoolInject<Hamster> _hamsters = default;
        private EcsPoolInject<Direction> _directions = default;
        private EcsPoolInject<Obstacle> _obstacles = default;
        private EcsPoolInject<CoolDown> _cooldowns = default;
        private EcsPoolInject<SpawnRequest> _spawnRequests = default;
        private EcsPoolInject<Disposable> _disposables = default;

        private const float EPSILON = 0.01f;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var hamster = ref _hamsters.Value.Get(i);
                ref var direction = ref _directions.Value.Get(i);

                // Проверка на столкновение с границами
                if (IsColliding(hamster.Collider, out var colliders))
                {
                    ChangeDirection(ref direction, hamster.GO.transform.position, colliders);
                }

                // Проверка на столкновение с другими хомяками
                foreach (var j in _filter.Value)
                {
                    if (i == j) continue;

                    ref var otherHamster = ref _hamsters.Value.Get(j);
                    ref var otherDirection = ref _directions.Value.Get(j);

                    if (Vector2.Distance(hamster.GO.transform.position, otherHamster.GO.transform.position) < 0.5f) // Радиус столкновения
                    {
                        HandleHamsterCollision(ref hamster, ref otherHamster, i, j);
                    }
                }
            }
        }

        private bool IsColliding(Collider2D collider, out IEnumerable<Collider2D> colliders)
        {
            var results = new List<Collider2D>();
            var filter = new ContactFilter2D();
            filter.SetLayerMask(Physics2D.GetLayerCollisionMask(collider.gameObject.layer));
            collider.OverlapCollider(filter, results);

            colliders = results;
            return results.Count > 0;
        }

        private void ChangeDirection(ref Direction direction, Vector2 position, IEnumerable<Collider2D> colliders)
        {
            foreach (var j in _obstacleFilter.Value)
            {
                ref var obstacle = ref _obstacles.Value.Get(j);

                if (colliders.Contains(obstacle.Collider))
                {
                    var closestPoint = obstacle.Collider.ClosestPoint(position);

                    var targetCollider = obstacle.Collider;

                    Vector2 pointAbove = targetCollider.ClosestPoint(closestPoint + new Vector2(0, EPSILON));
                    Vector2 pointBelow = targetCollider.ClosestPoint(closestPoint + new Vector2(0, -EPSILON));
                    Vector2 pointLeft = targetCollider.ClosestPoint(closestPoint + new Vector2(-EPSILON, 0));
                    Vector2 pointRight = targetCollider.ClosestPoint(closestPoint + new Vector2(EPSILON, 0));

                    Vector2 tangent = (pointRight - pointLeft).normalized + (pointAbove - pointBelow).normalized;
                    Vector2 normal = new Vector2(-tangent.y, tangent.x).normalized;

                    var dir = (closestPoint - position);
                    dir.Normalize();

                    direction.Value = Vector2.Reflect(dir, normal).normalized;
                    break;
                }
            }
        }

        private void HandleHamsterCollision(ref Hamster hamster1, ref Hamster hamster2, int i, int j)
        {
            ref var coolDown1 = ref _cooldowns.Value.Get(i);
            ref var coolDown2 = ref _cooldowns.Value.Get(j);

            if (hamster1.Gender != hamster2.Gender &&
                hamster1.Age == Age.Adult &&
                hamster2.Age == Age.Adult &&
                coolDown1.Value == 0 &&
                coolDown2.Value == 0 &&
                !hamster1.IsBreeding &&
                !hamster2.IsBreeding
                )
            {
                var ent = _world.Value.NewEntity();
                var coolDown = _cooldowns.Value.Add(ent);
                coolDown.Value = _configuration.Value.BreedingTime;
                var spawnRequest = _spawnRequests.Value.Add(ent);
                spawnRequest.Position = hamster1.GO.transform.position;
                _disposables.Value.Add(ent);

                hamster1.IsBreeding = true;
                hamster2.IsBreeding = true;

                hamster1.GO.SetActive(false);
                hamster2.GO.SetActive(false);
            }
        }
    }
}