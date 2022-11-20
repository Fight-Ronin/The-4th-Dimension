using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuUI : MonoBehaviour
{
    public GameObject mainPorp;
    public GameObject controlProp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LoadScene");
    }

    public void ClickControlButton()
    {
        audioManager.instance.PlayClickSound();
        mainPorp.SetActive(false);
        controlProp.SetActive(true);
    }

    public void ControlToMain()
    {
        mainPorp.SetActive(true);
        controlProp.SetActive(false);
    }


    public void QuitGame()
    {
        //应用退出
        Application.Quit();
    }


}
