using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
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
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }
}
