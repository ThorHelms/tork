using UnityEngine;

namespace Adrenak.Tork
{
    public class WheelCollisionDetectorViz : MonoBehaviour
    {
        private IWheelCollisionDetector _wheelCollisionDetector;

        private void Start()
        {
            _wheelCollisionDetector = GetComponent<IWheelCollisionDetector>();
        }

        private void Update()
        {
            if (_wheelCollisionDetector.TryGetForwardCollision(out var pointForward, out var directionForward))
            {
                Debug.DrawRay(pointForward, directionForward, Color.green);
            }

            if (_wheelCollisionDetector.TryGetBackwardCollision(out var pointBackward, out var directionBackward))
            {
                Debug.DrawRay(pointBackward, directionBackward, Color.red);
            }
        }
    }
}