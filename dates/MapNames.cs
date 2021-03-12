using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNames : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static MapNames instance;
    static public MapNames GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("MapNames");
            //DontDestroyOnLoad(go);
            instance = go.AddComponent<MapNames>();
        }
        return instance;
    }


    public string GetGKKey()
    {
        return Globals.mapTypeNums.ToString();
    }

    // 通过关卡 标记 获取 元素数组名 
    public string GetJingArrNameByGKKey(string jingArrName)
    {
        string JingArrName = jingArrName + "_" + GetGKKey();
        if(GetDateByName.GetInstance().GetListByName(JingArrName, MapNames.GetInstance()) == null)
        {
            JingArrName = "";
        }
        return JingArrName;
    }

    public Color GetColorByGKKey()
    {
        Color _color = GlobalTools.RandomColor();//new Color(0.1f, 1f, 1f, 0.3f); //Color.cyan;
        return _color;
    }


    //地板
    public List<string> db_pd_1 = new List<string> { "db_pd_1" };

    public List<string> tiaoyue_1 = new List<string> { "db_ty_1"};




    //要知道 类型 ？？  怎么给 列表 加标识


    //下层近景 近景 下 jin jing  down =jjd
    public List<string> jjd_1 = new List<string> { "jjd_1_1", "jing_caoh_2" , "hh_caocong_3", "hh_hua_4", "hh_hua_5", "hh_hua_8", "hh_zhiwu_4", "hh_zhiwu_5", "hh_zhiwu_6", "hh_zhiwu_14" };
    public List<string> jjd2_1 = new List<string> { "jjd_1_6", "jjd_1_7", "jjd_1_4", "jjd_1_5" };
    public List<string> shu_1 = new List<string> { "shu_1_1", "shu_1_2", "shu_1_3", "shu_1_4", "shu_1_5", "shu_1_6", "shu_1_8" };

    //远背景
    public List<string> jybj_1 = new List<string> { "jybj_1_1", "jybj_1_2", "jybj_1_3", "jybj_1_4", "jybj_1_5", "jybj_1_6" };
    public List<string> ybj_1 = new List<string> { "ybj_1_4" };
    public List<string> ybj2_1 = new List<string> { "ybj2_1_1", "ybj2_1_2" };

    //门 下方的近景
    public List<string> men_jjd_1 = new List<string> { "jjd_1_1", "jjd_1_2" };

    //门 上方的近景 
    public List<string> men_jju_1 = new List<string> { "jju_1_1", "jju_1_2", "jju_1_3", "jju_1_4" };

    //门 下方的 前景
    public List<string> men_qjd_1 = new List<string> { "qjd_1_3", "qjd_1_4", "qjd_1_5", "qjd_1_6" };
    //前景 下方
    public List<string> qjd_1 = new List<string> { "qjd_1_3", "qjd_1_6", "qjd_1_8" };
    public List<string> qjd2_1 = new List<string> { "qjd_1_1", "qjd_1_5", "qjd_1_7", "qjd_1_10" };
    public List<string> qjd3_1 = new List<string> { "qjd_1_9", "qjd_1_10" };


    //装饰物
    public List<string> zsw_1 = new List<string> { "lj_j_zhalan-1", "lj_j_zhalan-2" , "lj_j_muzhalan-1", "lj_j_muzhalan-2", "guangHua_min1_36" };
    public List<string> qZsw_1 = new List<string> { "lj_qj_muzhalan-1", "qj_muzhalan-1", "lj_qj_muzhalan-2", "qj_muzhalan-2","qj_muzhalan-3","qj_muzhalan-4" };

    public List<string> zswDuo_1 = new List<string> { "hh_caocong_3", "hh_hua_4", "hh_hua_5", "hh_hua_8", "hh_zhiwu_4", "hh_zhiwu_5",  "hh_zhiwu_14" };

    //特殊的 修饰物 地标类型
    public List<string> zswTS_1 = new List<string> { "deng_j_ludeng_1", "j_lupai_1", "lj_j_zhalan-1", "lj_j_zhalan-2" , "lj_j_muzhalan-1", "lj_j_muzhalan-2" };

    //连续的装饰物  比如 栅栏   
    public List<string> LXZsw_1 = new List<string> { };


    public List<string> yqj_1 = new List<string> {  "qjd_1_3",  "qjd_1_6", "qjd_1_7", "qjd_1_8"};
    //public List<string> yqj_1 = new List<string> { "qjd_1_3", "qjd_1_4", "qjd_1_5", "qjd_1_6", "qjd_1_7", "qjd_1_8", "qjd_1_12" };
    public List<string> yqj2_1 = new List<string> { "qjd_1_1"};
    public List<string> yqj3_1 = new List<string> { "qjd_d3_1"};

    //前远景大
    public List<string> qyjd_1 = new List<string> { "qjd_1_9", "qjd_1_10", "qyj_1_1" };
    //前景上方 倒挂的景
    public List<string> qju2_1 = new List<string> { "ju_caodui_1", "ju_caodui_2"};
    public List<string> qju_1 = new List<string> { "jju_1_1", "jju_1_3", "jju_1_4" };


    //较大的 前景 w>13 h>4
    public List<string> qjdd_1 = new List<string> { "qyj_1_1" };

    //public static List<string> qjdd_1 = new List<string> { "1", "2", "10" };

    public List<string> qyj_1 = new List<string> { "qyj_1_1" };
    //近远景
    public List<string> jyj_1 = new List<string> { "jyj_1_1", "jyj_1_2", "jyj_1_3", "jyj_1_4", "jyj_1_5", "jyj_1_6" };
    //中远景
    public List<string> zyj_1 = new List<string> { "zyj_1_1", "zyj_1_2", "zyj_1_3", "zyj_1_4", "zyj_1_5", "zyj_1_6", "zyj_1_7" };
    //大远景
    public List<string> dyj_1 = new List<string> { "dyj_1_1", "dyj_1_2", "dyj_1_3", "dyj_1_4", "dyj_1_5" };

    //粒子落叶
    public List<string> liziLY_1 = new List<string> { "liziLY_1_1" };
    //粒子 雾
    public List<string> liziWu_1 = new List<string> { "liziWu_1_1"};

    public List<string> liziWu2_1 = new List<string> { "lizi_wu3" };






    
    public List<string> jingYingGuai_1 = new List<string> { "G_bmws" };
    public List<string> xiaoGuai_1 = new List<string> { "G_dcr2","G_ciwei" };
    public List<string> kongZhongXiaoGuai_1 = new List<string> { "G_huiciyu4" ,"G_xemL"};

    //跳跃地板
    public List<string> tiaoyuediban_1 = new List<string> { "xdiban_1_10", "xdiban_1_11", "xdiban_1_12", "xdiban_1_13", "xdiban_1_14", "xdiban_1_15", "xdiban_1_16", "xdiban_1_17", "xdiban_1_18", "xdiban_1_19" };
    //移动的地板
    public List<string> mdiban_1 = new List<string> { "mdiban_1_1", "mdiban_1_2" };


    public List<string> tiaoyueDB_1 = new List<string> { "db_ty_1" };

    public List<string> tiaoyueDBD_1 = new List<string> { "db_tyd_1", "db_tyd_2", "db_tyd_3", "db_tyd_4" };

    public List<string> tiaoyuepingdiDB_1 = new List<string> { "db_ty_1", "db_pd_1", "db_pd_2" };






    //*****精英怪 随机*****
    public List<string> JingYingGuai_1 = new List<string> { "G_jydj" };
    //一般可以随机的 精英怪
    public List<string> YiBanJingYingGuai_1 = new List<string> { "G_zjjyft" };




    public string GetCanRandomUSEJYGName(string listName)
    {
        string _listName = listName + "_" + Globals.mapTypeNums;
        //print("取怪的 列表名字  " + _listName);
        List<string> list = GetDateByName.GetInstance().GetListByName(_listName, MapNames.GetInstance());
        if (list == null) return "";
        //print("   list "+list.Count);
        return list[GlobalTools.GetRandomNum(list.Count)];
    }

























    //-------------------------------------------------水面关卡----------------------------------------------------------------------------------------------------------------------------

    //******************************地板******************************
    //连接点 地图
    public List<string> lr_2 = new List<string> { "lr_pd_2" };
    //地板
    public List<string> db_pd_2 = new List<string> { "db_pd_2", "db_pd3_2", "db_pd2_2", "db_pd4_2", "db_pd5_2" };
    //public List<string> db_pd_2 = new List<string> { "db_pd_2", "db_pd6_2"};
    public List<string> tiaoyue_2 = new List<string> { "db_ty_1" };
    //跳跃地板
    public List<string> tiaoyuediban_2 = new List<string> { "xdiban_1_10", "xdiban_1_11", "xdiban_1_12", "xdiban_1_13", "xdiban_1_14", "xdiban_1_15", "xdiban_1_16", "xdiban_1_17", "xdiban_1_18", "xdiban_1_19" };
    //移动的地板
    public List<string> mdiban_2 = new List<string> { "mdiban_1_1", "mdiban_1_2" };


    public List<string> tiaoyueDB_2 = new List<string> { "db_ty_1" };

    public List<string> tiaoyueDBD_2 = new List<string> { "db_tyd2_1", "db_tyd2_2", "db_tyd2_3", "db_tyd2_4" };

    public List<string> tiaoyuepingdiDB_2 = new List<string> { "db_ty_1", "db_pd_1", "db_pd_2" };

    //******************************景部分******************************
    //门 下方的近景
    public List<string> men_jjd_2 = new List<string> { "jjd_1_1", "jjd_1_2" };
    //门 上方的近景 
    public List<string> men_jju_2 = new List<string> { "jju_1_1", "jju_1_2", "jju_1_3", "jju_1_4" };
    //门 下方的 前景
    public List<string> men_qjd_2 = new List<string> { "qjd_1_3", "qjd_1_4", "qjd_1_5", "qjd_1_6" };
    //前景上方 倒挂的景
    public List<string> qju2_2 = new List<string> { "ju_caodui_1", "ju_caodui_2" };
    public List<string> qju_2 = new List<string> { "jju_1_1", "jju_1_3", "jju_1_4" };
    //粒子 雾
    public List<string> liziWu_2 = new List<string> { "liziWu_1_1" };
    public List<string> liziWu2_2 = new List<string> { "lizi_wu3" };

    //******************************怪物部分******************************
    public List<string> kongZhongXiaoGuai_2 = new List<string> { "G_huiciyu4", "G_xemL" };
    public List<string> xiaoGuai_2 = new List<string> { "G_dcr2", "G_ciwei" };
    public List<string> jingYingGuai_2 = new List<string> { "G_bmws" };

    //*****精英怪 随机*****
    public List<string> JingYingGuai_2 = new List<string> { "G_jydj" };
    //一般可以随机的 精英怪
    public List<string> YiBanJingYingGuai_2 = new List<string> { "G_zjjyft" };




    //******************************景******************************
    public List<string> jjd_2 = new List<string> { "sd_zhiwu2_2", "sd_zhiwu_2", "sd_zhiwu3_2" , "hh_hua_4", "hh_caocong_3", "hh_hua_5", "hh_hua_8", "hh_zhiwu_4", "hh_zhiwu_5", "hh_zhiwu_6", "j_caoh_1", "sd_zhiwu_7", "sd_zhiwu_8", "sd_zhiwu_13" , "jjd_1_6", "dongnei_min1_8", "dongnei_min1_4", "dongnei_min1_5" };
    public List<string> jjd2_2 = new List<string> { "jjd_1_6", "jjd_1_7", "jjd_1_4", "jjd_1_5", "sd_zhiwu_2" , "dongnei_min1_1", "sd_zhiwu2_2", "sd_zhiwu3_2" };


    //前景 下方
    public List<string> qjd_2 = new List<string> { "qjd_1_3", "qjd_1_6", "qjd_1_8" };
    public List<string> qjd3_2 = new List<string> { "sd_ying_2", "sd_ying_6", "qjd_1_8" };
    //public List<string> qjd2_2 = new List<string> { "qj_shui_da_1", "qj_shui_da_2", "qjd_1_7", "qjd_1_10" };
    //public List<string> qjd2_2 = new List<string> { "qj_shui_da_1", "qj_shui_da_2" };

    public List<string> qjd4_2 = new List<string> { "qj_shui_da_1", "qj_shui_da_2"};
    public List<string> qjd5_2 = new List<string> { "yqj_dongnei_min1_39", "yqj_dongnei_min1_39" };

    //近远景
    public List<string> jyj_2 = new List<string> { "sd_ying_2", "sd_ying_6"};
    //中远景
    public List<string> zyj_2 = new List<string> { "jybg_2_1", "jybg_2_2", "jybg_2_3", "jybg_2_4", "jybg_2_5"};
    //大远景
    public List<string> dyj_2 = new List<string> { "ybg_2_1"};


    //装饰物
    public List<string> zsw_2 = new List<string> { "guangHua_min1_36" };
    //水面地板
    public List<string> db_shuimian_2 = new List<string> { "db_shuimian_1", "db_shuimian_2", "db_shuimianxc_1", "db_shuimianxc_2", "db_tyd2_1", "db_tyd2_4" };









    //**************洞内跳跃地图地形*************************
    //洞内跳跃连接点地板
    public List<string> lr_dnty_2 = new List<string> { "lr_pd_2" };
    //洞内跳跃地板
    public List<string> db_dnty_2 = new List<string> { "db_dnt_1", "db_dnt_2", "db_dnt_4" };


    //跳跃地板的 中间的跳跃地板
    public List<string> db_zjdnty_2 = new List<string> { "db_tyd2_1", "db_tyd2_2", "db_tyd2_3", "db_tyd2_4" , "db_tyd2_6", "db_tyd_7", "DB_xialuo_1", "DB_xialuo_2" };


































    //*********************************************************** 老版本*******************

    //eg: lr_pd_1 平地  lr_dn_1  洞内   这里需不需要建立数组  还是直接在 系统里面 跟景色一样 根据名字生成？ 
    public List<string> lr = new List<string> {"lr_pd_1"};
    public List<string> lr_1 = new List<string> { "lr_pd_1"};



    public List<string> lru_1 = new List<string> { "lru_dn_1", "lru_dn_2" };
    public List<string> lrd_1 = new List<string> { "lrd_dn_1"};

    public List<string> lud_1 = new List<string> { "lud_dn_1" };
    public List<string> rud_1 = new List<string> { "rud_dn_1" };
    public List<string> lrud_1 = new List<string> { "lrud_dn_1" };


    public List<string> lu_1 = new List<string> { "lu_dn_1"};
    public List<string> ld_1 = new List<string> { "ld_dn_1" };

    public List<string> ru_1 = new List<string> { "ru_dn_1" };
    public List<string> rd_1 = new List<string> { "rd_dn_1" };


    public List<string> ud_1 = new List<string> { "ud_dn_1" };

    public List<string> r_1 = new List<string> { "r_pd_1" };
    public List<string> d_1 = new List<string> { "l_pd_1" };
    public List<string> l_1 = new List<string> { "l_pd_1" };
    public List<string> u_1 = new List<string> { "r_pd_1" };



    //------地板---------
    public List<string> db_rd_1 = new List<string> { "db_dn_rd_1" };
    public List<string> db_ld_1 = new List<string> { "db_dn_ld_1" };

    public List<string> db_lu_1 = new List<string> { "db_dn_lu_1" };
    public List<string> db_ru_1 = new List<string> { "db_dn_ru_1" };

   

    public List<string> db_dn_heng_1 = new List<string> { "db_dn_heng_1"};
    public List<string> db_dn_shu_1 = new List<string> { "db_dn_shu_1" };






    //生成 地板 和门 平地 和地板 都做一套


    //门1是露天普通门 不高（防止 地图串了）
    //门2 是连接洞内的 
    //门3  高  带 自动关门？+出怪模式   2种 一种露天（怪物可用从天而降） 一种洞内





   





  


    //xiaoGuai_1 = {};
    //kongZhongXiaoGuai_1 = {};
    //jingYingGuai_1 = {};
    //Boss = {};  根据当前 大关卡 来选


  
}
