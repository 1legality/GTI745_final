using System;
using UnityEngine;

public class CommandManager: MonoBehaviour
{
    private static CommandManager _instance;

    public static CommandManager Instance => _instance;

    public static event Action<NPCCommand> _controlledNpcChanged;
    public static event Action<NPCPressurePlate> _controlledPressurePlateChanged;

    public NPCCommand NpcControlled { get; private set; } = null;
    public NPCPressurePlate PressurePlateController { get; private set; } = null;

    private void Awake()
    {
        _instance = this;
    }

    public void CompleteProgress(NPCCommandable commandable)
    {
        if (commandable is NPCCommand && NpcControlled != (NPCCommand) commandable)
        {
            if (NpcControlled) NpcControlled.UnCommand();
            
            NpcControlled = (NPCCommand) commandable;
            _controlledNpcChanged?.Invoke(NpcControlled);
            NpcControlled.Command();
        }

        if (commandable is NPCPressurePlate && NpcControlled != (NPCPressurePlate)commandable)
        {
            if (PressurePlateController) PressurePlateController.UnCommand();
            
            PressurePlateController = (NPCPressurePlate)commandable;
            _controlledPressurePlateChanged?.Invoke(PressurePlateController);
            PressurePlateController.Command();
        }
    }
}