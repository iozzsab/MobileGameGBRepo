using System;
using Features.Inventory;
using Features.Shed.Upgrade;
using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal class ShedContex : BaseContext
    {
        private static readonly ResourcePath _viewPath = new("Prefabs/Shed/ShedView");
         private static readonly ResourcePath  _dataSourcePath = new("Configs/Shed/UpgradeItenConfigDataSource");
        //private ShedController _shedController;
        // private readonly List<GameObject> _suObjects = new List<GameObject>();
        // private readonly List<IDisposable> _subDisposables = new List<IDisposable>();

        public ShedContex(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));
            if (profilePlayer == null)
                throw new ArgumentNullException(nameof(profilePlayer));
            CreateControllers(placeForUi, profilePlayer);

        }

        private ShedController CreateControllers(Transform placeForUi, ProfilePlayer profilePlayer)
        
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
        private InventoryContext CreateInventoryContext(Transform placeForUi, InventoryModel profilePlayerInventory)
        {
       
            var context = new InventoryContext(placeForUi, profilePlayerInventory);
            AddContext(context);

            return context;
        }
        private UpgradeHandlersRepository CreateShedRepository()
        {
            UpgradeItemConfig[] upgradeItemConfigs = loadConfigs();
            var upgradeHandlersRepository = new UpgradeHandlersRepository(upgradeItemConfigs);
            return upgradeHandlersRepository;
        }
        
        private UpgradeItemConfig[] loadConfigs () =>
            ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
        
        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);
            return objectView.GetComponent<ShedView>();
        }
    }
}