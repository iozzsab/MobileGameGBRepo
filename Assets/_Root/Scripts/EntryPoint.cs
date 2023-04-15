using System.Collections.Generic;
using Profile;
using Tool.Ads;
using Tool.Analytics;
using UnityEngine;
using UnityEngine.Analytics;


internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;
   
    

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;
    [SerializeField] private UnityAdsTools _adsTools;

    private MainController _mainController;


    private void Start() 
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsManager, _adsTools);
        
        _analyticsManager.SendMainMenuOpenEvent();
        
        // Analytics.CustomEvent("MainMenuOpened", new Dictionary<string, object>()
        // {
        //     ["speed"] = 5,
        //     ["player_name"] = "Jack"
        //
        // });
        
        //Analytics.Transaction()

    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
