using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using UnityEngine;

public class mirror : MonoBehaviour
{
    public GameObject lightObj;
    public GameObject controlObj;

    GameObject lightClone;
    public float rotY;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("light"))
        {
            if(lightClone == null)//控制了每个镜子只能生成一道光
            {
                lightClone = Instantiate(lightObj);
                //lightClone.transform.localPosition = new Vector3(0, controlObj.transform .rotation.eulerAngles.z==180? -2:2, 0);

                lightClone.transform.SetParent(controlObj.transform, false);
                lightClone.transform.position = controlObj.transform.position + new Vector3(0, 20, 0);
                //*(lightClone.transform.localRotation.eulerAngles.y / controlObj.transform.localRotation.eulerAngles.y)


                lightClone.transform.localEulerAngles = new Vector3(0, rotY, 0);
                controlObj.GetComponent<controlPanel>().lightObj = lightClone;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("light") && other.gameObject != lightClone)
        {
            Destroy(lightClone);
        }
    }
}
