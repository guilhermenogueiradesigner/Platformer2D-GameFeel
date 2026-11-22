using UnityEngine;
using UnityEngine.UI;

namespace JackODrain.UI
{
    public sealed class IconMeter : MonoBehaviour
    {
        [SerializeField] private Image[] _icons;
        [SerializeField] private Color _filledColor = Color.white;
        [SerializeField] private Color _emptyColor = new(1f, 1f, 1f, 0.2f);

        public void Show(int filledCount)
        {
            for (int index = 0; index < _icons.Length; index++)
                _icons[index].color = index < filledCount ? _filledColor : _emptyColor;
        }
    }
}
