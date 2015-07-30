using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Completed	
{
	public class BoardManager : MonoBehaviour
	{
		public int columns;
		public int rows;
		// public GameObject exit;
		public GameObject[] boardTiles;
		// public GameObject[] cornerTiles;
		// public GameObject[] riverTiles;
		public Transform cameraTarget;

		// used as parent to organize spawned tiles
		public Transform boardHolder;

		// contains the possible spawn locations for the tiles
		private List <Vector3> gridPositions = new List <Vector3> ();
		private Transform rotationPivot;
		
		// clears gridPositions and generates a new board
		void InitialiseList()
		{
			gridPositions.Clear ();

			columns = 8;
			rows = 8;

			// columns = Random.Range(8, 16);
			// rows = Random.Range(8, 16);

			// loop through x axis (columns)
			for(int x = 0; x < (columns * 3) - 1; x += 3)
			{
				// within each column, loop through z axis (rows)
				for(int z = 0; z < (rows * 3) - 1; z += 3)
				{
					// at each index of gridPositions, add a new coordinate
					gridPositions.Add (new Vector3(x, 0f, z));
				}
			}
		}

		void BoardSetup()
		{
			// instantiate new Board GameObject
			boardHolder.transform.position = new Vector3((float)(columns * 3) / 2, 0, (float)(rows * 3) / 2);
			cameraTarget.transform.position = new Vector3((float)((columns * 3) / 2) - 3, 0, (float)((rows * 3) / 2) - 3);

			for(int i = 0; i < gridPositions.Count; i++)
			{
				GameObject tileToInstantiate = boardTiles[Random.Range(0, boardTiles.Length)];

				GameObject instance = Instantiate(tileToInstantiate, gridPositions[i], Quaternion.identity) as GameObject;

				instance.transform.SetParent(boardHolder);
			}

			// First find a center for your bounds.
			Vector3 center = Vector3.zero;
			
			foreach (Transform child in boardHolder.transform)
			{

				center += child.GetComponent<Renderer>().bounds.center;
			}
			center /= boardHolder.transform.childCount; //center is average center of children
			
			//Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
			Bounds bounds = new Bounds(center,Vector3.zero); 
			
			foreach (Transform child in boardHolder.transform)
			{
				bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
			}

				                   Debug.Log (bounds.size.x / 2);
				                   Debug.Log (bounds.size.z / 2);
		}

		void Awake()
		{
			InitialiseList();
			BoardSetup();

			Debug.Log("Columns: " + columns);
			Debug.Log("Rows   : " + rows);
		}

		int speed = 50;
		void Update(){
			if (Input.GetKey(KeyCode.LeftArrow)){
				boardHolder.transform.RotateAround (cameraTarget.transform.position, Vector3.up, speed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.RightArrow)){
				boardHolder.transform.RotateAround (cameraTarget.transform.position, Vector3.up, -speed * Time.deltaTime);
			}
		}
	}
}