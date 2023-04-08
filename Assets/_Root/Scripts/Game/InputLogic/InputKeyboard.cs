using JoostenProductions;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal class InputKeyboard : BaseInputView
    {
        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        private void Move()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput > 0)
            {
                float moveValue = _speed * Time.deltaTime;
                OnRightMove(moveValue);
            }
            else if (horizontalInput < 0)
            {
                float moveValue = _speed * Time.deltaTime;
                OnLeftMove(moveValue);
            }
        }
    }
}