using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cherrydev
{
    public class SentencePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogNameText;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private Image dialogCharacterImage;
        [SerializeField] private Image secondSpriteImage;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private AudioSource audioSource;
        


        /// <summary>
        /// Setting dialogText max visible characters to zero
        /// </summary>
        public void ResetDialogText()
        {
            dialogText.maxVisibleCharacters = 0;
        }

        /// <summary>
        /// Set dialog text max visible characters to dialog text length
        /// </summary>
        /// <param name="text"></param>
        public void ShowFullDialogText(string text)
        {
            dialogText.maxVisibleCharacters = text.Length;
        }

        /// <summary>
        /// Assigning dialog name text, character image sprite and dialog text
        /// </summary>
        /// <param name="name"></param>
        public void Setup(string name, string text, Sprite sprite, Sprite secondSprite, AudioClip audioClip, string highlightSprite, Sprite background)
        {
            dialogNameText.text = name;
            dialogText.text = text;

            if (sprite == null)
            {
                dialogCharacterImage.color = new Color(dialogCharacterImage.color.r,
                    dialogCharacterImage.color.g, dialogCharacterImage.color.b, 0);
            } else
            {
                dialogCharacterImage.color = new Color(dialogCharacterImage.color.r,
                        dialogCharacterImage.color.g, dialogCharacterImage.color.b, 255);
                dialogCharacterImage.sprite = sprite;

            }


            if (secondSprite == null)
            {
                secondSpriteImage.color = new Color(secondSpriteImage.color.r,
                    secondSpriteImage.color.g, secondSpriteImage.color.b, 0);
            }
            else
            {
                secondSpriteImage.color = new Color(secondSpriteImage.color.r,
                        secondSpriteImage.color.g, secondSpriteImage.color.b, 255);
                secondSpriteImage.sprite = secondSprite;
            }
            ToggleSpritesColor(highlightSprite, sprite, secondSprite);

            //Debug.Log(audioSource);

            if (audioClip != null && audioSource != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }

            if (background != null) {
                backgroundImage.sprite = background;
            }
        }

        private void ToggleSpritesColor(string highlightSprite, Sprite sprite, Sprite secondSprite)
        {
            var grayColor = new Color(147f / 255f, 141f / 255f, 141f / 255f, 1f);
            var normalColor = Color.white; // Color normal para el personaje que habla
            var hideColor = new Color(1f, 1f, 1f, 0f);
            //Debug.Log(highlightSprite);
            if (highlightSprite == "Left")
            {
                dialogCharacterImage.color = normalColor;
                secondSpriteImage.color = grayColor;
                if (secondSprite != null)
                    secondSpriteImage.color = grayColor;
                else secondSpriteImage.color = hideColor;
            } 
            else if (highlightSprite == "None")
            {
                dialogCharacterImage.color = grayColor;
                secondSpriteImage.color = grayColor;
            }
            else if (highlightSprite == "Right")
            {
                secondSpriteImage.color = normalColor;
                if (sprite != null)
                    dialogCharacterImage.color = grayColor;
                else dialogCharacterImage.color = hideColor;
            }
            else if (highlightSprite == "Both")
            {
                dialogCharacterImage.color = normalColor;
                secondSpriteImage.color = normalColor;
            }
        }

        /// <summary>
        /// Increasing max visible characters
        /// </summary>
        public void IncreaseMaxVisibleCharacters()
        {
            dialogText.maxVisibleCharacters++;
        }
    }
}