using UnityEngine;

namespace Adrenak.Tork {
    [RequireComponent(typeof(TorkWheel))]
    public class VariableLateralFriction : MonoBehaviour {
        public SlipFrictionCurve curve;
        private TorkWheel wheel;

        private void Awake() => wheel = GetComponent<TorkWheel>();

        private void Update() =>
            wheel.lateralFrictionCoeff = curve.Evaluate(Vector3.Project(wheel.velocity, transform.right).magnitude);
    }
}
