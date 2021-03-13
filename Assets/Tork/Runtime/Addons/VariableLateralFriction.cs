using UnityEngine;

namespace Adrenak.Tork {
    [RequireComponent(typeof(TorkWheelCollider))]
    public class VariableLateralFriction : MonoBehaviour {
        public SlipFrictionCurve curve;
        private TorkWheelCollider wheel;

        private void Awake() => wheel = GetComponent<TorkWheelCollider>();

        private void Update() =>
            wheel.lateralFrictionCoeff = curve.Evaluate(Vector3.Project(wheel.velocity, transform.right).magnitude);
    }
}
