using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("hello!  createMap!");
        GetChildMsg("screenC");
        init();
	}
    //先把左右型搞好
    // 1.简单的单一型 主路只有一个模块的  中间多几个凸起的小模块   还可以额外安插几个其他的小模块
    //上下门 是左还是右

    string theMapMsg = "map_1-3!l,r";
    void init()
    {
        // map_1-3!l,r,u
        GetMapDate();

        // 地图 类型  第几大关的类型 根据类型 决定拼接元素
        //查找地图是否有 保存信息   （新游戏是否删除此信息？）  ******地图VO存什么 主路模块 主路景色杂物  前景  远景******
        //如果没有本地保存信息  先获取 地图基本信息  几个门  方向在哪  （地图几 知道从那边进的地图-上下左右门）  --- 地图建完后保存地图信息
        //是否生成简单1型地图
        //生成主角 主角位置  
        //生成怪物


       

    }


    string saveMapName;

    void GetMapDate()
    {
        saveMapName = this.theMapMsg.Split('!')[0];
        UserDate mapDate = GameSaveDate.GetInstance().GetSaveDateByName(saveMapName);
        if (mapDate == null)
        {
            //说明没有存档 要新生成地图
            //GenerateNewMap();

            //GetTest("jing_cao_2");
			Qidian();
        }
        else
        {
            //根据mapDate 创建地图



        }
    }

    void GetTest(string objName,float _x = 0,float _y =0)
    {
        GameObject obj = GlobalTools.GetGameObjectByName(objName);
        if(obj!=null)obj.transform.position = new Vector3(_x,_y,obj.transform.position.z);
        //print("_w "+obj.GetComponent<MeshFilter>().mesh.bounds.size.x);
        SpriteRenderer objRen = obj.GetComponent<SpriteRenderer>();
        print("w> "+ objRen.bounds.size+"  order  "+ objRen.sortingOrder);
        objRen.sortingOrder = 28;
        //objRen.bounds.size = new Vector3(60, 60, 0.2f);
        print("w2> " + objRen.bounds.size + "  order  " + objRen.sortingOrder);



        GameObject diban = GlobalTools.FindObjByName("diban1");
        print(">>>>   "+diban.GetComponent<Terrain>());

        GameObject jing = GlobalTools.FindObjByName("jing_cao_2");
        print("jing   "+jing);
        print("-------------->   "+GlobalTools.GetJingW(jing));

        
        print(GlobalTools.GetPointWorldPosByName(GlobalTools.FindObjByName("yj_shu_1"),"tl"));
    }

    void GenerateNewMap()
    {
        //几门 主路  简单1型   (基础型 1 2 3 4 5  还有随机型 )
        string[] menArr = this.theMapMsg.Split('!')[1].Split(',');
        if (menArr.Length == 2)
        {

            if ((menArr[0] == "l" && menArr[1] == "r") || (menArr[0] == "r" && menArr[1] == "l"))
            {
				//左右通道型
				//生成主干道
				//Zhugandao();
				//什么类型的门 洞内 还是洞外
				//起点
				Qidian();
            }
            else if ((menArr[0] == "u" && menArr[1] == "d") || (menArr[0] == "d" && menArr[1] == "u"))
            {
                //上下通道型
            }
        }




        if (menArr.Length == 1)
        {
            //只有一个门
        }

        //1 门
        //2门
        // r l
        // 


        //GameObject obj = GlobalTools.FindObjByName("screenC/diban_1_1/cao_1");
        //print("cao   " + obj.transform.position);
    }


	void Qidian(){
        // 根据关卡名字 是否右本地记录  右本地记录 按本地记录来  这一步好像不需要 右数据会按数据平推生成


		//知道是第几关 哪几类的 门

		//一小段2-10块组成
		//门 x_men_1
		GameObject men = GlobalTools.GetGameObjectByName("x_men_1");
		men.transform.parent = GlobalTools.FindObjByName("maps").transform;
		men.transform.position = Vector3.zero;
		//门的随机高度
		float distance = Mathf.Abs(men.transform.Find("d").transform.position.y - men.transform.Find("t").transform.position.y);
		float __y = men.transform.Find("d").transform.position.y + GlobalTools.GetRandomDistanceNums(distance);
		Vector2 menPot = men.transform.Find("men").transform.position;
		men.transform.Find("men").transform.position = new Vector2(menPot.x,__y);


        //记录门 和门的随机高度

		//门 的景色 设置
		print("主角  "+GlobalTools.FindObjByName("player").transform.position);
		if(GlobalTools.FindObjByName("player")){
			//GlobalTools.FindObjByName("player").transform.position = men.transform.Find("pp").position;
			print("pos   "+men.transform.Find("men").transform.Find("pp").transform.position);
			StartCoroutine(SetPlayerPos(men.transform.Find("men").transform.Find("pp").transform.position));

		}
		//是否 有墙 前景 近景 和背景 远景 
		Qianjing(men.transform.Find("men"));

        //起点连接点位置

        //记录到地图数据

	}
    
	void Qianjing(Transform obj){
		int nums = 1;
		if(obj.tag == "men"){
			//下层的 近景
			//哪几个景的类型
			List<string> jingNums = new List<string> { "1", "2" };
			//几个
			int jNs = GlobalTools.GetRandomNum(3);
			float _x1 = obj.Find("l").transform.position.x;
			float _x2 = obj.Find("r").transform.position.x;
			float _y = obj.Find("l").transform.position.y-0.3f;
            
			SetJingByNums(3,"jjd_1",jingNums,_x1,_x2,_y,0,0,-10);
            //上层景
			List<string> jingUNums = new List<string> { "1", "2" ,"3","4"};
			int jNUs = GlobalTools.GetRandomNum(13);
			float _xu1 = obj.Find("l2").transform.position.x;
            float _xu2 = obj.Find("r2").transform.position.x;
            float _yu = obj.Find("l2").transform.position.y + 0.3f;
			SetJingByNums(2, "jju_1", jingUNums, _xu1, _xu2, _yu,0,0,-10,true);
			SetJingByNums(1, "jju_1", jingUNums, _xu1, _xu2, _yu,0,0, 20, true);

            //下层前景
			List<string> jingUNums2 = new List<string> { "3", "4", "5","6" };
			SetJingByNums(3, "qjd_1", jingUNums2, _x1, _x2, _y-1.6f, -0.6f, 0.2f, 20);
		}
	}

	void SetJingByNums(int nums ,string jingNameTou,List<string> jingTypeNums,float _x1,float _x2,float _y,float _z,float dz,int sdfw,bool isDG = false){
		for (var i = 0; i < nums;i++){
			string jingName = jingNameTou + "_" + jingTypeNums[GlobalTools.GetRandomNum(jingTypeNums.Count)];
			GameObject jing = GlobalTools.GetGameObjectByName(jingName);
			jing.transform.parent = GlobalTools.FindObjByName("maps").transform;
			GlobalTools.SetJingTY(jing, _x1, _x2, _y,0,0, i, nums, 0, sdfw,isDG);

		}
	}

	public IEnumerator SetPlayerPos(Vector2 pos)
    {
        yield return new WaitForFixedUpdate();
		//yield return new WaitForSeconds(0.5f);
		if (GlobalTools.FindObjByName("player"))
        {
			print("????????");
			GlobalTools.FindObjByName("player").transform.position = pos;
			print("weizhi  "+GlobalTools.FindObjByName("player").transform.position);
        }
    }



    void Zhugandao()
    {
        //简单型 1 2 3 
        //GameObject zhugandao = ObjectPools.GetInstance().SwpanObject2(); //GlobalTools.GetGameObjectByName("diban_1_1");
        GameObject zhugandao = GlobalTools.GetGameObjectByName("diban_1_1");
        zhugandao.transform.position = Vector3.zero;
        //做还是不做哦 好烦躁啊
    }



    void GetChildMsg(string transformName)
    {
        GameObject screenC = GlobalTools.FindObjByName(transformName);
        if(screenC == null)
        {
            print("无此对象！");
            return;
        }
        foreach (Transform child in screenC.transform)
        {
            print(child.gameObject.name+"  -  "+child.gameObject.transform.position);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
