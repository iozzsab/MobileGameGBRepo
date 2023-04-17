using Profile;
using UnityEngine;

namespace Features.Game
{

    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Configs/" + nameof(GameConfig))]
    
    internal class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float SpeedCar { get; private set; }
       //public float SpeedCar; 
       //[field: SerializeField] public GameState InitialState { get; private set; }
    }
}