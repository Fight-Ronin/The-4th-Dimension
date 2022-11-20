using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class entranceDoor : MonoBehaviour
{
    public GameObject doorLeft;
    public GameObject doorRight;
    public float rotateSpeed;
    public float rotateAngel;

    bool isIn;//用来判断玩家是否在开门的区域内
    bool isOpening;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isIn)
        {
            if(Input.GetKeyDown(KeyCode.E) && !isOpening && UIManager.instance.isRight)
            {
                isOpening = true;
                StartCoroutine(OpenDoor());
            }
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<playerController>())
        {
            isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<playerController>())
        {
            isIn = false;
        }
    }

    IEnumerator OpenDoor()
    {
        while (doorRight.transform.eulerAngles.y <= rotateAngel) 
        {
            doorLeft.transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            doorRight.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }

        
    }
}
