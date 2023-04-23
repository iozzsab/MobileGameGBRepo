using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tween
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByInheritance : Button
    {
        public static string AnimationTypeName => nameof(_animationButtonType);
        public static string CurveEaseName => nameof(_curveEase);
        public static string DurationName => nameof(_duration);
        public static string DelayAnimation => nameof(_delayAnim);
        

        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private float _delayAnim = 5f;


       // [SerializeField] private float _ded = 5f;
        protected override void Awake()
        {
            base.Awake();
            InitRectTransform();
        }

        // protected override void OnValidate()
        // {
        //     // base.OnValidate();
        //     // InitRectTransform();
        // }

        private void InitRectTransform() =>
            _rectTransform ??= GetComponent<RectTransform>();


        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ActivateAnimation();
        }

        private void ActivateAnimation()
        {
            Delay(_delayAnim);
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase);
                    break;

                case AnimationButtonType.ChangePosition:
                    _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase);
                    break;
            }
        }

        
        private void Delay(float _delayAnim)
        {
            StartCoroutine(DelayCoroutine(_delayAnim));
        }
        private IEnumerator DelayCoroutine(float _delayAnim)
        {
            yield return new WaitForSeconds(_delayAnim);
        }
    }
}
