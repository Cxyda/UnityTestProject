using UnityEngine;
using Zenject;

namespace Managers
{
    public interface IUiManager
    {
        
    }
    public class UiManager : MonoBehaviour, IUiManager
    {
        [Inject] private IGameModeManager _gameModeManager;

        [Header("GameMenus")]
        [SerializeField] private GameObject MainMenu;

        [Header("GameMode UIs")]
        [SerializeField] private GameObject CreatorUI;
        [SerializeField] private GameObject TestingUI;
        [SerializeField] private GameObject PlayUI;

        private GameObject _currentUi;

        private void Awake()
        {
            _gameModeManager.GameModeSwitchedEvent += OnGameModeSwitched;
        }

        private void OnGameModeSwitched(GameModeManager.GameMode newGameMode)
        {
            if (_currentUi != null)
            {
                _currentUi.SetActive(false);
            }

            switch (newGameMode)
            {
                case GameModeManager.GameMode.Creator:
                    CreatorUI.SetActive(true);
                    break;
                case GameModeManager.GameMode.Testing:
                    TestingUI.SetActive(true);
                    break;
                case GameModeManager.GameMode.Play:
                    PlayUI.SetActive(true);
                    break;
                default:
                    MainMenu.SetActive(true);
                    break;
            }
        }
    }
}
