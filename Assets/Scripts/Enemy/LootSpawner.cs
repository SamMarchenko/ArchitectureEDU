using System.Threading.Tasks;
using Data;
using Infrastructure.Factory;
using Infrastructure.Services.Randomizer;
using UnityEngine;

namespace Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;
        private IRandomService _random;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _random = random;
            _factory = factory;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private void Start()
        {
            EnemyDeath.Happend += SpawnLoot;
        }

        private async void SpawnLoot()
        {
            LootPiece loot = await _factory.CreateLoot();
            loot.transform.position = transform.position;

            var lootItem = GenerateLoot();
            loot.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                Value = _random.Next(_lootMin, _lootMax)
            };
        }
    }
}