using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public class StateButton : MonoBehaviour
    {
        public event Action ButtonSelectedEvent;

        [SerializeField] private Button _button;
        [SerializeField] private Image _background;
        [SerializeField] private Color _selectedColor = new Color(0.1f, 0.7f, 1f);

        private Color _defaultBackgroundColor;
        private bool _isSelected;

        private void Awake()
        {
            _defaultBackgroundColor = _background.color;
            _button.onClick.AddListener(OnButtonSelected);
        }
        private void OnButtonSelected()
        {
            ButtonSelectedEvent?.Invoke();
        }

        public void Select(bool isSelected)
        {
            _isSelected = isSelected;
            _background.color = _isSelected ? _selectedColor : _defaultBackgroundColor;
        }
    }
}
