using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Size")]
    public int height = 30;
    public int width = 30;
    public float gap = 0.2f;

    [Header("References")]
    public GameObject gridParent;
    public GameObject[,] gridArray;
    public Camera cam;
    public GameObject cellPrefab;

    public void Start()
    {
        gridArray = new GameObject[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject go = Instantiate(cellPrefab, gridParent.transform);
                go.transform.position = new Vector3(j + j * gap, 0, i + i * gap);
                gridArray[i, j] = go;

            }
        }

        cam.transform.position = new Vector3((width + width * gap - gap) / 2, cam.transform.position.y, (height + height * gap - gap) / 2);
    }


    public void NextGeneration()
    {
        foreach(GameObject go in gridArray)
        {
            go.GetComponent<Cell>().CheckNeighbors();
        }

        foreach (GameObject go in gridArray)
        {
            if (go.GetComponent<Cell>().isAlive != go.GetComponent<Cell>().nextState)
            {
                go.GetComponent<Cell>().SwitchState();
            }
        }
    }

    public void ResetGame()
    {
        foreach(GameObject go in gridArray)
        {
            if (go.GetComponent<Cell>().isAlive)
                go.GetComponent<Cell>().SwitchState();
        }
    }
}

