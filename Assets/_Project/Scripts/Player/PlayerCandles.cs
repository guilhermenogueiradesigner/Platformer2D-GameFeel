using System;
using UnityEngine;

namespace JackODrain.Player
{
    public sealed class PlayerCandles : MonoBehaviour
    {
        public event Action<int> CountChanged;

        public int Count { get; private set; }

        public void Collect()
        {
            Count++;
            CountChanged?.Invoke(Count);
        }
    }
}
