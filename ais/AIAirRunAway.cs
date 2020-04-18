using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAirRunAway : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _airGameBody = GetComponent<AirGameBody>();
        _behavior = GetComponent<AIAirBehavior>();
        _pointDistance = GetComponent<PointDistaction>();

    }
    AirGameBody _airGameBody;
    AIAirBehavior _behavior;
    PointDistaction _pointDistance;


    // Update is called once per frame
    void Update()
    {

    }

    public Transform top;
    public Transform down;
    public Transform qianmian;

    [Header("地面图层")]
    public LayerMask groundLayer;

    float hitTestDistance = 0.2f;
    public bool IsHitTop
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

    public bool IsHitDown
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

    public bool IsHitQianmain
    {
        get
        {
            if (qianmian == null) return false;
            Vector2 start = qianmian.position;
            Vector2 end = new Vector2(start.x - hitTestDistance, start.y);
            Debug.DrawLine(start, end, Color.blue);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }






    GameObject _obj;
    public void GetStart(GameObject obj, float yuanliDistance = 10)
    {
        ReSetAll();
        _obj = obj;
        _yuanliDistance = yuanliDistance;
    }



    //可以写个 随机 走的 给其他的用
    [Header("远离类型 1是随机 2是在左右各自方向避开")]
    public int yuanliType = 1;

    List<string> fxListh = new List<string> {"s", "hs", "h" };
    List<string> fxListq = new List<string> { "qs", "s", "q"};
    List<string> fxList = new List<string> { "qs", "s", "q" ,"hs","h"};
    float _yuanliDistance = 4f;

    Vector2 ChoseYuanliPos()
    {
        string fx = "no";
        Vector2 yuanliPos = Vector2.zero;


        if(fxListh.Count ==0 || fxListq.Count == 0|| fxList.Count ==0)
        {
            return yuanliPos;
        }


        if(yuanliType == 1)
        {
            fx = fxList[GlobalTools.GetRandomNum(fxList.Count)];
            fxList.Remove(fx);
        }
        else
        {
            if (this.transform.position.x > _obj.transform.position.x)
            {
                fx = fxListh[GlobalTools.GetRandomNum(fxListh.Count)];
                fxListh.Remove(fx);
            }
            else
            {
                fx = fxListq[GlobalTools.GetRandomNum(fxListq.Count)];
                fxListq.Remove(fx);
            }
        }

       



        //if (fxList.Count > 0)
        //{
        //    fx = fxList[GlobalTools.GetRandomNum(fxList.Count)];
        //}
        //else
        //{
        //    return yuanliPos;
        //}
         
        
        
        switch (fx)
        {
            case "qs":
                yuanliPos = new Vector2(_obj.transform.position.x - _yuanliDistance, _obj.transform.position.y + _yuanliDistance);
                break;
            case "s":
                yuanliPos = new Vector2(_obj.transform.position.x, _obj.transform.position.y + _yuanliDistance);
                break;
            case "hs":
                yuanliPos = new Vector2(_obj.transform.position.x + _yuanliDistance, _obj.transform.position.y + _yuanliDistance);
                break;
            case "q":
                yuanliPos = new Vector2(_obj.transform.position.x - _yuanliDistance, _obj.transform.position.y);
                break;
            case "h":
                yuanliPos = new Vector2(_obj.transform.position.x + _yuanliDistance, _obj.transform.position.y);
                break;
        }
        if (!_pointDistance.IsPointCanStay(yuanliPos, 2)) {
            print("碰撞了 重选 远离的点！！！！");
            return ChoseYuanliPos();
            //print("???               搂过来了？？？        ");
        } 
        return yuanliPos;
    }

    public void ReSetAll()
    {
        isStart = false;
        //isStarting = false;
        if(_behavior) _behavior.ReSetAll();
        fxListq = new List<string> { "qs", "s",  "q"};
        fxListh = new List<string> { "s", "hs", "h" };
        fxList = new List<string> { "qs", "s", "q", "hs", "h" };
    }


    bool isYuanliOver = false;
    bool isStart = false;
   //bool isStarting = false;
    Vector2 yuanliVector2;
    Vector2 yuanliPos;
    public bool GetYuanliOver()
    {
        
        if (GetComponent<RoleDate>().isBeHiting || fxListq.Count == 0|| fxListh.Count == 0||fxList.Count == 0) {
            ReSetAll();
            print(" 远离 结束了啊！！！！！！");
            return true;
        } 
        if (!isStart)
        {
            isStart = true;

            yuanliPos = ChoseYuanliPos();
            if (yuanliPos == Vector2.zero) return true;
            //yuanliVector2 = GlobalTools.GetVector2ByPostion(yuanliPos, this.transform.position, this.GetComponent<GameBody>().speedX);
            //isStarting = true;
            print(" ???远离的目标点-------------------------》      "+ yuanliPos);
            _behavior.ZhuijiZuoBiao(yuanliPos,0);
            _airGameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
        }

        //this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = yuanliVector2;

        zhuijiRun();

        //碰到边墙 提前结束
       /* if (IsHitDown || IsHitQianmain || IsHitTop)
        {
            //isStarting = false;
            print("碰到墙了！！！！！！！！！！！！！！！！！！");
            ReSetAll();
            return true;
        }*/

      /*  if(!_pointDistance.IsPointCanStay(yuanliPos, 2))
        {
            print("选取位置 在墙体内 不能停留");
            ReSetAll();
            return true;
        }*/

        if (((Vector2)this.transform.position - yuanliPos).sqrMagnitude < 1f)
        {
            //isStarting = false;
            print("到达目的地！！！！！！！！！！！！！！");
            ReSetAll();
            return true;
        }
        //this.GetComponent<GameBody>().speedX;

        return isYuanliOver;
    }


    void zhuijiRun()
    {
        if (GetComponent<AIChongji>() && GetComponent<AIChongji>().isTanSheing) return;
        GetComponent<AirGameBody>().isRunYing = true;
        _airGameBody.Run();
    }
}
