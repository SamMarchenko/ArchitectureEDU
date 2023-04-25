using DefaultNamespace.Data;
using DefaultNamespace.Infrastructure.Services;

namespace Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}