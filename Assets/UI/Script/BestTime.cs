using System;
using TMPro;
using UnityEngine;

public class BestTime : MonoBehaviour
{
    private void Awake() {
        if (PlayerPrefs.HasKey("BestTime")){
            GetComponent<TMP_Text>().text = "Best time: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("BestTime")).ToString("mm:ss");
        } else {
            GetComponent<TMP_Text>().text = "Best time: --:--";
        }
    }
}
