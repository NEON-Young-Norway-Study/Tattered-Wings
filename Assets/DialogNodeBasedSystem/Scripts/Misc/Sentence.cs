using UnityEngine;

namespace cherrydev
{
    [System.Serializable]
    public struct Sentence
    {
        public string characterName;
        public string text;
        public Sprite characterSprite;
        public Sprite secondSprite;
        public AudioClip audioClip;
        public string highlightSprite;
        public Sprite background;

        public Sentence(string characterName, string text)
        {
            characterSprite = null;
            audioClip = null;
            secondSprite = null;
            background = null;
            highlightSprite = "Left";
            this.characterName = characterName;
            this.text = text;
        }
    }
}