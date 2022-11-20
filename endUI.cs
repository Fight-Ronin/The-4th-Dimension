using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endUI : MonoBehaviour
{
    public Text gameOverText;
    public GameObject prop;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        gameOverText.gameObject.SetActive(true);
        prop.SetActive(false);
    }
}
