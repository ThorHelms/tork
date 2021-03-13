using UnityEngine;

namespace Adrenak.Tork
{
    public class TorkAxle : MonoBehaviour
    {
        [SerializeField] private TorkWheel _leftWheel;
        public TorkWheel LeftWheel => _leftWheel;

        [SerializeField] private TorkWheel _rightWheel;
        public TorkWheel RightWheel => _rightWheel;

        [SerializeField] private float _maxTorque = 10000;

        public float GetAxleWidth()
        {
            return (_leftWheel.transform.position - _rightWheel.transform.position).magnitude;
        }

        public Vector3 GetAxlePosition()
        {
            return (_leftWheel.Collider.transform.position + _rightWheel.Collider.transform.position) / 2;
        }

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
    }
}