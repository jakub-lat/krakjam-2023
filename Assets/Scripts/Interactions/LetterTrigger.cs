using DefaultNamespace;
using UI;
using UnityEngine;

namespace Interactions
{
    public class LetterTrigger : BaseInteraction
    {
        [SerializeField, TextArea] private string content;

        protected override string InteractionText => "Read letter";

        protected override void Interact()
        {
            LetterController.Current.Show(content);
        }
    }
}
