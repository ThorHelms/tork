using UnityEngine;

namespace Adrenak.Tork
{
    public interface ISteerableWheel
    {
        void ResetSteering();
        void SteerTowards(Vector3 turningPoint);
        float GetTurningRadius();
        float GetTurningAngle();
    }
}