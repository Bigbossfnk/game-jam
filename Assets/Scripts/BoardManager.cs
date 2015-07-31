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
		public GameObject[] riverTiles;
		public Transform cameraTarget;

		// used as parent to organize spawned tiles
		public Transform boardHolder;

		// contains the possible spawn locations for the tiles
		private List<Vector3> gridPositions = new List <Vector3>();
		private Transform rotationPivot;

		// determines if the river tile should go from left-right (col) or up-down (row)
		private bool isColStart;
		
		// clears gridPositions and generates a new board
		void InitialiseList()
		{
			gridPositions.Clear();

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
			/*
			int temp = Random.Range (1, 100);
			if (temp <= 50)
			{
				isColStart = true;
			}
			else
			{
				isColStart = false;
			}
			*/

			isColStart = true;

			// instantiate new Board GameObject
			boardHolder.transform.position = new Vector3((float)(columns * 3) / 2, 0, (float)(rows * 3) / 2);
			cameraTarget.transform.position = new Vector3((float)((columns * 3) / 2) - 3, 0, (float)((rows * 3) / 2) - 3);

			// determine where to start spawning river tiles
			int currRiverTileIndex;
			Debug.Log("Column Start");
			currRiverTileIndex = Random.Range(0, columns - 1);

			// spawn tiles at coordinates in gridPositions
			for(int i = 0; i < gridPositions.Count; i++)
			{
				Debug.Log(currRiverTileIndex);

				GameObject tileToInstantiate;
				if(i == currRiverTileIndex)
				{
					tileToInstantiate = riverTiles[Random.Range(0, riverTiles.Length)];

					// determine the next currRiverTileIndex;
					currRiverTileIndex += 8;

					// pick the next riverTileIndex
					/*
					currRiverTileIndex = pickNextRiverTileIndex(isColStart, currRiverTileIndex);
					while(currRiverTileIndex < prevRiverTileIndex)
					{
						// re-roll
						riverTileIndex = pickNextRiverTileIndex(isColStart, riverTileIndex);
					}
					prevRiverTileIndex = riverTileIndex;
					*/
				}
				else
				{
					tileToInstantiate = boardTiles[Random.Range(0, boardTiles.Length)];
				}

				GameObject instance = Instantiate(tileToInstantiate, gridPositions[i], Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}

		int pickNextRiverTileIndex(bool isColStart, int currRiverTileIndex)
		{
			int nextRiverTileIndex = currRiverTileIndex;

			// go left-right
			if (isColStart)
			{
				int rand = Random.Range(1, 90);
				if(rand <= 30)
				{
					nextRiverTileIndex += columns;
				}
				else if(rand <= 60)
				{
					nextRiverTileIndex -= columns;
				}
				else
				{
					nextRiverTileIndex++;
				}
				return nextRiverTileIndex;
			}
			// go up-down
			else
			{
				int rand = Random.Range (1, 90);
				if(rand <= 30)
				{
					nextRiverTileIndex += rows;
				}
				else if(rand <= 60)
				{
					nextRiverTileIndex--;
				}
				else
				{
					nextRiverTileIndex++;
				}
				return nextRiverTileIndex;
			}
		}

		void Awake()
		{
			InitialiseList();
			BoardSetup();
		}

		int speed = 50;
		void Update(){
			if (Input.GetKey(KeyCode.Q))
			{
				Camera.main.transform.RotateAround(cameraTarget.transform.position, Vector3.up, speed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.E))
			{
				Camera.main.transform.RotateAround(cameraTarget.transform.position, Vector3.up, -speed * Time.deltaTime);
			}
		}
	}
}