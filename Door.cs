using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///灯出发的门
/// </summary>
public class Door : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "light")
        {
            Destroy(gameObject);
        }
    }
}
