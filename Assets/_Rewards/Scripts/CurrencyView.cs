using UnityEngine;

namespace Rewards
{
    internal class CurrencyView : MonoBehaviour
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);
        private const string CoinKey = nameof(CoinKey);
        private const string CrystalKey = nameof(CrystalKey);

        private static CurrencyView _instance;
        public static CurrencyView Instance => _instance;

        [SerializeField] private CurrencySlotView _currencyWood;
        [SerializeField] private CurrencySlotView _currentDiamond;
        [SerializeField] private CurrencySlotView _currentCoin;
        [SerializeField] private CurrencySlotView _currentCrystal;

        private int Wood
        {
            get => PlayerPrefs.GetInt(WoodKey);
            set => PlayerPrefs.SetInt(WoodKey, value);
        }

        private int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey);
            set => PlayerPrefs.SetInt(DiamondKey, value);
        }
        private int Coin
        {
            get => PlayerPrefs.GetInt(CoinKey);
            set => PlayerPrefs.SetInt(CoinKey, value);
        }
        private int Crystal
        {
            get => PlayerPrefs.GetInt(CrystalKey);
            set => PlayerPrefs.SetInt(CrystalKey, value);
        }


        private void Awake() =>
            _instance = this;

        private void OnDestroy() =>
            _instance = null;

        private void Start()
        {
            _currencyWood.SetData(Wood);
            _currentDiamond.SetData(Diamond);
            _currentCoin.SetData(Coin);
            _currentCrystal.SetData(Crystal);
        }


        public void AddWood(int value)
        {
            Wood += value;
            _currencyWood.SetData(Wood);
        }

        public void AddDiamond(int value)
        {
            Diamond += value;
            _currentDiamond.SetData(Diamond);
        }
        public void AddDCoin(int value)
        {
            Coin += value;
            _currentCoin.SetData(Coin);
        }
        public void AddCrystal(int value)
        {
            Crystal += value;
            _currentCrystal.SetData(Crystal);
        }
    }
}
