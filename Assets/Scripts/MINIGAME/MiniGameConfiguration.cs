using UnityEngine;

namespace Minigame
{
    [CreateAssetMenu(fileName = "Minigame", menuName = "Game Resources/Minigame/Configuration")]
    sealed class MiniGameConfiguration : ScriptableObject
    {
        [SerializeField] private float _breedingCooldown;
        [SerializeField] private float _breedingTime;
        [SerializeField] private float _hamsterSpeed;
        [SerializeField] private float _hamsterLifetime;
        [SerializeField] private float _hamsterCountLimit;
        [SerializeField] private GameObject _hamsterPrefab;

        public float BreedingCooldown => _breedingCooldown;
        public float BreedingTime => _breedingTime;
        public float HamsterLifetime => _hamsterLifetime;
        public float HamsterCountLimit => _hamsterCountLimit;
        public GameObject HamsterPrefab => _hamsterPrefab;
    }
}