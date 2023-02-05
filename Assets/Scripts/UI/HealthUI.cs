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

        private bool isAnimating = false;
        private TweenerCore<float, float, FloatOptions> tween;

        public void SetAmount(float fillAmount)
        {
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
        }
    }
}
