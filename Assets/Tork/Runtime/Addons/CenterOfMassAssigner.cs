using UnityEngine;

namespace Adrenak.Tork {
    public class CenterOfMassAssigner : MonoBehaviour
    {
        private void Start()
        {
            var rb = GetComponentInParent<Rigidbody>();

            if (rb == null)
                return;
            rb.centerOfMass = rb.transform.InverseTransformPoint(transform.position);
        }
    }
}
