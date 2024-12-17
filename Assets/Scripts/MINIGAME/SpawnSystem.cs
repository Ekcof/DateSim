using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
namespace Minigame
{
    public class SpawnSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsFilterInject<Inc<SpawnRequest, CoolDown, Disposable>> _filter = default;
        private EcsPoolInject<SpawnRequest> _spawnRequests = default;
        private EcsPoolInject<CoolDown> _coolDowns = default;
        private EcsPoolInject<Disposable> _disposables = default;
        private EcsCustomInject<MiniGameConfiguration> _configuration = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var spawn = ref _spawnRequests.Value.Get(i);
                ref var coolDown = ref _coolDowns.Value.Get(i);
                ref var disposable = ref _disposables.Value.Get(i);
                if (!disposable.Value && coolDown.Value < 0)
                {
                    disposable.Value = true;
                    Spawn(spawn);
                }
            }
        }

        void Spawn(SpawnRequest spawn)
        {
            var entity = _world.Value.NewEntity();

            var hamsters = _world.Value.GetPool<Hamster>();
            var directions = _world.Value.GetPool<Direction>();
            var lifeTimes = _world.Value.GetPool<LifeTime>();
            var disposables = _disposables.Value;

            ref var hamster = ref hamsters.Add(entity);
            hamster.Color = GetRandomColor();
            hamster.Gender = GetRandomGender();
            hamster.Age = Age.Child;
            hamster.Speed = 1f;

            ref var direction = ref directions.Add(entity);
            direction.Value = GetRandomDirection();

            ref var lifeTime = ref lifeTimes.Add(entity);
            lifeTime.Value = 60f;

            ref var disposable = ref disposables.Add(entity);
            disposable.Value = false;

            var position = spawn.Position;

            var hamsterGO = UnityEngine.GameObject.Instantiate(_configuration.Value.HamsterPrefab, position, Quaternion.identity);
            hamsterGO.GetComponent<SpriteRenderer>().color = hamster.Color;

            var hamsterCollider = hamsterGO.GetComponent<Collider2D>();
            hamster.Collider = hamsterCollider;

            hamster.Collisions = new();
        }

        Color GetRandomColor()
        {
            var colors = new Color[] { Color.white, Color.gray, Color.black };
            return colors[Random.Range(0, colors.Length)];
        }

        Gender GetRandomGender()
        {
            return (Gender)Random.Range(0, 2);
        }

        Vector2 GetRandomDirection()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}