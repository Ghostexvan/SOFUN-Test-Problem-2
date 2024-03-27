using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int maxTurn;

    [SerializeField]
    private int currentTurn = 0;

    [SerializeField]
    private int currentCharacterTurnIndex = -1;

    [SerializeField]
    private Transform characterTransfom;

    private Transform blueTeamSpawn;
    private Transform redTeamSpawn;

    [SerializeField]
    private GameObject testPrefab;

    [SerializeField]
    private TeamList teamList = new TeamList();

    [SerializeField]
    private List<GameObject> attackOrder = new List<GameObject>();

    public static GameController Instance;

    [SerializeField]
    private TMP_Text turnText;

    [SerializeField]
    private GameObject announcePanel;

    private void Awake() {
        if (GameController.Instance == null) {
            GameController.Instance = this;
        }

        blueTeamSpawn = GameObject.FindGameObjectWithTag("BlueTeamSpawn").transform;
        if (blueTeamSpawn == null){
            Debug.LogError("[GAME CONTROLLER] Missing Blue Team Spawn transform", this);
        }

        redTeamSpawn = GameObject.FindGameObjectWithTag("RedTeamSpawn").transform;
        if (redTeamSpawn == null){
            Debug.LogError("[GAME CONTROLLER] Missing Red Team Spawn transform", this);
        }

        announcePanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {   
        for (int index = 0; index < blueTeamSpawn.childCount; index++) {
            teamList.blueTeamList.Add(
                Instantiate(
                    testPrefab, 
                    blueTeamSpawn.GetChild(index).position, 
                    blueTeamSpawn.GetChild(index).rotation,
                    characterTransfom.GetChild(0)
                )
            );
            teamList.blueTeamList[index].GetComponent<CharacterActionController>().SetSpriteOrder(index);
        }

        for (int index = 0; index < redTeamSpawn.transform.childCount; index++) {
            teamList.redTeamList.Add(
                Instantiate(
                    testPrefab, 
                    redTeamSpawn.transform.GetChild(index).position, 
                    redTeamSpawn.transform.GetChild(index).rotation,
                    characterTransfom.GetChild(1)
                )
            );
            teamList.redTeamList[index].GetComponent<CharacterActionController>().SetSpriteOrder(index);
        }

        GetAttackOrder();
    }

    // Update is called once per frame
    void Update()
    {
        turnText.text = "Turn: " + currentTurn + "/" + maxTurn;
    }

    public List<GameObject> GetEnemiesTeam(GameObject caster){
        // Debug.Log("Try to get enemy team");
        if (teamList.blueTeamList.Contains(caster)){
            // Debug.Log("Return red team");
            List<GameObject> redTeamList = new List<GameObject>();
            foreach(GameObject member in teamList.redTeamList){
                if (member.GetComponent<CharacterActionController>().GetCurrentHealth() > 0){
                    redTeamList.Add(member);
                }
            }
            return redTeamList;
        } else {
            // Debug.Log("Return blue team");
            List<GameObject> blueTeamList = new List<GameObject>();
            foreach(GameObject member in teamList.blueTeamList){
                if (member.GetComponent<CharacterActionController>().GetCurrentHealth() > 0){
                    blueTeamList.Add(member);
                }
            }
            return blueTeamList;
        }
    }

    private void GetAttackOrder(){
        currentTurn += 1;

        attackOrder.Clear();

        attackOrder.AddRange(teamList.blueTeamList);
        attackOrder.AddRange(teamList.redTeamList);

        attackOrder.Sort(SortBySpeed);

        currentCharacterTurnIndex = 0;

        CharacterProcessEffect(currentCharacterTurnIndex);
    }

    private void CharacterProcessEffect(int index){
        attackOrder[index].GetComponent<CharacterActionController>().ProcessEffect();
    }

    private void CharacterProcessSkill(int index){
        attackOrder[index].GetComponent<CharacterActionController>().ProcessSkill();
    }

    public void CharacterProcessSkill(){
        attackOrder[currentCharacterTurnIndex].GetComponent<CharacterActionController>().ProcessSkill();
    }

    public void CharacterDie(GameObject character){
        if (teamList.blueTeamList.Contains(character)){
            teamList.blueTeamList.Remove(character);
        } else {
            teamList.redTeamList.Remove(character);
        }

        attackOrder.Remove(character);
    }

    public void CharacterTurnFinish(GameObject character){
        currentCharacterTurnIndex = currentCharacterTurnIndex == attackOrder.Count - 1 ?
                                    -1 :
                                    currentCharacterTurnIndex + 1;

        int gameState = CheckGameState();

        if (gameState == 1){
            Debug.Log("Player Win!");
            announcePanel.SetActive(true);
            announcePanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Player Win!";
            return;
        } else if (gameState == -1){
            Debug.Log("Player Lose!");
            announcePanel.SetActive(true);
            announcePanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Player Lose!";
            return;
        }

        if (currentCharacterTurnIndex == -1){
            if (currentTurn < maxTurn){
                GetAttackOrder();
            } else {
                Debug.Log("Out of turn");
                Debug.Log("Player Lose!");
                announcePanel.SetActive(true);
                announcePanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Player Win!\nOut of Turn";
            }
        } else {
            CharacterProcessEffect(currentCharacterTurnIndex);
        }
    }

    private int CheckGameState(){
        List<GameObject> redTeamList = new List<GameObject>();
        foreach(GameObject member in teamList.redTeamList){
            if (member.GetComponent<CharacterActionController>().GetCurrentHealth() > 0){
                redTeamList.Add(member);
            }
        }

        if (redTeamList.Count == 0){
            return 1;
        }

        List<GameObject> blueTeamList = new List<GameObject>();
        foreach(GameObject member in teamList.blueTeamList){
            if (member.GetComponent<CharacterActionController>().GetCurrentHealth() > 0){
                blueTeamList.Add(member);
            }
        }

        if (blueTeamList.Count == 0){
            return -1;
        }

        return 0;
    }

    static int SortBySpeed(GameObject character1, GameObject character2){
        return character2.GetComponent<CharacterActionController>().GetCharacterData().moveSpeed.Value.CompareTo(
            character1.GetComponent<CharacterActionController>().GetCharacterData().moveSpeed.Value
        );
    }
}

[Serializable]
public struct TeamList{
    public List<GameObject> blueTeamList;
    public List<GameObject> redTeamList;
}