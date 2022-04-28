using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_Guaizu1 : JG_ChufaBase
{
    // Start is called before the first frame update
    //怪组 机关1
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStartOutGuai) {
            OutGuais();
            IsGuaiOver = false;
        }

        if (!IsStartOutGuai&&!IsGuaiOver)
        {
            AllDieChufa();
        }
    }

    bool IsGuaiOver = true;

    protected override void GetStart()
    {
        
    }


    bool IsStartOutGuai = false;
    int i = 0;
    bool IsGuaiHasOut = false;
    void OutGuais()
    {
        if(!IsGuaiHasOut)
        {
            IsGuaiHasOut = true;
            ShowGuai();
        }

        jishiNums += Time.deltaTime;
        print("jishiNums    "+ jishiNums);
        if(jishiNums>= jiangeTimes)
        {
            print("  出怪啊！！！！！！！ ");
            jishiNums = 0;
            i++;
            if(i> GuaiZulist.Count - 1)
            {
                IsStartOutGuai = false;
                return;
            }
            ShowGuai();
        }

    }
    [Header("怪组每次 出现 怪数量")]
    public List<int> GuaiNumsList = new List<int> {1,2,2,2};


    public Transform GuaiOutPos1;
    public Transform GuaiOutPos2;


    void ShowGuai()
    {
        List<string> GuaiList = new List<string>(GuaiZulist[i].Split(',')); //GuaiZulist[i];
        int nums = GuaiNumsList[i];

        print("GuaiList   "+ GuaiList.Count);
        print("怪 数量   " + nums+"  怪组是多少了  "+i);

        if (GuaiList.Count == 1)
        {
            if(nums == 1)
            {
                CrtGuai(GuaiList[0]);
            }
            else
            {
                for (int s = 0; s < nums; s++)
                {
                    string GuaiName = GuaiList[0];
                    CrtGuai(GuaiName);
                }
            }
        }
        else
        {
            for(int s = 0; s < nums; s++)
            {
                int nn = s% GuaiList.Count;
                print("  出怪的 num  "+nn);
                string GuaiName = GuaiList[nn];
                CrtGuai(GuaiName);
            }
        }
    }


    void CrtGuai(string guaiName)
    {
        GameObject guai = GlobalTools.GetGameObjectInObjPoolByName(guaiName);
        guai.name = guaiName;
        //guai.transform.position = GuaiOutPos1.position;
        if (GlobalTools.GetRandomNum() >= 50)
        {
            guai.transform.position = GuaiOutPos1.position;
        }
        else
        {
            guai.transform.position = GuaiOutPos2.position;
        }
        guai.GetComponent<AIBase>().isFindEnemy = true;
        GuaiObjs.Add(guai);
    }



    //出怪 间隔时间
    public float jiangeTimes = 10;
    float jishiNums = 0;

    //预计的 出怪 顺序   原始 精英大剑  1.一个重甲斧头  2.2个 火跳跳 3.2个蓝恶魔 4.一个 毒跳 一个粉恶魔
    //怪物 顺序 生成 不随机  -- 在两点位置出现
    //出完怪 出boss 机关卡  出现 boss
    //怎么出  怪打死了  出  还是按时间 间隔 来出
    [Header("怪组名字")]
    public List<string> GuaiZulist = new List<string> { };

    List<GameObject> GuaiObjs = new List<GameObject> { };

    //怪物都 die 后 触发事件
    void AllDieChufa()
    {
        if(GuaiObjs.Count == 0)
        {
            //出现 boss 怪
            print("怪打完了！！！");
            IsStartOutGuai = false;
            IsGuaiOver = true;
            if(BossJGKuai) BossJGKuai.SetActive(true);
            RemoveSelf();
            return;
        }
        for (int s = GuaiObjs.Count-1;s>=0;s--)
        {
            if (GuaiObjs[s]) {
                print(GuaiObjs[s].gameObject.name);
                if (GuaiObjs[s].GetComponent<RoleDate>().isDie)
                {
                    GuaiObjs.Remove(GuaiObjs[s]);
                }
            }
            else
            {
                GuaiObjs.Remove(GuaiObjs[s]);
            }
            
            //if (!GuaiObjs[s].gameObject.activeSelf)
            //{
            //    GuaiObjs.Remove(GuaiObjs[s]) ;
            //}
        }

        print("GuaiObjs.Count   "+ GuaiObjs.Count);
    }

  


    protected override void Chufa()
    {
        IsStartOutGuai = true;
    }




    [Header("boss 机关块")]
    public GameObject BossJGKuai;



}
