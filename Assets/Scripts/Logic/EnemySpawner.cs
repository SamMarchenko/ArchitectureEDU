using System;
using DefaultNamespace.Data;
using DefaultNamespace.Infrastructure.Services;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Logic.StaticData;
using UnityEngine;

namespace Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        private string _id;

        public bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public bool Slain => _slain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
           var monster = _factory.CreateMonster(MonsterTypeId, transform);
           _enemyDeath = monster.GetComponent<EnemyDeath>();
           _enemyDeath.Happend+= Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happend -= Slay;
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}