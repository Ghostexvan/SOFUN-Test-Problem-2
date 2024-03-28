using UnityEngine;
using UnityEngine.UI;

public class BackButton2 : MonoBehaviour
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
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
