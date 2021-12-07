using UnityEngine;

namespace Agents
{
    /// <summary>
    /// Simple camera controller
    /// </summary>
    public class CameraTargetController : AgentController
    {
        protected override void OnLateUpdate()
        {
            var position = transform.localPosition;

            if (Physics.Raycast(transform.position + Vector3.up * 1000, Vector3.down, out var hit))
            {
                position.y = hit.point.y;
                transform.localPosition = position;
            }
            transform.localPosition += transform.TransformDirection(deltaVelocity * Time.deltaTime);
            // invert look input
            transform.RotateAround(position, Vector3.up, -deltaRotation.x);
        }
    }
}
