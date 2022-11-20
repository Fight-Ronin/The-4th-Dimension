using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 传送门
/// </summary>
public class Gateway : MonoBehaviour
{
    public GameObject winImage;//胜利面板
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "playerNew")
        {
            winImage.SetActive(true);
          //  Destroy(other.gameObject);
        }
    }
}
