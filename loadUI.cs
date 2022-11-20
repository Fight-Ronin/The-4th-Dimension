using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadUI : MonoBehaviour
{
    public Text loadText;
    public Image loadImg;

    public string[] storyText;
   // public Sprite[] storyImg;

    private float colorA;
    private int i = 0;
    public float timeDura;
    private float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        colorA = 0;
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if(colorA == 1)
        {
            timeCount += Time.deltaTime;
        }

        if(timeCount >= timeDura && colorA == 1)
        {
            StartCoroutine(FadeOut());
            timeCount = 0;
        }
    }

    IEnumerator FadeIn()
    {
        loadText.text = storyText[i];
     //   loadImg.sprite = storyImg[i];
        while (colorA < 1)
        {
            colorA += Time.deltaTime/2;
            loadImg.color = new Color(1, 1, 1, colorA);
            loadText.color = new Color(1, 1, 1, colorA);
            yield return new WaitForSeconds(0);
        }
        colorA = 1;
        timeCount = 0;
    }

    IEnumerator FadeOut()
    {
        while(colorA > 0)
        {
            colorA -= Time.deltaTime / 2;
            loadImg.color = new Color(1, 1, 1, colorA);
            loadText.color = new Color(1, 1, 1, colorA);
            yield return new WaitForSeconds(0);
        }
        colorA = 0;
        i += 1;
        if (i >= storyText.Length)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }
}
