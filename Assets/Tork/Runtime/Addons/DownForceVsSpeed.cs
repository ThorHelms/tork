using UnityEngine;

namespace Adrenak.Tork { 
    public class DownForceVsSpeed : MonoBehaviour
    {
        [Tooltip("The down force based on the speed (KMPH)")]
        [SerializeField] AnimationCurve curve = AnimationCurve.Linear(0, 0, 250, 2500);

        private Rigidbody _rigidbody;

        private void Start()
        {
            var vehicle = GetComponentInParent<Vehicle>();
            _rigidbody = vehicle.Rigidbody;
        }

        private void Update() {
            var downForce = GetDownForceAtSpeed(_rigidbody.velocity.magnitude * 3.6f);
            _rigidbody.AddForce(-Vector3.up * downForce);
        }

        private float GetDownForceAtSpeed(float speed) {
            return curve.Evaluate(speed);
        }
    }
}

