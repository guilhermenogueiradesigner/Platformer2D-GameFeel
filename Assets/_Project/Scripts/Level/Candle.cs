using JackODrain.Player;
using UnityEngine;

namespace JackODrain.Level
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class Candle : MonoBehaviour
    {
        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out PlayerCandles candles))
                return;

            candles.Collect();
            gameObject.SetActive(false);
        }
    }
}
