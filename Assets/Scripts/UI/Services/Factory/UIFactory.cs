using Infrastructure.AssetManagment;
using Infrastructure.Services;
using Infrastructure.Services.Ads;
using Infrastructure.Services.PersistentProgress;
using UI.Services.Windows;
using UI.Windows.Shop;
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
        private readonly IAdsService _adsService;

        public UIFactory(IAssets assets,
            IStaticDataService staticData,
            IPersistentProgressService progressService,
            IAdsService adsService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _adsService = adsService;
        }

        public void CreateShop()
        {
            var config = _staticData.ForWindow(WindowId.Shop);
            var shopWindow = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            shopWindow.Construct(_adsService, _progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
    }
}