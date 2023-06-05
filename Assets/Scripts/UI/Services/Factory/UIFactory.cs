using Infrastructure.AssetManagment;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UI.Services.Windows;
using UnityEngine;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private IAssets _assets;
        private IStaticDataService _staticData;
        private Transform _uiRoot;
        private IPersistentProgressService _progressService;

        public UIFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateShop()
        {
            var config = _staticData.ForWindow(WindowId.Shop);
            var shopWindow = Object.Instantiate(config.Prefab, _uiRoot);
            shopWindow.Construct(_progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
    }
}