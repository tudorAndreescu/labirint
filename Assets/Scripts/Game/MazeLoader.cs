using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class MazeLoader : MonoBehaviour {

	private int mazeRows, mazeColumns;

	public GameObject wall;
    public GameObject mazefloor;
    public GameObject mazeroof;
    public GameObject secret;
    public GameObject battery;
    public TextMeshProUGUI timerText;

    public float size = 2f;
    private float timer;
    private bool timerWasSet = false;
    private System.Random rnd = new System.Random();

    private MazeCell[,] mazeCells;


    // Use this for initialization
    void Start () {
        // set maze rows and columns based on selected difficulty
        mazeRows = StaticValues.GetMazeRows();
        mazeColumns = StaticValues.GetMazeColumns();

        InitializeMaze ();
		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
        AddSecret();
        AddBatteries();
        StartTimer();
    }


	private void InitializeMaze() {


		mazeCells = new MazeCell[mazeRows,mazeColumns];


		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				// For now, use the same wall object for the floor!
				mazeCells [r, c] .floor = Instantiate (mazefloor, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

                mazeCells[r, c].roof = Instantiate(mazeroof, new Vector3(r * size,(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].roof.name = "Roof " + r + "," + c;
                mazeCells[r, c].roof.transform.Rotate(Vector3.left, 90f);

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
        int secretRowIndex = 0;
        int secretComlumnIndex = rnd.Next(mazeColumns);
        while (secretRowIndex < 4)  {
            secretRowIndex = rnd.Next(mazeRows);
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

    private void AddBatteries()
    {
        int numberOfBatteries = StaticValues.GetNumberOfBatteries();
      
        for (int i = 0; i < numberOfBatteries; i++)
        {
            int batteryRowIndex = 0;
            int batteryComlumnIndex = rnd.Next(mazeColumns);
            while (batteryRowIndex < 4)
            {
                batteryRowIndex = rnd.Next(mazeRows);
            }

            mazeCells = new MazeCell[mazeRows, mazeColumns];
            for (int r = 0; r < mazeRows; r++)
            {
                for (int c = 0; c < mazeColumns; c++)
                {
                    if (r == batteryRowIndex && c == batteryComlumnIndex)
                    {
                        battery = Instantiate(battery, new Vector3(r * size, -1f, c * size), Quaternion.identity) as GameObject;
                    }
                }
            }
        }
    }


    private void StartTimer()
    {
        timer = StaticValues.GetTimer();
        timerWasSet = true;
    }

    private void Update()
    {
        if (!timerWasSet) return;
        if (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            int minutes = (int)(timer / 60);
            int seconds = (int)((timer - (minutes * 60)) % 60);
            timerText.SetText($"TIME LEFT\n{minutes}:{seconds}");
            // send the time value to a Text object as an int
        }

        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            StaticValues.gameWon = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }



}
