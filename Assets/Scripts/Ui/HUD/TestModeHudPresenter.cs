using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui.HUD
{
    public class TestModeHudPresenter : MonoBehaviour
    {
        [Inject] private IGameModeManager _gameModeManager;

        [SerializeField] private Button _toggleGameModeButton;
        [SerializeField] private TextMeshProUGUI _toggleGameModeLabel;

        private void Awake()
        {
            _toggleGameModeButton.onClick.AddListener(ToggleGameMode);
            _toggleGameModeButton.interactable = _gameModeManager.AvatarCamera != null;
            _gameModeManager.PlayerAvatarCreatedEvent += OnPlayerAvatarCreated;
        }

        private void OnPlayerAvatarCreated()
        {
            _toggleGameModeButton.interactable = _gameModeManager.AvatarCamera != null;
        }

        private void ToggleGameMode()
        {
            if (_gameModeManager.CurrentGameMode == GameModeManager.GameMode.Testing)
            {
                _gameModeManager.SwitchGameMode(GameModeManager.GameMode.Creator);
                _toggleGameModeLabel.text = "To TestMode";
            }
            else if (_gameModeManager.CurrentGameMode == GameModeManager.GameMode.Creator)
            {
                _toggleGameModeLabel.text = "To CreatorMode";
                _gameModeManager.SwitchGameMode(GameModeManager.GameMode.Testing);
            }
        }
    }
}
