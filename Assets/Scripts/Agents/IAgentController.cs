using UnityEngine;

namespace Agents
{
    public interface IAgentController
    {
        void ApplyMoveInput(Vector2 input);
        void ApplyLookInput(Vector2 input);
    }
}
