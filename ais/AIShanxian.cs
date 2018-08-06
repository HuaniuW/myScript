using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShanxian : MonoBehaviour, ISkill
{
    Transform obj;
    //目标对象
    GameObject enemyObj;
    //执行  是否需要用 静态常量的字符串来表示状态 ？？

   
    Vector3 shanxianToPos;
    Vector3 GetEnemyPos()
    {
        if (this.transform.localPosition.x > enemyObj.transform.localPosition.x) {
            shanxianToPos = new Vector3(enemyObj.transform.localPosition.x-1, this.transform.localPosition.y, this.transform.localPosition.z);
        }
        else
        {
            shanxianToPos = new Vector3(enemyObj.transform.localPosition.x + 1, this.transform.localPosition.y, this.transform.localPosition.z);
        }
        return shanxianToPos;
    }
    

    // Use this for initialization
    void Start () {
        obj = this.transform.Find("Armature");
        print("obj "+obj);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReSet()
    {
        getEnemyPos = false;
        enemyObj = null;
        isChanged = false;
    }

    bool getEnemyPos = false;
    bool isChanged = false;

    public bool IsStartAndOver(GameObject obj)
    {
        //获取对象位置
        //找到身后位置
        if (!getEnemyPos)
        {
            enemyObj = obj;
            getEnemyPos = true;
            shanxianToPos = GetEnemyPos();
        }

        //动画 什么的
        //obj 接口 播动画 是否播完 播到哪一帧的 接口 或者方法
        // 变换自身位置
        //动画
        //完成 这是新的吗
        if (obj.GetComponent<GameBody>().GetAcMsg("")==null)
        {
            //说明没有动作 直接切换位置
            return ChangePos();
        }
        else
        {
            if (obj.GetComponent<GameBody>().GetAcMsg("") == "completed")
            {
                return ChangePos();
            }
        }
       
        return false;
    }

    bool ChangePos()
    {
        if (!isChanged)
        {
            isChanged = true;
            this.transform.position = shanxianToPos;
            return true;
        }
        return false;
    }

    
}
