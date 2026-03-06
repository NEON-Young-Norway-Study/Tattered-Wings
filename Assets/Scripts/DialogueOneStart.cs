using cherrydev;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Xasu.HighLevel;

[Serializable]
public class SentenceExtras
{
    public string dialogueText;
    public Sprite secondSprite;
    public AudioClip audioClip;
    public bool highlightSecondImage;
    public bool hideFirstImage;
}

public class DialogueOneStart : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;
    [SerializeField] private bool autoStartDialogue = true;
    [SerializeField] private bool nextSceneOnDialogFinished = true;
    [SerializeField] private string sceneToLoad = "LevelOne";
    [SerializeField] private Image secondImage;
    [SerializeField] private Image characterImage;
    public SentenceExtras[] sentencesExtras;

    private int currentSentenceExtrasIndex = 0;
    private AudioSource audioSource;
    private int nodesShown = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (dialogBehaviour != null)
        {
            dialogBehaviour.OnSentenceNodeActive += () =>
            {
                nodesShown++;
                CompletableTracker.Instance.Progressed(dialogGraph.name, CompletableTracker.CompletableType.DialogNode, nodesShown / (float)dialogGraph.nodesList.Count);
            };
            dialogBehaviour.OnDialogTextSkipped += (text) => AccessibleTracker.Instance.Skipped(dialogBehaviour.GetCurrentNode().name, AccessibleTracker.AccessibleType.Cutscene);
        }

        if (secondImage != null)
            secondImage.gameObject.SetActive(false);
        else
            Debug.LogWarning("Second image is not set in the inspector");
        dialogBehaviour.BindExternalFunction("ShowSentenceExtras", ShowSentenceExtras);
        //dialogBehaviour.OnDialogTextSkipped += DialogBehaviour_OnDialogTextSkipped;
        if (nextSceneOnDialogFinished)
            dialogBehaviour.AddListenerToDialogFinishedEvent(OnDialogFinished);
        currentSentenceExtrasIndex = 0;
        if (autoStartDialogue)
        {
            CompletableTracker.Instance.Initialized(dialogGraph.name, CompletableTracker.CompletableType.DialogNode);
            dialogBehaviour.StartDialog(dialogGraph);
        }
            
        
    }


    //private void DialogBehaviour_OnDialogTextSkipped(string obj)
    //{
    //    Debug.Log("Dialog Text Skipped: " + obj);
    //}

    public void ShowSentenceExtras()
    {
        if (currentSentenceExtrasIndex > sentencesExtras.Length) return;

        var extras = sentencesExtras[currentSentenceExtrasIndex];
        if (extras == null) return;

        var secondSprite = extras.secondSprite;
        
        if (secondSprite != null)
        {
            secondImage.gameObject.SetActive(true);
            secondImage.sprite = secondSprite;
        } 
        else
        {
            secondImage.gameObject.SetActive(false);
        }

        ToggleSprites(extras);

        audioSource.clip = extras.audioClip;
        audioSource.Play();

        currentSentenceExtrasIndex++;
    }

    private void ToggleSprites(SentenceExtras extras)
    {
        var grayColor = new Color(147f / 255f, 141f / 255f, 141f / 255f, 1f);
        var normalColor = Color.white; // Color normal para el personaje que habla

        if (extras.highlightSecondImage)
        {
            characterImage.color = grayColor;
            secondImage.color = normalColor;
        }
        else
        {
            secondImage.color = grayColor;
            characterImage.color = normalColor;
        }

        if (extras.hideFirstImage)
        {
            characterImage.gameObject.SetActive(false);
        }
        else
        {
            characterImage.gameObject.SetActive(true);
        }
    }

    public void OnDialogFinished()
    {
        CompletableTracker.Instance.Completed(dialogGraph.name, CompletableTracker.CompletableType.DialogNode);
        AccessibleTracker.Instance.Accessed(sceneToLoad, AccessibleTracker.AccessibleType.Area);
        SceneManager.LoadScene(sceneToLoad);
    }

}
