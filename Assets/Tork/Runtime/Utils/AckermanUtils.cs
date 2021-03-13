using UnityEngine;

namespace Adrenak.Tork {
    public static class AckermannUtils {
        /// <summary>
        /// Returns current turning radius of secondary wheel
        /// </summary>
        /// <param name="primaryAngle">Angle the inner front wheel is making</param>
        /// <param name="separation">Distance between front and rear wheels</param>
        /// <param name="width">Distance between left and right wheels</param>
        /// <returns></returns>
        public static float GetSecondaryAngle(float primaryAngle, float separation, float width) {
            // To avoid NaN assume primary angle isn't within [-1,1]
            if (Mathf.Abs(primaryAngle) < 1)
                primaryAngle = Mathf.Abs(primaryAngle);
            var close = separation / Mathf.Tan(Mathf.Abs(primaryAngle) * Mathf.Deg2Rad);
            var far = close + width;
            return Mathf.Sign(primaryAngle) * Mathf.Atan(separation / far) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Returns the current turning radii of each wheel
        /// </summary>
        /// <param name="primaryAngle">Primary front angle. IE. if turning right, angle of front right wheel</param>
        /// <param name="separation">Distance between front and rear wheels</param>
        /// <param name="width">Distance between left and right wheels</param>
        /// <returns></returns>
        public static float[] GetRadii(float primaryAngle, float separation, float width) {
            // To avoid NaN we assume primaryAngle to be at least 1
            primaryAngle = Mathf.Clamp(primaryAngle, 1, Mathf.Infinity);

            var frontPrimary = separation / Mathf.Sin(primaryAngle * Mathf.Deg2Rad);
            var rearPrimary = separation / Mathf.Tan(primaryAngle * Mathf.Deg2Rad);
            var rearSecondary = width + rearPrimary;
            var frontSecondary = Mathf.Sqrt(separation * separation + rearSecondary * rearSecondary);

            return new[] {
                frontPrimary,
                frontSecondary,
                rearPrimary,
                rearSecondary
            };
        }
    }
}
