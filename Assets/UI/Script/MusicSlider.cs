using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private void Awake() {
        GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);

        if (PlayerPrefs.HasKey("Music")){
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music");
        } else {
            GetComponent<Slider>().value = 0.1f;
        }
        
        GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>().volume = GetComponent<Slider>().value;
    }
    
    public void OnValueChanged(float value){
        GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>().volume = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("Music", value);
    }
}
