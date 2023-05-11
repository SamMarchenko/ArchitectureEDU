using System;
using DefaultNamespace.Infrastructure.Services;
using Infrastructure.Factory;
using UnityEngine;

public class RotateToHero : Follow
{
    public float Speed;

    private Transform _heroTransform;
    private IGameFactory _gameFactory;
    private Vector3 _positionToLook;

    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();

        if (HeroExist())
        {
            InitializeHeroTransform();
        }
        else
        {
            _gameFactory.HeroCreated += InitializeHeroTransform;
        }       
    }

    private void Update()
    {
        if (Initialized())
        {
            RotateTowardsHero();
        }
    }

    private void RotateTowardsHero()
    {
        UpdatePositionToLookAt();
        transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
    }

    private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) => 
        Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

    private float SpeedFactor() => Speed * Time.deltaTime;

    private Quaternion TargetRotation(Vector3 position) => Quaternion.LookRotation(position);

    private void UpdatePositionToLookAt()
    {
        var positionDiff = _heroTransform.position - transform.position;
        _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
    }

    private bool Initialized() => _heroTransform != null;

    private void InitializeHeroTransform() => _heroTransform = _gameFactory.HeroGameObject.transform;

    private bool HeroExist() => _gameFactory.HeroGameObject != null;
}