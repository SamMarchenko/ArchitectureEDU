using System.Collections.Generic;
using DefaultNamespace.Enemy;
using DefaultNamespace.Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Logic.StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHUD();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        void Register(ISavedProgressReader savedProgress);
        GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
        LootPiece CreateLoot();
    }
}