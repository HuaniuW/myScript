using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReMap : MonoBehaviour
{
    //地形生成
    // Start is called before the first frame update
    void Start()
    {
        GetInDate();
    }


    //对比 当前地图内部的 地图 名字 是否就是需要生成的名字  用在 从非随机地图 进入 随机地图    这样可以不用生成   从随机地图 进入 非随机地图的时候 记录一下 是哪个Rmap_1 还是2 出来的

    //记录 关卡名字   获取地图的门信息

    //取数据 看看有没有关卡 的数据 有的话 直接 取数据生成地图  没有的话 生成地图

    //几个门 怎么连接  生成的 地图地形连接数

    //生成地图 判断 生成类型
    //户外 还是 洞内 还是混合
    //地板 是 石头地板 还是草皮地板  还是 茂盛的植被地板 等
    //景  前景 是什么  后景是什么
    //粒子效果
    //

    //当前地图 信息  eg->   map_1-1!0#0!r:map_1-2  
    string theMapMsg = "";
    public void GetInDate()
    {

        //判断 是否 有这个地图的数据 有的话 直接按数据生成地图




        print("本地图名字 "+GlobalSetDate.instance.CReMapName);
        //获取门信息
        menFXList = GetMenFXListByMapName(GlobalSetDate.instance.CReMapName);
        print("menFXList   "+ menFXList.Count);
        foreach (string m in menFXList) print(m);
        //这里要知道 从哪进来的 进入方向   保留一个门
        //-

        print("从哪个方向的门进来的  "+ GlobalSetDate.instance.DanqianMenweizhi);



        //判断是 哪种景色  洞外 洞内  相应的 景 图片是哪些

        //地图生成 的 顺序  从左往右 先上 后下？   如果有分支 先左右走完 再走分支
        //判断 地图的门数量 2 3 4 各自怎么开始



        //随机出每个 分支 的地图数量
        string type = GetStrByList(menFXList);


        //根据类型 生成 连接点

        //lr  平地 洞内   左右的 地形是最多的  有门的 打怪才能过的  有直线通道的
        //普通的直线  连接线 左右连接  向左  向右  向上  向下 4个方向 生成地图
        // 左右的 或者 2个门的 还有只要 碰到就下落的地板   上下移动   左右移动的地板
        // 2个门都适用   1.户外 2.洞内 3.户外洞内混合

        

        //lru
        //lrd

        //lud
        //rud

        //ru
        //rd
        //lu
        //ld
        //ud
        //lrud





    }




    string GetTypeByMenFXList(List<string> menFXList)
    {
        string str = GetStrByList(menFXList);
        //取中心连接点 地图 以及 随机出 每个 朝向的 模块数量    模块的最少数是多少？ 左右是1  上下是2（多一个直连+转弯） 然后连接门  门不算进数量 数量走完连接门
        //数元素 自带 落叶粒子
        // lr=l:mapR_1?3?pd?shu!r:map_r-2?3?dn?qiang



        return "";
    }


    string GetStrByList(List<string> menFXList)
    {
        string str = "";

        string arrNums = "1-l:2-r:3-u:4-d";

        string[] arrs = arrNums.Split(':');

        List<string> tempList = new List<string> { };

        for (int i = 0;i< menFXList.Count; i++)
        {
            string s = menFXList[i].Split(':')[0];
            for(var j=0;j< arrs.Length; j++)
            {
                if (s == arrs[j].Split('-')[1])
                {
                    tempList.Add(arrs[j].Split('-')[0]+"*"+ menFXList[i]);
                }
            }
        }


        tempList.Sort();

        for (int n =0;n< tempList.Count;n++)
        {
            print(n+" - "+ tempList[n]);
        }



        return str;
    }



    //生成门 按顺序找门 左 右 上 下  门后面连接的数量
    //数据类型  l:map_1-2$4^r:map_r-2$3




    //门方向列表 {"l","r"}
    List<string> menFXList = new List<string> { };
    List<string> GetMenFXListByMapName(string mapName)
    {
        string[] mapArr1 = GlobalSetDate.instance.gameMapDate.BigMapDate.Split('@');
        string mapMsg = "";
        foreach(string s in mapArr1)
        {
            if (s.Split('+')[0] == GlobalSetDate.instance.CMapTou)
            {
                mapMsg = s.Split('+')[1];
                break;
            }
        }

        string[] CMapMsgArr = mapMsg.Split('|');

        foreach(string m in CMapMsgArr)
        {
            if(m.Split('!')[0] == GlobalSetDate.instance.CReMapName)
            {
                theMapMsg = m;
                break;
            }
        }

        string[] TheMapMsgArr = theMapMsg.Split('!')[2].Split('^');
        for(var i=0;i< TheMapMsgArr.Length; i++)
        {
            menFXList.Add(TheMapMsgArr[i]);
        }
        return menFXList;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
