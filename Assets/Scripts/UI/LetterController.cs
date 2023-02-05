using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class LetterController : MonoSingleton<LetterController>
    {
        [SerializeField] private Text text;

        private CanvasGroup canvasGroup;

        public bool Visible => canvasGroup.alpha > 0;
        public bool FullVisible => canvasGroup.alpha == 1;

        protected override void Awake()
        {
            base.Awake();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
        }

        public void Show(string content)
        {
            canvasGroup.DOFade(1, 0.2f).SetUpdate(true);
            text.text = content;
            Time.timeScale = 0;
        }

        public void Hide()
        {
            canvasGroup.DOFade(0, 0.2f).SetUpdate(true);
            Time.timeScale = 1;
        }

        private void Update()
        {
            if (FullVisible && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
            {
                Hide();
            }
        }
    }
}
