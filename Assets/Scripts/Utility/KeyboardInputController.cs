using Managers;
using UnityEngine;
using Zenject;

namespace Utility
{
    public class KeyboardInputController : MonoBehaviour
    {
        [Inject] private InputManager _inputManager;

        private void Update()
        {
            var moveInputVector = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                moveInputVector.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveInputVector.x += 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                moveInputVector.y += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveInputVector.y -= 1;
            }
            if (moveInputVector.x != 0 || moveInputVector.y != 0)
            {
                _inputManager.VirtualMoveInput(moveInputVector);
            }
            var lookInputVector = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                lookInputVector.x -= 100;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                lookInputVector.x += 100;
            }
            if (lookInputVector.x != 0)
            {
                _inputManager.VirtualLookInput(lookInputVector);
            }
        }
    }
}