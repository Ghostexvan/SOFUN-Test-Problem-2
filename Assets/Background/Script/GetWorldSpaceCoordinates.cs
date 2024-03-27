using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWorldSpaceCoordinates : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(GetCoordinates());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetCoordinates(){
        return this.gameObject.GetComponent<RectTransform>().TransformPoint(
            this.transform.position
        );
    }
}
