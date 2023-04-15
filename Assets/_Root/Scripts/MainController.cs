using Ui;
using Game;
using Profile;
using Tool.Ads;
using Tool.Analytics;
using UnityEngine;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    
    private GameController _gameController;
    private AnalyticsManager _analyticsManager;
    private UnityAdsTools _adsTools;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager,  UnityAdsTools adsTools)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _analyticsManager = analyticsManager;
        _adsTools = adsTools;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _analyticsManager, _adsTools);
                _gameController?.Dispose();
                _settingsMenuController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _mainMenuController?.Dispose();
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                break;
            default:
                _settingsMenuController?.Dispose();
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
