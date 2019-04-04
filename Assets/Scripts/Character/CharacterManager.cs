using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private List<AbilityBase> _abilities = null;
    
    private void Awake()
    {
        _abilities = GetComponentsInChildren<AbilityBase>().ToList();
        Instance_AbilityChanged(Abilities.None);
    }

    private void Start()
    {
        AbilityManager.Instance.AbilityChanged += Instance_AbilityChanged;
    }

    private void Instance_AbilityChanged(Abilities ability)
    {
        AbilityBase toEnable = null;

        _abilities.ForEach(a =>
        {
            if (ability != a.Ability && a.gameObject.activeSelf)
            {
                a.gameObject.SetActive(false);
                a.OnDeactivated();
            }
        });
        

        switch (ability)
        {
            case Abilities.None:
                break;
            case Abilities.Gun:
                toEnable = _abilities.FirstOrDefault(a => a.Ability == Abilities.Gun);
                break;
            case Abilities.Laser:
                toEnable = _abilities.FirstOrDefault(a => a.Ability == Abilities.Laser);
                break;
            case Abilities.NPC_Commander:
                toEnable = _abilities.FirstOrDefault(a => a.Ability == Abilities.NPC_Commander);
                break;
            case Abilities.Movement:
                toEnable = _abilities.FirstOrDefault(a => a.Ability == Abilities.Movement);
                break;
        }

        if (toEnable != null)
        {
            toEnable.gameObject.SetActive(true);
            toEnable.OnActivated();
        }
    }


}