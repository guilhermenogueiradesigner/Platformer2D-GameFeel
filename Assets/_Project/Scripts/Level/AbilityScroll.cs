using JackODrain.Player;
using UnityEngine;

namespace JackODrain.Level
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class AbilityScroll : MonoBehaviour
    {
        [SerializeField] private Ability _ability;

        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out PlayerAbilities abilities))
                return;

            abilities.Unlock(_ability);
            gameObject.SetActive(false);
        }
    }
}
