using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterShooter: MonoBehaviour
{
    [SerializeField] private Transform _fireSocket;
    [SerializeField] private Projectile _projectilePrefab = null;
    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private float _fireLaunchSpeed = 5.0f;
    [SerializeField] private int _magazineCapacity = 300;

    private List<Projectile> _projectiles = new List<Projectile>();

    private int _currentIndex = 0;

    private void Start()
    {
        InitMagazine();
        StartCoroutine(Fire());
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

    private IEnumerator Fire()
    {
        while (true)
        {
            var projectile = _projectiles.ElementAt(_currentIndex++);

            if (_currentIndex >= _projectiles.Count)
                _currentIndex = 0;


            projectile.transform.position = _fireSocket.position;
            projectile.gameObject.SetActive(true);

            var eyesPosition = PlayerEyes.GetWorldLocation();
            var direction = eyesPosition - transform.position;

            projectile.Fire(direction.normalized * _fireLaunchSpeed);

            yield return new WaitForSeconds(_fireRate);
        }
    }
}