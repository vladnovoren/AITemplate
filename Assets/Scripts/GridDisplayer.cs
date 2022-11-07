using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridDisplayer : MonoBehaviour
{
    void Start()
    {
        _cellTexts = new List<List<GameObject>>();
        _cellTextMeshes = new List<List<TextMesh>>();
    }

    void Update()
    {
        if (IsVisible)
        {
            DrawLines();
        }
    }

    private Grid _ownerGrid;
    public Grid OwnerGrid
    {
        set
        {
            _ownerGrid = value;
            OnOwnerChange();
        }
        get => _ownerGrid;
    }

    public void Show()
    {
        ChangeVisibility(true);
    }

    public void Hide()
    {
        ChangeVisibility(false);
    }

    private void ChangeVisibility(bool newVisibility)
    {
        if (newVisibility == IsVisible)
            return;
        SetCellTextsVisibility(newVisibility);
        IsVisible = newVisibility;
    }

    private void DrawLines()
    {
        for (var i = 0; i <= _ownerGrid.Height; ++i)
        {
            var position = transform.position;
            Debug.DrawLine(
                position + new Vector3(0, 0, i) * _ownerGrid.CellSize,
                position + new Vector3(_ownerGrid.Width, 0, i) * _ownerGrid.CellSize
            );
        }
        for (var i = 0; i <= _ownerGrid.Width; ++i)
        {
            var position = transform.position;
            Debug.DrawLine(
                position + new Vector3(i, 0, 0) * _ownerGrid.CellSize,
                position + new Vector3(i, 0, _ownerGrid.Height) * _ownerGrid.CellSize
            );
        }
    }

    private void OnOwnerChange()
    {
        _ownerGrid.CellChanged += OnCellChanged;
        Clear();
        InitTextMeshes();
    }

    private void Clear()
    {
        foreach (var row in _cellTexts)
        {
            foreach (var cellText in row)
            {
                Destroy(cellText);
            }
        }
        _cellTexts.Clear();
        _cellTextMeshes.Clear();
    }

    private void InitTextMeshes()
    {
        for (var i = 0; i < _ownerGrid.Height; ++i)
        {
            _cellTexts.Add(new List<GameObject>());
            _cellTextMeshes.Add(new List<TextMesh>());
            for (var j = 0; j < _ownerGrid.Width; ++j)
            {
                var cellText = Instantiate(textMeshPrefab, gameObject.transform);
                cellText.transform.localPosition =
                    new Vector3(0.5f + j, 0, 0.5f + i) * _ownerGrid.CellSize;
                _cellTexts[i].Add(cellText);

                var textMesh = cellText.GetComponent<TextMesh>();
                textMesh.text = _ownerGrid[i, j].ToString();
                _cellTextMeshes[i].Add(textMesh);
            }
        }
    }

    private void SetCellTextsVisibility(bool isVisible)
    {
        foreach (var row in _cellTexts)
        {
            foreach (var cellText in row)
            {
                cellText.SetActive(isVisible);
            }
        }
    }

    void OnCellChanged(object sender, CellChangedEventArgs cell)
    {
        _cellTextMeshes[cell.i][cell.j].text = cell.value.ToString();
    }
    
    public GameObject textMeshPrefab;

    private List<List<GameObject>> _cellTexts;
    private List<List<TextMesh>> _cellTextMeshes;

    public bool IsVisible { get; private set; } = true;
}
