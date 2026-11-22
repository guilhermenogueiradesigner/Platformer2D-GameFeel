using JackODrain.Player;
using UnityEngine;

namespace JackODrain.UI
{
    [RequireComponent(typeof(IconMeter))]
    public sealed class HealthMeter : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;

        private IconMeter _meter;

        private void Awake()
        {
            _meter = GetComponent<IconMeter>();
        }

        private void OnEnable()
        {
            _health.HeartsChanged += _meter.Show;
        }

        private void OnDisable()
        {
            _health.HeartsChanged -= _meter.Show;
        }

        private void Start()
        {
            _meter.Show(_health.Hearts);
        }
    }
}
