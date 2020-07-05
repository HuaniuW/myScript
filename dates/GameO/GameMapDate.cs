using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapDate
{
    //大地图数据
    //map_r+map_r-1!0#0!r:map_r-2^u:map_r-3  |    map_r-2!1#0!r:map_r-4@map_u+map_u-1     !     0#0!r:map_u-2|map_u-2!1#0!map_u-3
    public string BigMapDate = "";
    //小地图数据
    public string MapDate = "";

    //关卡类型 预设
    public List<string> TypeList = new List<string>() { };


    


    //左右类型 背景  树 山 巨树  石头  天柱

    // s +  fzjl:10-90-0-0,bj:shu_1,bjyj:shuY_1,qj:qianjing_1,yqj:yqj_1,shipinh:shipinjing_1,guaizu:guaizu_1,jingyingguaizu:jingyingguaizu_1

    //分支几率  怪组类型


    public static string GetGuaiTypeByCenterZimu()
    {
        //第一关 古甲兵 +灰刺鱼   精英-重甲斧头 长剑士
        //2.古甲兵 重甲稻草人 +灰刺鱼 +小恶魔 + 蓝恶魔   精英- 精英法师

        return "";
    }


    //根据生成 地图名 中间的 字母 来 选 范围   map_s-1   就取 s 来区分
    public static string GetMapMsgByMapCenterZimu()
    {
        return "";
    }



    public static string GetLianjiedianTypeByCName(string cName)
    {
        cName = cName.Split('-')[0].Split('_')[1];
        string str = "1";
        if(cName == "s")
        {
            str = "1";
        }else if(cName == "s1")
        {
            str = "2";
        }
        return str;
    }


    //获取 平地 还是非平地的 几率
    public static string GetMapDiXingByCName(string cName)
    {
        string str = "";
        cName = cName.Split('-')[0].Split('_')[1];
        
        if (cName == "s")
        {
            //平地 跳跃 混合  洞内
            str = "pd_90|ty_10|hh_5|dn_0";
        }

        return str;
    }

    // typeName -> pd平地地形  ty跳跃地形      mapMsg "pd_90|ty_5|hh_5|dn_0"
    public static float GetJiLvByMapMsgStr(string mapMsg,string typeName)
    {
        float jilv = 0;
        string[] strArr = mapMsg.Split('|');
        for(var i = 0; i < strArr.Length; i++)
        {
            if(strArr[i].Split('_')[0] == typeName)
            {
                return float.Parse(strArr[i].Split('_')[1]);
            }
        }
        return jilv;
    }



    //左右平地类型  云中 跳跃地居多     森林  平地居多 洞内 和跳跃少
    //机关地形


    public GameMapDate()
    {

    }

    

}
