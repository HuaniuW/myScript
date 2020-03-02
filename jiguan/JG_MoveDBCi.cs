using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_MoveDBCi : MonoBehaviour
{
    //移动距离
    [Header("移动距离")]
    public float MoveDistance = 10;
    //移动方向  上下左右
    [Header("移动方向 上下左右")]
    public string MoveDirection = "left";
    //声音
    [Header("石墙移动声音")]
    public AudioSource MoveSound;
    //击中墙壁停止时候的声音
    [Header("石墙撞墙停止的 撞击声音")]
    public AudioSource HitSound;
    //震动
    //撞击灰尘
    [Header("撞击扬起的尘埃")]
    public ParticleSystem Chenai;


    [Header("移动速度")]
    public float MoveSpeed = 1;


    public bool IsStart = false;

    [Header("是否返回 做下压机关可以用")]
    public bool IsReturn = false;

    //初始位置
    Vector2 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = this.transform.position;

    }

    public void GetStart()
    {
        IsStart = true;
    }



    void MoveLeft()
    {
        float _x = this.transform.position.x - MoveSpeed; 
        this.transform.position = new Vector2(_x,this.transform.position.y);
        GSMoveX(-MoveSpeed);
        if (MoveSound) MoveSound.Play();
        

        if (Mathf.Abs(this.transform.position.x - initPos.x) >= MoveDistance) {
            if (HitSound) HitSound.Play();
            if (MoveSound) MoveSound.Stop();
            //播放撞击尘埃
            if (Chenai) Chenai.Play();
            IsStart = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStart)
        {
            if(MoveDirection == "left")
            {
                MoveLeft();
            }
        }
    }

    //角色在地板上的时候 跟随移动
    private void GSMoveX(float mSpeed)
    {
       
        foreach (Transform t in objList)
        {
            //print("????    "+t.name);
            if (t == null) return;
            float cx = t.transform.position.x + mSpeed;
            t.transform.position = new Vector3(cx, t.transform.position.y, t.transform.position.z);
        }
    }

    public void ChangeMoveSpeed(float moveSpeed)
    {
        MoveSpeed = moveSpeed;
    }


    Transform[] tarr = { };
    List<Transform> objList = new List<Transform>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("Trigger - A");
        //obj2 = collision.collider.transform;
        if (!objList.Contains(collision.collider.transform)) objList.Add(collision.collider.transform);

    }

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
}
