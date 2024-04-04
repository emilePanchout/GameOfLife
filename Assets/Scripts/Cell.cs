using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive = false;
    public bool nextState;

    public Color deadColor = Color.white;
    public Color aliveColor = Color.black;

    private Renderer render;

    public void Start()
    {
        render = GetComponent<Renderer>();
    }


    public void OnMouseDown()
    {
        SwitchState();
    }

    public void SwitchState()
    {
        isAlive = !isAlive;

        if (isAlive == true)
        {
            render.material.color = aliveColor;
        }
        else
        {
            render.material.color = deadColor;
        }
    }

    public void CheckNeighbors()
    {
        int neighborCount = 0;
        RaycastHit hit;


        Vector3[] directions = {
            Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.forward + Vector3.right,  Vector3.forward + Vector3.left,  Vector3.back + Vector3.right,  Vector3.back + Vector3.left
        };


        foreach (var direction in directions)
        {
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if(hit.collider.gameObject.GetComponent<Cell>().isAlive)
                {
                    neighborCount++;
                }
            }
        }

        Debug.Log(neighborCount);

        // Any live cell with two or three live neighbors lives on to the next generation.
        if (neighborCount >= 2 && neighborCount <= 3 && isAlive)
        {
            nextState = true;
        }
        else if(neighborCount == 3 && !isAlive)
        {
            nextState = true;
        }
        else
        {
            nextState = false;
        }
         
    }
}
