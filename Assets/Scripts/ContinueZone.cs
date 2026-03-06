using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu.HighLevel;

public class ContinueZone : MonoBehaviour
{

    [SerializeField] private string sceneToLoad = "Continue";
    [SerializeField] private float timeToWait = 1.5f;
    private bool timeElapsed = false;
    private float counter = 0f;
    private bool startCounter = false;

    // Update is called once per frame
    void Update()
    {
        if (startCounter)
        {
            counter += Time.deltaTime;
            if (counter >= timeToWait)
            {
                timeElapsed = true;
                startCounter = false;
            }
        }

        if (timeElapsed)
        {
            
            timeElapsed = false;
            counter = 0f;
            AccessibleTracker.Instance.Accessed(sceneToLoad, AccessibleTracker.AccessibleType.Area);
            SceneManager.LoadScene(sceneToLoad);

        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            startCounter = true;

    }
}
