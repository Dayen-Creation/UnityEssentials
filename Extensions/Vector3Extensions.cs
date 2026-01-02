using UnityEngine;

namespace DayenCreation.Utils.Extension
{
   /// <summary>
   /// Extension Methods for UnityEngine.Vector3
   /// </summary>
   public static class Vector3Extensions
   {
      /// <summary>
      /// Inverts a scale Vector3 by dividing 1 by each component
      /// </summary>
      public static void InvertSelf(this ref Vector3 vec)
      {
         vec.x = vec.x == 0 ? 0 : 1 / vec.x;
         vec.y = vec.y == 0 ? 0 : 1 / vec.y;
         vec.z = vec.z == 0 ? 0 : 1 / vec.z;
      }

      /// <summary>
      /// Returns a inverted Vector3 by dividing 1 by each component
      /// </summary>
      public static Vector3 Invert(this Vector3 vec)
      {
         vec.InvertSelf();
         return vec;
      }

      /// <summary>
      /// Sets absolute value of each component of a Vector3
      /// </summary>
      public static void AbsSelf(this ref Vector3 vec)
      {
         vec.x = Mathf.Abs(vec.x);
         vec.y = Mathf.Abs(vec.y);
         vec.z = Mathf.Abs(vec.z);
      }

      /// <summary>
      /// Returns a Vector3 with absolute value of each component
      /// </summary>
      public static Vector3 Abs(this Vector3 vec)
      {
         vec.AbsSelf();
         return vec;
      }

      /// <summary>
      /// Sets each component of a Vector3 with sign of them as -1, 0 or 1
      /// </summary>
      public static void SignSelf(this ref Vector3 vec)
      {
         vec.x = System.MathF.Sign(vec.x);
         vec.y = System.MathF.Sign(vec.y);
         vec.z = System.MathF.Sign(vec.z);
      }

      /// <summary>
      /// Returns a Vector3 with sign of each component as -1, 0 or 1
      /// </summary>
      public static Vector3 Sign(this Vector3 vec)
      {
         vec.SignSelf();
         return vec;
      }

      /// <summary>
      /// Divides itself by another Vector3
      /// </summary>
      /// <param name="divisor">The Vector3 by which this Vector3 is to be divided</param>
      public static void DivideSelf(this ref Vector3 vec, Vector3 divisor)
      {
         vec = Vector3.Scale(vec, divisor.Invert());
      }

      /// <summary>
      /// Returns a Vector3 with the division by dividing itself by another Vector3
      /// </summary>
      /// <param name="divisor">The Vector3 by which this Vector3 is to be divided</param>
      public static Vector3 Divide(this Vector3 vec, Vector3 divisor)
      {
         return Vector3.Scale(vec, divisor.Invert());
      }


      /// <summary>
      /// Adds to any x y values of a Vector3
      /// </summary>
      public static void AddSelf(this ref Vector3 vec, float x = 0, float y = 0, float z = 0)
      {
         vec.x += x;
         vec.y += y;
         vec.z += z;
      }

      /// <summary>
      /// Adds to any x y values of a Vector3 and returns a new one
      /// </summary>
      public static Vector3 Add(this Vector3 vec, float x = 0, float y = 0, float z = 0)
      {
         vec.AddSelf(x, y, z);
         return vec;
      }

      /// <summary>
      /// Sets any x y values of a Vector3
      /// </summary>
      public static void WithSelf(this ref Vector3 vec, float? x = null, float? y = null, float? z = null)
      {
         vec.x = x ?? vec.x;
         vec.y = y ?? vec.y;
         vec.z = z ?? vec.z;
      }

      /// <summary>
      /// Sets any x y values of a Vector3
      /// </summary>
      public static Vector3 With(this Vector3 vec, float? x = null, float? y = null, float? z = null)
      {
         vec.WithSelf(x, y, z);
         return vec;
      }

      /// <summary>
      /// Returns a Boolean indicating whether the current Vector3 is in a given range from another Vector3
      /// </summary>
      /// <param name="current">The current Vector3 position</param>
      /// <param name="target">The Vector3 position to compare against</param>
      /// <param name="range">The range value to compare against</param>
      /// <returns>True if the current Vector3 is in the given range from the target Vector3, false otherwise</returns>
      public static bool InRangeOf(this Vector3 current, Vector3 target, float range)
      {
         return (current - target).sqrMagnitude <= range * range;
      }

      /// <summary>
      /// Computes a random point in an annular cylinder (a hollow cylindrical volume) based on  
      /// minimum radius, maximum radius and height around a central Vector3 point (origin).
      /// </summary>
      /// <param name="origin">The center Vector3 point of the annular cyliner.</param>
      /// <param name="minRadius">Inner radius of the annular cyliner.</param>
      /// <param name="maxRadius">Outer radius of the annular cyliner.</param>
      /// <param name="height">Height of the annular cyliner.</param>
      /// <returns>A random Vector3 point within the specified annular cylinder.</returns>
      public static Vector3 RandomPointInAnnularCylinder(this Vector3 origin, float minRadius, float maxRadius, float height)
      {
         // Angle around Y-axis
         float angle = Random.value * Mathf.PI * 2f;

         // Uniform radial distribution
         float minRadiusSq = minRadius * minRadius;
         float maxRadiusSq = maxRadius * maxRadius;
         float radius = Mathf.Sqrt(Random.value * (maxRadiusSq - minRadiusSq) + minRadiusSq);

         // Uniform height distribution
         float y = Random.Range(-height * 0.5f, height * 0.5f);

         float x = Mathf.Cos(angle) * radius;
         float z = Mathf.Sin(angle) * radius;

         return origin + new Vector3(x, y, z);
      }

      /// <summary>
      /// Computes a random point in a torus (a donut shaped volume) based on  
      /// major and minor radius values around a central Vector3 point (origin).
      /// </summary>
      /// <param name="origin"></param>
      /// <param name="majorRadius">Distance from center to the middle of the tube</param>
      /// <param name="minorRadius">Radius of the tube itself</param>
      /// <returns></returns>

      public static Vector3 RandomPointInTorus( this Vector3 origin, float majorRadius, float minorRadius)
      {
         // Angle around the main ring
         float theta = Random.value * Mathf.PI * 2f;

         // Sample a point uniformly inside a circle (tube cross-section)
         float phi = Random.value * Mathf.PI * 2f;
         float r = Mathf.Sqrt(Random.value) * minorRadius;

         // Tube cross-section offset
         float tubeX = r * Mathf.Cos(phi);
         float tubeY = r * Mathf.Sin(phi);

         // Position in 3D
         float x = (majorRadius + tubeX) * Mathf.Cos(theta);
         float z = (majorRadius + tubeX) * Mathf.Sin(theta);
         float y = tubeY;

         return origin + new Vector3(x, y, z);
      }
   }
}