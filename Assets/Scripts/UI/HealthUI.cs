using UnityEngine;
using UnityEngine.UI;
using Utils;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace UI
{
    public class HealthUI : MonoSingleton<HealthUI>
    {
        [SerializeField] private Image image;
        [SerializeField] private Color healBlinkColor;

        private bool isAnimating = false;
        private TweenerCore<float, float, FloatOptions> tween;

        private Color defaultColor;

        protected override void Awake()
        {
            base.Awake();
            defaultColor = image.color;
        }

        public void SetAmount(float fillAmount)
        {
            if (tween != null && !tween.IsPlaying())
            {
                isAnimating = false;
                tween = null;
            }
            
            if (isAnimating)
            {
                tween.ChangeEndValue(fillAmount, true);
                return;
            }

            Debug.Log("Updating healthbar");
            isAnimating = true; 
            tween = image.DOFillAmount(fillAmount, 0.2f).OnComplete(() =>
            {
                isAnimating = false;
                tween = null;
            });

            if (fillAmount > image.fillAmount)
            {
                image.DOColor(healBlinkColor, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    image.DOColor(defaultColor, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.5f);
                });
            }
        }
    }
}
