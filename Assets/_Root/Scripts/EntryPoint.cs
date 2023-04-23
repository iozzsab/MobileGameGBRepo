using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private InitialProfilePlayer _initialProfilePlayer;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = CreateProfilePlayer(_initialProfilePlayer);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }


    private ProfilePlayer CreateProfilePlayer(InitialProfilePlayer initialProfilePlayer) =>
        new ProfilePlayer
        (
            initialProfilePlayer.Car.Speed,
            initialProfilePlayer.Car.JumpHeight,
            initialProfilePlayer.State
        );
}
