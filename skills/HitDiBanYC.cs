using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDiBanYC : MonoBehaviour
{
    [Header("碰撞地板探 测点")]
    public Transform HitPoint;

    [Header("地面图层")]
    public LayerMask groundLayer;

    [Header("击中地面是否震动")]
    public bool IsZhenDong = true;

    [Header("烟尘特效名字")]
    public string YanChenName;

    public virtual bool IsHitGround
    {
        get
        {
            //print("groundCheck 是否有这个 变量   "+ groundCheck);
            Vector2 start = HitPoint.position;
            Vector2 end = new Vector2(start.x, start.y - 1);
            Debug.DrawLine(start, end, Color.blue);
            return Physics2D.Linecast(start, end, groundLayer);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
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
