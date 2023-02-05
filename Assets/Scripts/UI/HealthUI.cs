using UnityEngine;
using UnityEngine.UI;
using Utils;
using DG.Tweening;

namespace UI
{
    public class HealthUI : MonoSingleton<HealthUI>
    {
        [SerializeField] private Image image;

        private bool isAnimating = false;
        private Tween tween;

        public void SetAmount(float fillAmount)
        {
            if (isAnimating)
            {
                tween.OnComplete(() =>
                {
                    SetAmount(fillAmount);
                });
                return;
            }
            
            isAnimating = true;
            tween = image.DOFillAmount(fillAmount, 0.2f).OnComplete(() =>
            {
                isAnimating = false;
            });
        }
    }
}
