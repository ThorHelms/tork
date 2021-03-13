// TODO: Add support for All WD, Rear WD, Front WD. Right now it is only All WD
using UnityEngine;

namespace Adrenak.Tork {
    public class Motor : MonoBehaviour {
        [Tooltip("The maximum torque that the motor generates")]
        public float maxTorque = 10000;

        [Tooltip("Multiplier to the maxTorque")]
        public float value;

        public float m_MaxReverseInput = -.5f;

        public Ackermann ackermann;

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

        private void ApplyMotorTorque() {
            float fs, fp, rs, rp;

            // If we have Ackerman steering, we apply torque based on the steering radius of each wheel
            var radii = AckermannUtils.GetRadii(ackermann.angle, ackermann.GetAxleSeparation(), ackermann.GetFrontAxleWidth());
            var total = radii[0] + radii[1] + radii[2] + radii[3];
            fp = radii[0] / total;
            fs = radii[1] / total;
            rp = radii[2] / total;
            rs = radii[3] / total;

            if (ackermann.angle > 0) {
                _vehicle.FrontAxle.RightWheel.Collider.motorTorque = value * maxTorque * fp;
                _vehicle.FrontAxle.LeftWheel.Collider.motorTorque = value * maxTorque * fs;
                _vehicle.BackAxle.RightWheel.Collider.motorTorque = value * maxTorque * rp;
                _vehicle.BackAxle.LeftWheel.Collider.motorTorque = value * maxTorque * rs;
            }
            else {
                _vehicle.FrontAxle.LeftWheel.Collider.motorTorque = value * maxTorque * fp;
                _vehicle.FrontAxle.RightWheel.Collider.motorTorque = value * maxTorque * fs;
                _vehicle.BackAxle.LeftWheel.Collider.motorTorque = value * maxTorque * rp;
                _vehicle.BackAxle.RightWheel.Collider.motorTorque = value * maxTorque * rs;
            }
        }
    }
}
