using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundAuto : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> backgroundSprite = new List<Sprite>();

    private int currentIndex = 0;

    [SerializeField]
    private float timeUntilChange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = backgroundSprite[currentIndex];
        StartCoroutine(ChangeBackground());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeBackground(){
        yield return new WaitForSeconds(timeUntilChange);
        currentIndex = currentIndex + 1 >= backgroundSprite.Count ?
                       0 :
                       currentIndex + 1;
        GetComponent<Image>().sprite = backgroundSprite[currentIndex];
        StartCoroutine(ChangeBackground());
    }
}
