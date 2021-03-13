using UnityEngine;

namespace Adrenak.Tork {
    public class CenterOfMassAssigner : MonoBehaviour
    {
        private void Start()
        {
            var vehicle = GetComponentInParent<Vehicle>();

            if (vehicle?.Rigidbody == null)
                return;
            vehicle.Rigidbody.centerOfMass = vehicle.Rigidbody.transform.InverseTransformPoint(transform.position);
        }
    }
}
