﻿using UnityEngine;

public class RotateToHero : Follow
{
    public float Speed;

    private Transform _heroTransform;
    private Vector3 _positionToLook;
    
    public void Construct(Transform heroTransform)
    {
        _heroTransform = heroTransform;
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
    


}