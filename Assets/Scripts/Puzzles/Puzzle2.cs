using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2: MonoBehaviour
{
    [SerializeField] private List<PuzzleElement2> _puzzleElements;
    [SerializeField] private float _wallSpeed = 20.0f;

    private void Awake()
    {
        _puzzleElements.ForEach(p => p.Connected += P_Connected);
    }

    private void P_Connected(PuzzleElement2 obj)
    {
        if (obj.ConnectionFailed)
            _puzzleElements.ForEach(p => p.ClearConnection());

        if (_puzzleElements.TrueForAll(p => p.ConnectionSuccess))
            StartCoroutine(Animate());
    }

    private void Update()
    {
    }

    private IEnumerator Animate()
    {
        float target = -10.5f;
        
        _puzzleElements.ForEach(p =>
        {
            p.Completed = true;
            p.ClearConnection();
        });

        while (transform.localPosition.y > target)
        {
            var pos = transform.localPosition;
            pos.y = Mathf.MoveTowards(pos.y, target, Time.deltaTime * _wallSpeed);
            transform.localPosition = pos;
            yield return null;
        }
        
        
    }
}