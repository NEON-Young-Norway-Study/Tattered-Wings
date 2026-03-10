using UnityEngine;
using TMPro;
using System.Collections;
using System;
public class PopUp : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] private Animator animator;
    [SerializeField] private float timeOnScreen = 1.5f;
    public static event Action OnPopUpDestroyed;

    void Start()
    {
        StartCoroutine(DestroyWithAnimation());
    }
    public void SetText(string t)
    {
        text.text = t;
    }
    private IEnumerator DestroyWithAnimation()
    {
        yield return new WaitForSeconds(timeOnScreen); 

        animator.Play("PopUp_Anim"); 

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnPopUpDestroyed?.Invoke();
    }
}

    
