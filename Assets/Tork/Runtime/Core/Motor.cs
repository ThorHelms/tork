// TODO: Add support for All WD, Rear WD, Front WD. Right now it is only All WD
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
            ApplyMotorTorque();
        }

        private void Update() {
            value = Mathf.Clamp(value, m_MaxReverseInput, 1);
        }

        private void ApplyMotorTorque()
        {
            var frontMaxTorque = _vehicle.FrontAxle.GetMaxTorque();
            var backMaxTorque = _vehicle.BackAxle.GetMaxTorque();

            var torque = Mathf.Abs(value * maxTorque);
            var maxTorquePerAxle = torque / 2;

            var frontTorque = maxTorquePerAxle;
            var backTorque = maxTorquePerAxle;

            if (frontMaxTorque < maxTorquePerAxle)
            {
                frontTorque = frontMaxTorque;
                backTorque = Mathf.Clamp(torque - frontMaxTorque, 0, backMaxTorque);
            }
            else if (backMaxTorque < maxTorquePerAxle)
            {
                frontTorque = Mathf.Clamp(torque - backMaxTorque, 0, frontMaxTorque);
                backTorque = backMaxTorque;
            }

            var sign = value < 0 ? -1 : 1;

            _vehicle.FrontAxle.ApplyTorque(frontTorque * sign);
            _vehicle.BackAxle.ApplyTorque(backTorque * sign);
        }
    }
}
