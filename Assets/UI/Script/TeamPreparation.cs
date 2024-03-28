using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class TeamPreparation : MonoBehaviour
{
    [SerializeField]
    private PlayerTeam playerTeam;

    [SerializeField]
    private CharacterPool characterPool;

    [SerializeField]
    private GameObject characterButtonPrefab;

    [SerializeField]
    private Button startButton;

    private List<GameObject> selectedCharacterButtons = new List<GameObject>();
    private List<GameObject> characterButtons = new List<GameObject>();
    private Transform characterButtonContent;
    private GameObject infoPanel;

    public static TeamPreparation Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        selectedCharacterButtons = GameObject.FindGameObjectsWithTag("SelectedCharacterButton").ToList();
        for (int index = 0; index < selectedCharacterButtons.Count; index++)
        {
            selectedCharacterButtons[index].GetComponent<SelectedCharacterButton>().SetButtonIndex(index);
        }

        characterButtonContent = GameObject.FindGameObjectWithTag("CharacterButtonContent").transform;
        foreach (CharacterInfo characterInfo in characterPool.characterInfos)
        {
            GameObject temp = Instantiate(characterButtonPrefab, characterButtonContent);
            temp.GetComponent<CharacterButton>().SetCharacterInfo(characterInfo);
            characterButtons.Add(temp);
        }

        infoPanel = transform.GetChild(1).gameObject;
        infoPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int currentTeamMember = 0;
        foreach (CharacterInfo characterInfo in playerTeam.GetCharacterInfos()){
            if (characterInfo != null) {
                currentTeamMember += 1;
            }
        }
        if (currentTeamMember == 0){
            startButton.interactable = false;
        } else {
            startButton.interactable = true;
        }
    }

    public void DisplayTeamMember()
    {
        
    }

    public void AddToTeam(CharacterInfo characterInfo)
    {
        Debug.Log("Try add to team " + characterInfo.characterPrefab.name);

        if (playerTeam.GetCharacterInfos().Contains(characterInfo)){
            return;
        }

        for (int index = 0; index < playerTeam.GetCharacterInfos().Length; index++)
        {
            Debug.Log("Try to find a place: " + index);
            if (playerTeam.GetCharacterInfos()[index] == null)
            {
                playerTeam.GetCharacterInfos()[index] = characterInfo;
                return;
            }
        }

    }

    public void RemoveFromTeam(int index)
    {
        playerTeam.GetCharacterInfos()[index] = null;
    }

    public PlayerTeam GetPlayerTeam()
    {
        return playerTeam;
    }

    public void DisplayCharacterInfo(CharacterInfo info){
        infoPanel.SetActive(true);

        infoPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = info.skillInfos[2].skillName;
        infoPanel.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = info.skillInfos[2].skillDescription;

        infoPanel.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = info.skillInfos[0].skillName;
        infoPanel.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = info.skillInfos[0].skillDescription;

        infoPanel.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = info.skillInfos[1].skillName;
        infoPanel.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = info.skillInfos[1].skillDescription;
    }

    public void HideCharacterInfo(){
        infoPanel.SetActive(false);
    }

    private void OnEnable()
    {
        DisplayTeamMember();
    }
}
