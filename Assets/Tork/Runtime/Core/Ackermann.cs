using UnityEngine;

namespace Adrenak.Tork {
    /// <summary>
    /// An implementation of Ackermann steering mechanism
    /// </summary>
    public class Ackermann : MonoBehaviour {
        public float angle;

        public float GetAxleSeparation()
        {
            var leftSeparation =
                (_vehicle.FrontAxle.LeftWheel.Collider.transform.position -
                 _vehicle.BackAxle.LeftWheel.Collider.transform.position).magnitude;
            var rightSeparation =
                (_vehicle.FrontAxle.RightWheel.Collider.transform.position -
                 _vehicle.BackAxle.RightWheel.Collider.transform.position).magnitude;
            return (leftSeparation + rightSeparation) / 2;
        }

        public float GetFrontAxleWidth() => _vehicle.FrontAxle.GetAxleWidth();

        private Vehicle _vehicle;

        private void Start()
        {
            _vehicle = GetComponentInParent<Vehicle>();
        }

        private void Update() {
            var farAngle = AckermannUtils.GetSecondaryAngle(angle, GetAxleSeparation(), GetFrontAxleWidth());

            // The rear wheels are always at 0 steer in Ackermann
            _vehicle.BackAxle.LeftWheel.Collider.steerAngle = _vehicle.BackAxle.RightWheel.Collider.steerAngle = 0;

            if (Mathf.Approximately(angle, 0))
                _vehicle.FrontAxle.LeftWheel.Collider.steerAngle = _vehicle.FrontAxle.RightWheel.Collider.steerAngle = 0;

            _vehicle.FrontAxle.LeftWheel.Collider.steerAngle = angle;
            _vehicle.FrontAxle.RightWheel.Collider.steerAngle = farAngle;
        }

        public float[] CurrentRadii {
            get { return AckermannUtils.GetRadii(angle, GetAxleSeparation(), GetFrontAxleWidth()); }
        }
    }
}
