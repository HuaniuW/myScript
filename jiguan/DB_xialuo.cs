using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_xialuo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _startY = this.transform.position.y;
    }

    [Header("震动 声音")]
    public AudioSource AudioZD;

    //下落地板
    // Update is called once per frame
    void Update()
    {
        OutYRemoveSelf();
        TanSheing();
        if (IsXiaLuoBegin)
        {
            jishi += Time.deltaTime;
            if (jishi >= YCMiao)
            {
                IsXiaLuoBegin = false;
                Xialuo();
            }
        }
    }

    float _startY = 0;
    void OutYRemoveSelf() {
        if(Mathf.Abs(this.transform.position.y - _startY) >= 40)
        {
            this.gameObject.SetActive(false);
        }
    }

    float SpeedX = 0.7f;
    bool IsTanSheStarting = false;
    float lucheng = 0;
    float TSDistances = 25;

    [Header("是否弹射")]
    public bool IsTanShe = false;
    bool IsTanSheing = false;

    GameObject cubeF;
    void TanSheing()
    {
        if (!IsTanSheing) return;
        if (!IsTanSheStarting)
        {
            IsTanSheStarting = true;
            lucheng = this.transform.position.x;
        }

        
        if (this.transform.position.x - lucheng>= TSDistances)
        {

            IsTanSheing = false;
            //SpeedX -= 0.04f;
            //if (SpeedX <= 0)
            //{
            //    SpeedX = 0;
            //    IsTanSheing = false;
            //}
            IsXiaLuoBegin = true;
            
        }
        else
        {
            this.transform.position = new Vector2(this.transform.position.x + SpeedX, this.transform.position.y);
            foreach (Transform t in objList)
            {
                float cx = t.transform.position.x + SpeedX+0.01f;
                t.transform.position = new Vector3(cx, t.transform.position.y, t.transform.position.z);

                float cmx = cubeF.transform.position.x + SpeedX;
                cubeF.transform.position = new Vector3(cmx, cubeF.transform.position.y, cubeF.transform.position.z);
            }
            
        }
        

    }



    private void OnCollisionEnter2D(Collision2D Coll)
    {
        if (Coll.collider.tag == "Player") {
            print(" hit!!!!!!!!!! ");
            
            if (IsTanShe)
            {
                if (YanMu) YanMu.Play();
                IsTanSheing = true;
                cubeF = GameObject.Find("/MainCamera");
                if (!objList.Contains(Coll.collider.transform)) objList.Add(Coll.collider.transform);

            }
            else
            {
                XiaCheng();
            }

        }
    }

    Transform[] tarr = { };
    List<Transform> objList = new List<Transform>();

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print("Trigger - B");
        if (objList.Contains(collision.collider.transform)) objList.Remove(collision.collider.transform);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //print("Trigger - C  " + collision.collider.name);
        //float my = collision.collider.transform.position.x + mspeed;
        //collision.collider.transform.position = new Vector3(my, collision.collider.transform.position.y, collision.collider.transform.position.z);
        //collision.collider.transform.position = new Vector3(collision.collider.transform.position.x, my, collision.collider.transform.position.z);

    }



    //冒烟
    [Header("烟幕")]
    public ParticleSystem YanMu;

    bool IsXiaLuoBegin = false;

    //下沉一下
    void XiaCheng()
    {
        //this.transform.position = new Vector2(this.transform.position.x,this.transform.position.y-0.2f);
        if (YanMu) YanMu.Play();
        IsXiaLuoBegin = true;
        if (AudioZD) AudioZD.Play();
    }

    //延迟 几秒后
    [Header("延迟秒数")]
    public float YCMiao = 0.4f;
    float jishi = 0;
    //改变 type 让自由落体
    void Xialuo()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
   
}
