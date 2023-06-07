using System.Collections.Generic;
using System.Threading.Tasks;
using Enemy;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Task<GameObject> CreateHero(Vector3 at);
        Task<GameObject> CreateHUD();
        Task CreateSpawner(Vector3 at, string id, MonsterTypeId monsterTypeId);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);
        Task<LootPiece> CreateLoot();
        Task WarmUp();
    }
}