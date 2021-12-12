using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_MoveBase : MonoBehaviour
{
    [Header("顶点")]
    public Transform PosTop;
    [Header("底部 点")]
    public Transform PosDown;

    [Header("左边 点")]
    public Transform PosLeft;
    [Header("右边***点")]
    public Transform PosRight;

    [Header("是否 左右移动")]
    public bool IsLeftRightMove = true;

    [Header("是否 **上下移动")]
    public bool IsTopDownMove = false;


    [Header("移动 速度")]
    public float MoveSpeed = 0.4f;

    protected void MoveLeftRight()
    {
        float __x = this.transform.position.x;
        if(__x>= PosRight.position.x|| __x <= PosLeft.position.x)
        {
            //__x = PosRight.position.x;
            MoveSpeed *= -1;
        }
        __x += MoveSpeed;
        this.transform.position = new Vector2(__x, this.transform.position.y);
    }

    protected void MoveTopDpwn()
    {
        float __y = this.transform.position.y;
        if (__y >= PosTop.position.y || __y <= PosDown.position.y)
        {
            //__x = PosRight.position.x;
            MoveSpeed *= -1;
        }
        __y += MoveSpeed;
        this.transform.position = new Vector2(this.transform.position.x, __y);
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLeftRightMove) MoveLeftRight();
        if (IsTopDownMove) MoveTopDpwn();
    }
}
