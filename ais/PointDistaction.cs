using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointDistaction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

   
    GameObject _obj;
    [Header("地面图层")]
    public LayerMask groundLayer;


    // Update is called once per frame
    void Update()
    {
        
    }

   



    /// <summary>
    /// 判断两点之间 直线 是否碰撞到地板 墙壁
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    public bool IsTwoPointLineHitObj(Vector2 point1,Vector2 point2)
    {
        return Physics2D.Linecast(point1, point2, groundLayer); ;
    }


    public bool IsTwoPointInDistance(Vector2 point1,Vector2 point2,float distance)
    {
        return (point2 - point1).sqrMagnitude < distance; ;
    }

    public bool IsTwoPointInDistanceY(Vector2 point1, Vector2 point2, float distance)
    {
        return Mathf.Abs(point2.y - point1.y) < distance;
    }


    public bool IsPointCanStay(Vector2 point, float radius) {
        //判断4个方向 是否会碰撞

        //Physics2D.Linecast(start, end, groundLayer);
        //Physics2D.BoxCast(point, new Vector2(radius, radius),0, new Vector2(radius, radius), groundLayer,);

        Debug.DrawLine(point, new Vector2(point.x - radius, point.y), Color.red);
        Debug.DrawLine(point, new Vector2(point.x + radius, point.y), Color.red);
        Debug.DrawLine(point, new Vector2(point.x, point.y - radius), Color.red);
        Debug.DrawLine(point, new Vector2(point.x, point.y + radius), Color.red);


        if (Physics2D.Linecast(point, new Vector2(point.x - radius, point.y), groundLayer) ||
            Physics2D.Linecast(point, new Vector2(point.x + radius, point.y), groundLayer) ||
            Physics2D.Linecast(point, new Vector2(point.x,point.y-radius), groundLayer)||
            Physics2D.Linecast(point, new Vector2(point.x, point.y + radius), groundLayer)
            )
        {
            return false;
        }
        return true;
    }
}
