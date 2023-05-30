using DefaultNamespace.Infrastructure.Services;
using Logic.StaticData;

namespace Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
    }
}