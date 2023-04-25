﻿using DefaultNamespace.Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService 
    {
        Vector2 Axis { get; }
        bool IsAttackButton();
    }
}