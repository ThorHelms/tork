using Adrenak.Tork;
using UnityEngine;

namespace Assets.Tork.Samples.Scripts
{
    public class SteerTowardsTestActor : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private TorkDrivetrain _drivetrain;

        private void Update()
        {
            var newX = transform.position.x + Input.GetAxis("Vertical") * -1 * _moveSpeed * Time.deltaTime;
            var newY = transform.position.y;
            var newZ = transform.position.z + Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
            transform.position = new Vector3(newX, newY, newZ);

            if (Mathf.Abs(newZ) < 1)
            {
                _drivetrain.ResetSteering();
            }
            else
            {
                var left = newZ < 0;

                _drivetrain.SteerTowards(transform.position, left);
            }
        }
    }
}