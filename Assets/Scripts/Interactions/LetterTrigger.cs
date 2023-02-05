using DefaultNamespace;
using UI;
using UnityEngine;

namespace Interactions
{
    public class LetterTrigger : BaseInteraction
    {
        [SerializeField, TextArea] private string content;
        [SerializeField] private AudioClip letterAudio;
        [SerializeField] private AudioSource source;

        protected override string InteractionText => "Read letter";

        protected override void Interact()
        {
            LetterController.Current.Show(content);
            if (letterAudio != null)
            {
                source.PlayOneShot(letterAudio);
            }
        }
    }
}
