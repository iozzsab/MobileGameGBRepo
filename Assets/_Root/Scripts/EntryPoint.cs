using Features.Game;
using Features.Shed.IDependencies;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    //[SerializeField] private float _speedCar;
    [SerializeField] private GameState _initialState;
    
    [SerializeField] private GameConfig _gameConfig;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;
    
    private IShedController _shedController;
   

    private void Start()
    {
        
        var profilePlayer = new ProfilePlayer(_gameConfig.SpeedCar,  _initialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
