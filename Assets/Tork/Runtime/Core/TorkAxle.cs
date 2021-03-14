using UnityEngine;

namespace Adrenak.Tork
{
    public class TorkAxle : MonoBehaviour
    {
        [SerializeField] private TorkWheel _leftWheel;
        [SerializeField] private TorkWheel _rightWheel;
        [SerializeField] private float _maxTorque = 10000;

        public void ApplyTorque(float torque)
        {
            _leftWheel.Collider.motorTorque = torque / 2;
            _rightWheel.Collider.motorTorque = torque / 2;
        }

        public float GetMaxTorque()
        {
            return _maxTorque;
        }

        public void SetSteering(float value)
        {
            _leftWheel.Collider.steerAngle = value;
            _rightWheel.Collider.steerAngle = value;
        }

        public void SteerTowards(Vector3 turningPoint, bool left)
        {
            _leftWheel.Collider.SteerTowards(turningPoint, left);
            _rightWheel.Collider.SteerTowards(turningPoint, left);
        }

        public void ResetSteering()
        {
            _leftWheel.Collider.ResetSteering();
            _rightWheel.Collider.ResetSteering();
        }
    }
}