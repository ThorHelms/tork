using UnityEngine;

namespace Adrenak.Tork
{
    public class Wheel : MonoBehaviour, IWheel
    {
        [SerializeField] private float _radius = 0.25f;
        [SerializeField] private float _width = 0.2f;

        public float GetRadius() => _radius;

        public float GetWidth() => _width;
    }
}