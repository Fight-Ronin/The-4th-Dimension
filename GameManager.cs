using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ParticleSystem explosion;
    public GameObject lavaArea;
    public float explosionRange;
    public float timeDura;
    internal float timeCount;
    internal Vector3 offset;
    internal bool isExplosed;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if(timeCount  >= timeDura)
        {
            float x = Random.Range(-explosionRange, explosionRange);
            float z = Random.Range(-explosionRange, explosionRange);
            offset = new Vector3(x, 0, z);
            ParticleSystem explosionClone =  Instantiate(explosion, lavaArea.transform.position + offset, new Quaternion());
            timeCount = 0;
            isExplosed = true;
            Destroy(explosionClone, 5f);
        }
    }
}
