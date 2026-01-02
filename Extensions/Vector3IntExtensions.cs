using UnityEngine;

namespace DayenCreation.Utils.Extension
{
	/// <summary>
	/// Extension Methods for UnityEngine.Vector3Int
	/// </summary>
	public static class Vector3IntExtensions
	{
		/// <summary>
		/// Inverts a scale Vector3Int by dividing 1 by each component
		/// </summary>
		public static void InvertSelf(this ref Vector3Int vec)
		{
			vec.x = vec.x == 0 ? 0 : 1 / vec.x;
			vec.y = vec.y == 0 ? 0 : 1 / vec.y;
			vec.z = vec.z == 0 ? 0 : 1 / vec.z;
		}

		/// <summary>
		/// Returns a inverted Vector3Int by dividing 1 by each component
		/// </summary>
		public static Vector3Int Invert(this Vector3Int vec)
		{
			vec.InvertSelf();
			return vec;
		}

		/// <summary>
		/// Sets absolute value of each component of a Vector3Int
		/// </summary>
		public static void AbsSelf(this ref Vector3Int vec)
		{
			vec.x = Mathf.Abs(vec.x);
			vec.y = Mathf.Abs(vec.y);
			vec.z = Mathf.Abs(vec.z);
		}

		/// <summary>
		/// Returns a Vector3Int with absolute value of each component
		/// </summary>
		public static Vector3Int Abs(this Vector3Int vec)
		{
			vec.AbsSelf();
			return vec;
		}

		/// <summary>
		/// Sets each component of a Vector3Int with sign of them as -1, 0 or 1
		/// </summary>
		public static void SignSelf(this ref Vector3Int vec)
		{
			vec.x = System.MathF.Sign(vec.x);
			vec.y = System.MathF.Sign(vec.y);
			vec.z = System.MathF.Sign(vec.z);
		}

		/// <summary>
		/// Returns a Vector3Int with sign of each component as -1, 0 or 1
		/// </summary>
		public static Vector3Int Sign(this Vector3Int vec)
		{
			vec.SignSelf();
			return vec;
		}

		/// <summary>
		/// Divides itself by another Vector3Int
		/// </summary>
		/// <param name="divisor">The Vector3Int by which this Vector3Int is to be divided</param>
		public static void DivideSelf(this ref Vector3Int vec, Vector3Int divisor)
		{
			vec = Vector3Int.Scale(vec, divisor.Invert());
		}

		/// <summary>
		/// Returns a Vector3Int with the division by dividing itself by another Vector3Int
		/// </summary>
		/// <param name="divisor">The Vector3Int by which this Vector3Int is to be divided</param>
		public static Vector3Int Divide(this Vector3Int vec, Vector3Int divisor)
		{
			return Vector3Int.Scale(vec, divisor.Invert());
		}


		/// <summary>
		/// Adds to any x y values of a Vector3Int
		/// </summary>
		public static void AddSelf(this ref Vector3Int vec, int x = 0, int y = 0, int z = 0)
		{
			vec.x += x;
			vec.y += y;
			vec.z += z;
		}

		/// <summary>
		/// Adds to any x y values of a Vector3Int and returns a new one
		/// </summary>
		public static Vector3Int Add(this Vector3Int vec, int x = 0, int y = 0, int z = 0)
		{
			vec.AddSelf(x, y, z);
			return vec;
		}

		/// <summary>
		/// Sets any x y values of a Vector3Int
		/// </summary>
		public static void WithSelf(this ref Vector3Int vec, int? x = null, int? y = null, int? z = null)
		{
			vec.x = x ?? vec.x;
			vec.y = y ?? vec.y;
			vec.z = z ?? vec.z;
		}

		/// <summary>
		/// Sets any x y values of a Vector3Int
		/// </summary>
		public static Vector3Int With(this Vector3Int vec, int? x = null, int? y = null, int? z = null)
		{
			vec.WithSelf(x, y, z);
			return vec;
		}

		/// <summary>
		/// Returns a Boolean indicating whether the current Vector3Int is in a given range from another Vector3Int
		/// </summary>
		/// <param name="current">The current Vector3Int position</param>
		/// <param name="target">The Vector3Int position to compare against</param>
		/// <param name="range">The range value to compare against</param>
		/// <returns>True if the current Vector3Int is in the given range from the target Vector3Int, false otherwise</returns>
		public static bool InRangeOf(this Vector3Int current, Vector3Int target, int range)
		{
			return (current - target).sqrMagnitude <= range * range;
		}
	}
}