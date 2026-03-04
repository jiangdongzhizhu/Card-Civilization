using UnityEngine;

public class LineRendererInstance : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public static LineRendererInstance Inst { get; private set; }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Inst = this;
    }

    public void SetLinePositions(params Vector3[] positions)
    {
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    public void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }
}
