using UnityEngine;

namespace Adrenak.Tork
{
    public class MultiRaycastSuspensionWheelCollisionDetector : MonoBehaviour, IWheelCollisionDetector, ISuspensionDistance
    {
        [SerializeField] private int _numberRaysLength = 36;
        [SerializeField] private int _numberRaysWidth = 3;
        [SerializeField] private float _radius = 0.25f;
        [SerializeField] private float _width = 0.2f;
        [SerializeField] private LayerMask _raycastLayers;
        [SerializeField] private float _suspensionTravel = 0.4f;
        [SerializeField] private float _sensitivity = 0.001f;

        private bool _foundHit;
        private Vector3 _forwardPoint = Vector3.zero;
        private Vector3 _backwardPoint = Vector3.zero;
        private Vector3 _forwardDirection = Vector3.zero;
        private Vector3 _backwardDirection = Vector3.zero;
        private float _suspensionDistance;

        private RaycastHit _hit;

        private void FixedUpdate()
        {
            _foundHit = false;
            var down = transform.up * -1;
            Debug.DrawRay(transform.position, down, Color.magenta);
            var minDist = _suspensionTravel;
            var backDist = float.MaxValue;

            for (var i = 0; i < _numberRaysLength; i++)
            {
                var x = _radius * 2 * i / _numberRaysLength - _radius;
                var y = Mathf.Sqrt(_radius * _radius - x * x);
                var localDirection = new Vector3(0, - y, x);
                var direction = transform.TransformDirection(localDirection);
                var normalDirection = Vector3.Cross(direction, transform.right).normalized;

                for (var j = 0; j < _numberRaysWidth; j++)
                {
                    var origin = transform.position + transform.up * _radius / 2;

                    if (_numberRaysWidth > 1)
                    {
                        origin -= transform.right * _width / 2;
                        origin += transform.right * _width * j / _numberRaysWidth;
                    }

                    if (_numberRaysLength > 1)
                    {
                        origin += transform.forward * x;
                        origin += transform.up * _suspensionTravel / 2;
                        origin -= transform.up * y;
                    }

                    var maxRayDist = minDist + _sensitivity;
                    var rayHit = Physics.Raycast(origin, down, out _hit, maxRayDist, _raycastLayers);
                    Debug.DrawRay(origin, down * maxRayDist, rayHit ? Color.cyan : Color.gray);

                    if (!rayHit) continue;

                    if (!_foundHit || _hit.distance < minDist - _sensitivity)
                    {
                        _foundHit = true;
                        _backwardPoint = _forwardPoint = _hit.point;
                        _forwardDirection = normalDirection;
                        _backwardDirection = normalDirection * -1;
                        minDist = backDist = _hit.distance;
                        continue;
                    }

                    if (_hit.distance < backDist - _sensitivity)
                    {
                        backDist = minDist;
                        _backwardPoint = _forwardPoint;
                        _backwardDirection = _forwardDirection * -1;
                    }

                    _forwardPoint = _hit.point;
                    _forwardDirection = normalDirection;
                    minDist = Mathf.Min(minDist, _hit.distance);
                }
            }

            _suspensionDistance = _foundHit ? _suspensionTravel - minDist : 0;
        }

        public bool TryGetForwardCollision(out Vector3 point, out Vector3 direction)
        {
            point = _forwardPoint;
            direction = _forwardDirection;
            return _foundHit;
        }

        public bool TryGetBackwardCollision(out Vector3 point, out Vector3 direction)
        {
            point = _backwardPoint;
            direction = _backwardDirection;
            return _foundHit;
        }

        public float GetSuspensionDistance() => _suspensionDistance;
    }
}