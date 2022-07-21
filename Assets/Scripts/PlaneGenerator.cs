using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
	[Header("Bindings")]
	[SerializeField] private MeshFilter filter = null;
	[SerializeField] private MeshRenderer render = null;
	[SerializeField] private Material solidMat = null;
	[SerializeField] private Material wireframeMat = null;

	[Header("Settings")]
	[SerializeField] private Vector2Int size = new Vector2Int(10, 10);
	[SerializeField] private Color vertsColor = new Color(0.851f, 0.894f, 0.984f);

	[SerializeField] private float amplitude = 1f;
	[SerializeField] private Vector2 noiseFrequency = new Vector2(0.21f, 0.21f);

	private void Start()
	{
		Mesh plane = new Mesh();

		int vertCount = (size.x + 1) * (size.y + 1);
		Vector3[] vertices = new Vector3[vertCount];
		int[] triangles = new int[size.x * size.y * 6];

		//Generate vertices
		int idx = 0;
		for(int y = 0; y <= size.y; y++)
			for(int x = 0; x <= size.x; x++)
			{
				float h = Mathf.Sin(x) * Mathf.Cos(y);
				vertices[idx++] = new Vector3(
					x, h * amplitude, y
				);
			}

		//Connect triangles
		for(int y = 0; y < size.y; y++)
		{
			for(int x = 0; x < size.x; x++)
			{
				int triangleIndex = (y * (size.x) + x) * 6;

				triangles[triangleIndex] = (y * (size.x + 1)) + x;
				triangles[triangleIndex + 1] = ((y + 1) * (size.x + 1)) + x;
				triangles[triangleIndex + 2] = ((y + 1) * (size.x + 1)) + x + 1;

				triangles[triangleIndex + 3] = (y * (size.x + 1)) + x;
				triangles[triangleIndex + 4] = ((y + 1) * (size.x + 1)) + x + 1;
				triangles[triangleIndex + 5] = (y * (size.x + 1)) + x + 1;
			}
		}

		plane.vertices = vertices;
		plane.triangles = triangles;
		plane.colors = Enumerable.Repeat(vertsColor, vertCount).ToArray();
		plane.RecalculateNormals();

		filter.sharedMesh = plane;
	}

	public void SwitchMaterial()
	{
		if(render.sharedMaterial == solidMat)
			render.sharedMaterial = wireframeMat;
		else
			render.sharedMaterial = solidMat;
	}
}