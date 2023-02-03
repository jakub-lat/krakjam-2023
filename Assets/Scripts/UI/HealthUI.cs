using UnityEngine;
using UnityEngine.UI;
using Utils;
using DG.Tweening;

namespace UI
{
    public class HealthUI : MonoSingleton<HealthUI>
    {
        [SerializeField] private Image image;

        public void SetAmount(float fillAmount)
        {
            image.DOFillAmount(fillAmount, 0.2f);
        }
    }
}
