using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    void Start()
    {
        _gridDisplayer = gridDisplayerObject.GetComponent<GridDisplayer>();
        _grid = new Grid(4, 3, 1);
        _gridDisplayer.OwnerGrid = _grid;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _gridDisplayer.Hide();
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            _gridDisplayer.Show();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _grid[0, 0] = _grid[0, 0] + 1;
        }
    }

    public GameObject gridDisplayerObject;
    private GridDisplayer _gridDisplayer; 
    private Grid _grid;
}
