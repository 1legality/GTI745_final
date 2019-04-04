using System;
using UnityEngine;

public class PuzzleElement2: MonoBehaviour
{
    private PuzzleElement2 _connectedElement;
    private LineRenderer _lineRenderer = null;

    [SerializeField] private PuzzleElement2 _nextElement;

    public bool ConnectionSuccess => _nextElement == _connectedElement;
    public bool ConnectionFailed => _connectedElement && _nextElement != _connectedElement;
    public bool Completed { get; set; }

    public event Action<PuzzleElement2> Connected;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
    }

    public void ConnectTo(PuzzleElement2 element)
    {
        _connectedElement = element;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, element.transform.position);
        Connected?.Invoke(this);
    }

    public void UpdateEndLine(Vector3 position)
    {
        if (_lineRenderer.positionCount == 0)
        {
            _lineRenderer.positionCount = 2;
        }
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, position);
    }

    public void ClearConnection()
    {
        _connectedElement = null;
        _lineRenderer.positionCount = 0;
    }
}