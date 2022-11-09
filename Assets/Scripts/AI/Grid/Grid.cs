using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public Grid(int width, int height, float cellSize)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        _map = new int[height, width];
    }

    public int this[int i, int j]
    {
        get => _map[i, j];
        set
        {
            _map[i, j] = value;
            CellChanged?.Invoke(this, new CellChangedEventArgs(i, j, _map[i, j]));
        }
    }

    public event EventHandler<CellChangedEventArgs> CellChanged;

    public int Width { get; private set; } = 0;
    public int Height { get; private set; } = 0;
    public float CellSize { get; private set; } = 0.0f;
    
    private int[,] _map;
}
