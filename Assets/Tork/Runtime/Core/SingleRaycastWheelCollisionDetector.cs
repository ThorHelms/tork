using UnityEngine;

namespace Adrenak.Tork
{
    public class SingleRaycastWheelCollisionDetector : MonoBehaviour, IWheelCollisionDetector
    {
        [SerializeField] private float _radius = 0.25f;
        [SerializeField] private LayerMask _raycastLayers;
        private bool _rayHit = false;
        private RaycastHit _hit;


        private void Start()
        {

        }

        private void FixedUpdate()
        {
            _rayHit = Physics.Raycast(transform.position, transform.up * -1, out var hit, _radius, _raycastLayers);
            _hit = hit;
        }

        public bool TryGetForwardCollision(out Vector3 point, out Vector3 direction)
        {
            if (_rayHit)
            {
                point = _hit.point;
                direction = transform.forward;
                return true;
            }

            point = transform.position;
            direction = transform.up * -1;
            return false;
        }

        public bool TryGetBackwardCollision(out Vector3 point, out Vector3 direction)
        {
            if (_rayHit)
            {
                point = _hit.point;
                direction = transform.forward * -1;
                return true;
            }

            point = transform.position;
            direction = transform.up * -1;
            return false;
        }
    }
}