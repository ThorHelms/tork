using UnityEngine;

namespace Adrenak.Tork {
    public class MaxSteerAngleVsSpeed : MonoBehaviour
    {
        [Tooltip("The steering angle based on the speed (KMPH)")]
        [SerializeField] AnimationCurve curve = AnimationCurve.Linear(0, 35, 250, 5);

        private Steering _steering;
        private Rigidbody _rigidbody;

        private void Start()
        {
            var vehicle = GetComponentInParent<Vehicle>();
            _rigidbody = vehicle.Rigidbody;
            _steering = vehicle.Steering;
        }


        void Update() {
            _steering.range = GetMaxSteerAtSpeed(_rigidbody.velocity.magnitude * 3.6f);
        }

        public float GetMaxSteerAtSpeed(float speed) {
            return curve.Evaluate(speed);
        }
    }
}
