using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject popUp_prefab;
    [SerializeField] Transform[] possible_pos;
    [SerializeField] public string[] possible_text_popUp;

    private int active_popUpCount = 0;
    private string lastTextUsed = "";

    public void CreatePopUp()
    {
        if (popUp_prefab != null && active_popUpCount == 0)
        {
            active_popUpCount++;
            int tmp = Random.Range(0, possible_pos.Length);
            GameObject popUp = Instantiate(popUp_prefab, possible_pos[tmp]);
            popUp.gameObject.GetComponent<PopUp>().SetText(PopUpText());
        }
    }

    private string PopUpText()
    {
        string newText = possible_text_popUp[Random.Range(0, possible_text_popUp.Length)];

        while (newText == lastTextUsed)
        {
            newText = possible_text_popUp[Random.Range(0, possible_text_popUp.Length)];
        }

        lastTextUsed = newText;

        return newText;
    }
    private void OnEnable()
    {
        PopUp.OnPopUpDestroyed += HandlePopUpDestroyed;
    }

    private void OnDisable()
    {
        PopUp.OnPopUpDestroyed -= HandlePopUpDestroyed;
    }

    private void HandlePopUpDestroyed()
    {
        active_popUpCount--; 
    }
}
