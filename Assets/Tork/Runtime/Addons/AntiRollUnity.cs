using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adrenak.Tork {
	public class AntiRollUnity : MonoBehaviour
    {
		[Serializable]
		public class Axle {
			public WheelCollider left;
			public WheelCollider right;
			public float force;
		}

        public List<Axle> axles;
        private Rigidbody _rigidbody;

		private void Start()
        {
            var vehicle = GetComponentInParent<Vehicle>();
            _rigidbody = vehicle.Rigidbody;
        }

		void FixedUpdate() {
			foreach(var axle in axles) {
				var wsDown = transform.TransformDirection(Vector3.down);
				wsDown.Normalize();

				var travelL = Mathf.Clamp01(axle.left.GetCompressionRatio()) ;
				var travelR = Mathf.Clamp01(axle.right.GetCompressionRatio());
				var antiRollForce = (travelL - travelR) * axle.force;

				if (axle.left.isGrounded)
                    _rigidbody.AddForceAtPosition(wsDown * -antiRollForce, axle.left.GetHit().point);
			
				if (axle.right.isGrounded)
                    _rigidbody.AddForceAtPosition(wsDown * antiRollForce, axle.right.GetHit().point);
			}
		}
	}
}
