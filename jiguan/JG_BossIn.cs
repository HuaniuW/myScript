using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_BossIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        IsBeingBoss();
    }
	
	// Update is called once per frame
	void Update () {
        YanChiStart();
    }
    //关联boss
    public string BossName;
    [Header("关联门1的名字  发送状态0")]
    public string Door1Name;
    [Header("关联门2的名字  发送状态0")]
    public string Door2Name;
    [Header("遇见boss音效")]
    public AudioSource SeeBossAudio;
    [Header("遇见boss背景音乐")]
    public AudioSource SeeBossBGAudio;
    [Header("改变摄像机边界")]
    public string ChangeKuaiName = "kuang1";


    [Header("特殊 遇见boss龙  吼声")]
    public AudioSource LongHou;


    [Header("摄像机是否跟随玩家和boss距离改变Z")]
    public bool IsChangeZByBossAndPlayerDistance = false;
    //防止重复碰撞播放
    bool IsPlayerIn = false;

    [Header("是否是在BOSS中使用  如果不是 就是没有boss 防止自己删除自己")]
    public bool IsInBoss = true;
    //关联boss是否存在 不存在就销毁自身



    [Header("boss 机关门1")]
    public GameObject Door1;
    [Header("boss 机关门2")]
    public GameObject Door2;

    void IsBeingBoss()
    {
        if (Boss && !Boss.activeSelf) return;
        if (!IsInBoss) return;
        if (BossName == null)
        {
            DistorySelf();
        }
        else
        {
            if (GlobalTools.FindObjByName(BossName) == null) {
                DistorySelf();
            }
            else
            {
                //关门
               
                //延迟几秒 boss开始行动
                //IsSatrtYC = true;
            }
        }
        
    }

    bool IsSatrtYC = false;
    public float YCTimes = 2.5f;
    float YCJiShi = 0;
    void YanChiStart()
    {
        if (IsSatrtYC)
        {
            YCJiShi += Time.deltaTime;
            if(YCJiShi>= YCTimes)
            {
                IsSatrtYC = false;
                CloseDoor();
                //显示 字幕
                ShowSeeBossTxt();
                //遇Boss音效
                //Boss战音乐
                SeeBossAudioPlay();
                //显示Boss血条
                ShowBossLiveBar();

                DistorySelf();
            }
        }
    }


   



    private void OnDisable()
    {
        //print("我被消除了！？？？？？？");
    }

    [Header("1 下 0上")]
    public int OpenOrClose = 0;
    //关联门 碰撞关门
    void CloseDoor()
    {
        if (Door1Name != null) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, Door1Name + "-"+ OpenOrClose), this);
        if (Door2Name != null) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, Door2Name + "-"+ OpenOrClose), this);


        if (Door1)
        {
            //Door1.
        }

    }

    //遇见boss显示的字幕
    void ShowSeeBossTxt()
    {
        TheBossName = "";
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_NAME, TheBossName), this);
    }


    //G1
    //boss1 小恶魔
    //Boss2 恐惧之王
    //G2
    //boss3 巨像
    //G3
    //boss4 花妖
    //boss5 毒精灵
    //G4
    //boss6 电水母
    //boss7 行刑者
    //G5
    //boss8 幽灵法师
    //boss9 小七
    //boss10 巨像 
    //G6
    //boss11神之手
    //G7
    //boss12 灵魂立方
    //boss13 大锤怪
    //boss14 雷精灵
    //boss15 暗影
    //boss16 龙王


    string theBossName = "";
    public string BossID = "1";

    public string TheBossName
    {
        //GET访问器，可以理解成另类的方法，返回已经被赋了值的私有变量a
        get { return theBossName; }
        //SET访问器，将我们打入的值赋给私有变量money
        set
        {
            if (BossID == "1")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "小恶魔";
                        break;
                    case Globals.JAPAN:
                        theBossName = "小悪魔";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "little devil";
                        break;
                    case Globals.Portugal:
                        theBossName = "pequeño diablo";
                        break;
                    case Globals.KOREAN:
                        theBossName = "작은 악마";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "小惡魔";
                        break;
                    case Globals.German:
                        theBossName = "kleiner Teufel";
                        break;
                    case Globals.French:
                        theBossName = "petit diable";
                        break;
                    case Globals.Italy:
                        theBossName = "piccolo diavolo";
                        break;

                }
            }
            else if (BossID == "2")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "恐惧之王";
                        break;
                    case Globals.JAPAN:
                        theBossName = "恐怖の王";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "king of fear";
                        break;
                    case Globals.Portugal:
                        theBossName = "rey del miedo";
                        break;
                    case Globals.KOREAN:
                        theBossName = "공포의 왕";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "恐懼之王";
                        break;
                    case Globals.German:
                        theBossName = "König der Angst";
                        break;
                    case Globals.French:
                        theBossName = "roi de la peur";
                        break;
                    case Globals.Italy:
                        theBossName = "re della paura";
                        break;
                }
            }
            else if (BossID == "3")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "巨像";
                        break;
                    case Globals.JAPAN:
                        theBossName = "コロッサス";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "colossus";
                        break;
                    case Globals.Portugal:
                        theBossName = "coloso";
                        break;
                    case Globals.KOREAN:
                        theBossName = "거상";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "巨像";
                        break;
                    case Globals.German:
                        theBossName = "Koloss";
                        break;
                    case Globals.French:
                        theBossName = "colosse";
                        break;
                    case Globals.Italy:
                        theBossName = "colosso";
                        break;
                }
            }
            else if (BossID == "4")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "花妖";
                        break;
                    case Globals.JAPAN:
                        theBossName = "花の悪魔";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "flower demon";
                        break;
                    case Globals.Portugal:
                        theBossName = "demonio de flores";
                        break;
                    case Globals.KOREAN:
                        theBossName = "꽃 악마";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "花妖";
                        break;
                    case Globals.German:
                        theBossName = "Blumendämon";
                        break;
                    case Globals.French:
                        theBossName = "démon fleur";
                        break;
                    case Globals.Italy:
                        theBossName = "demone dei fiori";
                        break;
                }
            }
            else if (BossID == "5")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "腥毒恶魔";
                        break;
                    case Globals.JAPAN:
                        theBossName = "毒な悪魔";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "poisonous devil";
                        break;
                    case Globals.Portugal:
                        theBossName = "diablo venenoso";
                        break;
                    case Globals.KOREAN:
                        theBossName = "유독 한 악마";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "腥毒惡魔";
                        break;
                    case Globals.German:
                        theBossName = "giftiger Teufel";
                        break;
                    case Globals.French:
                        theBossName = "diable venimeux";
                        break;
                    case Globals.Italy:
                        theBossName = "diavolo velenoso";
                        break;
                }
            }
            else if (BossID == "6")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "电水母";
                        break;
                    case Globals.JAPAN:
                        theBossName = "電気クラゲ";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "Electric Jellyfish";
                        break;
                    case Globals.Portugal:
                        theBossName = "Medusa eléctrica";
                        break;
                    case Globals.KOREAN:
                        theBossName = "전기해파리";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "電水母";
                        break;
                    case Globals.German:
                        theBossName = "Elektrische Qualle";
                        break;
                    case Globals.French:
                        theBossName = "Méduse électrique";
                        break;
                    case Globals.Italy:
                        theBossName = "Medusa elettrica";
                        break;
                }
            }
            else if (BossID == "7")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "行刑者";
                        break;
                    case Globals.JAPAN:
                        theBossName = "死刑執行人";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "Executioner";
                        break;
                    case Globals.Portugal:
                        theBossName = "Verdugo";
                        break;
                    case Globals.KOREAN:
                        theBossName = "실행자";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "行刑者";
                        break;
                    case Globals.German:
                        theBossName = "Henker";
                        break;
                    case Globals.French:
                        theBossName = "Bourreau";
                        break;
                    case Globals.Italy:
                        theBossName = "Boia";
                        break;
                }
            }
            else if (BossID == "8")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "幽灵法师";
                        break;
                    case Globals.JAPAN:
                        theBossName = "ゴーストメイジ";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "Ghost Mage";
                        break;
                    case Globals.Portugal:
                        theBossName = "Mago fantasma";
                        break;
                    case Globals.KOREAN:
                        theBossName = "유령 마법사";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "幽靈法師";
                        break;
                    case Globals.German:
                        theBossName = "Geistermagier";
                        break;
                    case Globals.French:
                        theBossName = "Mage fantôme";
                        break;
                    case Globals.Italy:
                        theBossName = "Mago Fantasma";
                        break;
                }
            }
            else if (BossID == "9")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "小七";
                        break;
                    case Globals.JAPAN:
                        theBossName = "Xiaoqi";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "Xiaoqi";
                        break;
                    case Globals.Portugal:
                        theBossName = "Xiaoqi";
                        break;
                    case Globals.KOREAN:
                        theBossName = "小七";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "小七";
                        break;
                    case Globals.German:
                        theBossName = "Xiaoqi";
                        break;
                    case Globals.French:
                        theBossName = "Xiaoqi";
                        break;
                    case Globals.Italy:
                        theBossName = "Xiaoqi";
                        break;
                }
            }
            else if (BossID == "10")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "巨像";
                        break;
                    case Globals.JAPAN:
                        theBossName = "コロッサス";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "colossus";
                        break;
                    case Globals.Portugal:
                        theBossName = "coloso";
                        break;
                    case Globals.KOREAN:
                        theBossName = "거상";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "巨像";
                        break;
                    case Globals.German:
                        theBossName = "Koloss";
                        break;
                    case Globals.French:
                        theBossName = "colosse";
                        break;
                    case Globals.Italy:
                        theBossName = "colosso";
                        break;
                }
            }
            else if (BossID == "11")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "神之手";
                        break;
                    case Globals.JAPAN:
                        theBossName = "神の手";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "hand of God";
                        break;
                    case Globals.Portugal:
                        theBossName = "mano de Dios";
                        break;
                    case Globals.KOREAN:
                        theBossName = "신의 손";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "神之手";
                        break;
                    case Globals.French:
                        theBossName = "la main de Dieu";
                        break;
                    case Globals.German:
                        theBossName = "Hand Gottes";
                        break;
                    case Globals.Italy:
                        theBossName = "mano di Dio";
                        break;
                }
            }
            else if (BossID == "12")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "灵魂立方";
                        break;
                    case Globals.JAPAN:
                        theBossName = "ソウルキューブ";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "Soul Cube";
                        break;
                    case Globals.Portugal:
                        theBossName = "Cubo del alma";
                        break;
                    case Globals.KOREAN:
                        theBossName = "소울 큐브";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "靈魂立方";
                        break;
                    case Globals.German:
                        theBossName = "Seelenwürfel";
                        break;
                    case Globals.French:
                        theBossName = "Cube d'âme";
                        break;
                    case Globals.Italy:
                        theBossName = "Cubo dell'anima";
                        break;
                }
            }
            else if (BossID == "13")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "血腥大锤";
                        break;
                    case Globals.JAPAN:
                        theBossName = "血まみれのハンマー";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "bloody sledgehammer";
                        break;
                    case Globals.Portugal:
                        theBossName = "mazo sangriento";
                        break;
                    case Globals.KOREAN:
                        theBossName = "피 묻은 망치";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "血腥大錘";
                        break;
                    case Globals.German:
                        theBossName = "verdammter Vorschlaghammer";
                        break;
                    case Globals.French:
                        theBossName = "marteau sanglant";
                        break;
                    case Globals.Italy:
                        theBossName = "mazza insanguinata";
                        break;
                }
            }
            else if (BossID == "14")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "雷灵";
                        break;
                    case Globals.JAPAN:
                        theBossName = "レイリング";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "Rayling";
                        break;
                    case Globals.Portugal:
                        theBossName = "Rayling";
                        break;
                    case Globals.KOREAN:
                        theBossName = "레일링";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "雷靈";
                        break;
                    case Globals.German:
                        theBossName = "Rayling";
                        break;
                    case Globals.French:
                        theBossName = "Rayling";
                        break;
                    case Globals.Italy:
                        theBossName = "Rayling";
                        break;
                }
            }
            else if (BossID == "15")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "暗影";
                        break;
                    case Globals.JAPAN:
                        theBossName = "風邪";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "shadow";
                        break;
                    case Globals.Portugal:
                        theBossName = "sombra";
                        break;
                    case Globals.KOREAN:
                        theBossName = "그림자";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "暗影";
                        break;
                    case Globals.German:
                        theBossName = "Schatten";
                        break;
                    case Globals.French:
                        theBossName = "ombre";
                        break;
                    case Globals.Italy:
                        theBossName = "ombra";
                        break;
                }
            }
            else if (BossID == "16")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        theBossName = "龙王";
                        break;
                    case Globals.JAPAN:
                        theBossName = "ドラゴンキング";
                        break;
                    case Globals.ENGLISH:
                        theBossName = "Dragon King";
                        break;
                    case Globals.Portugal:
                        theBossName = "Rey Dragon";
                        break;
                    case Globals.KOREAN:
                        theBossName = "드래곤 킹";
                        break;
                    case Globals.CHINESEF:
                        theBossName = "龍王";
                        break;
                    case Globals.German:
                        theBossName = "Drachenkönig";
                        break;
                    case Globals.French:
                        theBossName = "Roi Dragon";
                        break;
                    case Globals.Italy:
                        theBossName = "re Drago";
                        break;
                }
            }
        }
    }










    //播放遇到Boss的音效
    void SeeBossAudioPlay()
    {
        if (SeeBossAudio)
        {
            //SeeBossAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossAudio.Play();
        }

        if (SeeBossBGAudio)
        {
            //SeeBossBGAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossBGAudio.Play();
        }


        if (LongHou)
        {
            LongHou.Play();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.4"), this);
        }
    }

    //战斗打完停止 播放音乐
    void SeeBossBGAudioStop()
    {
        if (SeeBossBGAudio)
        {
            //SeeBossBGAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossBGAudio.Stop();
        }
    }


    void ShowBossLiveBar()
    {
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_OUT, ShowSelf);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_OUT, BossName), this);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_RUN_AC_2, ""), this);
        //if(IsChangeZByBossAndPlayerDistance) GlobalTools.FindObjByName("MainCamera").GetComponent<CameraController>().GetBossAndMaxZPos(GlobalTools.FindObjByName(BossName));
        if (ChangeKuaiName!="kuang1"&&ChangeKuaiName!="")GlobalTools.FindObjByName("MainCamera").GetComponent<CameraController>().GetBounds(GlobalTools.FindObjByName(ChangeKuaiName).GetComponent<BoxCollider2D>(),true);
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "y"), this);
    }


    [Header("boss")]
    public GameObject Boss;

    [Header("boss 出现位置")]
    public Transform BossOutPos;

    public bool IsBossInPos = false;

    public string otherEvent;
    [Header("boss出现时候 显示特效")]
    public ParticleSystem TX_ShowBossTX;


    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (IsSatrtYC) return;
        if(BossOutPos && Boss && !IsBossInPos)
        {
            IsBossInPos = true;
            if(TX_ShowBossTX) TX_ShowBossTX.Play();
            
            Boss.transform.position = BossOutPos.position;

        }


        //return;
        if (!IsPlayerIn&&Coll.tag == "Player")
        {
            IsPlayerIn = true;
            //碰撞了 关门 
            CloseDoor();

            if (otherEvent != "")
            {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.JG_OTHER_EVENT, otherEvent), this);
            }

            if (IsInBoss)
            {
                //显示 字幕
                ShowSeeBossTxt();
                //遇Boss音效
                //Boss战音乐
                SeeBossAudioPlay();
                //显示Boss血条
                ShowBossLiveBar();

                if(!IsSatrtYC)DistorySelf();
            }
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {

        if (Coll.tag == "Player")
        {
        }
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {

        //print("Trigger - C");
    }

    public void DistorySelf()
    {
        StartCoroutine(IEDestoryByEnd(this.gameObject));
    }
    public IEnumerator IEDestoryByEnd(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(obj, true);
    }
}
