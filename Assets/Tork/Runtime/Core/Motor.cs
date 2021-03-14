using UnityEngine;

namespace Adrenak.Tork {
    public class Motor : MonoBehaviour {
        [Tooltip("The maximum torque that the motor generates")]
        public float maxTorque = 10000;

        [Tooltip("Multiplier to the maxTorque")]
        public float value;

        public float m_MaxReverseInput = -.5f;

        private Vehicle _vehicle;

        private void Start()
        {
            _vehicle = GetComponentInParent<Vehicle>();
        }

        private void FixedUpdate() {
            _vehicle.Drivetrain.ApplyMotorTorque(value * maxTorque);
        }

        private void Update() {
            value = Mathf.Clamp(value, m_MaxReverseInput, 1);
        }
    }
}
