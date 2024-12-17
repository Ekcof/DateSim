using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Minigame
{
    public class MoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Hamster, Direction>> _filter = default;
        private EcsPoolInject<Hamster> _hamsters = default;
        private EcsPoolInject<Direction> _directions = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var hamster = ref _hamsters.Value.Get(i);
                ref var direction = ref _directions.Value.Get(i);

                var speedFactor = hamster.Speed * Time.deltaTime;
                var hamsterGO = hamster.GO;
                if (hamsterGO != null)
                {
                    hamsterGO.transform.position = new Vector2(hamsterGO.transform.position.x + direction.Value.x * speedFactor,
                        hamsterGO.transform.position.y + direction.Value.y * speedFactor);
                }
                else
                {
                    Debug.LogAssertion("MoveSystem: GameObject of hamster is null!");
                }
            }
        }
    }
}