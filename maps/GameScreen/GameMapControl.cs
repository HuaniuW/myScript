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

            //由几大块组成
            int FormNums = 3 + GlobalTools.GetRandomNum(10);
            //大体的 地面路形  比如 混合的 主地板的  主洞内的  跳跃类的 等
            //各种类型 要分list 在类型中去取 组成类型
            /**
             * 1.平地森林  
             * 2.曲地森林 
             * 3.跳跃洞外森林 
             * 4.跳跃洞内  
             * 5.跳跃 有移动快的  
             * 6.洞内跳跃有移动块的 
             * 7.洞内的 上下有拦住 中间可以过的地形  
             * 8.洞内弯道
             * 9.上下路线 连接口设计
             */


            //大块类型 洞外  洞内   机关类
            //只是左右的 最少3块  门+直地/洞内 +门
            //上下最少的也是3快  门 +直下洞内/斜着向下 +门 



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
            //pp : player position
			StartCoroutine(SetPlayerPos(men.transform.Find("men").transform.Find("pp").transform.position));

		}
        //是否 有墙 前景 近景 和背景 远景 
        //Qianjing(men.transform.Find("men"));
        MenQianjingD(men.transform.Find("men"),ScreenDate.men_qjd_1,3);
        MenJinjingD(men.transform.Find("men"), ScreenDate.men_jjd_1);
        MenJinjingU(men.transform.Find("men"),ScreenDate.men_jju_1,0.3f);


		//上一个主 路部件  --  写入临时
		GlobalTools.tempRecordMapObj = men;

        GlobalTools.SaveGameObj(men);

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

        int LandNums = 2 + GlobalTools.GetRandomNum(4);
        List<string> landNames = new List<string> { "xdiban_1_1", "xdiban_1_2" };
        
        //平地
        pingdisenlinCJ(landNames, LandNums,ScreenDate.shu_1,ScreenDate.jyj_1,ScreenDate.zyj_1,ScreenDate.qjdd_1,ScreenDate.qjd_1, 3);

        LandNums = 2 + GlobalTools.GetRandomNum(4);
        //pingdisenlinCJ(landNames, LandNums, ScreenDate.shu_1, ScreenDate.jyj_1, ScreenDate.zyj_1, ScreenDate.qjdd_1, ScreenDate.qjd_1, 1);


        SetJumpScreen(ScreenDate.tiaoyuediban_1, 6, null, true, ScreenDate.mdiban_1, null,3);
    }

    
    void SetJumpScreen(List<string> dibanNameList,int landNums,List<string> dyjList,bool isMoveDB,List<string> moveDibanList,List<string> diciList,int type = 1)
    {
        Vector2 qidian = Vector2.zero;
        Vector2 zhongdian = Vector2.zero;
        Vector2 lianjiedian = Vector2.zero;
        float __x = 0;
        float __y = 0;

        float _distanceX = 0;
        float _distanceY = 0;

        bool isMFloor = false;
        float lastMFloorW = 0;

        for (var i = 0; i < landNums; i++)
        {
            string _name = "";//dibanNameList[GlobalTools.GetRandomNum(dibanNameList.Count)]; 
            if (i != landNums - 1)
            {
                if (GlobalTools.GetRandomNum() < 100)
                {
                    //普通地板
                    _name = dibanNameList[GlobalTools.GetRandomNum(dibanNameList.Count)];
                }
                else
                {
                    //使用 mfloor
                    _name = moveDibanList[GlobalTools.GetRandomNum(moveDibanList.Count)];
                    isMFloor = true;
                }
            }
            else
            {
                //最后一个floor 不能是 mfloor
                _name = dibanNameList[GlobalTools.GetRandomNum(dibanNameList.Count)];
            }
           

            GameObject lu = GlobalTools.GetGameObjectByName(_name);            
            lu.transform.parent = GlobalTools.FindObjByName("maps").transform;
            lu.transform.position = Vector2.zero;
            _distanceX = 5f+GlobalTools.GetRandomDistanceNums(10f);
            

            //连接点
            if (GlobalTools.tempRecordMapObj.tag == "men")
            {
                lianjiedian = GlobalTools.tempRecordMapObj.transform.Find("ljd").position;
            }
            else
            {
                print("==================================================>   "+ GlobalTools.tempRecordMapObj.transform.position);
                lianjiedian = GlobalTools.GetXMapLJDian(GlobalTools.tempRecordMapObj);
                print("=================================================lianjiedian=>   " + lianjiedian);
            }


            //大模块起点
            if (i == 0)
            {
                qidian = lianjiedian;
            }

            float _x = lianjiedian.x + _distanceX;
            
            if (lastMFloorW!=0)
            {
                _x -= lastMFloorW * 0.5f; 
            }

            if (isMFloor) {
                isMFloor = false;
                lastMFloorW = GlobalTools.GetHasPointMapObjW(lu);
                 _x += lastMFloorW * 0.5f;
            }

            float _y = qidian.y;

            if (type == 1)
            {
                //平地
                
            }
            else if (type == 2)
            {
                //小波动
                if (GlobalTools.GetRandomNum() > 50)
                {
                    _y = qidian.y + GlobalTools.GetRandomDistanceNums(2);
                }
                else
                {
                    _y = qidian.y - GlobalTools.GetRandomDistanceNums(2);
                }

            }
            else if (type == 3)
            {
                //上
                _y = lianjiedian.y +2+ GlobalTools.GetRandomDistanceNums(5);

            }
            else if (type == 4)
            {
                //下
                _y = lianjiedian.y - 2 - GlobalTools.GetRandomDistanceNums(5);
            }

            lu.transform.position = new Vector2(_x, _y);

            print("lu------------------------------------------------------------>  qidian  "+qidian+"   lu   "+lu.transform.position);

            //大模块终点
            Vector2 _rd = lu.transform.Find("rd").transform.position;
            zhongdian = new Vector2(_rd.x + _distanceX, _rd.y);

            GlobalTools.tempRecordMapObj = lu;
            GlobalTools.SaveGameObj(lu);
        }

        //设置 地刺

        //设置大的远背景
    }


    /**
     * 可以增加参数 
     * 1.几块组成 
     * 2.List 主路数组  
     * 3.类型 
     *      平地  
     *      起伏  
     *      上  
     *      下   
     *      间隔有刺的 
     *      间隔 并且 有移动地板连接的
     *      
     */
    //普通 有树 的平路 很少会波动 上下起伏  1平地 2上升 3下降
    void pingdisenlinCJ(List<string> dibanNameList,int landNums,List<string> shuList,List<string> jyjList,List<string> zyjList,List<string> dqjList,List<string> qjdList, int type = 1){
		//获取 模组的地板 数量
		//int LandNums = 2 + GlobalTools.GetRandomNum(4);
        //List<string> landNames = new List<string> { "1", "2" };

        int LandNums = landNums;

        //起点和终点 放置共同的大型背景
        Vector2 qidian = Vector2.zero;
        Vector2 zhongdian = Vector2.zero;
        Vector2 lianjiedian = Vector2.zero;


        for (var i = 0; i < LandNums;i++){
            string _name = dibanNameList[GlobalTools.GetRandomNum(dibanNameList.Count)];  //(1+GlobalTools.GetRandomNum(landNames.Count));
			//print("_name  "+_name);
			GameObject lu = GlobalTools.GetGameObjectByName(_name);
			//print("lu  "+lu);
			lu.transform.parent = GlobalTools.FindObjByName("maps").transform;

            //寻找连接点
            
            if (GlobalTools.tempRecordMapObj.tag == "men")
            {
                lianjiedian = GlobalTools.tempRecordMapObj.transform.Find("ljd").position;
            }
            else
            {
                lianjiedian = GlobalTools.GetXMapLJDian(GlobalTools.tempRecordMapObj);
            }

            float __x = 0;
            float __y = 0;
            //设置路的位置
            if (type == 1) {
                lu.transform.position = lianjiedian;
                GlobalTools.SetCameraKuai(lu, 7);
            }
            else if(type == 2)
            {
                //上坡型
                //不能在一起做 只能分类做  起伏的地板 远景 前景都缩小范围 放置穿帮
                if (GlobalTools.tempRecordMapObj.tag != "men")
                {
                    __x = lianjiedian.x;
                    float h = GlobalTools.GetHasPointMapObjH(GlobalTools.tempRecordMapObj);
                    float tempH = GlobalTools.GetRandomDistanceNums(h);
                    tempH = tempH >= h * 0.5f ? tempH : h * 0.5f;
                    __y = lianjiedian.y + tempH;
                    lu.transform.position = new Vector2(__x, __y);

                }
                else
                {
                    lu.transform.position = lianjiedian;
                }
                if (i != LandNums - 1) lu.transform.Find("ying").transform.position = new Vector2(lu.transform.Find("ying").transform.position.x + 0.5f, lu.transform.Find("ying").transform.position.y);
                GlobalTools.SetCameraKuai(lu, 7);
            }
            else if (type == 3)
            {
                //下坡型
                if (GlobalTools.tempRecordMapObj.tag != "men")
                {
                    __x = lianjiedian.x;
                    float h = GlobalTools.GetHasPointMapObjH(GlobalTools.tempRecordMapObj);
                    float tempH = GlobalTools.GetRandomDistanceNums(h);
                    tempH = tempH >= h * 0.5f ? tempH : h * 0.5f;
                    __y = lianjiedian.y - tempH;
                    lu.transform.position = new Vector2(__x, __y);
                    
                }
                else
                {
                    lu.transform.position = lianjiedian;
                }
                if (i != LandNums - 1) {
                    GlobalTools.SetCameraKuai(lu, 7, "down", 1);
                }
                
                lu.transform.Find("ying").transform.position = new Vector2(lu.transform.Find("ying").transform.position.x - 0.5f, lu.transform.Find("ying").transform.position.y);
            }

            //一个模块地图的 起点和终点
             if (i == 0)
            {
                qidian = lu.transform.Find("tl").transform.position;//new Vector2(lu.transform.FindChild("tl").position.x, lu.transform.FindChild("rd").position.y);
                //print("qidian  ----------------------------------------------------------------------> " + qidian + "   ?????????????????   " + lu.transform.Find("tl").transform.position + "   localposition " + lu.transform.Find("tl").transform.localPosition);
                zhongdian = lu.transform.Find("rd").transform.position;//new Vector2(lu.transform.FindChild("tl").position.x, lu.transform.FindChild("rd").position.y);
            }
            else
            {
                if (lu.transform.Find("tl").position.x < qidian.x) qidian = new Vector2(lu.transform.Find("tl").transform.position.x, qidian.y);
                if (lu.transform.Find("tl").position.y > qidian.y) qidian = new Vector2(qidian.x, lu.transform.Find("tl").transform.position.y);
                if (lu.transform.Find("rd").position.x > zhongdian.x) zhongdian = new Vector2(lu.transform.Find("rd").transform.position.x, zhongdian.y);
                if (lu.transform.Find("rd").position.y < zhongdian.y) zhongdian = new Vector2(zhongdian.x, lu.transform.Find("rd").transform.position.y);
                //print("qidian  ----------------------------------------------------------------------> " + qidian + "   ?????????????????2222222222222222222222   " + lu.transform.Find("tl").transform.position + "   localposition " + lu.transform.Find("tl").transform.localPosition);
            }

            int sd = 0;
            int qishiSD = 12;
            //设置地板深度  这里要查找上一个地板的深度  防止新板块 地图深度重叠
            if (GlobalTools.tempRecordMapObj.tag != "men")
            {
                if (qishiSD == 0 &&GlobalTools.tempRecordMapObj.GetComponent<Ferr2DT_PathTerrain>() != null)
                {
                    qishiSD = GlobalTools.tempRecordMapObj.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder + 1;
                }
            }
			sd = qishiSD + i % 7;
			//GlobalTools.SetMapObjOrder(lu, sd);
			//print("lu-name--------   " + lu.transform.Find("diban").name);
			//print("lu------------->>>>>>>>>   " + lu.transform.Find("diban").GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder);
			lu.transform.Find("diban").GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd;
            //小几率 路面不平 波动

            //储存 路 
            GlobalTools.SaveGameObj(lu);
            //生成地板的景 雾效果 等
            //景 树
            Shu(lu.transform, shuList, true);
            if (type == 1)
            {
                //背面近景 前景  景 草 石头 什么的
                //Qianjing(lu.transform,"",-3,-5);
                QianjingD(lu.transform, qjdList, 50,-0.2f,-0.6f);
                QianjingD(lu.transform, qjdList, 18, -1.8f);

                // 前面的远景  蔓藤 之类的
                DaqianjingYJ(lu.transform,dqjList);
               
                //中远景
                if(i == 0||i == LandNums - 1)
                {
                    GetJYJ(lu.transform, jyjList,0.6f);
                    GetJYJ(lu.transform, jyjList,0.4f, -35, 1.2f, 5f);
                    GetZYJ(lu.transform, zyjList,0.8f);
                }
                else
                {
                    GetJYJ(lu.transform, jyjList,2);
                    GetJYJ(lu.transform, jyjList, 3, -45, 1.2f, 5f);
                    GetZYJ(lu.transform, zyjList,3.5f);
                }
                
                //粒子落叶
                GetLiziLY(lu.transform, ScreenDate.liziLY_1, 2, -2.5f);
                GetLiziLY(lu.transform, ScreenDate.liziWu_1, 2, 0f);
            }
            else if (type == 2||type== 3)
            {
                //Qianjing(lu.transform,"",-1,0,0,true);
                QianjingD(lu.transform, qjdList, 50, -0.2f,-0.6f);
                QianjingD(lu.transform, qjdList, 16,-0.8f);
                GetJYJ(lu.transform, jyjList,0.4f);
                GetJYJ(lu.transform, jyjList, 1.5f, -45, 1.2f, 5f);
                GetLiziLY(lu.transform, ScreenDate.liziLY_1, 2, -2.5f);
                GetLiziLY(lu.transform, ScreenDate.liziWu_1, 2, 0f);
            }
            GlobalTools.tempRecordMapObj = lu; //这里做为记录 和连接点 来使用
                                               //生成的地图记录到本地
        }

        //记录长度  x1 x2 y 
        //全面的 远景  雾 背景树 远景树 

        //雾  控制雾的宽度
        //雾
        Color color1 = new Color(0.1f, 1f, 1f, 0.1f);
        GetWu("", qidian, zhongdian,-30, color1);
        Color color2 = new Color(1f, 1f, 1f, 0.1f);
        GetWu("", qidian, zhongdian, -60, color2);
    }

    void GetLiziLY(Transform obj,List<string> liziList, int _nums= 2 , float xzY = -2)
    {
        //产生个数
        int nums = _nums;
        float _x1 = obj.Find("tl").transform.position.x;
        float _x2 = obj.Find("rd").transform.position.x;
        float _y = obj.Find("tl").transform.position.y - xzY;
        float _rd = UnityEngine.Random.Range(5, 9);
        //通过宽 来计算 多少树
        //float d = Mathf.Abs(_x1 - _x2) / midu;
        //nums = (int)d;     //GlobalTools.GetRandomNum(10);
        //print("============================================================================================  name " + obj.name + "   _x " + _x1 + "  _x2 " + _x2 + "  _y  " + _y);
        SetLiziByNums(nums, liziList, _x1, _x2, _y);
    }


    void SetLiziByNums(int nums, List<string> liziList, float _x1, float _x2, float _y)
    {
        for (var i = 0; i < nums; i++)
        {
            //print("---------->  "+jingNameTou);
            string jingName = liziList[GlobalTools.GetRandomNum(liziList.Count)];
            //print("-----------------> 啥啊  "+jingName);
            GameObject jing = GlobalTools.GetGameObjectByName(jingName);
            jing.transform.parent = GlobalTools.FindObjByName("maps").transform;
            //public static void SetLizi(GameObject jingObj, float _x1, float _x2, float _y, int i, int nums)
            GlobalTools.SetLizi(jing, _x1, _x2, _y,  i, nums);
        }
    }

    //近远景 路面对象  Z的位置 深度   修正Y  密度（间隔距离 越小越多）
    void GetJYJ(Transform obj,List<string> jyjList,float _z = 1,int SDOrder =-29,float xzY = 1.2f,float midu = 2) {
        //产生个数
        int nums = 7;
        //string nameFst = "jyj" + "_" + ScreenDate.GetGKNum();
        //List<string> jingNums = ScreenDate.GetInstance().GetListByFstNameAndGKNums("jyj_");  //new List<string> { "1", "2","3","4","5","6","8"};
        float _x1 = obj.Find("tl").transform.position.x;
        float _x2 = obj.Find("rd").transform.position.x;
        float _y = obj.Find("tl").transform.position.y - xzY;
        float _rd = UnityEngine.Random.Range(5, 9);
        //通过宽 来计算 多少树
        float d = Mathf.Abs(_x1 - _x2) / midu;
        nums = (int)d;     //GlobalTools.GetRandomNum(10);
        //print("============================================================================================  name " + obj.name + "   _x " + _x1 + "  _x2 " + _x2 + "  _y  " + _y);
        SetJingByNums(nums, jyjList, _x1, _x2, _y, _z, 0.9f, SDOrder, false, 0.5f, false);
    }

    void GetZYJ(Transform obj,List<string> zyjList,float __z = 6)
    {
        //产生个数
        int nums = 7;
        string nameFst = "zyj" + "_" + ScreenDate.GetGKNum();
        List<string> jingNums = ScreenDate.GetInstance().GetListByFstNameAndGKNums("zyj_");  //new List<string> { "1", "2","3","4","5","6","8"};
        float _x1 = obj.Find("tl").transform.position.x;
        float _x2 = obj.Find("rd").transform.position.x;
        float _y = obj.Find("tl").transform.position.y - 4.2f;
        float _rd = UnityEngine.Random.Range(5, 9);
        //通过宽 来计算 多少树
        float d = Mathf.Abs(_x1 - _x2) / 2f;
        nums = (int)d;     //GlobalTools.GetRandomNum(10);
        //print("============================================================================================  name " + obj.name + "   _x " + _x1 + "  _x2 " + _x2 + "  _y  " + _y);
        SetJingByNums(nums, zyjList, _x1, _x2, _y, __z, 1f, -39, false, 0.5f, false);
    }


    void GetWu(string wuName,Vector2 qidian,Vector2 zhongdian,int SDOrder,Color color)
    {
        string _wuName = "wu_1_1";
        GameObject _wu = GlobalTools.GetGameObjectByName(_wuName);
        //补的雾 看后面的需求
        //GameObject _wu2 = GlobalTools.GetGameObjectByName(_wuName);
        _wu.transform.parent = GlobalTools.FindObjByName("maps").transform; 
        float _w = GlobalTools.GetJingW(_wu);
        float _h = GlobalTools.GetJingH(_wu);
        float _w2 = Mathf.Abs(zhongdian.x - qidian.x);
        float _h2 = Mathf.Abs(zhongdian.y - qidian.y)+5;

       
        _wu.transform.localScale = new Vector3(_w2/_w+0.6f,_h2/_h*3,1);
        _wu.transform.position = new Vector2(qidian.x +_w2*0.5f + (_w2 - GlobalTools.GetJingW(_wu))*0.5f, zhongdian.y + GlobalTools.GetJingH(_wu)*0.5f+2);

        //print(">?????????????????????????????????????????????????????????????    "+ _w+"  >-缩放后  " +GlobalTools.GetJingW(_wu));

        GlobalTools.SetMapObjOrder(_wu, SDOrder);

        //print("雾的宽度  "+_w+"  _w2宽度  "+_w2+"   宽度缩放比例    "+_w2/_w+"   weizhi "+_wu.transform.position);
        //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  "+_wu.transform.position+"   起点  "+qidian+"   终点 "+zhongdian);


        //改变雾的颜色
        _wu.GetComponent<SpriteRenderer>().color = color;// new Color(0.1f,1f,1f,0.5f);//new Color((129 / 255)f, (69 / 255)f, (69 / 255)f, (255 / 255)f); //Color.red;
    }


    void DaqianjingYJ(Transform obj, List<string> dqjList){
		int n = GlobalTools.GetRandomNum();

        float _x1 = 0;
        float _x2 = 0;
        float _y = 0;

		if(n<100){
            int jNs = 1;
            _x1 = obj.Find("tl").transform.position.x;
            _x2 = obj.Find("rd").transform.position.x;
            _y = obj.Find("tl").transform.position.y - 0.3f;

			SetJingByNums(jNs, dqjList, _x1, _x2, _y, -5, 2, 30,false,3);
		}
	}

    //景的放置
	void Shu(Transform obj,List<string> shuList,bool isGetTree){
		int nums = 0;
        //树的排放 是否放树？
		if(isGetTree){
			//树需要取到关卡字节头吗？？
			//  "shu_1"
			//string nameFst = "shu"+"_"+ScreenDate.GetGKNum();
            List<string> jingNums = shuList;////ScreenDate.GetInstance().GetListByFstNameAndGKNums("shu_");  //new List<string> { "1", "2","3","4","5","6","8"};
			float _x1 = obj.Find("tl").transform.position.x;
            float _x2 = obj.Find("rd").transform.position.x;
			float _y = obj.Find("tl").transform.position.y - 0.3f;
			float _rd =  UnityEngine.Random.Range(5, 9);
			float d = Mathf.Abs(_x1 - _x2)/6f;            
			nums = (int)d;     //GlobalTools.GetRandomNum(10);
			//print("============================================================================================  name "+obj.name+"   _x "+_x1+"  _x2 "+_x2+"  _y  "+_y);
			SetJingByNums(nums, shuList, _x1, _x2, _y, 0, 0, -20,false,1,true);
		}
	}


    //近景 下方
    void JinjingD(Transform obj, List<string> jjList,int jNums,bool isLBsuoduan = false) {
        float _x1 = 0;
        float _x2 = 0;
        float _y = 0;
        _x1 = obj.Find("tl").transform.position.x;
        _x2 = obj.Find("rd").transform.position.x;
        _y = obj.Find("tl").transform.position.y - 0.3f;
        SetJingByNums(jNums, jjList, _x1, _x2, _y, 0, 0, -30, false, 0, false, isLBsuoduan);
    }

    //前景 下
    void QianjingD(Transform obj, List<string> qjdList, int jNums, float qjdJL = 0, float qjdY = 0.25f, bool isLBsuoduan = false)
    {
        float _x1 = 0;
        float _x2 = 0;
        float _y = 0;
        _x1 = obj.Find("tl").transform.position.x;
        _x2 = obj.Find("rd").transform.position.x;
        _y = obj.Find("tl").transform.position.y + qjdY;
        //jNums = 35 + GlobalTools.GetRandomNum(10);
        SetJingByNums(jNums, qjdList, _x1, _x2, _y - 1.6f, qjdJL, 0.2f, 20, false, 0, false, isLBsuoduan);
    }

    //大前景 下
    void DaQianjingD(Transform obj, List<string> dqjdList, int jNums, float dqjdJL = 0, float qjdY = 0.25f, bool isLBsuoduan = false)
    {
        float _x1 = 0;
        float _x2 = 0;
        float _y = 0;
        _x1 = obj.Find("tl").transform.position.x;
        _x2 = obj.Find("rd").transform.position.x;
        //jNums = 35 + GlobalTools.GetRandomNum(10);
        _y = obj.Find("tl").transform.position.y + qjdY;
        SetJingByNums(jNums, dqjdList, _x1, _x2, _y - 1.6f, dqjdJL, 0.2f, 20, false, 0, false, isLBsuoduan);
    }

    void MenQianjingD(Transform obj, List<string> mqjList,int jNums, float qjdJL = -0.6f, float qjdY = -0.3f, bool isLBsuoduan = false) {
        float _x1 = 0;
        float _x2 = 0;
        float _y = 0;
        _x1 = obj.Find("l").transform.position.x;
        _x2 = obj.Find("r").transform.position.x;
        _y = obj.Find("l").transform.position.y + qjdY;
        SetJingByNums(jNums, mqjList, _x1, _x2, _y - 1.6f, qjdJL, 0.2f, 20);
    }



    //门近景 上
    void MenJinjingU(Transform obj, List<string> jjListUp, float qjdY = 0.3f, bool isLBsuoduan = false) {
        float _xu1 = obj.Find("l2").transform.position.x;
        float _xu2 = obj.Find("r2").transform.position.x;
        float _yu = obj.Find("l2").transform.position.y + qjdY;

        SetJingByNums(2, jjListUp, _xu1, _xu2, _yu, 0, 0, -10, true);
        SetJingByNums(1, jjListUp, _xu1, _xu2, _yu, 0, 0, 20, true);
    }

    //门 近景下
    void MenJinjingD(Transform obj, List<string> jjListDown, float qjdY = -0.3f, bool isLBsuoduan = false) {
        float _x1 = 0;
        float _x2 = 0;
        float _y = 0;
        _x1 = obj.Find("l").transform.position.x;
        _x2 = obj.Find("r").transform.position.x;
        _y = obj.Find("l").transform.position.y + qjdY;
        SetJingByNums(3, jjListDown, _x1, _x2, _y, 0, 0, -10);
    }


    /**
    //这里需要调节 参数  如果是第二关  并且 前景的 名字数组不一样  这里就需要改了  现在想到的办法 存一个在 专门的 数据里面 到时候直接取
    void Qianjing(Transform obj,List<string> qjList ,string jingNameTou = "",float qjdJL = 0,float dqjdJL = 0,float qjdY = 0.25f, bool isLBsuoduan = false)
    {

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
			SetJingByNums(jNums, jjdList, _x1, _x2, _y, 0, 0, -30, false, 0, false, isLBsuoduan);

			_y = obj.Find("tl").transform.position.y+ qjdY;
			jNums = 35 + GlobalTools.GetRandomNum(10);
			jingNs =   ScreenDate.GetInstance().GetListByFstNameAndGKNums("qjd_"); //new List<string> { "3", "4", "6","7", "8","5"};
            //景 名字 头
			nameFst = "qjd_" + ScreenDate.GetGKNum();
			SetJingByNums(jNums, qjdList, _x1, _x2, _y - 1.6f, qjdJL, 0.2f, 20, false, 0, false, isLBsuoduan);

            //大前景距离
            if(dqjdJL != 0)
            {
                //大的前景 数量1到2
                _y = obj.Find("tl").transform.position.y + 0.25f;
                jNums = 1 + GlobalTools.GetRandomNum(2);
                jingNs = ScreenDate.GetInstance().GetListByFstNameAndGKNums("qjdd_");//new List<string> { "1", "2", "10"};
                SetJingByNums(jNums, qjddList, _x1, _x2, _y - 1.6f, dqjdJL, 0.2f, 20,false,0,false, isLBsuoduan);
            }
          

		}
	}
    */

    //isLBsuoduan 是否两边缩短
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums">生成景的数量</param>
    /// <param name="jingNameList">景名字 列表</param>
    /// <param name="_x1">最左边 x坐标</param>
    /// <param name="_x2">最右边 x坐标</param>
    /// <param name="_y">景 y坐标</param>
    /// <param name="_z">景 z坐标</param>
    /// <param name="dz">景 z范围调整</param>
    /// <param name="sdfw">order 深度范围  </param>
    /// <param name="isDG"> 是否 是倒挂 </param>
    /// <param name="xzds">旋转度数 </param>
    /// <param name="isTree">是否是 树</param>
    /// <param name="isLBsuoduan"> 是否 两边 缩短 （放置两边穿帮）</param>
    void SetJingByNums(int nums ,List<string> jingNameList,float _x1,float _x2,float _y,float _z,float dz,int sdfw,bool isDG = false,float xzds=0,bool isTree = false, bool isLBsuoduan = false)
    {
		for (var i = 0; i < nums;i++){
			//print("---------->  "+jingNameTou);
			string jingName = jingNameList[GlobalTools.GetRandomNum(jingNameList.Count)];
			//print("-----------------> 啥啊  "+jingName);
			GameObject jing = GlobalTools.GetGameObjectByName(jingName);
			jing.transform.parent = GlobalTools.FindObjByName("maps").transform;

            
            if (isLBsuoduan && (i == 0 || i == nums - 1))
            {
                GlobalTools.SetJingTY(jing, _x1, _x2, _y, _z * 0.1f, dz*0.1f, i, nums, xzds, sdfw, isDG, isTree,isLBsuoduan);
            }
            else {
                GlobalTools.SetJingTY(jing, _x1, _x2, _y, _z, dz, i, nums, xzds, sdfw, isDG, isTree);
            }
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
			//print("weizhi  "+GlobalTools.FindObjByName("player").transform.position);
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
