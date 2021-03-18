using UnityEngine;

namespace Adrenak.Tork
{
    public class WheelVisualSync : MonoBehaviour
    {
        [SerializeField] private Transform _visualWheel;
        private TorkWheelCollider _wheelCollider; // TODO: Use an interface, or remove this logic

        private float _rotationAngle;

        private void Start()
        {
            _wheelCollider = GetComponentInChildren<TorkWheelCollider>();
        }

        private void Update()
        {
            SyncPosition();
            SyncRotation();
        }

        private void SyncPosition()
        {
            _visualWheel.position = new Vector3(
                _wheelCollider.transform.position.x,
                _wheelCollider.transform.position.y - (_wheelCollider.springLength - _wheelCollider.compressionDistance),
                _wheelCollider.transform.position.z
            );
        }

        private void SyncRotation()
        {
            _visualWheel.localEulerAngles = Vector3.zero;
            _rotationAngle += (Time.deltaTime * _visualWheel.InverseTransformDirection(_wheelCollider.velocity).z) / (2 * Mathf.PI * _wheelCollider.radius) * 360;
            _visualWheel.Rotate(new Vector3(0, 1, 0), _wheelCollider.steerAngle - _visualWheel.localEulerAngles.y);
            _visualWheel.Rotate(new Vector3(1, 0, 0), _rotationAngle);
        }
    }
}