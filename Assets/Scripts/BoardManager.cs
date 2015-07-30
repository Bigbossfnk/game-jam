using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Completed	
{
	public class BoardManager : MonoBehaviour
	{
		public int columns = 8;
		public int rows = 8;
		// public GameObject exit;
		public GameObject[] boardTiles;
		// public GameObject[] cornerTiles;
		// public GameObject[] riverTiles;

		// used as parent to organize spawned tiles
		private Transform boardHolder;

		// contains the possible spawn locations for the tiles
		private List <Vector3> gridPositions = new List <Vector3> ();  
		
		// clears gridPositions and generates a new board
		void InitialiseList()
		{
			gridPositions.Clear ();
			
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
			boardHolder = new GameObject("Board").transform;

			for(int i = 0; i < gridPositions.Count; i++)
			{
				GameObject tileToInstantiate = boardTiles[Random.Range(0, boardTiles.Length)];

				GameObject instance = Instantiate(tileToInstantiate, gridPositions[i], Quaternion.identity) as GameObject;

				instance.transform.SetParent(boardHolder);
			}
		}

		void Start()
		{
			InitialiseList();
			BoardSetup();
		}
		
		/*
		//RandomPosition returns a random position from our list gridPositions.
		Vector3 RandomPosition ()
		{
			//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
			int randomIndex = Random.Range (0, gridPositions.Count);
			
			//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
			Vector3 randomPosition = gridPositions[randomIndex];
			
			//Remove the entry at randomIndex from the list so that it can't be re-used.
			gridPositions.RemoveAt (randomIndex);
			
			//Return the randomly selected Vector3 position.
			return randomPosition;
		}
		*/
		
		/*
		//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
		void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
		{
			//Choose a random number of objects to instantiate within the minimum and maximum limits
			int objectCount = Random.Range (minimum, maximum+1);
			
			//Instantiate objects until the randomly chosen limit objectCount is reached
			for(int i = 0; i < objectCount; i++)
			{
				//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
				Vector3 randomPosition = RandomPosition();
				
				//Choose a random tile from tileArray and assign it to tileChoice
				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				
				//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
		}
		*/

		/*
		//SetupScene initializes our level and calls the previous functions to lay out the game board
		public void SetupScene (int level)
		{
			//Creates the outer walls and floor.
			BoardSetup ();
			
			//Reset our list of gridpositions.
			InitialiseList ();

			//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
			LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
			
			//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
			LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
			
			//Determine number of enemies based on current level number, based on a logarithmic progression
			int enemyCount = (int)Mathf.Log(level, 2f);
			
			//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
			LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
			
			//Instantiate the exit tile in the upper right hand corner of our game board
			Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
		}
		*/
	}
}