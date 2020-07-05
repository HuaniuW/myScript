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

    //要知道 类型 ？？  怎么给 列表 加标识

    //eg: lr_pd_1 平地  lr_dn_1  洞内   这里需不需要建立数组  还是直接在 系统里面 跟景色一样 根据名字生成？ 
    public List<string> lr = new List<string> {"lr_pd_1","lr_dn_1"};
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

    public List<string> db_pd_1 = new List<string> {"db_pd_1", "db_pd_2"};

    public List<string> db_dn_heng_1 = new List<string> { "db_dn_heng_1"};
    public List<string> db_dn_shu_1 = new List<string> { "db_dn_shu_1" };






    //生成 地板 和门 平地 和地板 都做一套


    //门1是露天普通门 不高（防止 地图串了）
    //门2 是连接洞内的 
    //门3  高  带 自动关门？+出怪模式   2种 一种露天（怪物可用从天而降） 一种洞内





    //下层近景 近景 下 jin jing  down =jjd
    public List<string> jjd_1 = new List<string> { "jjd_1_1", "jjd_1_2" };
    public List<string> shu_1 = new List<string> { "shu_1_1", "shu_1_2", "shu_1_3", "shu_1_4", "shu_1_5", "shu_1_6", "shu_1_8" };

    //门 下方的近景
    public List<string> men_jjd_1 = new List<string> { "jjd_1_1", "jjd_1_2" };

    //门 上方的近景 
    public List<string> men_jju_1 = new List<string> { "jju_1_1", "jju_1_2", "jju_1_3", "jju_1_4" };

    //门 下方的 前景
    public List<string> men_qjd_1 = new List<string> { "qjd_1_3", "qjd_1_4", "qjd_1_5", "qjd_1_6" };
    //前景 下方
    public List<string> qjd_1 = new List<string> { "qjd_1_3", "qjd_1_4", "qjd_1_6", "qjd_1_8"  };


    public List<string> zsw_1 = new List<string> { "j_ludeng_1", "j_lupai_1", "j_zhalan_1", "j_zhalan_2" };
    


    public List<string> qjd2_1 = new List<string> { "qjd_1_1", "qjd_1_5", "qjd_1_7" , "qjd_1_10" };
    public List<string> qjd3_1 = new List<string> { "qjd_1_9", "qjd_1_10"};

    public List<string> qyjd_1 = new List<string> { "qjd_1_9", "qjd_1_10", "qyj_1_1" };

    //前景上方 倒挂的景
    public List<string> qju_1 = new List<string> { "jju_1_1",  "jju_1_3", "jju_1_4" };


    //较大的 前景 w>13 h>4
    public List<string> qjdd_1 = new List<string> { "qyj_1_1" };

    //public static List<string> qjdd_1 = new List<string> { "1", "2", "10" };

    public List<string> qyj_1 = new List<string> { "qyj_1_1" };
    //近远景
    public List<string> jyj_1 = new List<string> { "jyj_1_1", "jyj_1_2", "jyj_1_3", "jyj_1_4", "jyj_1_5", "jyj_1_6" };
    //中远景
    public List<string> zyj_1 = new List<string> { "zyj_1_1", "zyj_1_2", "zyj_1_3", "zyj_1_4", "zyj_1_5", "zyj_1_6", "zyj_1_7" };
    //大远景
    public List<string> dyj_1 = new List<string> {"dyj_1_1", "dyj_1_2", "dyj_1_3", "dyj_1_4", "dyj_1_5" };

    //粒子落叶
    public List<string> liziLY_1 = new List<string> { "liziLY_1_1" };
    //粒子 雾
    public List<string> liziWu_1 = new List<string> { "liziWu_1_1" };





    //跳跃地板
    public List<string> tiaoyuediban_1 = new List<string> { "xdiban_1_10", "xdiban_1_11", "xdiban_1_12", "xdiban_1_13", "xdiban_1_14", "xdiban_1_15", "xdiban_1_16", "xdiban_1_17", "xdiban_1_18", "xdiban_1_19" };
    //移动的地板
    public List<string> mdiban_1 = new List<string> { "mdiban_1_1", "mdiban_1_2" };


    public List<string> tiaoyueDB_1 = new List<string> { "db_ty_1"};

    public List<string> tiaoyueDBD_1 = new List<string> { "db_tyd_1", "db_tyd_2", "db_tyd_3", "db_tyd_4" };

    public List<string> tiaoyuepingdiDB_1 = new List<string> { "db_ty_1", "db_pd_1","db_pd_2"};


    //xiaoGuai_1 = {};
    //kongZhongXiaoGuai_1 = {};
    //jingYingGuai_1 = {};
    //Boss = {};  根据当前 大关卡 来选


    public List<string> xiaoGuai_2 = new List<string> {"G_dcrj1"};
    public List<string> xiaoGuai_1 = new List<string> { "G_dcr2" };
    public List<string> kongZhongXiaoGuai_1 = new List<string> { "G_huiciyu4" };
    public List<string> jingYingGuai_1 = new List<string> { "G_bmws" };
}
