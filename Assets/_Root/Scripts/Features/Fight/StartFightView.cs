using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Features.Fight
{
    internal class StartFightView : MonoBehaviour
    {
        [SerializeField] private Button _startFightButton;
        [SerializeField] private Button _escapeToMenuButton;
        

        public void Init(UnityAction startFight, UnityAction escapeMenu)
        {
            _startFightButton.onClick.AddListener(startFight);
            _escapeToMenuButton.onClick.AddListener(escapeMenu);
        }


        private void OnDestroy()
        {
            _startFightButton.onClick.RemoveAllListeners();
            _escapeToMenuButton.onClick.RemoveAllListeners();
        }
    }
}
