using Infrastructure.AssetManagment;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at) =>
            _assets.Instantiate(AssetPath.HeroPath, at: at.transform.position);

        public void CreateHUD() =>
            _assets.Instantiate(AssetPath.UiHUDPath);
    }
}