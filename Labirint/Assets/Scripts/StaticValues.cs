using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticValues : MonoBehaviour
{

    public static int difficulty = 0;

    public static int GetMazeRows()
    {
        switch (difficulty)
        {
            case 0: return 5;
            case 1: return 7;
            case 2: return 10;
            case 3: return 14;
            case 4: return 20;

            default: return 5;
        }
    }

    public static int GetMazeColumns()
    {
        switch (difficulty)
        {
            case 0: return 6;
            case 1: return 9;
            case 2: return 12;
            case 3: return 18;
            case 4: return 25;

            default: return 6;
        }
    }

    public static string GetDifficultyDescription()
    {
        switch (difficulty)
        {
            case 0: return "Begginer\nMaze size: 5x6\nTime: 1:30";
            case 1: return "Easy\nMaze size: 7x9\n Time: 2:00";
            case 2: return "Medium\nMaze size: 10x12\nTime: 3:00";
            case 3: return "Hard\nMaze size: 14x18\nTime: 5:00";
            case 4: return "Impossible\nMaze size: 20x25\nTime: 10:00";

            default: return "Easy \n Maze size: 6x6 \n Time: 2:00";
        }
    }

    public static int GetNumberOfBatteries()
    {
        switch (difficulty)
        {
            case 0: return 1;
            case 1: return 1;
            case 2: return 3;
            case 3: return 5;
            case 4: return 10;

            default: return 6;
        }
    }
}
