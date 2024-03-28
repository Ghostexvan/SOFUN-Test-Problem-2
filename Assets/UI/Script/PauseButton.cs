using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
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
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void OnClick(){
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
}
