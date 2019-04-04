using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityGun : AbilityBase
{
    public override Abilities Ability => Abilities.Gun;

    [SerializeField] private Transform _fireSocket = null;
    [SerializeField] private Projectile _projectilePrefab = null;
    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private float _fireLaunchSpeed = 5.0f;
    [SerializeField] private int _magazineCapacity = 300;

    private List<Projectile> _projectiles = new List<Projectile>();

    private int _currentIndex = 0;
    private float _lastFireTime = 0.0f;

    private void Start()
    {
        InitMagazine();
    }

    private void Update()
    {
        FireProjectile();
    }

    private void InitMagazine()
    {
        var magazine = new GameObject("Magazine");

        for (int i = 0; i < _magazineCapacity; i++)
        {
            var projectile = Instantiate(_projectilePrefab, magazine.transform);
            _projectiles.Add(projectile);
            projectile.gameObject.SetActive(false);
        }
    }

    private void FireProjectile()
    {
        if (Time.time - _lastFireTime < _fireRate)
            return;

        var projectile = _projectiles.ElementAt(_currentIndex++);

        if (_currentIndex >= _projectiles.Count)
            _currentIndex = 0;


        projectile.transform.position = _fireSocket.position;
        projectile.gameObject.SetActive(true);

        var eyesPosition = PlayerEyes.GetWorldLocation();
        var direction = eyesPosition - transform.position;

        projectile.Fire(direction.normalized * _fireLaunchSpeed);

        _lastFireTime = Time.time;
    }
}