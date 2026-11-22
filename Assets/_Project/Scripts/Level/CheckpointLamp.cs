using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JackODrain.Level
{
    [RequireComponent(typeof(Checkpoint))]
    public sealed class CheckpointLamp : MonoBehaviour
    {
        [SerializeField] private Light2D _flame;

        private Checkpoint _checkpoint;

        private void Awake()
        {
            _checkpoint = GetComponent<Checkpoint>();
            _flame.enabled = _checkpoint.IsLit;
        }

        private void OnEnable()
        {
            _checkpoint.Lit += Ignite;
        }

        private void OnDisable()
        {
            _checkpoint.Lit -= Ignite;
        }

        private void Ignite()
        {
            _flame.enabled = true;
        }
    }
}
