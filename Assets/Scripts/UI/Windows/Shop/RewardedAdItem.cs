using Infrastructure.Services.Ads;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        public Button ShowAdBtn;
        public GameObject[] AdActiveObjects;
        public GameObject[] AdInactiveObjects;
        private IAdsService _adsService;
        private IPersistentProgressService _progressService;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;

        }

        public void Initialize()
        {
            ShowAdBtn.onClick.AddListener(OnShowAddClicked);

            RefreshAvailableAd();
        }

        public void Subscribe() => 
            _adsService.RewardedVideoReady += RefreshAvailableAd;


        public void Cleanup() => 
            _adsService.RewardedVideoReady -= RefreshAvailableAd;

        private void RefreshAvailableAd()
        {
            //var videoReady = _adsService.IsRewardedVideoReady;

            foreach (var adActiveObject in AdActiveObjects) 
                adActiveObject.SetActive(true);
            
            //foreach (var adInactiveObject in AdInactiveObjects) 
                //adInactiveObject.SetActive(!videoReady);
        }

        private void OnShowAddClicked() => 
            _adsService.ShowRewardedVideo(OnVideoFinished);

        private void OnVideoFinished() => 
            _progressService.Progress.WorldData.LootData.Add(_adsService.Reward);
    }
}