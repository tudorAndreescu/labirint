using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
    // public GameObject finish;
    
	public GameObject wall;
    public GameObject secret;
    public float size = 2f;
    private System.Random rnd = new System.Random();

    private MazeCell[,] mazeCells;

    // Use this for initialization
    void Start () {
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
        AddSecret();
    }
	
	// Update is called once per frame
	void Update () {
	}

	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];
        

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				// For now, use the same wall object for the floor!
				mazeCells [r, c] .floor = Instantiate (wall, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
				}

				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

				if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);

                
                
                
       
			}
		}
	}


    // Adds Secret at a random point in the Maze
    private void AddSecret() {
        int secretRowIndex = rnd.Next(mazeRows);

        int secretComlumnIndex = 0;
        while (secretComlumnIndex < 5)  {
            secretComlumnIndex = rnd.Next(mazeColumns);
        }

        mazeCells = new MazeCell[mazeRows, mazeColumns];
        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                if (r == secretRowIndex && c == secretComlumnIndex)
                {
                    secret = Instantiate(secret, new Vector3(r * size, 0f, c * size), Quaternion.identity) as GameObject;
                }
            }
        }
    }

    // adauga finish object pe maze cell
    // dupa ce a construit maze-u, seteaza un maze cell random ca is finish
}
