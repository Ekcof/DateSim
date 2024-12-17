using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
namespace Minigame
{
    public class UserInteractionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Hamster, Disposable>> _filter = default;
        private EcsPoolInject<Disposable> _disposables;
        private EcsPoolInject<Hamster> _hamsters;
        public void Run(IEcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                foreach (var i in _filter.Value)
                {

                    ref var hamster = ref _hamsters.Value.Get(i);

                    if (hamster.GO != null)
                    {
                        var pos = hamster.GO.transform.position;

                        if (Vector2.Distance(pos, mousePosition) < 0.5f) // Радиус клика
                        {
                            ref var deletable = ref _disposables.Value.Get(i);
                            deletable.Value = true;
                            break;
                        }
                    }
                    else
                    {
                        Debug.LogAssertion("UserInteractionSystem: GameObject of hamster is null!");
                    }
                }
            }
        }
    }
}