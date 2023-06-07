using System.Threading.Tasks;
using Infrastructure.AssetManagement;
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
        private const string UIRootPath = "UIRoot";
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticData;
        private Transform _uiRoot;
        private IPersistentProgressService _progressService;
        private readonly IAdsService _adsService;

        public UIFactory(IAssetProvider assetProvider,
            IStaticDataService staticData,
            IPersistentProgressService progressService,
            IAdsService adsService)
        {
            _assetProvider = assetProvider;
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

        public async Task CreateUIRoot()
        {
            var root = await _assetProvider.Instantiate(UIRootPath);
            _uiRoot = root.transform;
        }
    }
}