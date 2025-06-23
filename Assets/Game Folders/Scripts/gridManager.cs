using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour
{
    public static gridManager Instance;
    [SerializeField] private tile Tile;
    [SerializeField,Range(1f , 1.5f)] private float size;
    [SerializeField] private int startHorizontal, startVertical, maxHorizontal, maxVertical;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

    }

    private void Start()
    {
        //CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = startVertical; x < maxVertical; x++)
        {
            for (int y = startHorizontal; y < maxHorizontal; y++)
            {
                tile t = Instantiate(Tile, transform);
                t.transform.position = new Vector3(x * size, y * size, 1);
            }
        }
    }
}
