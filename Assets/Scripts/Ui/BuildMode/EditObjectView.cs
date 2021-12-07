using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Ui.BuildMode
{
    public class EditObjectView : MonoBehaviour
    {
        private const int EditPixelThreshold = 10;

        private enum EditMode
        {
            None = 0,
            Rotate = 1,
            Scale = 2
        }

        [SerializeField] private BuildMenuPresenter _buildMenuPresenter;

        [SerializeField] private StateButton _rotateObjectButton;
        [SerializeField] private StateButton _scaleObjectButton;
        [SerializeField] private Button _deleteObjectButton;
        [SerializeField] private Button _confirmPlacementButton;

        private EditMode _currentMode;
        private GameObject _gameObject;
        
        private Vector2? _firstTouchPosition;
        private Vector2? _secondTouchPosition;
        private float _deltaUniformScale;
        
        private Vector3 _initialScale;

        private void Awake()
        {
            _rotateObjectButton.ButtonSelectedEvent += ActivateRotateMode;
            _scaleObjectButton.ButtonSelectedEvent += ActivateScaleMode;
            _deleteObjectButton.onClick.AddListener(DeleteObject);
            _confirmPlacementButton.onClick.AddListener(_buildMenuPresenter.ConfirmObjectPlacement);
        }

        public void Edit(GameObject go)
        {
            _initialScale = go.transform.localScale;
            _gameObject = go;
        }

        private void ActivateScaleMode()
        {
            SwitchMode(EditMode.Scale);
        }

        private void ActivateRotateMode()
        {
            SwitchMode(EditMode.Rotate);
        }

        private void DeleteObject()
        {
            _gameObject = null;
            SwitchMode(EditMode.None);
            _buildMenuPresenter.DestroyObject();   
        }

        private void OnDisable()
        {
            SwitchMode(EditMode.None);
            _gameObject = null;
        }

        private void Update()
        {
            if (_gameObject == null)
            {
                return;
            }

            if (_currentMode == EditMode.Scale)
            {
                CheckForScaleInput();
            }
            else if (_currentMode == EditMode.Rotate)
            {
                CheckForRotateInput();
            }

        }
        private void CheckForRotateInput()
        {
            if (Input.touchCount == 0)
            {
                return;
            }
            var firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Began)
            {
                _firstTouchPosition = firstTouch.position;
            }
            if (firstTouch.phase == TouchPhase.Ended)
            {
                _firstTouchPosition = null;
            }
            Vector2 deltaRotation = Vector2.zero;
            if (_firstTouchPosition.HasValue && firstTouch.phase == TouchPhase.Moved)
            {
                deltaRotation = (_firstTouchPosition.Value - firstTouch.position);
            }
            if (deltaRotation.magnitude < EditPixelThreshold)
            {
                return;
            }

            _gameObject.transform.RotateAround(_gameObject.transform.position, Vector3.up, deltaRotation.x / 50f);
        }

        private void CheckForScaleInput()
        {
            Touch firstTouch = default;
            Touch secondTouch = default;

            if (Input.touchCount > 0)
            {
                firstTouch = Input.GetTouch(0);
            }
            if (Input.touchCount > 1)
            {
                firstTouch = Input.GetTouch(1);
            }

            if (Input.touchCount >= 1 && firstTouch.phase == TouchPhase.Began)
            {
                _firstTouchPosition = firstTouch.position;
            }
            // if no touches at all or first touch released, reset positions
            if (Input.touchCount == 0 || firstTouch.phase == TouchPhase.Ended)
            {
                _firstTouchPosition = null;
                _secondTouchPosition = null;
            }

            if (Input.touchCount >= 2 && secondTouch.phase == TouchPhase.Began)
            {
                _secondTouchPosition = secondTouch.position;
            }
            if (secondTouch.phase == TouchPhase.Ended)
            {
                _secondTouchPosition = null;
            }
            if (_firstTouchPosition.HasValue && _secondTouchPosition.HasValue)
            {
                var initialTouchDistance = Vector2.Distance(firstTouch.position, secondTouch.position);
                var currentTouchDistance = Vector2.Distance(_firstTouchPosition.Value, _secondTouchPosition.Value);
                if (Mathf.Abs(initialTouchDistance - currentTouchDistance) < EditPixelThreshold)
                {
                    return;
                }
                _deltaUniformScale = currentTouchDistance / initialTouchDistance;

                _gameObject.transform.localScale = _initialScale * _deltaUniformScale;
            }
            else
            {
                _deltaUniformScale = 1;
                _initialScale = _gameObject.transform.localScale;
            }
        }

        private void SwitchMode(EditMode newMode)
        {
            // If button is selected and clicked again, deselect it.
            if (newMode == _currentMode)
            {
                newMode = EditMode.None;
            }
            _currentMode = newMode;
            SelectStateButtonForMode(_currentMode);
        }
        private void SelectStateButtonForMode(EditMode currentMode)
        {
            _scaleObjectButton.Select(currentMode == EditMode.Scale);
            _rotateObjectButton.Select(currentMode == EditMode.Rotate);
        }
    }
}
