using System;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private static AbilityManager _instance;

    public static AbilityManager Instance => _instance;
    public event Action<Abilities> AbilityChanged;
    
    private void Awake()
    {
        _instance = this;
    }

    public void TriggerAbility(Abilities ability)
    {
        AbilityChanged?.Invoke(ability);
    }
}