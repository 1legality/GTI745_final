using UnityEngine;
using UnityEngine.UI;

public abstract class NPCCommandable : MonoBehaviour
{
    private Image _image = null;

    protected virtual void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _image.fillAmount = 0.0f;
    }

    protected virtual void Update()
    {
    }

    public void SetFillAmount(float value)
    {
        _image.fillAmount = value;
    }

    public abstract void Command();
    public abstract void UnCommand();
}