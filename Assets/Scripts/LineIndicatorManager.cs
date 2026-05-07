using System.Collections.Generic;
using UnityEngine;

public class LineIndicatorManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private readonly List<Vector3> positions = new List<Vector3>();
    private bool isEnabled = false;


    public static LineIndicatorManager Inst { get; private set; }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Inst = this;
    }

    private void FixedUpdate()
    {
        if (!isEnabled) return;
        Vector3[] positionsWithMouse = new Vector3[positions.Count + 1];
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 position = positions[i];
            positionsWithMouse[i] = position;
        }
        positionsWithMouse[^1] = GetMousePosition();
        lineRenderer.positionCount = positionsWithMouse.Length;
        lineRenderer.SetPositions(positionsWithMouse);
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void EnableLine(Transform start)
    {
        isEnabled = true;
        AddPoint(start);
    }

    public void AddPoint(Transform t)
    {
        Vector3 position = t.position;
        position.z = 0f;
        positions.Add(position);
    }

    public void Clear()
    {
        lineRenderer.positionCount = 0;
        isEnabled = false;
        positions.Clear();
    }
}
