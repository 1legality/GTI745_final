using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NPCCommand: MonoBehaviour
{
    private Image _image = null;

    private NavMeshAgent _navMeshAgent = null;
    private Animator _animator = null;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _image = GetComponentInChildren<Image>();
        _animator = GetComponent<Animator>();
        _image.fillAmount = 0.0f;
    }

    private void Update()
    {
        _animator.SetFloat("SpeedRatio", _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
    }

    public void MoveTo(Vector3 position)
    {
        _navMeshAgent.destination = position;
    }

    public void SetFillAmount(float value)
    {
        _image.fillAmount = value;
    }

    public void Command()
    {
        _animator.SetBool("WithLeg", true);
        _navMeshAgent.destination = new Vector3(-15,-1,5);
    }
}