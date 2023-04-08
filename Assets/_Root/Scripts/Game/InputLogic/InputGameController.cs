using Game.Car;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputGameController : BaseController
    {
        private readonly ResourcePath _resourcePath = GetResourcePath();
        private BaseInputView _view;

        private static ResourcePath GetResourcePath()
        {
#if UNITY_EDITOR
            return new ResourcePath("Prefabs/KeyboardMoveController");
#elif UNITY_ANDROID
            return new ResourcePath("Prefabs/MobileSingleStickControl");
#endif
        }

        public InputGameController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            CarModel car)
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, car.Speed);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private BaseInputView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<BaseInputView>();
        }
    }
}