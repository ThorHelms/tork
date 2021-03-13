using System.Linq;
using UnityEngine;

namespace Adrenak.Tork{
    public class MidAirStabilization : MonoBehaviour
    {
        [Header("Mid Air Stabilization")]
        public float stabilizationTorque = 15000;

        public TorkWheel[] m_Wheels;

        private Rigidbody _rigidbody;

        private void Start()
        {
            var vehicle = GetComponentInParent<Vehicle>();
            _rigidbody = vehicle.Rigidbody;
        }

        private void FixedUpdate() {
            Stabilize();
        }

        private void Stabilize() {
            var inAir = m_Wheels.Where(x => x.isGrounded);
            if (inAir.Count() == 4) return;

            // Try to keep vehicle parallel to the ground while jumping
            var locUp = transform.up;
            var wsUp = new Vector3(0.0f, 1.0f, 0.0f);
            var axis = Vector3.Cross(locUp, wsUp);
            var force = stabilizationTorque;

            _rigidbody.AddTorque(axis * force);
        }
    }
}
