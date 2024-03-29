using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick(){
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("LauncherScene");
    }
}
