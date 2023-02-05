using System;
using DG.Tweening;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private CanvasGroup endScreen;
    [SerializeField] private CanvasGroup button;

    private void Start()
    {
        endScreen.alpha = 0f;
        button.alpha = 0f;
        endScreen.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(("Player"))) return;
        
        FadeOut();
    }

    private void FadeOut()
    {
        var vcam = VCamInstance.Current;
        DOVirtual
            .Float(vcam.m_Lens.OrthographicSize, vcam.m_Lens.OrthographicSize + 5f, 2f, (v) => vcam.m_Lens.OrthographicSize = v)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                button.DOFade(1, 0.5f);
            });
        
        endScreen.gameObject.SetActive(true);
        endScreen.DOFade(1f, 3f)
            .SetEase(Ease.OutQuart);
    }
}
