using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIYidongZidan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameBody = GetComponent<GameBody>();
        myPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Header("是否允许 开启")]
    public bool IsCanStart = false;


    float flyXSpeed = 0.6f;
    float patrolDistance = 8;


    float JiangeTimes = 2;
    float JianGeJishi = 0;

    public void MoveZiDan()
    {
        if (!IsAtking) {
            JianGeJishi += Time.deltaTime;
        }
        else
        {
            ACToZidan();
            return;
        }
        
        if (JianGeJishi>= JiangeTimes)
        {
            JianGeJishi = 0;
            IsAtking = true;
            return;
        }
        Move();
    }


    bool isRunLeft = true;
    bool isRunRight = false;
    GameBody gameBody;
    Vector2 myPosition;

    private void Move()
    {
        if (isRunLeft)
        {
            gameBody.RunLeft(flyXSpeed);
            //print("   左 "+flySpeed);
            if (this.transform.position.x - myPosition.x < -patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                isRunLeft = false;
                isRunRight = true;

            }
        }
        else if (isRunRight)
        {
            gameBody.RunRight(flyXSpeed);
            //print("   右！！！ " + flySpeed);
            if (this.transform.position.x - myPosition.x > patrolDistance || gameBody.IsEndGround || gameBody.IsHitWall)
            {
                isRunLeft = true;
                isRunRight = false;

            }
        }
    }

    bool IsAtking = false;
    [Header("攻击动作名字")]
    public string ATK_1AC = "pengzidan_1";

    bool IsAtked = false;

    void ACToZidan()
    {
        if (!IsAtking) return;
        IsAtking = false;
        IsAtked = false;
        ToziDan();
        return;




        print(gameBody.GetDB().animation.lastAnimationName + "   p  " + gameBody.GetDB().animation.isCompleted+"  p111  "+gameBody.GetDB().animation.GetStates());
        if(gameBody.GetDB().animation.lastAnimationName == ATK_1AC&& gameBody.GetDB().animation.isCompleted)
        {
            print("------------->吐子弹！！！");
            IsAtking = false;
            IsAtked = false;
            gameBody.isAcing = false;
            ToziDan();
            return;
        }


        if(!IsAtked &&gameBody.GetDB().animation.lastAnimationName != ATK_1AC)
        {
            IsAtked = true;
            gameBody.isAcing = true;
            gameBody.GetDB().animation.GotoAndPlayByFrame(ATK_1AC);
        }
    }

    [Header("超出的 子弹数")]
    public int MoveZidanNums = 3;
    [Header("子弹名字")]
    public string ZidanName = "TX_zidan1";

    [Header("子弹 发射点")]
    public Transform ZidanFasheiPos;

    [Header("子弹 发射特效")]
    public ParticleSystem TX_ZidanFS;

    [Header("子弹 发射 音效")]
    public AudioSource Audio_ZDFS;

    void ToziDan()
    {
        if (TX_ZidanFS) TX_ZidanFS.Play();
        if (Audio_ZDFS) Audio_ZDFS.Play();

        int zidanNums = 2 + GlobalTools.GetRandomNum(MoveZidanNums);
        for (int i =0;i< zidanNums; i++)
        {

            float __x = 1+GlobalTools.GetRandomDistanceNums(3);
            __x = this.transform.localScale.x>0?-__x: __x;
            float __y = GlobalTools.GetRandomDistanceNums(-3)+ GlobalTools.GetRandomDistanceNums(3);
            Vector2 v2 = new Vector2(__x,__y);

            GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(ZidanName) as GameObject);
            zidan.GetComponent<TX_zidan>().CloseAutoFire();
            zidan.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            zidan.transform.localScale = this.transform.localScale;
            zidan.transform.position = ZidanFasheiPos.position;
            zidan.GetComponent<Rigidbody2D>().velocity = v2;

        }


    }


}
