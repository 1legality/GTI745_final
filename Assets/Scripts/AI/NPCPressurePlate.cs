using System;
using System.Collections;
using UnityEngine;

public class NPCPressurePlate : NPCCommandable
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private GameObject _plateObject = null;
    [SerializeField] private float _plateSpeed = 5.0f;

    private int _overlappingActorCount = 0;
    private Coroutine _coroutine = null;

    public bool Pressed => _overlappingActorCount > 0;

    private void Awake()
    {
        base.Awake();

        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NPCCommand>() || other.GetComponent<CharacterManager>())
        {
            _overlappingActorCount++;

            if (_overlappingActorCount == 1)
            {
                StopAllCoroutines();
                StartCoroutine(Animate(-1));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NPCCommand>() || other.GetComponent<CharacterManager>())
        {
            _overlappingActorCount--;

            if (_overlappingActorCount == 0)
            {
                StopAllCoroutines();
                StartCoroutine(Animate(1));
            }
        }
    }

    private IEnumerator Animate(float direction)
    {
        float target = direction > 0 ? 0.0f : -0.25f;

        while (Math.Abs(_plateObject.transform.localPosition.y - target) > float.Epsilon)
        {
            var localPos = _plateObject.transform.localPosition;
            localPos.y = Mathf.MoveTowards(localPos.y, target, Time.deltaTime * _plateSpeed);
            _plateObject.transform.localPosition = localPos;

            yield return null;
        }
    }

    public override void Command()
    {
        _animator.SetBool("Visible", true);
        CommandManager._controlledNpcChanged += CommandManager__controlledNpcChanged;
    }


    public override void UnCommand()
    {
        _animator.SetBool("Visible", false);
        CommandManager._controlledNpcChanged -= CommandManager__controlledNpcChanged;
    }

    private void CommandManager__controlledNpcChanged(NPCCommand obj)
    {
        if (obj)
            obj.MoveTo(transform.position);
    }
}