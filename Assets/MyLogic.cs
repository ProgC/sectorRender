using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyLogic : MonoBehaviour 
{
	MeshFilter meshFilter;
	private float dt = 0;
	public float r = 10;
	public int seg = 360;
	// Use this for initialization
	void Start () 
	{
		meshFilter = GetComponent<MeshFilter> ();

		Mesh mesh = meshFilter.sharedMesh;
		if (mesh == null) 
		{
			meshFilter.mesh = new Mesh ();
			mesh = meshFilter.sharedMesh;
			mesh.name = "CirclePolygon";
		}

		mesh.Clear ();

		List<Vector3> vertices = new List<Vector3>();
		List<int> index = new List<int>();
		int indexValue = 0;

		// generate circle
		vertices.Add( new Vector3(0,0,0) ); // center.

		float theta_scale = (2 * Mathf.PI) / seg;
		for (float theta = 0; theta <= 2 * Mathf.PI; theta += theta_scale)
		{
			float x = r * Mathf.Cos (theta);
			float y = 0;
			float z = r * Mathf.Sin (theta);

			vertices.Add (new Vector3 (x, y, z));

			/*GameObject createdObject = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			createdObject.transform.position = new Vector3 (x, y, z);
			createdObject.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);*/
		}

		int n = vertices.Count-1;
		for (int i = 0; i < n-1; ++i) 
		{
			index.Add (0);
			index.Add (i + 1);
			index.Add (i + 2);
		}

		index.Add (0);
		index.Add (n);
		index.Add (1);


		mesh.vertices = vertices.ToArray();
		mesh.triangles = index.ToArray();

		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
		mesh.Optimize ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		dt += Time.deltaTime;
		if (dt >= 0.5f) 
		{
			dt = 0;
			seg++;
			Start ();
		}
	}
}
