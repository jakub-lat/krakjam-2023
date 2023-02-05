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

        private Action onHide;

        protected override void Awake()
        {
            base.Awake();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
        }

        public void Show(string content, Action onHide = null)
        {
            canvasGroup.DOFade(1, 0.2f).SetUpdate(true);
            text.text = content;
            Time.timeScale = 0;
            this.onHide = onHide;
        }

        public void Hide()
        {
            canvasGroup.DOFade(0, 0.2f).SetUpdate(true);
            Time.timeScale = 1;
            onHide?.Invoke();
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
