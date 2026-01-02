/// -----------------------------------------------------------------
/// By DayenCreation
/// License: The Unlicense (public domain)
/// https://www.youtube.com/@dayencreation
/// https://github.com/Dayen-Creation
/// 
/// WARNING: 3-space indentation ahead. Proceed at your own risk! :P
/// -----------------------------------------------------------------

using UnityEngine;

namespace DayenCreation.Utils.Extension
{
	/// <summary>
	/// Extension Methods for UnityEngine.Transform
	/// </summary>
	public static class TransformExtensions 
   {
		public static void ResetTransformation(this Transform transform)
		{
			transform.position = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
	}
}