using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LauncherController : MonoBehaviour
{
    private List<GameObject> windowList = new List<GameObject>();
    private List<GameObject> historyWindow = new List<GameObject>();
    
    private void Awake() {
        windowList = GameObject.FindGameObjectsWithTag("Window").ToList();
        windowList.Sort(SortByName);
        OpenWindow(0);      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenWindow(int indexOpen){
        for (int index = 0; index < windowList.Count; index++){
            if (index != indexOpen){
                windowList[index].SetActive(false);
            }
        }

        windowList[indexOpen].SetActive(true);
        historyWindow.Add(windowList[indexOpen]);
    }

    public void Back(){
        historyWindow[historyWindow.Count - 1].SetActive(false);
        historyWindow.RemoveAt(historyWindow.Count - 1);
        historyWindow[historyWindow.Count - 1].SetActive(true);
    }

    public void StartGame(){
        SceneManager.LoadSceneAsync("BattleScene");
    }

    public void QuitGame(){
        Application.Quit();
    }

    static int SortByName(GameObject window1, GameObject window2)
    {
        return window1.name.CompareTo(
            window2.name
        );
    }
}
