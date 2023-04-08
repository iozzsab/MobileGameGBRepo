using JoostenProductions;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal class InputKeyboard : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 0.05f;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        private void Move()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput > 0)
            {
                float moveValue = _speed * _inputMultiplier * Time.deltaTime;
                OnRightMove(moveValue);

            }
            else if (horizontalInput < 0)
            {
                float moveValue = _speed * _inputMultiplier * Time.deltaTime;
                OnLeftMove(moveValue);
            }
        }
        
    }
}