using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Speak");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Speak()
    {
        while (true)
        {
            print("Hello");
            yield return new WaitForSeconds(1);
            print("unity");
            yield return new WaitForSeconds(1);
        }

    }
}
