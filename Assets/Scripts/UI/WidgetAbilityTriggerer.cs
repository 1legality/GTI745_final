using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WidgetAbilityTriggerer : MonoBehaviour
{
    [SerializeField] private Abilities _ability = Abilities.None;
    [SerializeField] private float _activationSpeed = 0.5f;
    [SerializeField] private Image _loading = null;
    [SerializeField] private Image _activation = null;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        AbilityManager.Instance.AbilityChanged += AbilityChanged;
    }

    private void AbilityChanged(Abilities ability)
    {
        StartCoroutine(AnimateActivation(ability == _ability ? 1.0f : 0.0f));
    }

    private void Update()
    {
        _loading.fillAmount = AreEyesOver()
            ? Mathf.MoveTowards(_loading.fillAmount, 1.0f, Time.deltaTime * _activationSpeed)
            : 0.0f;

        if (Math.Abs(_loading.fillAmount - 1.0f) < float.Epsilon)
            AbilityManager.Instance.TriggerAbility(_ability);
        else if (AreEyesOver())
            AbilityManager.Instance.TriggerAbility(Abilities.None);
    }

    private bool AreEyesOver()
    {
        var eyesLocation = _image.rectTransform.InverseTransformPoint(PlayerEyes.GetEyesLocation());
        return _activation.rectTransform.rect.Contains(eyesLocation);
    }

    private IEnumerator AnimateActivation(float target)
    {
        while (Math.Abs(_activation.fillAmount - target) > float.Epsilon)
        {
            _activation.fillAmount = Mathf.MoveTowards(_activation.fillAmount, target, Time.deltaTime * _activationSpeed * 20.0f);
            yield return null;
        }
    }
}