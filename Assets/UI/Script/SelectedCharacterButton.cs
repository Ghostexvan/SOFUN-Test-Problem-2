using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCharacterButton : MonoBehaviour
{
    [SerializeField]
    private Sprite defaultSprite;

    private int buttonIndex = 0;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TeamPreparation.Instance.GetPlayerTeam().GetCharacterInfos().Length <= buttonIndex ||
            TeamPreparation.Instance.GetPlayerTeam().GetCharacterInfos()[buttonIndex] == null){
            transform.GetChild(0).GetComponent<Image>().sprite = defaultSprite;
            GetComponent<Button>().interactable = false;
        } else {
            transform.GetChild(0).GetComponent<Image>().sprite = TeamPreparation.Instance.GetPlayerTeam().GetCharacterInfos()[buttonIndex].characterSprite;
            GetComponent<Button>().interactable = true;
        }
    }

    public void SetButtonIndex(int index){
        buttonIndex = index;
    }

    public void OnClick(){
        TeamPreparation.Instance.RemoveFromTeam(buttonIndex);
        TeamPreparation.Instance.HideCharacterInfo();
    }
}
