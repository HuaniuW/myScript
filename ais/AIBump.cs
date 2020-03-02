using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (_player == null) _player = GlobalTools.FindObjByName("player");
    }

    GameObject _player;
    bool isStart = false;
    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform top;
    public Transform down;
    public Transform left;
    public Transform right;

    [Header("地面图层")]
    public LayerMask groundLayer;

    float hitTestDistance = 0.3f;
    bool IsHitTop
    {
        get
        {
            if (top == null) return false;
            Vector2 start = top.position;
            Vector2 end = new Vector2(start.x, start.y + hitTestDistance);
            Debug.DrawLine(start, end, Color.yellow);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }

    bool IsHitDown
    {
        get
        {
            if (down == null) return false;
            Vector2 start = down.position;
            Vector2 end = new Vector2(start.x, start.y - hitTestDistance);
            Debug.DrawLine(start, end, Color.yellow);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }

    bool IsHitLeft
    {
        get
        {
            if (left == null) return false;
            Vector2 start = left.position;
            Vector2 end = new Vector2(start.x- hitTestDistance, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }

    bool IsHitRight
    {
        get
        {
            if (right == null) return false;
            Vector2 start = right.position;
            Vector2 end = new Vector2(start.x + hitTestDistance, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }



    //接近 选点  同X前 同X后  前上  后上
    //冲击  计算冲击推力？还是直接给速度 
    //被攻击 是打断 还是不打断
    public bool IsBeHitOut = true;
    //冲击速度
    float bumpSpeed = 10;
    //冲击角度
    Vector2 GetJiaodu()
    {
        return _player.transform.position - this.transform.position;
    }
    //冲击
    public bool GetBump()
    {
        if (isStart)
        {
            gameBody.GetPlayerRigidbody2D().velocity = BumpSpeed;


            if (IsHitTop)
            {
                BumpSpeed = new Vector2(BumpSpeed.x, -BumpSpeed.y);
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y- hitTestDistance);
            }

            if (IsHitDown)
            {
                BumpSpeed = new Vector2(BumpSpeed.x, -BumpSpeed.y);
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + hitTestDistance);
            }

            if (IsHitLeft)
            {
                BumpSpeed = new Vector2(-BumpSpeed.x, BumpSpeed.y);
                this.transform.position = new Vector2(this.transform.position.x + hitTestDistance, this.transform.position.y );
            }

            if (IsHitRight)
            {
                BumpSpeed = new Vector2(-BumpSpeed.x, BumpSpeed.y);
                this.transform.position = new Vector2(this.transform.position.x - hitTestDistance, this.transform.position.y);
            }


            if (Time.deltaTime >= BumpTime)
            {
                isStart = false;
                TuoWeiTXStop();
                return false;
            }
        }
        return false;
    }

    float BumpTime = 1; 
    Vector2 BumpSpeed;
    GameBody gameBody;



    public void GetStart()
    {
        //停止移动 取消 寻路 和移动
        GetComponent<AirGameBody>().GetStop();
        //动作
        gameBody = GetComponent<GameBody>();
        if (gameBody.GetDB().animation.HasAnimation("")) gameBody.GetAcMsg("");
        //动作做完冲刺弹射  速度用推力还是速度？ 测试再看
        BumpSpeed = GetJiaodu();
        //gameBody.GetPlayerRigidbody2D().AddForce(BumpSpeed*400);
        //弹力炸弹
        //速度的话 要计算速度 总速度 或者 横向速度

        //播放特效
        TuoWeiTXPlay();

        isStart = true;
    }

    void TuoWeiTXPlay()
    {

    }

    void TuoWeiTXStop()
    {

    }
}
