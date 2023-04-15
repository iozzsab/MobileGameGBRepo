using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAds;
        [SerializeField] private Button _buttonPurchase;
        
       
        
        public void Init(UnityAction startGame) =>
            _buttonStart.onClick.AddListener(startGame);


        public void InitSettings(UnityAction settings) =>
            _buttonSettings.onClick.AddListener(settings);

        public void InitAds(UnityAction ads) =>
            _buttonAds.onClick.AddListener(ads);
        public void InitBuy(UnityAction purchase) =>
            _buttonPurchase.onClick.AddListener(purchase);


        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonAds.onClick.RemoveAllListeners();
            _buttonPurchase.onClick.RemoveAllListeners();
        }
        
        
    }
}