using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_cizuNove : MonoBehaviour
{
    public AudioSource AudioS;
    public ParticleSystem YanChen;
    public GameObject CiZu;
    public Transform TLPos;
    public GameObject HitKuai;

    public float MoveXSpeed = 1;

    [Header("前期 刺组Y 上升的 速度")]
    public float SpeedCiZuY = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
       
        //CiZu.transform.position = new Vector2(0,-1.56f);
        //HitKuai.transform.position = new Vector2();
        if (AudioS) AudioS.Play();
        if (YanChen) YanChen.Play();

        if (HitKuai) HitKuai.transform.position = new Vector2(HitKuai.transform.position.x, HitKuai.transform.position.y - 1.42f);
        if (CiZu) CiZu.transform.position = new Vector2(CiZu.transform.position.x, CiZu.transform.position.y-1.42f);

        //print(" xzScaleX     " + GetComponent<JN_Date>().xzScaleX);
    }

    bool SetChuShiX = false;
    float ChushiX = 0;
    float MaxMoveDistance = 60;

    void MoveMaxDistanceDisSelf()
    {
        if(Mathf.Abs(this.transform.position.x - ChushiX)>= MaxMoveDistance)
        {
            //print(ChushiX+" ?-  "+this.transform.position.x);
            if (AudioS) AudioS.Stop();
            if (YanChen) YanChen.Stop();
            SetChuShiX = false;
            ObjectPools.GetInstance().DestoryObject2(this.gameObject);
        }
    }





    // Update is called once per frame
    void Update()
    {
        if (!SetChuShiX)
        {
            SetChuShiX = true;
            ChushiX = this.transform.position.x;
            //print("*****************************************************************************************************************");
            //print("初始位置   " + ChushiX + "    ");
        }
        if(this.CiZu.transform.position.y< TLPos.position.y)
        {
            //print("");
            this.CiZu.transform.position = new Vector2(this.CiZu.transform.position.x, this.CiZu.transform.position.y + SpeedCiZuY);
            HitKuai.transform.position = new Vector2(HitKuai.transform.position.x, HitKuai.transform.position.y + SpeedCiZuY);
        }

        this.transform.position = new Vector2(this.transform.position.x + MoveXSpeed * -this.transform.localScale.x, this.transform.position.y);

        MoveMaxDistanceDisSelf();
    }
}
