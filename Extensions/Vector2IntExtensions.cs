using UnityEngine;

namespace DayenCreation.Utils.Extension
{
	/// <summary>
	/// Extension Methods for UnityEngine.Vector2Int
	/// </summary>
	public static class Vector2IntExtensions
	{
		/// <summary>
		/// Inverts a scale Vector2Int by dividing 1 by each component
		/// </summary>
		public static void InvertSelf(this ref Vector2Int vec)
		{
         vec.x = vec.x == 0 ? 0 : 1 / vec.x;
         vec.y = vec.y == 0 ? 0 : 1 / vec.y;
      }

		/// <summary>
		/// Returns a inverted Vector2Int by dividing 1 by each component
		/// </summary>
		public static Vector2Int Invert(this Vector2Int vec)
		{
			vec.InvertSelf();
			return vec;
		}

		/// <summary>
		/// Sets absolute value of each component of a Vector2Int
		/// </summary>
		public static void AbsSelf(this ref Vector2Int vec)
		{
			vec.x = Mathf.Abs(vec.x);
			vec.y = Mathf.Abs(vec.y);
		}

		/// <summary>
		/// Returns a Vector2Int with absolute value of each component
		/// </summary>
		public static Vector2Int Abs(this Vector2Int vec)
		{
			vec.AbsSelf();
			return vec;
		}

		/// <summary>
		/// Sets each component of a Vector2Int with sign of them as -1, 0 or 1
		/// </summary>
		public static void SignSelf(this ref Vector2Int vec)
		{
			vec.x = System.MathF.Sign(vec.x);
			vec.y = System.MathF.Sign(vec.y);
		}

		/// <summary>
		/// Returns a Vector2Int with sign of each component as -1, 0 or 1
		/// </summary>
		public static Vector2Int Sign(this Vector2Int vec)
		{
			vec.SignSelf();
			return vec;
		}

		/// <summary>
		/// Divides itself by another Vector2Int
		/// </summary>
		/// <param name="divisor">The Vector2Int by which this Vector2Int is to be divided</param>
		public static void DivideSelf(this ref Vector2Int vec, Vector2Int divisor)
		{
			vec = Vector2Int.Scale(vec, divisor.Invert());
		}

		/// <summary>
		/// Returns a Vector2Int with the division by dividing itself by another Vector2Int
		/// </summary>
		/// <param name="divisor">The Vector2Int by which this Vector2Int is to be divided</param>
		public static Vector2Int Divide(this Vector2Int vec, Vector2Int divisor)
		{
			return Vector2Int.Scale(vec, divisor.Invert());
		}


		/// <summary>
		/// Adds to any x y values of a Vector2Int
		/// </summary>
		public static void AddSelf(this ref Vector2Int vec, int x = 0, int y = 0)
		{
			vec.x += x;
			vec.y += y;
		}

		/// <summary>
		/// Adds to any x y values of a Vector2Int and returns a new one
		/// </summary>
		public static Vector2Int Add(this Vector2Int vec, int x = 0, int y = 0)
		{
			vec.AddSelf(x, y);
			return vec;
		}

		/// <summary>
		/// Sets any x y values of a Vector2Int
		/// </summary>
		public static void WithSelf(this ref Vector2Int vec, int? x = null, int? y = null)
		{
			vec.x = x ?? vec.x;
			vec.y = y ?? vec.y;
		}

		/// <summary>
		/// Sets any x y values of a Vector2Int
		/// </summary>
		public static Vector2Int With(this Vector2Int vec, int? x = null, int? y = null)
		{
			vec.WithSelf(x, y);
			return vec;
		}

		/// <summary>
		/// Returns a Boolean indicating whether the current Vector2Int is in a given range from another Vector2Int
		/// </summary>
		/// <param name="current">The current Vector2Int position</param>
		/// <param name="target">The Vector2Int position to compare against</param>
		/// <param name="range">The range value to compare against</param>
		/// <returns>True if the current Vector2Int is in the given range from the target Vector2Int, false otherwise</returns>
		public static bool InRangeOf(this Vector2Int current, Vector2Int target, int range)
		{
			return (current - target).sqrMagnitude <= range * range;
		}
		
	}
}