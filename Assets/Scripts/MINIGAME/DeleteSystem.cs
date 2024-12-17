using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
namespace Minigame
{
    public class DeleteSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Disposable>> _filter = default;
        private EcsPoolInject<Disposable> _disposables;
        private EcsPoolInject<Hamster> _hamsters;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var disposable = ref _disposables.Value.Get(i);

                if (disposable.Value)
                {
                    DeleteEntity(i);
                }
            }
        }

        private void DeleteEntity(int entityId)
        {
            // Try Get the hamster component
            ref var hamster = ref _hamsters.Value.Get(entityId);
            var go = hamster.GO != null ? hamster.GO : null;


            _world.Value.DelEntity(entityId);
            if (go != null)
                UnityEngine.GameObject.Destroy(go);
        }
    }
}