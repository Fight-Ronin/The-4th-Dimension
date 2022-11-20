using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  button : MonoBehaviour
{

    bool isInArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(isInArea)
        {
            print("open the door");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<playerController>())
        {
            isInArea = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<playerController>())
        {
            isInArea = false;
        }
    }

}
