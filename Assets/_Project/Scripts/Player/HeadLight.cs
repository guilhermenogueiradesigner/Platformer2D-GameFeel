using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JackODrain.Player
{
    [RequireComponent(typeof(Light2D))]
    public sealed class HeadLight : MonoBehaviour
    {
        [Tooltip("Intensity added by each candle collected.")]
        [SerializeField, Min(0f)] private float _intensityPerCandle = 0.1f;

        [Tooltip("Tiles of radius added by each candle collected.")]
        [SerializeField, Min(0f)] private float _radiusPerCandle = 0.25f;

        [Tooltip("Candles beyond this number no longer make the light stronger.")]
        [SerializeField, Min(0)] private int _maxBoostingCandles = 5;

        private Light2D _light;
        private PlayerCandles _candles;
        private float _baseIntensity;
        private float _baseOuterRadius;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _candles = GetComponentInParent<PlayerCandles>();

            _baseIntensity = _light.intensity;
            _baseOuterRadius = _light.pointLightOuterRadius;
        }

        private void OnEnable()
        {
            _candles.CountChanged += ApplyBoost;
        }

        private void OnDisable()
        {
            _candles.CountChanged -= ApplyBoost;
        }

        private void ApplyBoost(int candleCount)
        {
            int boostingCandles = Mathf.Min(candleCount, _maxBoostingCandles);

            _light.intensity = _baseIntensity + boostingCandles * _intensityPerCandle;
            _light.pointLightOuterRadius = _baseOuterRadius + boostingCandles * _radiusPerCandle;
        }
    }
}
