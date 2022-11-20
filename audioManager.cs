using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;
    //创建一个表示audioSource的变量
    AudioSource audioS;

    public AudioClip clickSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);//在场景切换的时候不摧毁物体
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClickSound()
    {
        //播放点击的音效
        audioS.PlayOneShot(clickSound);
    }

}
