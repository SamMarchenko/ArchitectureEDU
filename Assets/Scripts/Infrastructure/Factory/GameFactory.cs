using System.Collections.Generic;
using System.Threading.Tasks;
using Enemy;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.Randomizer;
using Logic;
using Logic.EnemySpawners;
using StaticData;
using UI.Elements;
using UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _progressService;
        private readonly IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject Hero { get; set; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, IRandomService randomService,
            IPersistentProgressService progressService, IWindowService windowService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = progressService;
            _windowService = windowService;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }


        public async Task WarmUp()
        {
           await _assetProvider.Load<GameObject>(AssetAddress.Loot);
           await _assetProvider.Load<GameObject>(AssetAddress.Spawner);
        }
        

        public async Task CreateSpawner(Vector3 at, string id, MonsterTypeId monsterTypeId)
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.Spawner);
            
            var spawner = InstantiateRegistered(prefab, at)
                .GetComponent<SpawnPoint>();
            spawner.Construct(this);

            spawner.Id = id;
            spawner.MonsterTypeId = monsterTypeId;
        }

        public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            var monsterData = _staticData.ForMonster(typeId);

            GameObject prefab = await _assetProvider.Load<GameObject>(monsterData.PrefabReference);

            GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

            var health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToHero>().Construct(Hero.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            var attack = monster.GetComponent<Attack>();
            attack.Construct(Hero.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(Hero.transform);

            return monster;
        }

        public async Task<LootPiece> CreateLoot()
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.Loot);
            
            var lootPiece = InstantiateRegistered(prefab).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public async Task<GameObject> CreateHero(Vector3 at)
        {
            Hero = await InstantiateRegisteredAsync(AssetAddress.HeroPath, at);
            return Hero;
        }


        public async Task<GameObject> CreateHUD()
        {
            var hud = await InstantiateRegisteredAsync(AssetAddress.UiHUDPath);
            hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);

            foreach (var button in hud.GetComponentsInChildren<OpenWindowButton>())
                button.Construct(_windowService);

            return hud;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assetProvider.CleanUp();
        }


        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 position)
        {
            GameObject gameObject = await _assetProvider.Instantiate(prefabPath, at: position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        } 
        
        
        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            GameObject gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _assetProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (var progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
    }
}