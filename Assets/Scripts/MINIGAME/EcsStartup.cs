using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
namespace Minigame
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] SceneData _sceneData;
        [SerializeField] MiniGameConfiguration _configuration;
        private EcsWorld _world;
        private EcsSystems _systems;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems
                .Add(new MoveSystem())
                .Add(new CollisionSystem())
                .Add(new AgeSystem())
                .Add(new UserInteractionSystem())
                .Add(new DeleteSystem())
                .Add(new SpawnSystem())
#if UNITY_EDITOR
                    .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(_sceneData, _configuration)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}