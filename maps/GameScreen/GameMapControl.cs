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
			CreateScreen();
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

		//上一个主 路部件  --  写入临时
		GlobalTools.tempRecordMapObj = men;

        //起点连接点位置 

        //记录到地图数据

	}

    //创建地图
	void CreateScreen(){
		//模块数量 最小地图2个模块  上下 或者 左右
		//多一个门 最小模块增加  上 下 左 右 类型 最少4个模块 
		//随机模块 2起步 最多 20个  这里只管左右
		int ModeNums = 2 + GlobalTools.GetRandomNum(18);
		//按方向 分模块  上下 左右  上下 一般5个  没有 左 或者 右  当上下 做完就结束  

		//连接的地板 起伏偏少
		//空刺 地板
		//空刺 只有中间留位置的 地板 上下游封顶
		//洞内 类型 转弯（一定要到平地 终止  上下转弯的 不计数 或者在结尾 判断 多加一段）
		//左右类型 向上 或者向下延伸
		//一个模块 1-4块主地板


		//for (var i = 0; i < ModeNums;i++){

		//}

        //之前判断类型  如果是户外 
		CreateZhuLu1();
        
	}


    //普通平路 很少会波动 上下起伏
	void CreateZhuLu1(){
		//获取 模组的地板 数量
		int LandNums = 1 + GlobalTools.GetRandomNum(4);
		List<string> landNames = new List<string> { "1", "2" };
		for (var i = 0; i < LandNums;i++){
			string _name = "xdiban_1"+"_"+(1+GlobalTools.GetRandomNum(landNames.Count));
			print("_name  "+_name);
			GameObject lu = GlobalTools.GetGameObjectByName(_name);
			print("lu  "+lu);
			lu.transform.parent = GlobalTools.FindObjByName("maps").transform;
			Vector2 lianjiedian = Vector2.zero;
            if (GlobalTools.tempRecordMapObj.tag == "men")
            {
                lianjiedian = GlobalTools.tempRecordMapObj.transform.Find("ljd").position;
            }
            else
            {
                lianjiedian = GlobalTools.GetXMapLJDian(GlobalTools.tempRecordMapObj);
            }
            
            lu.transform.position = lianjiedian;
			int sd = 12 + i % 7;
			//GlobalTools.SetMapObjOrder(lu, sd);
			print("lu-name--------   " + lu.transform.Find("diban").name);
			print("lu------------->>>>>>>>>   " + lu.transform.Find("diban").GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder);
			lu.transform.Find("diban").GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd;
            //小几率 路面不平 波动
            


			//生成地板的景 雾效果 等
			//景 树
			Jing(lu.transform,true);

			//背面近景 前景  景 草 石头 什么的
			Qianjing(lu.transform);

			// 前面的远景  蔓藤 之类的
			DaqianjingYJ(lu.transform);



            //中远景
            //大远景
            //雾  控制雾的宽度
            //各种效果


			GlobalTools.tempRecordMapObj = lu;
            //生成的地图记录到本地
            



		}
    }


	void DaqianjingYJ(Transform obj){
		int n = GlobalTools.GetRandomNum();

		List<string> jingNs = new List<string> { };
        float _x1 = 0;
        float _x2 = 0;
        float _y = 0;
        string nameFst = "";

		if(n<100){
			//前远景
			jingNs = ScreenDate.GetInstance().GetListByFstNameAndGKNums("qyj_");  //new List<string> { "1", "2" };
                                                                                      //几个
            int jNs = 1;
            _x1 = obj.Find("tl").transform.position.x;
            _x2 = obj.Find("rd").transform.position.x;
            _y = obj.Find("tl").transform.position.y - 0.3f;
            nameFst = "qyj_" + ScreenDate.GetGKNum();

			SetJingByNums(jNs, nameFst, jingNs, _x1, _x2, _y, -5, 2, 30,false,3);
		}
	}

    //景的放置
	void Jing(Transform obj,bool isGetTree){
		int nums = 1;
        //树的排放 是否放树？
		if(isGetTree){
			//树需要取到关卡字节头吗？？
			//  "shu_1"
			string nameFst = "shu"+"_"+ScreenDate.GetGKNum();
			List<string> jingNums = ScreenDate.GetInstance().GetListByFstNameAndGKNums("shu_");  //new List<string> { "1", "2","3","4","5","6","8"};
			float _x1 = obj.Find("tl").transform.position.x;
            float _x2 = obj.Find("rd").transform.position.x;
			float _y = obj.Find("tl").transform.position.y - 0.3f;
			float _rd =  UnityEngine.Random.Range(5, 9);
			float d = Mathf.Abs(_x1 - _x2)/6f;            
			nums = (int)d;     //GlobalTools.GetRandomNum(10);
			//print("  name "+obj.name+"   _x "+_x1+"  _x2 "+_x2+"  _y  "+_y);
			SetJingByNums(nums, nameFst, jingNums, _x1, _x2, _y, 0, 0, -20,false,1,true);
		}
	}




    //这里需要调节 参数  如果是第二关  并且 前景的 名字数组不一样  这里就需要改了  现在想到的办法 存一个在 专门的 数据里面 到时候直接取
	void Qianjing(Transform obj,string jingNameTou = ""){

		List<string> jingNs = new List<string> { };
		float _x1 = 0;
		float _x2 = 0;
		float _y = 0;
		string nameFst = "";

		if(obj.tag == "men"){
			//下层的 近景
			//哪几个景的类型  这里要根据 关卡来获取 数据 list
			jingNs = ScreenDate.GetInstance().GetListByFstNameAndGKNums("men_jjd_");  //new List<string> { "1", "2" };
			//几个
			int jNs = GlobalTools.GetRandomNum(3);
			_x1 = obj.Find("l").transform.position.x;
			_x2 = obj.Find("r").transform.position.x;
			_y = obj.Find("l").transform.position.y-0.3f;
			nameFst = "jjd_" + ScreenDate.GetGKNum();

			SetJingByNums(3,nameFst,jingNs,_x1,_x2,_y,0,0,-10);
            //上层景
			jingNs = ScreenDate.GetInstance().GetListByFstNameAndGKNums("men_jju_");//new List<string> { "1", "2" ,"3","4"};
			int jNUs = GlobalTools.GetRandomNum(13);
			float _xu1 = obj.Find("l2").transform.position.x;
            float _xu2 = obj.Find("r2").transform.position.x;
            float _yu = obj.Find("l2").transform.position.y + 0.3f;

			nameFst = "jju_" + ScreenDate.GetGKNum();
			SetJingByNums(2, nameFst, jingNs, _xu1, _xu2, _yu,0,0,-10,true);
			SetJingByNums(1, nameFst, jingNs, _xu1, _xu2, _yu,0,0, 20, true);

            //下层前景
			jingNs = ScreenDate.GetInstance().GetListByFstNameAndGKNums("men_qjd_"); //new List<string> { "3", "4", "5","6" };
			nameFst = "qjd_" + ScreenDate.GetGKNum();
			SetJingByNums(3, "qjd_1", jingNs, _x1, _x2, _y-1.6f, -0.6f, 0.2f, 20);
		}else{
			//近景
			jingNs = ScreenDate.GetInstance().GetListByFstNameAndGKNums("jjd_");//new List<string> { "1", "2" };
			_x1 = obj.Find("tl").transform.position.x;
			_x2 = obj.Find("rd").transform.position.x;
			_y = obj.Find("tl").transform.position.y-0.3f;
			int jNums = 2+GlobalTools.GetRandomNum(4);
			nameFst = "jjd_" + ScreenDate.GetGKNum();
			SetJingByNums(jNums, "jjd_1", jingNs, _x1, _x2, _y, 0, 0, -30);

			_y = obj.Find("tl").transform.position.y+0.25f;
			jNums = 35 + GlobalTools.GetRandomNum(10);
			jingNs =   ScreenDate.GetInstance().GetListByFstNameAndGKNums("qjd_"); //new List<string> { "3", "4", "6","7", "8","5"};
            //景 名字 头
			nameFst = "qjd_" + ScreenDate.GetGKNum();
			SetJingByNums(jNums, nameFst, jingNs, _x1, _x2, _y - 1.6f, -0.6f, 0.2f, 20);

            //大的前景 数量1到2
			_y = obj.Find("tl").transform.position.y + 0.25f;
            jNums = 1 + GlobalTools.GetRandomNum(2);
			jingNs = ScreenDate.GetInstance().GetListByFstNameAndGKNums("qjdd_");//new List<string> { "1", "2", "10"};
			SetJingByNums(jNums, nameFst, jingNs, _x1, _x2, _y - 1.6f, -0.6f, 0.2f, 20);

		}
	}



	void SetJingByNums(int nums ,string jingNameTou,List<string> jingTypeNums,float _x1,float _x2,float _y,float _z,float dz,int sdfw,bool isDG = false,float xzds=0,bool isTree = false){
		for (var i = 0; i < nums;i++){
			print("---------->  "+jingNameTou);
			string jingName = jingNameTou + "_" + jingTypeNums[GlobalTools.GetRandomNum(jingTypeNums.Count)];
			print("-----------------> 啥啊  "+jingName);
			GameObject jing = GlobalTools.GetGameObjectByName(jingName);
			jing.transform.parent = GlobalTools.FindObjByName("maps").transform;

			GlobalTools.SetJingTY(jing, _x1, _x2, _y, _z, dz, i, nums, xzds, sdfw, isDG,isTree); 
		}
	}


	public IEnumerator SetPlayerPos(Vector2 pos)
    {
        yield return new WaitForFixedUpdate();
		//yield return new WaitForSeconds(0.5f);
		if (GlobalTools.FindObjByName("player"))
        {
			GlobalTools.FindObjByName("player").transform.position = pos;
			GlobalTools.FindObjByName("MainCamera").transform.position = new Vector3(pos.x,pos.y,GlobalTools.FindObjByName("MainCamera").transform.position.z);
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
