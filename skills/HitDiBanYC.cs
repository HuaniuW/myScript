using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDiBanYC : MonoBehaviour
{
    [Header("碰撞地板探 测点")]
    public Transform HitPoint;

    [Header("判断是否在墙内 测点")]
    public Transform HitPoint2InWall;

    [Header("地面图层")]
    public LayerMask groundLayer;

    [Header("击中地面是否震动")]
    public bool IsZhenDong = true;

    [Header("烟尘特效名字")]
    public string YanChenName;

    [Header("烟尘特效")]
    public ParticleSystem TX_YanChen;


    


    public virtual bool IsHitGround
    {
        get
        {
            
            Vector2 start = HitPoint.position;
            Vector2 end = new Vector2(start.x, start.y - 1.8f);
            Debug.DrawLine(start, end, Color.white);
            bool _IsHitGround = Physics2D.Linecast(start, end, groundLayer);
            if (_IsHitGround)
            {
                print(" ft---  撞到地板了  ");
            }
            return _IsHitGround;
        }
    }


    public virtual bool IsInWall
    {

        get
        {
            Vector2 start = HitPoint2InWall.position;
            Vector2 end = new Vector2(start.x, start.y+2.2f);
            Debug.DrawLine(start, end, Color.magenta);
            bool _IsInWall = Physics2D.Linecast(start, end, groundLayer);
            if (_IsInWall)
            {
                Time.timeScale = 0;
                print("ft--- 在墙里面 ");
            }
            return _IsInWall;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        print("ft-----------------------------------------------------");
        //TX_YanChen.transform.parent = this.transform;
        if (IsHitGround && !IsInWall)
        {
            //GameObject YanChen = Resources.Load(YanChenName) as GameObject;
            //ObjectPools.GetInstance().SwpanObject2(YanChen);
            //YanChen.transform.position = HitPoint.position;
            //if (IsZhenDong) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.6"), this);
            //YanChen.transform.parent = this.transform.parent;
            print(" ft---  出烟尘。。。。。。。。  ");
            TX_YanChen.Simulate(0.0f);
            TX_YanChen.Play();
            //TX_YanChen.transform.parent = this.transform.parent;
            if (IsZhenDong) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.6"), this);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //if (IsHitGround && !IsInWall)
        //{
        //    //GameObject YanChen = Resources.Load(YanChenName) as GameObject;
        //    //ObjectPools.GetInstance().SwpanObject2(YanChen);
        //    //YanChen.transform.position = HitPoint.position;
        //    //if (IsZhenDong) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.6"), this);
        //    //YanChen.transform.parent = this.transform.parent;
        //    //print(" ft---  出烟尘。。。。。。。。  ");
        //    //TX_YanChen.Play();
        //    ////TX_YanChen.transform.parent = this.transform.parent;
        //    //if (IsZhenDong) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.6"), this);
        //}


        //if (HitPoint&& IsHitGround)
        //{
        //    //YanChen.transform.parent = this.transform.parent;
        //    //YanChen.Play();
        //    GameObject obj = Resources.Load(YanChenName) as GameObject;
        //    ObjectPools.GetInstance().SwpanObject2(obj);
        //    obj.transform.position = HitPoint.position;
        //    if (IsZhenDong) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.6"), this);
        //}
    }
}
