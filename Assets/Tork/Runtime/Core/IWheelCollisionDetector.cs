using UnityEngine;

namespace Adrenak.Tork
{
    public interface IWheelCollisionDetector
    {
        bool TryGetForwardCollision(out Vector3 point, out Vector3 direction);
        bool TryGetBackwardCollision(out Vector3 point, out Vector3 direction);
    }
}