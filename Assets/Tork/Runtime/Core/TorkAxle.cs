using UnityEngine;

namespace Adrenak.Tork
{
    public class TorkAxle : MonoBehaviour
    {
        [SerializeField] private TorkWheel _leftWheel;
        public TorkWheel LeftWheel => _leftWheel;

        [SerializeField] private TorkWheel _rightWheel;
        public TorkWheel RightWheel => _rightWheel;

        public float GetAxleWidth()
        {
            return (_leftWheel.transform.position - _rightWheel.transform.position).magnitude;
        }
    }
}