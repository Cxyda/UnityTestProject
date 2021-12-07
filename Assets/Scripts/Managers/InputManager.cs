using Agents;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [Inject] private IGameModeManager _gameModeManager;

        [SerializeField] private CameraTargetController CreatorCameraTarget;
        private IAgentController _agentController;

        [SerializeField] private UIVirtualJoystick _moveJoystick;
        [SerializeField] private UIVirtualJoystick _lookJoystick;

        private void Awake()
        {
            _gameModeManager.GameModeSwitchedEvent += OnGameModeSwitched;
            _moveJoystick.joystickOutputEvent += VirtualMoveInput;
            _lookJoystick.joystickOutputEvent += VirtualLookInput;
        }

        private void OnGameModeSwitched(GameModeManager.GameMode gameMode)
        {
            if (gameMode == GameModeManager.GameMode.Testing || gameMode == GameModeManager.GameMode.Play)
            {
                _agentController = FindPlayerCharacter();
            }
            else
            {
                _agentController = CreatorCameraTarget;
            }
        }

        private IAgentController FindPlayerCharacter()
        {
            var characterController = FindObjectOfType<Agents.PlayerCharacterController>();
            if (characterController != null)
            {
                return characterController;
            }
            // TODO: show notification to the player that a character is required 
            return null;
        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            _agentController.ApplyMoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            _agentController.ApplyLookInput(virtualLookDirection);
        }
    }
}
