using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEditor;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    float horizontalInput, verticalInput;
    float rotateInputX, rotateInputY;

    Vector3 startHeight;
    Vector3 finalHeight;

    public float moveSpeed;
    public float jumpSpeed;
    public float fallSpeed;

    public float rayDistance;
    public float rotateAngel;

    public  float healthNumber = 100;
    public float heightDistance;
    public float fallDamage;
    public float explosedDistance;
    public float explosedDamage;
    public Transform pickUpPos;
    public float lightRotSpeed;


    private bool canPick;
    private GameObject pickUpObj;
    private bool isPicking;
    private bool canPoint;
    private GameObject lightObj;
    private float minRot;
    private float maxRot;
    private float offsetRot;
    private bool isRotating;
    //internal指的是变量的类型是公开的,但是不会unity窗口中的显示

    // Start is called before the first frame update
    void Start()
    {
        offsetRot = 5;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        #region 人物移动
        if(isRotating == false)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }

        anim.SetFloat("speed", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        rotateInputX = Input.GetAxis("Mouse X");
        rotateInputY = Input.GetAxis("Mouse Y");

        if(Time.timeScale == 1)
        {
            Camera.main.transform.Rotate(-rotateInputY, 0, 0);
            transform.Rotate(0, rotateInputX, 0);
        }

        Vector3 speedWorld = new Vector3(horizontalInput * moveSpeed, rb.velocity.y - fallSpeed, verticalInput * moveSpeed);
        Vector3 speedLocal = transform.TransformVector(speedWorld);
        if(!UIManager.instance.pointProp.activeSelf )//玩家在没有用转盘的时候才可以移动
        {
            rb.velocity = speedLocal;
        }
        #endregion

        #region 相机控制
        if (Camera.main.transform.localEulerAngles.x >= rotateAngel && Camera.main.transform.localEulerAngles.x <= 180)
        {
            Camera.main.transform.localEulerAngles = new Vector3(rotateAngel, 0, 0);
        }

        if (Camera.main.transform.localEulerAngles.x <= 360 - rotateAngel && Camera.main.transform.localEulerAngles.x >= 180)
        {
            Camera.main.transform.localEulerAngles = new Vector3(360 - rotateAngel, 0, 0);
        }
        #endregion

        #region 人物跳跃/掉落
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            anim.SetTrigger("jump");
        }

        if(isGrounded() == false && rb.velocity.y < 0 && startHeight == new Vector3(0,0,0))
        {
            startHeight = transform.position;
        }
        #endregion

        #region 爆炸检测
        if (GameManager.instance.isExplosed)
        {
            float distance = Vector3.Distance(transform.position, GameManager.instance.lavaArea.transform.position + GameManager.instance.offset);
           // print(distance);
            if(distance < explosedDistance)
            {
                healthNumber -= explosedDamage * (explosedDistance - distance);
            }
            GameManager.instance.isExplosed = false;
        }
        #endregion

        //将人物的血量赋值给UI中显示血量滑条的value
        UIManager.instance.healthBar.value = healthNumber;

        #region 捡/用道具
        if(canPick)
        {
            if (Input.GetMouseButtonDown(0) && !isPicking)
            {
                pickUpObj.GetComponent<Rigidbody>().isKinematic = true;
                pickUpObj.transform.position = pickUpPos.position;
                pickUpObj.transform.SetParent(this.transform);
                isPicking = true;
            }
        }



        if (isPicking)//你捡到了工具
        {
            if (Input.GetMouseButtonDown(1) && isRotating == false)//你按下了鼠标右键
            {
                if(lightObj != null)//你在控制台内
                {
                    if (lightObj.transform.localEulerAngles == new Vector3(0,minRot,0))
                    {
                        Debug.Log("正角度"+lightObj.transform.localEulerAngles);
                        offsetRot = 5;
                    }
                    if (lightObj.transform.localEulerAngles == new Vector3(0, maxRot, 0))
                    {
                        offsetRot = -5;
                        Debug.Log("负角度"+lightObj.transform.localEulerAngles);
                    }
                    //lightObj.transform.eulerAngles += new Vector3(0, offsetRot, 0);//每次光的角度转5度
                    StartCoroutine(StartRotate(offsetRot));
                }
            }
        }
        #endregion

        #region 使用转盘
        if(canPoint && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.instance.pointProp.SetActive(true);
        }
        #endregion
    }

    bool isGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, rayDistance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, rayDistance))
        {
            if (hitInfo.collider.CompareTag("Ground"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("startLandStone"))
        {
            UIManager.instance.startLandText.SetActive(true);
        }

        if(other.gameObject.GetComponent<controlPanel>())
        {
            lightObj = other.gameObject.GetComponent<controlPanel>().lightObj;
            minRot = other.gameObject.GetComponent<controlPanel>().minRot;
            maxRot = other.gameObject.GetComponent<controlPanel>().maxRot;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<prop>())
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if(Physics.Raycast(ray,out hitInfo))
                {
                    if (hitInfo.collider.GetComponent<prop>())
                    {
                        print("get it");
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("startLandStone"))
        {
            UIManager.instance.startLandText.SetActive(false);
        }

        if (other.gameObject.GetComponent<controlPanel>())
        {
            lightObj = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ground ground = collision.gameObject.GetComponent<ground>();
        if (ground && startHeight != new Vector3(0,0,0))
        {
            finalHeight = transform.position;
            float distance = Vector3.Distance(startHeight, finalHeight);
            print(distance);
            if (distance >= heightDistance)
            {
                healthNumber -= distance * fallDamage * ground.groundDamage;
            }
            startHeight = new Vector3(0, 0, 0);
        }

        if (collision.gameObject.CompareTag("key"))
        {
            canPick = true;
            pickUpObj = collision.gameObject;
        }

        if(collision.gameObject.CompareTag("pointProp"))
        {
            canPoint = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            canPick = false;
            pickUpObj = null;
        }

        if (collision.gameObject.CompareTag("pointProp"))
        {
            canPoint = false;
        }
    }

    IEnumerator StartRotate(float rot)
    {
        isRotating = true;
        float count = 0;
        Vector3 oriRot = lightObj.transform.localEulerAngles;
        float dir;
        if(rot > 0)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        while (count <= Math.Abs(rot))
        {
            count += Time.deltaTime * lightRotSpeed;
            lightObj.transform.Rotate(0, dir * Time.deltaTime * lightRotSpeed, 0);
            yield return new WaitForEndOfFrame();
        }
        lightObj.transform.localEulerAngles = oriRot + new Vector3(0, rot, 0);
        isRotating = false;

    }
}
