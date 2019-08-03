using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuaiOutControl : MonoBehaviour {
    //机关的字符串数据
    public string JGStr = "wait*3:guai*G1_daocaoren2,5,3,-1|guai*G1_daocaoren2,-5,3,1:cundang:wait*2:boss*BOSS_GGZdaocaoren,5,3,-1:cundang:wait*2:boss*BOSS_yanguai,5,3,-1:end";
    //获取关卡名字 来对照匹配 如果是当前关卡 有多少组怪出来  eg:JG_screenName_1
    public int JGNum = 0;
    //是否显示波次
    public bool isShowBoci = false;

    string JGDate = "";
    string[] jiguanArr;
    string currentJG;
    // Use this for initialization
    void Start () {
       
        if (GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().JGNum != 0) JGNum = GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().JGNum;
        JGDate = "JG_" + SceneManager.GetActiveScene().name + "-" + JGNum;

        print("机关信息   "+JGDate);
        jiguanArr = JGStr.Split(':');
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, this.DieOutDo);

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_SAVEING, this.SaveOver);
    }

    void SaveOver(UEvent e) {
        isBegin = true;
    }

    void OnDistory()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_SAVEING, this.SaveOver);
    }

    void DieOutDo(UEvent e)
    {
        if (enemyObjArr.Count > 0)
        {
            for (int i = enemyObjArr.Count - 1; i >= 0; i--)
            {
                if (enemyObjArr[i] == null || enemyObjArr[i].GetComponent<RoleDate>().isDie == true)
                {
                    enemyObjArr.Remove(enemyObjArr[i]);
                }
            }
        }
        else
        {
            isFighting = false;
        }

        if(enemyObjArr.Count == 0)
        {
            enemyObjArr.Clear();
            isBegin = true;
        }
    }

    void GetJGDateBySave()
    {
        if(GlobalSetDate.instance.currentGKDate != "")
        {
            string[] arr = GlobalSetDate.instance.currentGKDate.Split(',');
            if (GlobalTools.IsHasDate(JGDate, arr))
            {
                //有数据
                JGDate = GlobalTools.GetHasDate(JGDate, arr);
                JGNum = int.Parse(JGDate.Split('-')[1]);
            }
        }
    }

    void SaveJGDate()
    {
        JGDate = "JG_" + SceneManager.GetActiveScene().name + "-" + JGNum;
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, JGDate), this);
    }

    string GetGuaiStr()
    {
        string str = "";
        //类型,名字,x,y,scaleX
        //begin:guai-daocaoren,12,2,1|guai-daocaoren,20,2,1:cundang:boss-jiansheng,12,12,1:cundang:guai-youling,12,1,1:changeScreen-screenName:end
        //guai-G1_daocaoren,5,3,-1|guai-G1_daocaoren,-5,3,1:cundang:boss-BOSS_GGZdaocaoren:end
        return str;
    }

    bool isBegin = true;
	// Update is called once per frame
	void Update () {
        if (isDaojishi)
        {
            if (jishiTime.IsPauseTimeOver())
            {
                isDaojishi = false;
                isBegin = true;
                print("倒计时结束！");
            }
        }

        if (isFighting)
        {
            //print("enemyObjArr.Count    "+ enemyObjArr.Count);
            if (enemyObjArr.Count > 0)
            {
                for (int i = enemyObjArr.Count - 1; i >= 0; i--)
                {
                    if (enemyObjArr[i] == null || enemyObjArr[i].GetComponent<RoleDate>().isDie == true)
                    {
                        enemyObjArr.Remove(enemyObjArr[i]);
                    }
                }
            }
            else
            {
                isBegin = true;
                isFighting = false;
            }
        }



        if (isBegin)
        {
            isBegin = false;
            GetOutJG();
        }
	}

    

    void GetOutJG()
    {
        if(JGNum< jiguanArr.Length)
        {
            currentJG = jiguanArr[JGNum];
            string[] JGTempArr = currentJG.Split('|');
            int n = JGTempArr.Length;

            enemyObjArr.Clear();

            for(var i = 0; i < n; i++)
            {
                string _name = JGTempArr[i].Split('*')[0];
                switch (_name)
                {
                    case "wait":
                        //倒计时
                        Daojishi(int.Parse(JGTempArr[i].Split('*')[1]));
                        break;
                    case "guai":
                        Guai(JGTempArr[i].Split('*')[1]);
                        break;
                    case "boss":
                        Guai(JGTempArr[i].Split('*')[1],true);
                        break;
                    case "cundang":
                        ShowOutSaveObj();
                        break;
                    case "changeScreen":
                        break;
                    case "end":
                        //结束
                        print("over! win!!!");
                        break;
                }
            }
            JGNum++;
            if (enemyObjArr.Count > 0) isFighting = true;
      
        }
    }

    TheTimer jishiTime;
    bool isDaojishi = false;
    void Daojishi(int n)
    {
        if (!jishiTime) jishiTime = new TheTimer();
        jishiTime.GetStopByTime(n);
        isDaojishi = true;
        print("倒计时开始"+n+"秒！");
    }

    void ShowOutSaveObj()
    {
        GameObject saveObj =  GlobalTools.GetGameObjectByName("WP_linshicundang");
        GameObject _player = GlobalTools.FindObjByName("player");
        if (_player.transform.position.x >= 0)
        {
            saveObj.transform.position = new Vector2(_player.transform.position.x - 2, 3);
        }
        else
        {
            saveObj.transform.position = new Vector2(_player.transform.position.x + 2, 3);
        }
    }

    bool isFighting = false;
    List<GameObject> enemyObjArr = new List<GameObject>();
    void Guai(string str,bool isBoss = false)
    {
        print(str);
        string[] guaiMsgArr = str.Split(',');
        print("怪物名字  "+ guaiMsgArr[0]);
        GameObject gameObject = GlobalTools.GetGameObjectByName(guaiMsgArr[0]);
        gameObject.transform.position = new Vector2(float.Parse(guaiMsgArr[1]), float.Parse(guaiMsgArr[2]));
        if(float.Parse(guaiMsgArr[3]) == 1)
        {
            gameObject.GetComponent<GameBody>().TurnLeft();
        }
        else
        {
            gameObject.GetComponent<GameBody>().TurnRight();
        }
        if (isBoss)
        {
            //显示血条
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_OUT, guaiMsgArr[0].ToString()), this);
        }
        enemyObjArr.Add(gameObject);
    }



}
