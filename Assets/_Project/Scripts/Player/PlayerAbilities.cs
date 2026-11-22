using System;
using System.Collections.Generic;
using UnityEngine;

namespace JackODrain.Player
{
    public sealed class PlayerAbilities : MonoBehaviour
    {
        [Tooltip("Abilities available from the start. Useful for test scenes.")]
        [SerializeField] private List<Ability> _startingAbilities = new();

        private readonly HashSet<Ability> _unlocked = new();

        public event Action<Ability> Unlocked;

        private void Awake()
        {
            _unlocked.UnionWith(_startingAbilities);
        }

        public bool Has(Ability ability)
        {
            return _unlocked.Contains(ability);
        }

        public void Unlock(Ability ability)
        {
            if (_unlocked.Add(ability))
                Unlocked?.Invoke(ability);
        }
    }
}
