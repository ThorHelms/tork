using UnityEngine;

namespace Adrenak.Tork
{
    public class TorkWheel : MonoBehaviour
    {
        [SerializeField] private Transform _visualWheel;
        private TorkWheelCollider _wheelCollider;
        public TorkWheelCollider Collider => _wheelCollider;

        private void Start()
        {
            _wheelCollider = GetComponentInChildren<TorkWheelCollider>();
        }

        private void Update()
        {
            _visualWheel.localEulerAngles = _wheelCollider.transform.localEulerAngles;
        }
    }
}