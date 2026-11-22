using JackODrain.Player;
using UnityEngine;

namespace JackODrain.Level
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class Hazard : MonoBehaviour
    {
        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth health))
                health.TakeHit();
        }
    }
}
