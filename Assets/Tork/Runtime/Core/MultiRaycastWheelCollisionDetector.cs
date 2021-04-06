using UnityEngine;

namespace Adrenak.Tork
{
    public class MultiRaycastWheelCollisionDetector : MonoBehaviour, IWheelCollisionDetector
    {
        [SerializeField] private int _numberRaysRound = 36;
        [SerializeField] private int _numberRaysWidth = 3;
        [SerializeField] private float _radius = 0.25f;
        [SerializeField] private float _width = 0.2f;
        [SerializeField] private float _angleLimit = 180;
        [SerializeField] private LayerMask _raycastLayers;

        private bool _forwardFound = false;
        private bool _backwardFound = false;
        private Vector3 _forwardPoint = Vector3.zero;
        private Vector3 _backwardPoint = Vector3.zero;
        private Vector3 _forwardDirection = Vector3.zero;
        private Vector3 _backwardDirection = Vector3.zero;

        private void FixedUpdate()
        {
            _forwardFound = false;
            _backwardFound = false;
            RaycastHit hit;
            for (var i = 0; i < _numberRaysRound; i++)
            {
                var angle = 180 - _angleLimit / 2 + i * _angleLimit / _numberRaysRound;
                var direction = Quaternion.AngleAxis(angle, transform.right) * transform.up;
                for (var j = 0; j < _numberRaysWidth; j++)
                {
                    var origin = transform.position;
                    if (_numberRaysWidth > 1)
                    {
                        origin -= transform.right * _width / 2;
                        origin += transform.right * _width * j / _numberRaysWidth;
                    }
                    var rayHit = Physics.Raycast(origin, direction, out hit, _radius, _raycastLayers);
                    Debug.DrawRay(origin, direction.normalized * _radius, rayHit ? Color.cyan : Color.gray);
                    if (!rayHit) continue;
                    if (_forwardFound)
                    {
                        _backwardPoint = hit.point;
                        _backwardDirection = Vector3.Cross(transform.right, direction).normalized;
                    }
                    else
                    {
                        _backwardFound = _forwardFound = true;
                        _backwardPoint = _forwardPoint = hit.point;
                        _forwardDirection = Vector3.Cross(direction, transform.right).normalized;
                        _backwardDirection = _forwardDirection * -1;
                    }
                }
            }
        }

        public bool TryGetForwardCollision(out Vector3 point, out Vector3 direction)
        {
            point = _forwardPoint;
            direction = _forwardDirection;
            return _forwardFound;
        }

        public bool TryGetBackwardCollision(out Vector3 point, out Vector3 direction)
        {
            point = _backwardPoint;
            direction = _backwardDirection;
            return _backwardFound;
        }
    }
}