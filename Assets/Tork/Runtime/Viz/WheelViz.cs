﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Adrenak.Tork {
    public class WheelViz : MonoBehaviour {
#if UNITY_EDITOR
        TorkWheelCollider target;

        private void Start()
        {
            target = GetComponent<TorkWheelCollider>();
        }

        private void OnDrawGizmos() {
            if (target == null)
            {
                Start();
                if (target == null) return;
            };

            var t = target.transform;
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(t.position, t.right, target.radius);

            Handles.color = Color.red;
            var p1 = t.position + t.up * TorkWheelCollider.k_ExtraRayLength;
            var p2 = t.position - t.up * (target.RayLength - TorkWheelCollider.k_ExtraRayLength);
            Handles.DrawLine(p1, p2);

            var pos = t.position + (-t.up * (target.RayLength - TorkWheelCollider.k_ExtraRayLength - target.compressionDistance - target.radius));
            Handles.DrawWireDisc(pos, t.right, target.radius);
        }
#endif
    }
}
