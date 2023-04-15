using Profile;
using Tool;
using Tool.Ads;
using Tool.Analytics;
using UnityEditor;
using UnityEngine;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private AnalyticsManager _analyticsManager;
        private UnityAdsTools _adsTools;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager, UnityAdsTools adsTools)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame);
            _view.InitSettings(Settings);
             _view.InitAds(Ads);
            // _view.InitBuy(Purchase);
            _adsTools = adsTools;
            _analyticsManager = analyticsManager; 
            
        }

        private void Ads()
        {
 _adsTools.StartAds();
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);
            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
            Debug.Log("StartGame Enent Send");
            _analyticsManager.SendGameStartedEvent();
        }

        private void Settings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;
        
        
        

        
        

    }
}
