using System;
using System.Collections;
using UnityEngine;

public class PuzzleDoor1: MonoBehaviour
{
    [SerializeField] private NPCPressurePlate _pressurePlate1 = null;
    [SerializeField] private NPCPressurePlate _pressurePlate2 = null;
    [SerializeField] private GameObject _door = null;
    [SerializeField] private float _doorSpeed = 10.0f;

    private float _yawTarget = -125.0f;
    private bool _openingDoor = false;
    private bool _hasPlaySound = false;

    private void Update()
    {
        if (_pressurePlate1.Pressed && _pressurePlate2.Pressed && !_openingDoor)
            StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        _openingDoor = true;

        if (_hasPlaySound == false)
        {
            GameLoop gameLoop = GameObject.Find("GameLoop").GetComponent<GameLoop>();
            gameLoop.PlayGoodSound();

            _hasPlaySound = true;
        }

        while (Math.Abs(_door.transform.localEulerAngles.y - _yawTarget) > float.Epsilon)
        {
            var localRot = _door.transform.localEulerAngles;
            localRot.y = Mathf.MoveTowards(localRot.y, _yawTarget, Time.deltaTime * _doorSpeed);
            _door.transform.localEulerAngles = localRot;

            yield return null;
        }
    }
}