using UnityEngine;

namespace Adrenak.Tork {
    public class Vehicle : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }

        public Steering Steering { get; private set; }

        public Motor Motor { get; private set; }

        public TorkDrivetrain Drivetrain { get; private set; }

        private void Start()
        {
            Rigidbody = GetComponentInChildren<Rigidbody>();
            Steering = GetComponentInChildren<Steering>();
            Motor = GetComponentInChildren<Motor>();
            Drivetrain = GetComponentInChildren<TorkDrivetrain>();
        }
    }
}
