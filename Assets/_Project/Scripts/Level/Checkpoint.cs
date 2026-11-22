using System;
using JackODrain.Player;
using UnityEngine;

namespace JackODrain.Level
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class Checkpoint : MonoBehaviour
    {
        [Tooltip("Where the player reappears. Uses this object when empty.")]
        [SerializeField] private Transform _spawnPoint;

        public event Action Lit;

        public bool IsLit { get; private set; }

        private Vector2 SpawnPosition => (_spawnPoint != null ? _spawnPoint : transform).position;

        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsLit || !other.TryGetComponent(out PlayerRespawn respawn))
                return;

            respawn.SetCheckpoint(SpawnPosition);
            IsLit = true;
            Lit?.Invoke();
        }
    }
}
