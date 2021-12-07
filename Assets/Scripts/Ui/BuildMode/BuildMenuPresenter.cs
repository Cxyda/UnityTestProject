using Agents;
using Managers;
using UnityEngine;
using Utility;
using Zenject;

namespace Ui.BuildMode
{
    public class BuildMenuPresenter : MonoBehaviour
    {
        [Inject] private PrefabFactory _prefabFactory;
        [Inject] private IGameModeManager _gameModeManager;

        [SerializeField] protected CameraTargetController CreatorCameraTarget;
        [SerializeField] protected RectTransform ContentContainer;
        [SerializeField] protected RectTransform BuildMenuPanel;
        [SerializeField] protected EditObjectView EditObjectView;

        private BuildableObjectItemView[] _buildableObjects;
        private GameObject _currentObjectToBuild;
        private EntityType _currentObjectType;
        private Rigidbody _rigidBody;
        private Collider _objectCollider;
        
        private void Awake()
        {
            _gameModeManager.GameModeSwitchedEvent += OnGameModeSwitched;
            // TODO: Load and spawn buildable objects from the GD configuration
            _buildableObjects = ContentContainer.GetComponentsInChildren<BuildableObjectItemView>();
            foreach (var buildableObjectItemView in _buildableObjects)
            {
                buildableObjectItemView.PointerDownEvent += OnBuildObject;
            }
            EditObjectView.gameObject.SetActive(false);
        }

        private void OnGameModeSwitched(GameModeManager.GameMode gameMode)
        {
            ConfirmObjectPlacement();
            var isCreatorMode = gameMode == GameModeManager.GameMode.Creator;
            gameObject.SetActive(isCreatorMode);
            EditObjectView.gameObject.SetActive(isCreatorMode && _currentObjectToBuild != null);
        }

        private void OnBuildObject(BuildableObjectItemView obj)
        {
            if (_currentObjectToBuild != null)
            {
                ConfirmObjectPlacement();
            }
            var position = CreatorCameraTarget.transform.position;

            _currentObjectType = obj.EntityType;
            // spawn object slightly above ground
            _currentObjectToBuild = _prefabFactory.CreateGameObject(obj.Prefab, position, Quaternion.identity);
            _rigidBody = _currentObjectToBuild.GetComponent<Rigidbody>();
            _objectCollider = _currentObjectToBuild.GetComponentInChildren<Collider>();
            _rigidBody.useGravity = false;
            _rigidBody.detectCollisions = false;
            _objectCollider.enabled = false;
            
            EditObjectView.gameObject.SetActive(true);
            EditObjectView.Edit(_currentObjectToBuild);
        }

        public void Update()
        {
            CheckDeviceOrientation();
            if (_currentObjectToBuild == null)
            {
                return;
            }
            SnapObjectToGrid(_currentObjectToBuild);
        }
        private void CheckDeviceOrientation()
        {
            if (Screen.width > 1400)
            {
                BuildMenuPanel.anchoredPosition = new Vector2(0, 100);
                BuildMenuPanel.sizeDelta = new Vector2(-1000, 200);
            }
            else
            {
                BuildMenuPanel.sizeDelta = new Vector2(0, 200);
                BuildMenuPanel.anchoredPosition = new Vector2(0, 500);

            }
        }

        public void ConfirmObjectPlacement()
        {
            if (_currentObjectToBuild == null)
            {
                return;
            }
            if (_currentObjectType == EntityType.Actor)
            {
                _gameModeManager.SetPLayerAvatar(_currentObjectToBuild);
            }
            _rigidBody.useGravity = true;
            _rigidBody.detectCollisions = true;
            _objectCollider.enabled = true;
            EditObjectView.gameObject.SetActive(false);
            ResetState();
        }

        public void DestroyObject()
        {
            EditObjectView.gameObject.SetActive(false);
            Destroy(_currentObjectToBuild);
            ResetState();
        }
        private void ResetState()
        {
            _currentObjectToBuild = null;
            _rigidBody = null;
            _objectCollider = null;
            _currentObjectType = EntityType.Invalid;
        }

        private void SnapObjectToGrid(GameObject currentObjectToBuild)
        {
            var position = CreatorCameraTarget.transform.position;

            var snappedPosition = new Vector3(Mathf.RoundToInt(position.x * 10) / 10f, position.y, Mathf.RoundToInt(position.z * 10) / 10f);
            currentObjectToBuild.transform.localPosition = snappedPosition;
        }
    }
}
