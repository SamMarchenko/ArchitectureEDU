using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Infrastructure.Services.Ads
{
    public class AdsService : IAdsService
    {
        private const string AndroidGameId = "5303291";
        private const string IOSGameId = "5303290";

        private const string RewardedVideoPlacementId = "rewardedVideo";

        private string _gameId;
        private bool _isRewardedVideoReady;

        public event Action RewardedVideoReady;
        private Action _onVideoFinished;


        public bool IsRewardedVideoReady => _isRewardedVideoReady;
        public int Reward => 13;

        public void Initialize()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameId = AndroidGameId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _gameId = AndroidGameId;
                    break;
                default:
                    Debug.Log("Такая платформа не поддерживается");
                    break;
            }

            //Advertisement.Initialize(_gameId, true, this);
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            //Advertisement.Show(RewardedVideoPlacementId, this);
            _onVideoFinished = onVideoFinished;
        }
        
        
        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"OnUnityAdsAdLoaded {placementId}");

            if (placementId == RewardedVideoPlacementId)
            {
                //_isRewardedVideoReady = true;
                RewardedVideoReady?.Invoke();
            }
        }

        // public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) =>
        //     Debug.Log($"OnUnityAdsFailedToLoad {message}");
        //
        // public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) =>
        //     Debug.Log($"OnUnityAdsShowFailure {message}");
        //
        // public void OnUnityAdsShowStart(string placementId) =>
        //     Debug.Log($"OnUnityAdsShowStart {placementId}");
        //
        // public void OnUnityAdsShowClick(string placementId) => 
        //     Debug.Log($"OnUnityAdsShowClick {placementId}");
        //
        // public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        // {
        //     switch (showCompletionState)
        //     {
        //         case UnityAdsShowCompletionState.SKIPPED:
        //             Debug.Log($"OnUnityAdsShowComplete {showCompletionState}");
        //             break;
        //         case UnityAdsShowCompletionState.COMPLETED:
        //             _onVideoFinished?.Invoke();
        //             break;
        //         case UnityAdsShowCompletionState.UNKNOWN:
        //             break;
        //         default:
        //             Debug.Log($"OnUnityAdsShowComplete {showCompletionState}");
        //             break;
        //     }
        //
        //     _isRewardedVideoReady = false;
        //     _onVideoFinished = null;
        // }
        //
        // public void OnInitializationComplete()
        // {
        //     _isRewardedVideoReady = true;
        //     Advertisement.Load(RewardedVideoPlacementId);
        // }
        //
        // public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        // {
        //     _isRewardedVideoReady = false;
        // }
    }
}