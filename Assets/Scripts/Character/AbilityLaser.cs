﻿
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AbilityLaser: AbilityBase
{
    [SerializeField] private ParticleSystem _laserParticle = null;
    [SerializeField] private Transform _fireSocket = null;
    [SerializeField] private float _damagePerSecond = 50.0f;
    [SerializeField] private AudioSource _audioSource;

    public override Abilities Ability => Abilities.Laser;

    private LineRenderer _lineRenderer = null;
    private ParticleSystem _particleSystem = null;
    private PuzzleElement2 _previousElement = null;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _particleSystem = Instantiate(_laserParticle);
        _particleSystem.Stop(true);
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, _fireSocket.transform.position);
        _lineRenderer.SetPosition(1, PlayerEyes.GetWorldLocation());
        _particleSystem.transform.position = PlayerEyes.GetWorldLocation();

        var direction = (PlayerEyes.GetWorldLocation() - _fireSocket.transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(new Ray(_fireSocket.transform.position, direction), out hit))
        {
            _particleSystem.transform.rotation = Quaternion.LookRotation(hit.normal);

            var puzzleElement = hit.collider.GetComponent<PuzzleElement2>();
            if (puzzleElement && !_previousElement)
            {
                _previousElement = puzzleElement;
                _audioSource.Play();
            }

            if (puzzleElement && _previousElement && puzzleElement != _previousElement)
            {
                _previousElement.ConnectTo(puzzleElement);
                _previousElement = puzzleElement;
            }
            if (_previousElement && !_previousElement.ConnectionSuccess && !_previousElement.Completed)
            {
                _previousElement.UpdateEndLine(hit.point);
            }
        }

        if (_audioSource.time > 0.5f)
        {
            _audioSource.Stop();
        }

    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();

        _particleSystem.gameObject.SetActive(false);
    }

    public override void OnActivated()
    {
        base.OnActivated();

        _particleSystem.gameObject.SetActive(true);
    }
}
