using UnityEngine;

namespace Adrenak.Tork{
    public class MidAirSteering : MonoBehaviour
    {
        [Header("Mid Air Steer")]
        public float midAirSteerTorque = 1500;
        public float midAirSteerInput;
        private Rigidbody _rigidbody;

        private void Start()
        {
            var vehicle = GetComponentInParent<Vehicle>();
            _rigidbody = vehicle.Rigidbody;
        }

        private void FixedUpdate() {
            SteerMidAir();
        }

        private void SteerMidAir() {
            if (!Mathf.Approximately(midAirSteerInput, 0))
                _rigidbody.AddTorque(new Vector3(0, midAirSteerInput * midAirSteerTorque, 0));
        }
    }
}
