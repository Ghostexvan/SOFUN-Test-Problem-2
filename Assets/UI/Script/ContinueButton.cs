using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject settingsPanel;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick(){
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
}
