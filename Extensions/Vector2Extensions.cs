using UnityEngine;

namespace DayenCreation.Utils.Extension
{
	/// <summary>
	/// Extension Methods for UnityEngine.Vector2
	/// </summary>
	public static class Vector2Extensions
	{
		/// <summary>
		/// Inverts a scale Vector2 by dividing 1 by each component
		/// </summary>
		public static void InvertSelf(this ref Vector2 vec)
		{
         vec.x = vec.x == 0 ? 0 : 1 / vec.x;
         vec.y = vec.y == 0 ? 0 : 1 / vec.y;
      }

		/// <summary>
		/// Returns a inverted Vector2 by dividing 1 by each component
		/// </summary>
		public static Vector2 Invert(this Vector2 vec)
		{
			vec.InvertSelf();
			return vec;
		}

		/// <summary>
		/// Sets absolute value of each component of a Vector2
		/// </summary>
		public static void AbsSelf(this ref Vector2 vec)
		{
			vec.x = Mathf.Abs(vec.x);
			vec.y = Mathf.Abs(vec.y);
		}

		/// <summary>
		/// Returns a Vector2 with absolute value of each component
		/// </summary>
		public static Vector2 Abs(this Vector2 vec)
		{
			vec.AbsSelf();
			return vec;
		}

		/// <summary>
		/// Sets each component of a Vector2 with sign of them as -1, 0 or 1
		/// </summary>
		public static void SignSelf(this ref Vector2 vec)
		{
			vec.x = System.MathF.Sign(vec.x);
			vec.y = System.MathF.Sign(vec.y);
		}

		/// <summary>
		/// Returns a Vector2 with sign of each component as -1, 0 or 1
		/// </summary>
		public static Vector2 Sign(this Vector2 vec)
		{
			vec.SignSelf();
			return vec;
		}

		/// <summary>
		/// Divides itself by another Vector2
		/// </summary>
		/// <param name="divisor">The Vector2 by which this Vector2 is to be divided</param>
		public static void DivideSelf(this ref Vector2 vec, Vector2 divisor)
		{
			vec = Vector2.Scale(vec, divisor.Invert());
		}

		/// <summary>
		/// Returns a Vector2 with the division by dividing itself by another Vector2
		/// </summary>
		/// <param name="divisor">The Vector2 by which this Vector2 is to be divided</param>
		public static Vector2 Divide(this Vector2 vec, Vector2 divisor)
		{
			return Vector2.Scale(vec, divisor.Invert());
		}


		/// <summary>
		/// Adds to any x y values of a Vector2
		/// </summary>
		public static void AddSelf(this ref Vector2 vec, float x = 0, float y = 0)
		{
			vec.x += x;
			vec.y += y;
		}

		/// <summary>
		/// Adds to any x y values of a Vector2 and returns a new one
		/// </summary>
		public static Vector2 Add(this Vector2 vec, float x = 0, float y = 0)
		{
			vec.AddSelf(x, y);
			return vec;
		}

		/// <summary>
		/// Sets any x y values of a Vector2
		/// </summary>
		public static void WithSelf(this ref Vector2 vec, float? x = null, float? y = null)
		{
			vec.x = x ?? vec.x;
			vec.y = y ?? vec.y;
		}

		/// <summary>
		/// Sets any x y values of a Vector2
		/// </summary>
		public static Vector2 With(this Vector2 vec, float? x = null, float? y = null)
		{
			vec.WithSelf(x, y);
			return vec;
		}

		/// <summary>
		/// Returns a Boolean indicating whether the current Vector2 is in a given range from another Vector2
		/// </summary>
		/// <param name="current">The current Vector2 position</param>
		/// <param name="target">The Vector2 position to compare against</param>
		/// <param name="range">The range value to compare against</param>
		/// <returns>True if the current Vector2 is in the given range from the target Vector2, false otherwise</returns>
		public static bool InRangeOf(this Vector2 current, Vector2 target, float range)
		{
			return (current - target).sqrMagnitude <= range * range;
		}

		/// <summary>
		/// Returns a random point in a circle with the given center and radius.
		/// </summary>
		public static Vector2 RandomPointInCircle(this Vector2 origin, float radius)
		{
			return origin + Random.insideUnitCircle * radius;
		}

		/// <summary>
		/// Returns a random point on the edge of a circle with the given center and radius.
		/// </summary>
		public static Vector2 RandomPointOnCircleEdge(this Vector2 origin, float radius)
		{
			return origin + Random.insideUnitCircle.normalized * radius;
		}

		/// <summary>
		/// Computes a random point in an annulus (a ring-shaped area) based on minimum and 
		/// maximum radius values around a central Vector2 point (origin).
		/// </summary>
		/// <param name="origin">The center Vector2 point of the annulus.</param>
		/// <param name="minRadius">Inner radius of the annulus.</param>
		/// <param name="maxRadius">Outer radius of the annulus.</param>
		/// <returns>A random Vector2 point within the specified annulus.</returns>
		public static Vector2 RandomPointInAnnulus(this Vector2 origin, float minRadius, float maxRadius)
		{
			float angle = Random.value * Mathf.PI * 2f;
			Vector2 direction = new (Mathf.Cos(angle), Mathf.Sin(angle));

			// Squaring and then square-rooting radii to ensure uniform distribution within the annulus
			float minRadiusSquared = minRadius * minRadius;
			float maxRadiusSquared = maxRadius * maxRadius;
			float distance = Mathf.Sqrt(Random.value * (maxRadiusSquared - minRadiusSquared) + minRadiusSquared);

			// Calculate the position vector
			Vector2 position = direction * distance;
			return origin + position;
		}

		
	}
}