using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2: MonoBehaviour
{
    [SerializeField] private List<PuzzleElement2> _puzzleElements;

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
        yield return null;
    }
}