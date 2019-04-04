using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public abstract Abilities Ability { get; }

    public virtual void OnActivated() { }
    public virtual void OnDeactivated() { }
}