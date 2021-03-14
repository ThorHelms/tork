using UnityEngine;

namespace Adrenak.Tork
{
    public class TorkDrivetrain : MonoBehaviour
    {
        [SerializeField] private TorkAxle _frontAxle;
        public TorkAxle FrontAxle => _frontAxle;

        [SerializeField] private TorkAxle _backAxle;
        public TorkAxle BackAxle => _backAxle;

        public void ApplyMotorTorque(float torque)
        {
            var frontMaxTorque = FrontAxle.GetMaxTorque();
            var backMaxTorque = BackAxle.GetMaxTorque();

            var absTorque = Mathf.Abs(torque);
            var maxTorquePerAxle = absTorque / 2;

            var frontTorque = maxTorquePerAxle;
            var backTorque = maxTorquePerAxle;

            if (frontMaxTorque < maxTorquePerAxle)
            {
                frontTorque = frontMaxTorque;
                backTorque = Mathf.Clamp(absTorque - frontMaxTorque, 0, backMaxTorque);
            }
            else if (backMaxTorque < maxTorquePerAxle)
            {
                frontTorque = Mathf.Clamp(absTorque - backMaxTorque, 0, frontMaxTorque);
                backTorque = backMaxTorque;
            }

            var sign = torque < 0 ? -1 : 1;

            FrontAxle.ApplyTorque(frontTorque * sign);
            BackAxle.ApplyTorque(backTorque * sign);
        }

    }
}