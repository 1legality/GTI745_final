using UnityEngine;

public class TobiiMouseToogler: MonoBehaviour
{
    [SerializeField] private bool _useMouseAsInput = false;

    public void Awake()
    {
        PlayerEyes.UseMouseAsInput = _useMouseAsInput;
    }
}