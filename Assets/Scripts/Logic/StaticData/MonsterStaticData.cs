using UnityEngine;

namespace Logic.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject

    {
        public MonsterTypeId MonsterTypeId;
        
        [Range(1, 100)]
        public int Hp;
        
        [Range(1f, 30)]
        public float Damage;
        
        [Range(1f, 30)]
        public float MoveSpeed;
        
        [Range(0.5f, 1f)]
        public float EffectiveDistance = 0.666f;
        
        [Range(0.5f, 1f)]
        public float Cleavage;

        public int MaxLoot;
        public int MinLoot;
        

        public GameObject Prefab;
        
    }
}