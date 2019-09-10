using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Men_C : MonoBehaviour {
    public Transform men_u;
    public Transform men_d;
    public Transform men_l;
    public Transform men_r;
    public Transform men_top;
    public Transform men_down;

    public string fx = "";

    // Use this for initialization
    void Start () {
        //第三速度执行
        print("start");
        //SetPosY();
        SetRondomMen();
    }

   /* void OnEnable()
    {
        //第二速度执行
        print("OnEnble");
    }

    void Awake()
    {
        //最先执行
        print("Awake!");
    }*/

    //如果有存档的话 直接取存档数据来布置 门
    void SetMenByDate()
    {
        //取全局的 当前关卡数据  渠道门的数据  
    }

    //没有数据的情况下 执行生成门的位置
    void SetRondomMen()
    {
        //设置Y随机位置
        SetPosY();
        //设置布景
        SetJing();


        foreach (GameObject obj in mapObjList) {
            print(obj.name +"  postion:   "+obj.transform.position);
        }
    }

    int jingNums;
    List<GameObject> jingObjList = new List<GameObject> { };
    void SetJing()
    {
        jingNums = 4+(int)UnityEngine.Random.Range(0, 3);
        string weizhi = "";
        for(int i = 0; i < jingNums; i++)
        {
            GameObject jingObj;
            string objName = null;
            int rNums = (int)UnityEngine.Random.Range(0, 100);
            
            if (rNums > 80)
            {
                //上边的景
                objName = GetNameRandomByMapList(MapObjList.xsQJS_1) ;
                weizhi = "top";
            } else if (rNums >60) {
                //后
                objName = GetNameRandomByMapList(MapObjList.xsHJ_1);
                weizhi = "";
            }
            else
            {
                //下
                objName = GetNameRandomByMapList(MapObjList.xsQJ_1);
                weizhi = "";
            }
            if(objName !=null) CreateMapObjByName(objName,weizhi);

        }

    }

    string GetNameRandomByMapList(List<string> mapList)
    {
        int nums = mapList.Count;
        int ranNum = (int)UnityEngine.Random.Range(0, nums);
        return mapList[ranNum];
    }

    List<GameObject> mapObjList = new List<GameObject> { };
    void CreateMapObjByName(string mapObjName, string weizhi = "")
    {
        GameObject mapObj = GlobalTools.GetGameObjectByName(mapObjName);
        mapObj.transform.parent = this.transform.Find("jing");
        //位置设置
        float doorDistanceW = Mathf.Abs(this.men_r.transform.position.x - this.men_l.transform.position.x);
        float objHalfW = mapObj.GetComponent<SpriteRenderer>().size.y * 0.5f;
        float _px = 0;
        float _py = 0;
        print(this.name+"   "+this.transform.position);

        if(fx == "l")
        {
            _px = this.men_l.transform.position.x + UnityEngine.Random.Range(0, (doorDistanceW - objHalfW));
        }
        else if (fx == "r")
        {
            _px = this.men_l.transform.position.x + UnityEngine.Random.Range(0, (doorDistanceW - objHalfW))+objHalfW;



            while (mapObj.transform.TransformPoint(mapObj.transform.localPosition).x - men_l.TransformPoint(men_l.localPosition).x < 0)
            {
                float __x = mapObj.transform.position.x;
                __x++;
                mapObj.transform.position = new Vector3(__x, mapObj.transform.position.y, mapObj.transform.position.z);
            }
        }
        


        if (weizhi == "")
        {
            _py = this.men_down.transform.position.y+ mapObj.GetComponent<SpriteRenderer>().size.y*0.3f;
        }
        else if (weizhi == "top")
        {
            _py = this.men_top.transform.position.y- mapObj.GetComponent<SpriteRenderer>().size.y * 0.3f;
        }
        mapObj.transform.position = new Vector3(_px, _py, mapObj.transform.position.y);
        mapObjList.Add(mapObj);
    }

    //设置门 Y的位置
    void SetPosY()
    {
        if (men_u == null || men_d == null) return;
        float distanceY = men_u.position.y - men_d.position.y;
        int nums = UnityEngine.Random.Range(0, 100);
        float _posY = men_d.position.y;
        //print("---------------------------------------------------->men_xia.position.y  " + men_xia.position.y+"  menshang   "+men_shang.position.y);

        if (this.transform.Find("jing/jing_caos_1")) {
            Transform t = this.transform.Find("jing/jing_caos_1");
            float w = t.Find("downRight").transform.position.x - t.Find("topLeft").transform.position.x;
        } 

        if (nums >= 30)
        {
            _posY = men_d.position.y + UnityEngine.Random.Range(0, distanceY);
        }

        PosY(_posY);
    }

    void PosY(float _py)
    {
        this.transform.position = new Vector3(this.transform.position.x, _py, this.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
