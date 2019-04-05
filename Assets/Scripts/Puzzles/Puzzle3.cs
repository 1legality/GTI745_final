
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3: MonoBehaviour
{
    [SerializeField] private List<Target> _targets;

    [SerializeField] private GameObject _door;
    [SerializeField] private float _doorSpeed = 0.05f;

    private int _targetToExplode = 0;
    private int _targetExploded = 0;
    private bool _hasPlaySound;

    private void Awake()
    {
        _targets.ForEach(t => t.Explode += T_Explode);
        _targetToExplode = _targets.Count;
    }

    private void T_Explode()
    {
        _targetExploded++;

        if (_targetToExplode == _targetExploded)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        float target = 0.5f;

        while (_door.transform.localPosition.y < target)
        {
            var pos = _door.transform.localPosition;
            pos.y = Mathf.MoveTowards(pos.y, target, Time.deltaTime * _doorSpeed);
            _door.transform.localPosition = pos;
            yield return null;
        }

        if (_hasPlaySound == false)
        {
            GameLoop gameLoop = GameObject.Find("GameLoop").GetComponent<GameLoop>();
            gameLoop.PlayGoodSound();

            _hasPlaySound = true;
        }
    }
}