using JackODrain.Player;
using UnityEngine;

namespace JackODrain.UI
{
    [RequireComponent(typeof(IconMeter))]
    public sealed class CandleMeter : MonoBehaviour
    {
        [SerializeField] private PlayerCandles _candles;

        private IconMeter _meter;

        private void Awake()
        {
            _meter = GetComponent<IconMeter>();
        }

        private void OnEnable()
        {
            _candles.CountChanged += _meter.Show;
        }

        private void OnDisable()
        {
            _candles.CountChanged -= _meter.Show;
        }

        private void Start()
        {
            _meter.Show(_candles.Count);
        }
    }
}
