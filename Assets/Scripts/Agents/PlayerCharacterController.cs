using UnityEngine;

namespace Agents
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerCharacterController : AgentController
	{
		private static readonly int AnimatorSpeedStringHash = Animator.StringToHash("Speed");
		private static readonly int AnimatorMotionSpeedStringHash = Animator.StringToHash("MotionSpeed");

		private const float AnimatorRunThreshold = 6f;

		[SerializeField] protected Animator animator;
		[SerializeField] protected Rigidbody rigidBody;

		protected override void OnLateUpdate()
		{
			if (deltaRotation.sqrMagnitude > 0.1f)
			{
				// Add look rotation
				rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(0, deltaRotation.x,0) );
			}

			var speed = deltaVelocity.magnitude;
			if (speed < 0.05f)
			{
				animator.SetFloat(AnimatorSpeedStringHash,  0);
				return;
			}

			// Add movement rotation from strafing
			rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(0, deltaVelocity.x * Time.deltaTime * 30,0) );
			var relativeVelocity = rigidBody.transform.TransformDirection(new Vector3(0,0, deltaVelocity.z) * Time.deltaTime);
			rigidBody.MovePosition(rigidBody.transform.localPosition + relativeVelocity);
			animator.SetFloat(AnimatorSpeedStringHash,  Mathf.Abs(deltaVelocity.z * AnimatorRunThreshold / MaxVelocity));
			animator.SetFloat(AnimatorMotionSpeedStringHash, deltaVelocity.z >= 0 ? 1 : -1);
		}
	}
}