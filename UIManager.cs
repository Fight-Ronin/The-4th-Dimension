using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image ballImage;
    public RectTransform[] ballPostions;
    public float ballSpeed;
    public Image pointImage;
    public float rotateTime;
    public Slider healthBar;
    public Image fadeInImg;
    public GameObject startLandText;
    public GameObject pointProp;
    public GameObject pauseProp;
    public GameObject sliderProp;
    public GameObject buttonProp;
    public AudioMixer audioM;
    public Text mastserSliderText;
    public Text musicSliderText;

    internal bool isRight;

    Vector3 oriPos;
    Vector3 tarPos;
    Vector3 oriRot;
    Vector3 tarRot;
    Vector3 offsetRot;

    float timeCount;
    float colorA;
    bool isMoving;
    bool isRotating;

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        colorA = 1;
        StartCoroutine(FadeIn());
        instance = this;
        isMoving = true;
        oriPos = ballPostions[i].position;
        tarPos = ballPostions[i + 1].position;
        offsetRot = new Vector3(0, 0, 60);
        oriRot = new Vector3(0, 0, 0);
        tarRot = oriRot + offsetRot;
        startLandText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        #region ballSystem
        if (isMoving)
        {
            if (timeCount <= 1)
            {
                timeCount += Time.deltaTime * ballSpeed;
                // ballImage.rectTransform.position = Vector3.Lerp(oriPos, tarPos, timeCount);
                Vector3 center = (oriPos + tarPos) * 0.5f;
                center -= new Vector3(0, 1, 0);
                Vector3 oriCenter = oriPos - center;
                Vector3 tarCenter = tarPos - center;
                ballImage.rectTransform.position = Vector3.Slerp(oriCenter, tarCenter, timeCount);
                ballImage.rectTransform.position += center;
            }
            else
            {
                i += 1;
                if (i == ballPostions.Length - 1)
                {
                    isMoving = false;
                   // print("Game over");
                }
                else
                {
                    timeCount = 0;
                    oriPos = ballPostions[i].position;
                    tarPos = ballPostions[i + 1].position;
                }
            }
        }
        #endregion

        if(pointImage.rectTransform.eulerAngles.z != 300)
        {
            isRight = false;
        }
        else
        {
            isRight = true;
        }

        #region 暂停界面
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseProp.SetActive(true);
        }

        #endregion

    }

    public void PointRotate()
    {
        if(!isRotating)
        {
            StartCoroutine(StartRotate());
        }
    }

    IEnumerator StartRotate()
    {
        /*
        //count指的是转60所需要的帧数
        float count = rotateTime / Time.deltaTime;
        //i是用来记录总共目前过了多少帧
        float i = 0;
        while (i < count)
        {
            i += 1;
            pointImage.rectTransform.Rotate(0, 0, 60/count);
            yield return new WaitForEndOfFrame();
        }
        */
        isRotating = true;
        float count = 0;
        while (count <= 1)
        {
            count += Time.deltaTime;
            pointImage.rectTransform.localRotation = Quaternion.Lerp(Quaternion.Euler(oriRot), Quaternion.Euler(tarRot), count);
            yield return new WaitForEndOfFrame();
        }
        oriRot = tarRot;
        tarRot += offsetRot;
        isRotating = false;
    }

    IEnumerator FadeIn()
    {
        while(colorA > 0)
        {
            colorA -= Time.deltaTime / 2;
            fadeInImg.color = new Color(0, 0, 0, colorA);
            yield return new WaitForEndOfFrame();
        }
    }

    public void QuitPointBg()
    {
        pointProp.SetActive(false);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        pauseProp.SetActive(false);
    }

    public void GameToMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public  void SettingButton()
    {
        buttonProp.SetActive(false);
        sliderProp.SetActive(true);
    }

    public void BackSettingButton()
    {
        buttonProp.SetActive(true);
        sliderProp.SetActive(false);
    }

    public void ChangerMasterSlider(float value)
    {
        mastserSliderText.text = value.ToString();
        audioM.SetFloat("Master", (value - 80) * 50 / 100);
    }

    public void ChangerMusicSlider(float value)
    {
        musicSliderText.text = value.ToString();
        audioM.SetFloat("Music", (value - 80) * 50 / 100);
    }


}
