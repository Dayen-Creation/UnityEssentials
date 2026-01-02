using DayenCreation.Utils.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace DayenCreation.Tests
{
	/// <summary>
	/// Random point generation test for Annular Cylinder and Torus
	/// </summary>
	public class TestAnnularCylinderTorusRandomPoint : MonoBehaviour
	{
		[Header("cylinder")]
		public Color cyColor;
		public Vector3 cyPos;
		public float cyMinR;
		public float cyMaxR;
		public float cyH;
		public int cySeg;

		[Header("torus")]
		public Color tColor;
		public Vector3 tPos;
		public float tMajorR;
		public float tMinorR;
		public int tMajorSeg;
		public int tMinorSeg;

		[Header("controls")]
		public bool update;
		public bool clear;
		public Color markerColor;
		public float markerR;
		public List<Vector3> markers = new();

		private void Update()
		{
			if (update)
			{
				markers.Add(cyPos.RandomPointInAnnularCylinder(cyMinR, cyMaxR, cyH));
				markers.Add(tPos.RandomPointInTorus(tMajorR, tMinorR));
				update = false;
			}

			if (clear)
			{
				markers.Clear();
				clear = false;
			}
		}



		private void OnDrawGizmos()
		{
			Gizmos.color = cyColor;
			Gizmos.DrawSphere(cyPos, .1f);
			DrawAnnularCylinder(cyPos, cyMaxR, cyMinR, cyH, cySeg);

			Gizmos.color = tColor;
			Gizmos.DrawSphere(tPos, .1f);
			DrawTorus(tPos, tMajorR, tMinorR, tMajorSeg, tMinorSeg);

			Gizmos.color = markerColor;
			foreach(var marker in markers)
			{
				Gizmos.DrawSphere(marker, markerR);
			}
		}

		public static void DrawAnnularCylinder(
			Vector3 center,
			float minRadius,
			float maxRadius,
			float height,
			int segments = 32)
		{
			float halfHeight = height * 0.5f;
			float angleStep = Mathf.PI * 2f / segments;

			Vector3 prevOuterTop = Vector3.zero;
			Vector3 prevOuterBottom = Vector3.zero;
			Vector3 prevInnerTop = Vector3.zero;
			Vector3 prevInnerBottom = Vector3.zero;

			for (int i = 0; i <= segments; i++)
			{
				float angle = i * angleStep;
				float cos = Mathf.Cos(angle);
				float sin = Mathf.Sin(angle);

				Vector3 outerOffset = new Vector3(cos * maxRadius, 0f, sin * maxRadius);
				Vector3 innerOffset = new Vector3(cos * minRadius, 0f, sin * minRadius);

				Vector3 outerTop = center + outerOffset + Vector3.up * halfHeight;
				Vector3 outerBottom = center + outerOffset - Vector3.up * halfHeight;
				Vector3 innerTop = center + innerOffset + Vector3.up * halfHeight;
				Vector3 innerBottom = center + innerOffset - Vector3.up * halfHeight;

				if (i > 0)
				{
					// Top rings
					Gizmos.DrawLine(prevOuterTop, outerTop);
					Gizmos.DrawLine(prevInnerTop, innerTop);

					// Bottom rings
					Gizmos.DrawLine(prevOuterBottom, outerBottom);
					Gizmos.DrawLine(prevInnerBottom, innerBottom);

					// Vertical edges (outer + inner)
					Gizmos.DrawLine(prevOuterTop, prevOuterBottom);
					Gizmos.DrawLine(prevInnerTop, prevInnerBottom);
				}

				prevOuterTop = outerTop;
				prevOuterBottom = outerBottom;
				prevInnerTop = innerTop;
				prevInnerBottom = innerBottom;
			}
		}


		public static void DrawTorus(
			Vector3 center,
			float majorRadius,
			float minorRadius,
			int majorSegments = 32,
			int minorSegments = 12)
		{
			float majorStep = Mathf.PI * 2f / majorSegments;
			float minorStep = Mathf.PI * 2f / minorSegments;

			for (int i = 0; i < majorSegments; i++)
			{
				float theta = i * majorStep;
				float nextTheta = (i + 1) * majorStep;

				Vector3 ringCenterA = center + new Vector3(
					 Mathf.Cos(theta) * majorRadius,
					 0f,
					 Mathf.Sin(theta) * majorRadius
				);

				Vector3 ringCenterB = center + new Vector3(
					 Mathf.Cos(nextTheta) * majorRadius,
					 0f,
					 Mathf.Sin(nextTheta) * majorRadius
				);

				// Draw main ring
				Gizmos.DrawLine(ringCenterA, ringCenterB);

				// Draw tube cross-sections
				Vector3 prevPoint = Vector3.zero;
				for (int j = 0; j <= minorSegments; j++)
				{
					float phi = j * minorStep;
					Vector3 localOffset = new Vector3(
						 Mathf.Cos(theta) * Mathf.Cos(phi) * minorRadius,
						 Mathf.Sin(phi) * minorRadius,
						 Mathf.Sin(theta) * Mathf.Cos(phi) * minorRadius
					);

					Vector3 point = ringCenterA + localOffset;

					if (j > 0)
						Gizmos.DrawLine(prevPoint, point);

					prevPoint = point;
				}
			}

		}
	}
}
