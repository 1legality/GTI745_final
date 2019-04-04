using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public abstract Abilities Ability { get; }

    public virtual void OnActivated() { Debug.Log("Actiae\ted"); }
    public virtual void OnDeactivated() { }
}