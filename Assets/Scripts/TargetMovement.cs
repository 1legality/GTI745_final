using UnityEditor;
using UnityEngine;

public class TargetMovement: MonoBehaviour
{
    [SerializeField] private Vector3 _movement = Vector3.left;
    [SerializeField] private float _speed = 1.0f;

    private Vector3 _initialPosition;
    private Vector3 _to;
    private Vector3 _currentDestination;

    private void Start()
    {
        _initialPosition = transform.position;
        _to = _initialPosition + _movement;
        _currentDestination = _to;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentDestination, Time.deltaTime * _speed);
        if (Vector3.Distance(transform.position, _currentDestination) < 0.05f)
        {
            _currentDestination = _to == _currentDestination ? _initialPosition : _to;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawLine(transform.position, transform.position + _movement);
    }
}