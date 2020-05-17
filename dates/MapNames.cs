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



    //eg: lr_pd_1 平地  lr_dn_1  洞内   这里需不需要建立数组  还是直接在 系统里面 跟景色一样 根据名字生成？ 
    public List<string> lr = new List<string> {"lr_pd_1","lr_dn_1"};

    public List<string> lru = new List<string> { "lru_dn_1", "lru_dn_2" };
    public List<string> lrd = new List<string> { "lrd_dn_1"};

    public List<string> lud = new List<string> { "lud_dn_1" };
    public List<string> rud = new List<string> { "rud_dn_1" };
    public List<string> lrud = new List<string> { "lrud_dn_1" };


    public List<string> lu = new List<string> { "lu_dn_1"};
    public List<string> ld = new List<string> { "ld_dn_1" };

    public List<string> ru = new List<string> { "ru_dn_1" };
    public List<string> rd = new List<string> { "rd_dn_1" };

    public List<string> r = new List<string> { "r_pd_1" };
    public List<string> d = new List<string> { "l_pd_1" };
    public List<string> l = new List<string> { "l_pd_1" };
    public List<string> u = new List<string> { "r_pd_1" };



    //------地板---------
    public List<string> db_rd = new List<string> { "db_dn_rd_1" };
    public List<string> db_ld = new List<string> { "db_dn_ld_1" };

    public List<string> db_lu = new List<string> { "db_dn_lu_1" };
    public List<string> db_ru = new List<string> { "db_dn_ru_1" };

    public List<string> db_pd = new List<string> {"db_pd_1", "db_pd_2"};

    public List<string> db_dn_heng = new List<string> { "db_dn_heng_1"};
    public List<string> db_dn_shu = new List<string> { "db_dn_shu_1" };






    //生成 地板 和门 平地 和地板 都做一套


    //门1是露天普通门 不高（防止 地图串了）
    //门2 是连接洞内的 
    //门3  高  带 自动关门？+出怪模式   2种 一种露天（怪物可用从天而降） 一种洞内 

}
