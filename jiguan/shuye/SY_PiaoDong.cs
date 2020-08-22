using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SY_PiaoDong : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XuanZhuanChushi = this.transform.rotation.z;
        DangqianXuanzhuanDushu = XuanZhuanChushi;

        XChushi = this.transform.position.x;
        DangqianXMoveDistance = XChushi;

        YChushi = this.transform.position.y;
        DangqianYMoveDistance = YChushi;

        XSFChushi = this.transform.localScale.x;
        XDangqianSFChengdu = XSFChushi;

        YSFChushi = this.transform.localScale.y;
        YDangqianSFChengdu = YSFChushi;

    }

    float XuanZhuanChushi = 0;
    [Header("旋转的度数")]
    public float XuanzhuanDushu = 0;
    float DangqianXuanzhuanDushu = 0;

    float XChushi = 0;
    [Header("X移动的距离")]
    public float XMoveDistance = 0;
    float DangqianXMoveDistance = 0;

    float YChushi = 0;
    [Header("Y移动的距离")]
    public float YMoveDistance = 0;
    float DangqianYMoveDistance = 0;

    float XSFChushi = 1;
    [Header("缩放程度")]
    public float XSFChengdu = 1;
    float XDangqianSFChengdu= 0;

    float YSFChushi = 1;
    [Header("缩放程度")]
    public float YSFChengdu = 1;
    float YDangqianSFChengdu = 0;




    void Xuanzhuan()
    {
        if (XuanzhuanDushu == 0) return;
        DangqianXuanzhuanDushu += (XuanzhuanDushu - DangqianXuanzhuanDushu)*0.02f;
        if(DangqianXuanzhuanDushu >= XuanzhuanDushu - 0.1f|| DangqianXuanzhuanDushu<= XuanzhuanDushu + 0.1f)
        {
            //DangqianXuanzhuanDushu = XuanzhuanDushu;
            XuanzhuanDushu *= -1;
        }
        //this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, DangqianXuanzhuanDushu);
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, DangqianXuanzhuanDushu);
        print("@@d  "+ this.transform.eulerAngles);



    }




    // Update is called once per frame
    void Update()
    {
        Xuanzhuan();
    }
}
