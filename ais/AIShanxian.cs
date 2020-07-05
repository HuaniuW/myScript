using UnityEngine;
using System.Collections;
using System;

public class AIShanxian : MonoBehaviour
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
		_objTras = GlobalTools.FindObjByName("player").transform;
	}

	public float sxDistance = 15;

	// Update is called once per frame
	void Update()
	{
		
	}

	public GameObject _jueseDB;
	Transform _objTras;

	//闪现的位置X距离
	public float dX = 1.5f;
	public float dY = 0;

	[Header("地面图层")]
	public LayerMask groundLayer;

	//探索点位置 如果 前后 都没位置 直接over
	//变黑 缩小 拉长 或者 变透明
	//位置切换  
	//显示 边白还原 或者 显示透明
	//完成

	Vector2 sxPos;
	public bool isSXOver = false;
	public bool IsShanxianOver() {
		if (sxPos!=null&&sxPos == new Vector2(-1000, -1000)) return true;
		return isSXOver;
	}


	public void GetShanXian()
	{
		isSXOver = false;
		sxPos = GetSXPoint();
		GetComponent<RoleDate>().isCanBeHit = false;
		if (sxPos == new Vector2(-1000, -1000)) return;
		this.transform.GetComponent<GameBody>().SpeedXStop();
		//变黑 和缩小
		GetComponent<GameBody>().GetBoneColorChange(Color.black);
		iTween.ScaleBy(this.gameObject, iTween.Hash("x", 0.1f,"y",1.6f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Sx"));
		//可以播放声音
	}

	void Sx()
	{
		//print("-----------------------------------------sx");
		//缩小完成后 切换位置
		this.transform.position = sxPos;
		//颜色 和大小还原
		//可以播放声音
		iTween.ScaleBy(this.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.2f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Hy"));
	}

	void Hy()
	{
		print("??????????????还原啊-------------------------------");
		this.gameObject.transform.localScale = new Vector3(1,1,1);
		GetComponent<GameBody>().GetBoneColorChange(Color.white);
		GetComponent<RoleDate>().isCanBeHit = true;
		isSXOver = true;
	}

	


	Vector2 GetSXPoint()
	{
		Vector2 start;
		Vector2 end;
		//Vector2 start = top.position;
		//Vector2 end = new Vector2(start.x, start.y + hitTestDistance);
		//Debug.DrawLine(start, end, Color.yellow);
		//return Physics2D.Linecast(start, end, groundLayer);
		if(this.transform.position.x>= _objTras.position.x)
		{
			start = new Vector2(_objTras.position.x-dX, _objTras.position.y + dY);
			end = new Vector2(start.x-2, start.y);
		}
		else
		{
			start = new Vector2(_objTras.position.x + dX, _objTras.position.y + dY);
			end = new Vector2(start.x + 2, start.y);
		}

		

		if (!Physics2D.Linecast(start, end, groundLayer))
		{
			return start;
		}

		if (this.transform.position.x >= _objTras.position.x)
		{
			start = new Vector2(_objTras.position.x + dX, _objTras.position.y + dY);
			end = new Vector2(start.x + 2, start.y);
		}
		else
		{
			start = new Vector2(_objTras.position.x - dX, _objTras.position.y + dY);
			end = new Vector2(start.x - 2, start.y);
		}


		if (!Physics2D.Linecast(start, end, groundLayer))
		{
			return start;
		}

		return new Vector2(-1000,-1000);
	}

	public void ReSet()
	{
		//getEnemyPos = false;
		//这里重置敌人 会导致找不到敌人而报错 敌人是 拖进编辑器的  也就是这个闪现技能只适合敌人AI用
		//enemyObj = null;
		//isChanged = false;
		isStart = false;
		isOver = false;
	}

	public bool IsStartAndOver(GameObject obj)
	{
		return false;
	}


	/*public void ReSet()
	{
		getEnemyPos = false;
        //这里重置敌人 会导致找不到敌人而报错 敌人是 拖进编辑器的  也就是这个闪现技能只适合敌人AI用
		//enemyObj = null;
		isChanged = false;
		isStart = false;
		isOver = false;
	}
    
	bool getEnemyPos = false;
	bool isChanged = false;

    //触发闪现
	public void GetTheEnemyPos(GameObject obj){
		if(obj!=null)enemyObj = obj;
        GetComponent<RoleDate>().isCanBeHit = false;
		this.transform.GetComponent<GameBody>().GetAcMsg("stand2");
		this.transform.GetComponent<GameBody>().SpeedXStop();
		shanxianToPos = GetEnemyPos();
        GetScaleX();
		isStart = true;
	}



	void GetScaleX(){
		//print("缩小");
		iTween.ScaleBy(obj, iTween.Hash("x", 0.1f, "time", 0.1f, "easeType", iTween.EaseType.easeInOutExpo,"oncomplete","Sx"));
        //播放声音 和特效
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
        GetComponent<RoleDate>().isCanBeHit = true;
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
        //print("1  " + enemyObj);
        //print("2  " + enemyObj.transform.localPosition.x);
        //if (enemyObj == null) return;
        if (this.transform.localPosition.x > enemyObj.transform.localPosition.x)
        {
			this.transform.localScale = new Vector3(1, this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
			this.transform.localScale = new Vector3(-1, this.transform.localScale.y, this.transform.localScale.z);
        }
	}*/


}
