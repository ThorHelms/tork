/*
 * NOTE (A)
 * A car with wheels of diameter ~55cm usually has axle of diameter 2.2cm. That's a ratio of 25.
   Could not get values of real cars to verify the 55 and 2.2 cm claim but hat's what I've read somewhere 
   in a physics problem. This gets me good results. For example: A Civic has 170Nm of torque, at the first 
   gear that's about 530Nm of torque after transmission with a gear ratio of 3.1. If I set the max toque 
   in the motor at 530, I get an acceleration that I'd expect when someone in a Civic floors it.

   I'd imagine heavier vehicles have thicker axles, but their wheels are also large, so may be the 
   ratio would still hold. Or maybe it won't. Please change the value as you prefer.
 */

using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Adrenak.Tork {
    public class TorkWheelCollider : MonoBehaviour, IPoweredWheel, IRpmProvider {
        // See at the top of the file for NOTE (A) to read about this.
        const float engineShaftToWheelRatio = 25;

        [Tooltip("The radius of the wheel")]
        public float radius = 0.25f;

        [Tooltip("A height offset for applying forces, to prevent the vehicle from rolling as much.")]
        [SerializeField] private float _applyForcesOffset;

        [Header("Friction")]
        [Tooltip("Determines the friction force that enables the wheel to exert sideways force while turning." +
        "Values below 1 will cause the wheel to drift. Values above 1 will cause sharp turns." +
        "Values outside 0 and 1 are unnatural. Tip: Reduce this for ice, increase for asphalt.")]
        public float lateralFrictionCoeff = 1;

        [Tooltip("Determines the friction force that enables the wheel to exert forward force while experiencing torque. " +
        "Values below 1 will cause the wheel to slip (like ice). Values above 1 will cause the wheel to have high force (and thus higher speeds). " +
        "Values outside 0 and 1 are unnatural. Tip: Reduce this for ice, increase for asphalt.")]
        public float forwardFrictionCoeff = 1;

        public SlipFrictionCurve curve;

        public Vector3 velocity { get; private set; }

        public float motorTorque { get; set; }

        public float lateralFrictionMultiplier = 300;


        private Rigidbody rb;

        private IWheelCollisionDetector _wheelCollisionDetector;

        private float _rpm;

        private void Start()
        {
            rb = GetComponentInParent<Rigidbody>();
            _wheelCollisionDetector = GetComponent<IWheelCollisionDetector>();
        }

        private void FixedUpdate()
        {
            velocity = rb.GetPointVelocity(transform.position);

            CalculateFriction();
            UpdateRpm();
        }

        private void CalculateFriction()
        {
            var isGrounded = motorTorque > 0
                ? _wheelCollisionDetector.TryGetForwardCollision(out var point, out var direction)
                : _wheelCollisionDetector.TryGetBackwardCollision(out point, out direction);

            if (!isGrounded)
            {
                return;
            }

            var forceOffset = transform.up * _applyForcesOffset;

            var right = transform.right;
            var forward = transform.forward;

            var lateralVelocity = Vector3.Project(velocity, right);
            var forwardVelocity = Vector3.Project(velocity, forward);
            var slip = (forwardVelocity + lateralVelocity) / 2;

            var lateralFriction = Vector3.Project(right, slip).magnitude * lateralVelocity.magnitude * lateralFrictionMultiplier / Time.fixedDeltaTime;
            rb.AddForceAtPosition(-Vector3.Project(slip, lateralVelocity).normalized * lateralFriction, point + forceOffset);

            var motorForce = Mathf.Abs(motorTorque / radius);
            var maxForwardFriction = motorForce * forwardFrictionCoeff;
            var appliedForwardFriction = Mathf.Clamp(motorForce, 0, maxForwardFriction);

            var forwardForce = direction.normalized * appliedForwardFriction * engineShaftToWheelRatio;
            rb.AddForceAtPosition(forwardForce, point + forceOffset);
        }

        public void ApplyTorque(float torque)
        {
            motorTorque = torque;
        }

        private void UpdateRpm()
        {
            var forwardVelocity = Vector3.Dot(velocity, transform.forward);
            var rotPerSec = forwardVelocity / (2 * Mathf.PI * radius);
            _rpm = rotPerSec * 60;
        }

        public float GetRpm() => _rpm;
    }
}