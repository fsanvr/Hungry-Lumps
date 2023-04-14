using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    public void Draw(IEnumerable<Cell> cells)
    {
        var positions = cells.Select(cell => new Vector3(cell.position.x, cell.position.y, 0)).ToArray();
        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.SetPositions(positions);
    }

    public void Hide()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.SetPositions(Array.Empty<Vector3>());
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
    }
}