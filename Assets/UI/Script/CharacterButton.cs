using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour 
{
    private CharacterInfo characterInfo;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetCharacterInfo(CharacterInfo info){
        characterInfo = info;
        transform.GetChild(0).GetComponent<Image>().sprite = info.characterSprite;
    }

    public void OnClick(){
        TeamPreparation.Instance.AddToTeam(characterInfo);
        TeamPreparation.Instance.DisplayCharacterInfo(characterInfo);
    }
}