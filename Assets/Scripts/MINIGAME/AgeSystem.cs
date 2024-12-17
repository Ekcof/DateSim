using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Minigame
{
    public class AgeSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Hamster, LifeTime, Disposable>> _filter = default;
        private EcsPoolInject<Hamster> _hamsters;
        private EcsPoolInject<LifeTime> _lifetimes;
        private EcsPoolInject<Disposable> _deletables;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var lifeTime = ref _lifetimes.Value.Get(i);
                ref var hamster = ref _hamsters.Value.Get(i);
                ref var deletable = ref _deletables.Value.Get(i);

                lifeTime.Value -= Time.deltaTime;

                if (lifeTime.Value <= 0)
                {
                    deletable.Value = true;
                }
                else if (lifeTime.Value < 20f && hamster.Age != Age.Elder)
                {
                    hamster.Age = Age.Elder;
                    hamster.Speed = 0.5f;
                }
                else if (lifeTime.Value < 40f && hamster.Age != Age.Adult)
                {
                    hamster.Age = Age.Adult;
                    hamster.Speed = 2f;
                }
            }
        }
    }
}