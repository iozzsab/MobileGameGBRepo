using UnityEngine;

namespace BattleScripts
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 5f;
        private const float KPower = 1.5f;
        private const float MaxHealthPlayer = 20;
        private const float multiplePower = 0.2f;
        
        private const float levelCrime = 1f;
        private int _levelCrimePlayer;
        

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;


        public Enemy(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = playerData.Value;
                    break;
                
                case DataType.Crime:
                    _levelCrimePlayer = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            float healthRatio = _healthPlayer / MaxHealthPlayer;
            float moneyRatio = Mathf.Pow((_moneyPlayer / KMoney), 0.5f);
            float powerRatio = Mathf.Pow((_powerPlayer / KPower), 0.2f);
            float crimeRatio = Mathf.Pow((_levelCrimePlayer / levelCrime), 0.1f);
            float healthMoneyRatio = Mathf.Pow((healthRatio * moneyRatio), 0.3f);
            float powerHealthRatio = Mathf.Pow((powerRatio / healthRatio), 0.1f);
            

            return Mathf.RoundToInt((healthMoneyRatio + powerHealthRatio) + multiplePower * crimeRatio);
            // int kHealth = CalcKHealth();
            // float moneyRatio = _moneyPlayer / KMoney;
            // float powerRatio = (_powerPlayer / KPower) * 2f;
            //
            // return (int)(moneyRatio + kHealth + powerRatio * multiplePower);
        }

        private int CalcKHealth() =>
            _healthPlayer > MaxHealthPlayer ? 100 : 5;
    }
}

