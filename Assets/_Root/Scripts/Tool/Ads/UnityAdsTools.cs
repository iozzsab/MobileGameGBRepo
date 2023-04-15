using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

namespace Tool.Ads
{
    public class UnityAdsTools : MonoBehaviour
    {
        public void ShowInterstitial()
        {
            throw new NotImplementedException();
        }

        public void ShowVideo(Action successShow)
        {
            throw new NotImplementedException();
        }

        public string androidAdUnitId; 
        public string iosAdUnitId;
        public string iosGameId;
        public string androidGameId;

        [Header("Banner options")] public BannerAdAnchor bannerAdAnchor = BannerAdAnchor.TopCenter;
        public BannerAdPredefinedSize bannerSize = BannerAdPredefinedSize.Banner;
        IBannerAd m_BannerAd;
        
        public void StartAds()
        {
            try
            {
                UnityServices.InitializeAsync(GetGameId());
                InitializationComplete();
            }
            catch (Exception e)
            {
                InitializationFailed(e);
            }
        }

        void OnDestroy()
        {
            m_BannerAd.Dispose();
        }

        InitializationOptions GetGameId()
        {
            var initializationOptions = new InitializationOptions();

#if UNITY_IOS
                if (!string.IsNullOrEmpty(iosGameId))
                {
                    initializationOptions.SetGameId(iosGameId);
                }
#elif UNITY_ANDROID
            if (!string.IsNullOrEmpty(androidGameId))
            {
                initializationOptions.SetGameId(androidGameId);
            }
#endif

            return initializationOptions;
        }

        void InitializationComplete()
        {
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
            var bannerAdSize = bannerSize.ToBannerAdSize();
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    m_BannerAd =
                        MediationService.Instance.CreateBannerAd(androidAdUnitId, bannerAdSize, bannerAdAnchor);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    m_BannerAd = MediationService.Instance.CreateBannerAd(iosAdUnitId, bannerAdSize, bannerAdAnchor);
                    break;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.LinuxEditor:
                    m_BannerAd = MediationService.Instance.CreateBannerAd(
                        !string.IsNullOrEmpty(androidAdUnitId) ? androidAdUnitId : iosAdUnitId, bannerAdSize,
                        bannerAdAnchor);
                    break;
                default:
                    return;
            } 
            LoadAd();
        }
        async void LoadAd()
        {
            await m_BannerAd.LoadAsync();
        }
        
        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        }

        void InitializationFailed(Exception error)
        {
            var initializationError = SdkInitializationError.Unknown;
            if (error is InitializeFailedException initializeFailedException)
            {
                initializationError = initializeFailedException.initializationError;
            }
        }
    }
}