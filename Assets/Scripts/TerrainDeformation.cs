using UnityEngine;
using System.Collections;

public class TerrainDeformation : MonoBehaviour {

	public Terrain terrain;
	public int mouseX;
	public int mouseZ;

	private float strength = 0.01f;
	private int heightmapWidth;
	private int heightmapHeight;
	private float[,] heights;
	private float[,] modifiedHeights = new float[3, 3];
	private TerrainData terrainData;



	void Start () 
	{
		terrainData = terrain.terrainData;
		heightmapWidth = terrainData.heightmapWidth;
		heightmapHeight = terrainData.heightmapHeight;
		heights = terrainData.GetHeights (0, 0, heightmapWidth, heightmapHeight);
	}

	void Update () 
	{
		RaycastHit hit;
		Camera cam = Camera.main;
		Ray ray = new Ray (cam.transform.position, cam.transform.forward);

		//Raise Terrain
		if (Input.GetMouseButton (0)) 
		{
			if (Physics.Raycast (ray, out hit))
			{
				raiseTerrain (hit.point);
			}
		}

		//Lower Terrain
		if (Input.GetMouseButton (1)) 
		{
			if (Physics.Raycast (ray, out hit))
			{
				lowerTerrain (hit.point);
			}
		}
	}

	private void raiseTerrain(Vector3 point)
	{
		mouseX = (int)((point.x / terrainData.size.x) * heightmapWidth);
		mouseZ = (int)((point.z / terrainData.size.z) * heightmapHeight);

		//ArrayList list;

		modifiedHeights [0, 0] = heights [mouseX - 1, mouseZ + 1] += strength * Time.deltaTime;
		modifiedHeights [0, 1] = heights [mouseX - 0, mouseZ + 1] += strength * Time.deltaTime;
		modifiedHeights [0, 2] = heights [mouseX + 1, mouseZ + 1] += strength * Time.deltaTime;
		modifiedHeights [1, 0] = heights [mouseX - 1, mouseZ + 0] += strength * Time.deltaTime;
		modifiedHeights [1, 1] = heights [mouseX - 0, mouseZ + 0] += strength * Time.deltaTime;
		modifiedHeights [1, 2] = heights [mouseX + 1, mouseZ + 0] += strength * Time.deltaTime;
		modifiedHeights [2, 0] = heights [mouseX - 1, mouseZ - 1] += strength * Time.deltaTime;
		modifiedHeights [2, 1] = heights [mouseX - 0, mouseZ - 1] += strength * Time.deltaTime;
		modifiedHeights [2, 2] = heights [mouseX + 1, mouseZ - 1] += strength * Time.deltaTime;

		//float y = heights [mouseX, mouseZ];
		//y += strength * Time.deltaTime;

		//if (y > terrainData.size.y) 
			//y = terrainData.size.y;
		
		//modifiedHeights [0, 0] = y;
		//heights [mouseX, mouseZ] = y;
		terrainData.SetHeights (mouseX, mouseZ, modifiedHeights);
	}

	private void lowerTerrain(Vector3 point)
	{
		mouseX = (int)((point.x / terrainData.size.x) * heightmapWidth);
		mouseZ = (int)((point.z / terrainData.size.z) * heightmapHeight);

		float y = heights [mouseX, mouseZ];
		y -= strength * Time.deltaTime;

		if (y < 0) 
			y = 0;
		
		modifiedHeights [0, 0] = y;
		heights [mouseX, mouseZ] = y;
		terrainData.SetHeights (mouseX, mouseZ, modifiedHeights);
	}
}
