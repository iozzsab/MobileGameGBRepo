using System;
using System.Collections.Generic;
using Features.Inventory;
using Ui;
using Game;
using Profile;
using UnityEngine;
using Features.Shed;
using Features.Shed.Upgrade;
using Tool;
using Object = UnityEngine.Object;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private GameController _gameController;
    private ShedContex _shedContext;
    
    //private ShedController _shedController;
    // private readonly List<GameObject> _suObjects = new List<GameObject>();
    // private readonly List<IDisposable> _subDisposables = new List<IDisposable>();


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        DisposeChildObjects();
        // DisposeControllers();
        // DiposeSupDisposables();
        // DisposeSubObjects();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }
    private void OnChangeGameState(GameState state)
    {
        //DisposeControllers();
        DisposeChildObjects();
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                //_shedController = CreateShedController(_placeForUi, _profilePlayer);
                //_shedController = new ShedController(_placeForUi, _profilePlayer);
                _shedContext = new ShedContex(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                break;
        }
    }
    private void DisposeChildObjects()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _gameController?.Dispose();
        _shedContext?.Dispose();
    }

    // private void DisposeSubObjects()
    // {
    //     foreach (GameObject gameObject in _suObjects)
    //         Object.Destroy(gameObject);
    //     _suObjects.Clear();
    // }
    //
    // private void DiposeSupDisposables()
    // {
    //     foreach (IDisposable disposable in _subDisposables)
    //         disposable.Dispose();
    //         //Object.Destroy(disposable);
    //     _subDisposables.Clear();
    // }
    //


    

    // private ShedController CreateShedController(Transform placeForUi, ProfilePlayer profilePlayer)
    // {
    //     
    //     InventoryContext inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
    //     UpgradeHandlersRepository shedRepository = CreateShedRepository();
    //     ShedView shedView = LoadView(placeForUi);
    //
    //     return new ShedController
    //     (
    //         shedView,
    //         profilePlayer,
    //         shedRepository
    //     );
    //         
    //     
    // }

    // private ShedView LoadView(Transform placeForUi)
    // {
    //     
    //     var path = new ResourcePath("Prefabs/Shed/ShedView");
    //     GameObject prefab = ResourcesLoader.LoadPrefab(path);
    //     GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
    //     AddGameObject(objectView);
    //     return objectView.GetComponent<ShedView>();
    // }

    // private UpgradeHandlersRepository CreateShedRepository()
    // {
    //    var path = new ResourcePath("Configs/Shed/UpgradeItenConfigDataSource");
    //    UpgradeItemConfig[] upgradeItemConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(path);
    //    var upgradeHandlersRepository = new UpgradeHandlersRepository(upgradeItemConfigs);
    //    _subDisposables.Add(upgradeHandlersRepository);
    //    return upgradeHandlersRepository;
    // }

    // private InventoryContext CreateInventoryContext(Transform placeForUi, InventoryModel profilePlayerInventory)
    // {
    //    
    //     var context = new InventoryContext(placeForUi, profilePlayerInventory);
    //     AddContext(context);
    //
    //     return context;
    // }

   
}
