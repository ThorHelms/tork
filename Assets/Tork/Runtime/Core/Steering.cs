using UnityEngine;

namespace Adrenak.Tork {
    public class Steering : MonoBehaviour {
        public float range = 35;
        public float value; // 0..1
        [SerializeField] private float rate = 45;

        private float _angle;

        private Vehicle _vehicle;

        private void Start()
        {
            _vehicle = GetComponentInParent<Vehicle>();
        }

        private void Update() {
            // TODO: Enable Ackermann-steering, and maybe even multi-axle steering

            var destination = value * range;

            _angle = Mathf.MoveTowards(_angle, destination, Time.deltaTime * rate);
            _angle = Mathf.Clamp(_angle, -range, range);

            if (Mathf.Approximately(_angle, 0))
            {
                _angle = 0;
            }

            _vehicle.Drivetrain.BackAxle.SetSteering(0);
            _vehicle.Drivetrain.FrontAxle.SetSteering(_angle);
        }
    }
}
