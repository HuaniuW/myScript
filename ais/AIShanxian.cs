using UnityEngine;
using System.Collections;
using System;

public class AIShanxian : MonoBehaviour, ISkill
{
	public GameObject obj;
	//目标对象
	public GameObject enemyObj;

	public bool isStart = false;
	public bool isOver = false;
	//执行  是否需要用 静态常量的字符串来表示状态 ？？
   
	// Use this for initialization
	void Start()
	{
		//obj =(GameObject)this.transform;
		//getScaleX();
		//print(this.transform.name);
		//getTheEnemyPos(null);
	}

	public float sxDistance = 15;

	// Update is called once per frame
	void Update()
	{
		
	}

	public void ReSet()
	{
		getEnemyPos = false;
		enemyObj = null;
		isChanged = false;
		isStart = false;
		isOver = false;
	}
    
	bool getEnemyPos = false;
	bool isChanged = false;

    //触发闪现
	public void GetTheEnemyPos(GameObject obj){
		if(obj!=null)enemyObj = obj;
		this.transform.GetComponent<GameBody>().GetAcMsg("stand2");
		this.transform.GetComponent<GameBody>().SpeedXStop();
		shanxianToPos = GetEnemyPos();
        GetScaleX();
		isStart = true;
	}



	void GetScaleX(){
		//print("缩小");
		iTween.ScaleBy(obj, iTween.Hash("x", 0.1f, "time", 0.2f, "easeType", iTween.EaseType.easeInOutExpo,"oncomplete","Sx"));

    }

	void Sx(){
		ChangePos();
	}
		
    public bool IsStartAndOver(GameObject obj)
    {
        return false;
    }

    //闪现到目标地点
    bool ChangePos()
    {
        if (!isChanged)
        {
            isChanged = true;
            this.transform.position = shanxianToPos;
			SetScaleX();
			//obj.transform.localScale = Vector3.one;
			//fd();
			iTween.ScaleBy(obj, iTween.Hash("x", 1f, "time", 0.1f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fd"));
            return true;
        }
        return false;
    }
	void Fd(){
		isOver = true;
	}

    //获取闪现位置
	Vector3 shanxianToPos;
    Vector3 GetEnemyPos()
    {
        if (this.transform.localPosition.x > enemyObj.transform.localPosition.x)
        {
            shanxianToPos = new Vector3(enemyObj.transform.localPosition.x - 1, this.transform.localPosition.y, this.transform.localPosition.z);
        }
        else
        {
            shanxianToPos = new Vector3(enemyObj.transform.localPosition.x + 1, this.transform.localPosition.y, this.transform.localPosition.z);
        }

        return shanxianToPos;
    }

    //设置落点位置时候朝向 保证面朝敌人
	void SetScaleX(){
		if (this.transform.localPosition.x > enemyObj.transform.localPosition.x)
        {
			this.transform.localScale = new Vector3(1, this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
			this.transform.localScale = new Vector3(-1, this.transform.localScale.y, this.transform.localScale.z);
        }
	}

    
}
