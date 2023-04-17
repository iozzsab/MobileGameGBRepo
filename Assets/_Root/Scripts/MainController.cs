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
    private ShedController _shedController;
    private GameController _gameController;
    
    private readonly List<GameObject> _suObjects = new List<GameObject>();
    private readonly List<IDisposable> _subDisposables = new List<IDisposable>();


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        
        DisposeControllers();
        DiposeSupDisposables();
        DisposeSubObjects();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void DisposeSubObjects()
    {
        foreach (GameObject gameObject in _suObjects)
            Object.Destroy(gameObject);
        _suObjects.Clear();
    }

    private void DiposeSupDisposables()
    {
        foreach (IDisposable disposable in _subDisposables)
            disposable.Dispose();
            //Object.Destroy(disposable);
        _subDisposables.Clear();
    }
    


    private void OnChangeGameState(GameState state)
    {
        DisposeControllers();

        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedController = CreateShedController(_placeForUi, _profilePlayer);
              //  _shedController = new ShedController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                break;
        }
    }

    private ShedController CreateShedController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        
        InventoryContext inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
        UpgradeHandlersRepository shedRepository = CreateShedRepository();
        ShedView shedView = LoadView(placeForUi);

        return new ShedController
        (
            shedView,
            profilePlayer,
            shedRepository
        );
            
        
    }

    private ShedView LoadView(Transform placeForUi)
    {
        
        var path = new ResourcePath("Prefabs/Shed/ShedView");
        GameObject prefab = ResourcesLoader.LoadPrefab(path);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);
        return objectView.GetComponent<ShedView>();
    }

    private UpgradeHandlersRepository CreateShedRepository()
    {
       var path = new ResourcePath("Configs/Shed/UpgradeItenConfigDataSource");
       UpgradeItemConfig[] upgradeItemConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(path);
       var upgradeHandlersRepository = new UpgradeHandlersRepository(upgradeItemConfigs);
       _subDisposables.Add(upgradeHandlersRepository);
       return upgradeHandlersRepository;
    }

    private InventoryContext CreateInventoryContext(Transform placeForUi, InventoryModel profilePlayerInventory)
    {
       
        var context = new InventoryContext(placeForUi, profilePlayerInventory);
        AddContext(context);

        return context;
    }

    private void DisposeControllers()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _shedController?.Dispose();
        _gameController?.Dispose();
    }
}
