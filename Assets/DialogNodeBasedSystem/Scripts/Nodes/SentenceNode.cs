using UnityEditor;
using UnityEngine;

namespace cherrydev
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Nodes/Sentence Node", fileName = "New Sentence Node")]
    public class SentenceNode : Node
    {
        [SerializeField] private Sentence sentence;

        [Space(10)]
        public Node parentNode;
        public Node childNode;

        [Space(7)]
        [SerializeField] private bool isExternalFunc;
        [SerializeField] private string externalFunctionName;

        private string externalButtonLable;

        private const float lableFieldSpace = 47f;
        private const float textFieldWidth = 100f;

        private const float externalNodeHeight = 155f;

        // Opciones para el dropdown
        private string[] dropdownOptions = new string[] { "Left", "Right", "Both" };
        private int selectedOptionIndex = 0;

        /// <summary>
        /// Returning external function name
        /// </summary>
        /// <returns></returns>
        public string GetExternalFunctionName()
        {
            return externalFunctionName;
        }

        /// <summary>
        /// Returning sentence character name
        /// </summary>
        /// <returns></returns>
        public string GetSentenceCharacterName()
        {
            return sentence.characterName;
        }

        /// <summary>
        /// Setting sentence text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public void SetSentenceText(string text)
        {
            sentence.text = text;
        }

        /// <summary>
        /// Returning sentence text
        /// </summary>
        /// <returns></returns>
        public string GetSentenceText()
        {
            return sentence.text;
        }

        /// <summary>
        /// Returning sentence character sprite
        /// </summary>
        /// <returns></returns>
        public Sprite GetCharacterSprite()
        {
            return sentence.characterSprite;
        }


        /// <summary>
        /// Returning second sprite of character if applicable
        /// </summary>
        /// <returns></returns>
        public Sprite GetSecondSprite()
        {
            return sentence.secondSprite;
        }

        /// <summary>
        /// Returning second sprite of character if applicable
        /// </summary>
        /// <returns></returns>
        public AudioClip GetAudioClip()
        {
            return sentence.audioClip;
        }

        /// <summary>
        /// Returns the value of a highlightSprite field
        /// </summary>
        /// <returns></returns>
        public string GetHighlightSprite()
        {
            return sentence.highlightSprite;
        }

        /// <summary>
        /// Returning second sprite of character if applicable
        /// </summary>
        /// <returns></returns>
        public Sprite GetBackground()
        {
            return sentence.background;
        }



        /// <summary>
        /// Returns the value of a isExternalFunc boolean field
        /// </summary>
        /// <returns></returns>
        public bool IsExternalFunc()
        {
            return isExternalFunc;
        }

#if UNITY_EDITOR

        /// <summary>
        /// Draw Sentence Node method
        /// </summary>
        /// <param name="nodeStyle"></param>
        /// <param name="lableStyle"></param>
        public override void Draw(GUIStyle nodeStyle, GUIStyle lableStyle)
        {
            base.Draw(nodeStyle, lableStyle);

            rect.height += 100f;

            GUILayout.BeginArea(rect, nodeStyle);

            EditorGUILayout.LabelField("Sentence Node", lableStyle);

            DrawCharacterNameFieldHorizontal();
            DrawSentenceTextFieldHorizontal();
            DrawCharacterSpriteHorizontal();
            DrawSecondSpriteHorizontal();
            DrawBackgroundHorizontal();
            DrawHighlightSpriteHorizontal();
            DrawAudioClipHorizontal();

            DrawExternalFunctionTextField();

            if (GUILayout.Button(externalButtonLable))
            {
                isExternalFunc = !isExternalFunc;

            }

            GUILayout.EndArea();
        }

        /// <summary>
        /// Draw label and text fields for char name
        /// </summary>
        private void DrawCharacterNameFieldHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Name ", GUILayout.Width(lableFieldSpace));
            sentence.characterName = EditorGUILayout.TextField(sentence.characterName, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draw label and text fields for sentence text
        /// </summary>
        private void DrawSentenceTextFieldHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Text ", GUILayout.Width(lableFieldSpace));
            sentence.text = EditorGUILayout.TextField(sentence.text, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draw label and text fields for char sprite
        /// </summary>
        private void DrawCharacterSpriteHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Sprite ", GUILayout.Width(lableFieldSpace));
            sentence.characterSprite = (Sprite)EditorGUILayout.ObjectField(sentence.characterSprite,
                typeof(Sprite), false, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draw label and text fields for external function, 
        /// depends on IsExternalFunc boolean field
        /// </summary>
        private void DrawExternalFunctionTextField()
        {
            if (isExternalFunc)
            {
                externalButtonLable = "Remove external func";

                EditorGUILayout.BeginHorizontal();
                rect.height = externalNodeHeight;
                EditorGUILayout.LabelField($"Func Name ", GUILayout.Width(lableFieldSpace));
                externalFunctionName = EditorGUILayout.TextField(externalFunctionName,
                    GUILayout.Width(textFieldWidth));
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                externalButtonLable = "Add external func";
                rect.height = standartHeight;
            }
        }

        /// <summary>
        /// Checking node size
        /// </summary>
        /// <param name="rect"></param>
        public void CheckNodeSize(float width, float height)
        {
            rect.width = width;

            if (standartHeight == 0)
            {
                standartHeight = height;
            }

            if (isExternalFunc)
            {
                rect.height = externalNodeHeight;
            }
            else
            {
                rect.height = standartHeight;
            }
        }

        /// <summary>
        /// Adding nodeToAdd Node to the childNode field
        /// </summary>
        /// <param name="nodeToAdd"></param>
        /// <returns></returns>
        public override bool AddToChildConnectedNode(Node nodeToAdd)
        {
            SentenceNode sentenceNodeToAdd;

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                nodeToAdd = (SentenceNode)nodeToAdd;

                if (nodeToAdd == this)
                {
                    return false;
                }
            }

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                sentenceNodeToAdd = (SentenceNode)nodeToAdd;

                if (sentenceNodeToAdd != null && sentenceNodeToAdd.childNode == this)
                {
                    return false;
                }
            }

            childNode = nodeToAdd;
            return true;
        }

        /// <summary>
        /// Adding nodeToAdd Node to the parentNode field
        /// </summary>
        /// <param name="nodeToAdd"></param>
        /// <returns></returns>
        public override bool AddToParentConnectedNode(Node nodeToAdd)
        {
            SentenceNode sentenceNodeToAdd;

            if (nodeToAdd.GetType() == typeof(AnswerNode))
            {
                return false;
            }

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                nodeToAdd = (SentenceNode)nodeToAdd;

                if (nodeToAdd == this)
                {
                    return false;
                }
            }

            parentNode = nodeToAdd;

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                sentenceNodeToAdd = (SentenceNode)nodeToAdd;

                if (sentenceNodeToAdd.childNode == this)
                {
                    return true;
                }
                else
                {
                    parentNode = null;
                }
            }

            return true;
        }


        /// <summary>
        /// Draw label and text fields for char sprite
        /// </summary>
        private void DrawAudioClipHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"AudioClip ", GUILayout.Width(lableFieldSpace));
            sentence.audioClip = (AudioClip)EditorGUILayout.ObjectField(sentence.audioClip,
                typeof(AudioClip), false, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSecondSpriteHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"2nd Sprite ", GUILayout.Width(lableFieldSpace));
            sentence.secondSprite = (Sprite)EditorGUILayout.ObjectField(sentence.secondSprite,
                typeof(Sprite), false, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();
        }

        private void DrawBackgroundHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Background ", GUILayout.Width(lableFieldSpace));
            sentence.background = (Sprite)EditorGUILayout.ObjectField(sentence.background,
                typeof(Sprite), false, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();
        }

        private void DrawHighlightSpriteHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Hightlight Sprite ", GUILayout.Width(lableFieldSpace));
            sentence.highlightSprite = EditorGUILayout.TextField(sentence.highlightSprite, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();
        }

#endif
    }
}