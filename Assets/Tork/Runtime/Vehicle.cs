using UnityEngine;

namespace Adrenak.Tork {
    public class Vehicle : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody;

        [Header("Core Components")]
        [SerializeField] private Ackermann _ackermann;
        public Ackermann Ackermann => _ackermann;

        [SerializeField] private Steering _steering;
        public Steering Steering => _steering;

        [SerializeField] private Motor _motor;
        public Motor Motor => _motor;
    }
}
