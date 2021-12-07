using UnityEngine;

namespace Agents
{
    /// <summary>
    /// Base class for all agent controllers
    /// </summary>
    public abstract class AgentController : MonoBehaviour, IAgentController
    {
        [SerializeField] protected float MaxVelocity = 3f;
        [SerializeField] protected float MaxRotationSpeed = 1f;
        [SerializeField] protected float Damping = .9f;
        
        protected Vector3 deltaVelocity;
        protected Vector2 deltaRotation;

        public void ApplyMoveInput(Vector2 input)
        {
            deltaVelocity += new Vector3(input.x, 0, input.y);
        }

        public void ApplyLookInput(Vector2 input)
        {
            deltaRotation = input * Time.deltaTime * MaxRotationSpeed;
        }
        
        private void Update()
        {
            deltaVelocity = Vector3.ClampMagnitude(deltaVelocity, MaxVelocity);
        }

        private void LateUpdate()
        {
            OnLateUpdate();
            deltaRotation = Vector2.zero;
            deltaVelocity *= Damping;
        }
        protected virtual void OnLateUpdate()
        {
        }
    }
}
