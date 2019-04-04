using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosion = null;
    [SerializeField] private float _initialLife = 10;
    [SerializeField] private float _lifeAnimationSpeed = 0.5f;
    [SerializeField] private Image _lifeImage = null;
    
    private float _targetLife;
    private float _currentLife;
    private float _lifeWidth;

    public event Action Explode;

    private void Awake()
    {
        _targetLife = _initialLife;
        _currentLife = _initialLife;
        _lifeWidth = _lifeImage.rectTransform.rect.width;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        _targetLife--;
    }

    void Update()
    {
        _currentLife = Mathf.MoveTowards(_currentLife, _targetLife, Time.deltaTime * _lifeAnimationSpeed);
        var lifeRect = _lifeImage.rectTransform.rect;
        lifeRect.width = (_currentLife / _initialLife) * _lifeWidth;
        _lifeImage.rectTransform.sizeDelta = lifeRect.size;
        _lifeImage.rectTransform.anchoredPosition = new Vector2(lifeRect.width/2.0f, 0.0f);
        
        if (_currentLife <= 0.001f)
        {
            Explode?.Invoke();
            Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Damage(float amount)
    {
        _targetLife -= amount;
    }
}
