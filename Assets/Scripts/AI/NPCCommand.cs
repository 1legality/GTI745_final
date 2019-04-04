using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NPCCommand: NPCCommandable
{
    private NavMeshAgent _navMeshAgent = null;
    private Animator _animator = null;

    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        
        _animator.SetFloat("SpeedRatio", _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
    }
    
    public override void Command()
    {
        _animator.SetBool("WithLeg", true);
        CommandManager._controlledPressurePlateChanged += CommandManager__controlledPressurePlateChanged;
    }

    public override void UnCommand()
    {
        _animator.SetBool("WithLeg", false);
        CommandManager._controlledPressurePlateChanged -= CommandManager__controlledPressurePlateChanged;
    }

    private void CommandManager__controlledPressurePlateChanged(NPCPressurePlate obj)
    {
        if (obj)
            _navMeshAgent.destination = obj.transform.position;
    }

    public void MoveTo(Vector3 position)
    {
        _navMeshAgent.destination = position;
    }
}