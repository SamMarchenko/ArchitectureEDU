using DefaultNamespace.Data;
using DefaultNamespace.Infrastructure.Services;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}