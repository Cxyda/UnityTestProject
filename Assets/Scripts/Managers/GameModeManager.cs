using System;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public interface IGameModeManager
    {
        event Action<GameModeManager.GameMode> GameModeSwitchedEvent;
        event Action PlayerAvatarCreatedEvent;

        GameModeManager.GameMode CurrentGameMode { get; }
        CinemachineVirtualCamera AvatarCamera { get; }

        void SwitchGameMode(GameModeManager.GameMode gameMode);
        void SetPLayerAvatar(GameObject avatarObject);
    }
    public class GameModeManager : MonoBehaviour, IGameModeManager
    {
        public enum GameMode
        {
            None = 0,
            Creator = 10,
            Testing = 20,
            Play = 30,
            
        }
        public event Action<GameMode> GameModeSwitchedEvent;
        public event Action PlayerAvatarCreatedEvent;

        public GameMode CurrentGameMode => _currentGameMode;
        public CinemachineVirtualCamera AvatarCamera => _playModeCamera;

        [SerializeField] private CinemachineVirtualCamera _creatorModeCamera;
        [SerializeField] private CinemachineVirtualCamera _playModeCamera;

        private GameMode _currentGameMode;

        private void Start()
        {
            SwitchGameMode(GameMode.Creator);
        }

        public void SwitchGameMode(GameMode gameMode)
        {
            if (gameMode == GameMode.Play || gameMode == GameMode.Testing)
            {
                _creatorModeCamera.enabled = false;
                if (_playModeCamera == null)
                {
                    // TODO: show notification to the player if an actor is missing
                    throw new Exception("Playable avatar is missing. Please place one before switching to test mode");
                }

                _playModeCamera.enabled = true;
            }
            else
            {
                _creatorModeCamera.enabled = true;
                if (_playModeCamera != null)
                {
                    _playModeCamera.enabled = false;
                }
            }
            _currentGameMode = gameMode;
            GameModeSwitchedEvent?.Invoke(_currentGameMode);
        }

        public void SetPLayerAvatar(GameObject avatarObject)
        {
            _playModeCamera = avatarObject.GetComponentInChildren<CinemachineVirtualCamera>();
            PlayerAvatarCreatedEvent?.Invoke();
        }
    }
}
