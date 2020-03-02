using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform FrontPoint;

    public Transform TopPoint;

    public Transform BottomPoint;


    //找点 哪个点    4个点中寻找 
    bool IsChosePoint = false;
    int choseNums = 0;
    float ChoseDistance = 0;
    public void SetChoseDistance(float nums)
    {
        ChoseDistance = nums;
    }
    bool isStart = false;
    void GetChosePointStart()
    {
        if (!isStart) return;
        if (!IsChosePoint)
        {
            IsChosePoint = true;
           
        }
    }


    List<int> typeList = new List<int> { 0,1,2,3,4};
    List<int> TempTypeList = new List<int> { };
    bool isChoseTypeOver = false;
    float pointDistanceTest = 1;

    Transform enemyTra;
    Vector2 GetChosePoint()
    {
        Vector2 v2;
        if (!isChoseTypeOver)
        {
            isChoseTypeOver = true;
            TempTypeList = typeList;
            choseNums = GlobalTools.GetRandomNum(5);

            if (choseNums == 0)
            {
                //前面 
                v2 = new Vector2(enemyTra.position.x - ChoseDistance, enemyTra.position.y);
            }
            else if (choseNums == 1)
            {
                //后面
                v2 = new Vector2(enemyTra.position.x + ChoseDistance, enemyTra.position.y);
            }
            else if (choseNums == 2)
            {
                //上面 
                v2 = new Vector2(enemyTra.position.x, enemyTra.position.y + ChoseDistance);
            }
            else if (choseNums == 3)
            {
                //斜前
                v2 = new Vector2(enemyTra.position.x - ChoseDistance * 0.7f, enemyTra.position.y + ChoseDistance * 0.7f);

            }
            else if (choseNums == 4)
            {
                //斜后
                v2 = new Vector2(enemyTra.position.x + ChoseDistance * 0.7f, enemyTra.position.y + ChoseDistance * 0.7f);
            }
        }
        

        TempTypeList.Remove(choseNums);


        //判断 点位是否 被阻挡

        //判断点周围 是否碰撞
        //连线 如果被阻断 调用自己



        isChoseTypeOver = false;
        return Vector2.zero;
    }





    //先 探测 点（点的四个方向 是否有障碍 根据 自身体型大小判断）  周围障碍

    float maxProbeDistance = 10;
    //连线  是否最短路径 有障碍  有的话 先上扫 最大距离

    // Update is called once per frame
    void Update()
    {
        
    }
}
