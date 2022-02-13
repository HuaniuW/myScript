using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class B_sanjiaoYi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<AirGameBody>();
        GetDB();
        HideHui();
        _player = GlobalTools.FindObjByName(GlobalTag.PlayerJijiaObj);
    }

    protected UnityArmatureComponent DBBody;
    public UnityArmatureComponent GetDB()
    {
        if (!DBBody) DBBody = GetComponentInChildren<UnityArmatureComponent>();
        return DBBody;
    }


    [Header("推进器 喷射点 1")]
    public ParticleSystem TX_PenshePos1;
    [Header("推进器 喷射点 2")]
    public ParticleSystem TX_PenshePos2;
    [Header("推进器 喷射点 3")]
    public ParticleSystem TX_PenshePos3;
    [Header("推进器 喷射点 4")]
    public ParticleSystem TX_PenshePos4;


    [Header("推进器 喷射点 1 爆炸")]
    public ParticleSystem TX_PenshePos1BZ;
    [Header("推进器 喷射点 2 爆炸")]
    public ParticleSystem TX_PenshePos2BZ;
    [Header("推进器 喷射点 3 爆炸")]
    public ParticleSystem TX_PenshePos3BZ;
    [Header("推进器 喷射点 4 爆炸")]
    public ParticleSystem TX_PenshePos4BZ;

    [Header("推进器 喷射点 1 爆炸后 燃烧")]
    public ParticleSystem TX_PenshePosRanshao1;

    [Header("推进器 喷射点 2 爆炸后 燃烧")]
    public ParticleSystem TX_PenshePosRanshao2;

    [Header("推进器 喷射点 3 爆炸后 燃烧")]
    public ParticleSystem TX_PenshePosRanshao3;

    [Header("推进器 喷射点 4 爆炸后 燃烧")]
    public ParticleSystem TX_PenshePosRanshao4;




    GameBody _gameBody;    

    void HideHui()
    {
        //GetDB().armature.GetSlot("辫子右")._SetDisplayIndex(1);
        _gameBody.GetDB().armature.GetSlot("hui_3")._SetDisplayIndex(-1);//引擎1 从下往上数
        _gameBody.GetDB().armature.GetSlot("hui_2")._SetDisplayIndex(-1);
        _gameBody.GetDB().armature.GetSlot("hui_1")._SetDisplayIndex(-1);
        _gameBody.GetDB().armature.GetSlot("hui_4")._SetDisplayIndex(-1);
    }

    bool IsPenshe_1_Bad = false;
    bool IsPenshe_2_Bad = false;
    bool IsPenshe_3_Bad = false;
    bool IsPenshe_4_Bad = false;

    float FlyMaxSpeedX = 140;

    public void TuijinqiBoomByNum(int num)
    {
        print("jinlaimei ????  -------->  "+num);
        FlyMaxSpeedX -= 10;

        if (num == 1)
        {
            _gameBody.GetDB().armature.GetSlot("hui_3")._SetDisplayIndex(0);
            IsPenshe_1_Bad = true;
            
            TX_PenshePos1.Stop();
            //爆炸 特效
            TX_PenshePos1BZ.gameObject.SetActive(true);
            TX_PenshePos1BZ.Play();
            //着火 特效
            TX_PenshePosRanshao1.gameObject.SetActive(true);
            TX_PenshePosRanshao1.Play();
        }else if (num == 2) {
            _gameBody.GetDB().armature.GetSlot("hui_2")._SetDisplayIndex(0);
            IsPenshe_2_Bad = true;
            TX_PenshePos2.Stop();
            //爆炸 特效
            TX_PenshePos2BZ.gameObject.SetActive(true);
            TX_PenshePos2BZ.Play();
            //着火 特效
            TX_PenshePosRanshao2.gameObject.SetActive(true);
            TX_PenshePosRanshao2.Play();
        }
        else if (num == 3)
        {
            _gameBody.GetDB().armature.GetSlot("hui_1")._SetDisplayIndex(0);
            IsPenshe_3_Bad = true;
            TX_PenshePos3.Stop();
            //爆炸 特效
            TX_PenshePos3BZ.gameObject.SetActive(true);
            TX_PenshePos3BZ.Play();
            //着火 特效
            TX_PenshePosRanshao3.gameObject.SetActive(true);
            TX_PenshePosRanshao3.Play();
        }
        else if (num == 4)
        {
            _gameBody.GetDB().armature.GetSlot("hui_4")._SetDisplayIndex(0);
            IsPenshe_4_Bad = true;
            TX_PenshePos4.Stop();
            //爆炸 特效
            TX_PenshePos4BZ.gameObject.SetActive(true);
            TX_PenshePos4BZ.Play();
            //着火 特效
            TX_PenshePosRanshao4.gameObject.SetActive(true);
            TX_PenshePosRanshao4.Play();
        }
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);

    }


    float YibanSpeedX = 80;
    float Tuili = 40;


    void Jiasu()
    {
        if (!IsPenshe_1_Bad&& TX_PenshePos1.isStopped)
        {
            TX_PenshePos1.Play();
        }

        if (!IsPenshe_2_Bad && TX_PenshePos2.isStopped)
        {
            TX_PenshePos2.Play();
        }

        if (!IsPenshe_3_Bad && TX_PenshePos3.isStopped)
        {
            TX_PenshePos3.Play();
        }

        if (!IsPenshe_4_Bad && TX_PenshePos4.isStopped)
        {
            TX_PenshePos4.Play();
        }
        IsJialiFly = true;
    }

    void StopJiasu()
    {
        if (!IsPenshe_1_Bad && TX_PenshePos1.isPlaying)
        {
            TX_PenshePos1.Stop();
        }

        if (!IsPenshe_2_Bad && TX_PenshePos2.isPlaying)
        {
            TX_PenshePos2.Stop();
        }

        if (!IsPenshe_3_Bad && TX_PenshePos3.isPlaying)
        {
            TX_PenshePos3.Stop();
        }

        if (!IsPenshe_4_Bad && TX_PenshePos4.isPlaying)
        {
            TX_PenshePos4.Stop();
        }
        IsJialiFly = false;
    }





    protected bool IsJialiFly = false;
    float SpeedY = 0;


    protected void FlyX()
    {
        //print(" ------------>  tuili  "+Tuili);
        SpeedY = GetComponent<Rigidbody2D>().velocity.y;
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(Tuili, SpeedY));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Tuili, 0));
        if (IsJialiFly)
        {
            if (GetComponent<Rigidbody2D>().velocity.x > FlyMaxSpeedX)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(FlyMaxSpeedX, SpeedY);
            }
        }
        else
        {
            if (YibanSpeedX > FlyMaxSpeedX)
            {
                YibanSpeedX = FlyMaxSpeedX;
            }
            if(GetComponent<Rigidbody2D>().velocity.x> YibanSpeedX)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(YibanSpeedX, SpeedY);
            }
            
        }
        //print("GetComponent<Rigidbody2D>().velocity      " + GetComponent<Rigidbody2D>().velocity.x+"    max "+FlyMaxSpeedX);
    }

    GameObject _player;
    bool IsJiasuing = false;
    float JiasuTimes = 0;
    void FlyAI()
    {
        if (this.transform.position.x - _player.transform.position.x < 25)
        {
            IsJiasuing = true;
            IsGanraodan = false;
            GanraodanStart = true;
            Jiasu();
        }
        else
        {
            if (IsJiasuing)
            {
                JiasuTimes += Time.deltaTime;
                if (JiasuTimes>=2)
                {
                    JiasuTimes = 0;
                    IsJiasuing = false;
                }
                return;
            }

            StopJiasu();
        }
    }


    string GANRAODAN = "";

    [Header("干扰弹 发射点 1")]
    public UnityEngine.Transform GandaodanPos1;
    [Header("干扰弹 发射点 2")]
    public UnityEngine.Transform GandaodanPos2;

    bool GanraodanStart = false;
    bool IsGanraodan = false;
    void Ganraodan()
    {
        if (!GanraodanStart) return;
        if (!IsGanraodan)
        {
            IsGanraodan = true;
            DBBody.animation.FadeIn(GANRAODAN,0.4f,1);
            StartJinageZidan();
            //机盖 打开的  机械声
        }

        if(GetDB().animation.lastAnimationName == GANRAODAN&& GetDB().animation.isCompleted)
        {
            //发射 干扰弹
            JiangePenZidan();
        }

    }

    void StartJinageZidan()
    {
        IsJiangePenZidan = true;
        jishi = 0;
        ZidanNums = 10;
    }


    bool IsJiangePenZidan = false;
    float jishi = 0;
    float jiange = 0.1f;
    int ZidanNums = 10;

    void JiangePenZidan()
    {
        if (IsJiangePenZidan)
        {
            jishi += Time.deltaTime;
            if (jishi >= jiange)
            {
                jishi = 0;
                ZidanNums--;
                ShowZidan();
                ShowZidan2();
                if (ZidanNums == 0)
                {
                    IsJiangePenZidan = false;

                    ZidanNums = 10;
                    GanraodanStart = false;
                }

            }
        }
    }

    string ZidanName = "TX_zidanJijia_1";
    void ShowZidan()
    {
        GameObject zidan1 = GlobalTools.GetGameObjectInObjPoolByName(ZidanName);
        zidan1.name = ZidanName;
        zidan1.transform.position = GandaodanPos1.position;
        zidan1.transform.parent = this.transform.parent;
        float _speedY = -300 - GlobalTools.GetRandomDistanceNums(300);
        //if (this.transform.position.y < _player.transform.position.y)
        //{
        //    _speedY *= -1;
        //}
        zidan1.GetComponent<Rigidbody2D>().AddForce(new Vector2(300 + GlobalTools.GetRandomDistanceNums(300), _speedY));
    }

    void ShowZidan2()
    {
        GameObject zidan1 = GlobalTools.GetGameObjectInObjPoolByName(ZidanName);
        zidan1.name = ZidanName;
        zidan1.transform.position = GandaodanPos2.position;
        zidan1.transform.parent = this.transform.parent;
        float _speedY = 300 + GlobalTools.GetRandomDistanceNums(300);
        //if (this.transform.position.y < _player.transform.position.y)
        //{
        //    _speedY *= -1;
        //}
        zidan1.GetComponent<Rigidbody2D>().AddForce(new Vector2(300 + GlobalTools.GetRandomDistanceNums(300), _speedY));
    }







    // Update is called once per frame
    void Update()
    {
        FlyX();
        FlyAI();
        //------干扰弹---------
        Ganraodan();
        JiangePenZidan();
    }
}
