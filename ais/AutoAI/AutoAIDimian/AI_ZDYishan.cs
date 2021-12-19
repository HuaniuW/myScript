using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZDYishan : AI_SkillBase
{
    //ZD_ZDYishan
    protected override void TheStart()
    {
        base.TheStart();
        this.ZSName = "ZDYishan";
        thisChushiX = this.transform.position.x;
        this.IsSpeAISkill = true;
    }

    protected override void GetTheStart()
    {
        //base.GetTheStart();
        thisChushiX = this.transform.position.x;

        //判断 前面距离
        Vector2 start;
        //前面的距离测试 
        Vector2 end;
        //print("ZSyishanZSys -1");
        //jishiNums = 0;
        //_XZtimes = 0;
        if (this.transform.localScale.x < 0)
        {
            start = this.transform.position;
            end = new Vector2(start.x + YishanDistance + 1, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            if (Physics2D.Linecast(start, end, _gameBody.groundLayer))
            {
                //Time.timeScale = 0;
                //print("撞墙 右！！！");
                GetOver();
                return;
            }

        }
        else
        {
            start = this.transform.position;
            end = new Vector2(start.x - YishanDistance - 1, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            if (Physics2D.Linecast(start, end, _gameBody.groundLayer))
            {
                //Time.timeScale = 0;
                //print("撞墙 左！！！");
                GetOver();
                return;
            }
        }





    }

    public override void ReSetAll()
    {
        base.ReSetAll();
        ResetThisAll();
    }

    protected void ResetThisAll()
    {
        DisX = 0;
        GetComponent<JN_Date>().HitInSpecialEffectsType = 5;
        _gameBody.IsJiasu = false;
        IsTanshe = false;
        if (YishanHitKuai) YishanHitKuai.SetActive(false);
        if (StartParticle)
        {
            StartParticle.Stop();
            StartParticle.gameObject.SetActive(false);
        }
        _gameBody.isAcing = false;
        _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
    }

    [Header("前面探测点 2")]
    public UnityEngine.Transform qianmianjiance;
    [Header("是否 撞墙探测")]
    public bool IsHitQiangQainmian;
    [Header("地面图层 包括机关")]
    public LayerMask groundLayer;
    public bool IsHitWall2
    {
        get
        {
            if (qianmianjiance == null) return false;
            Vector2 start = qianmianjiance.position;
            float __x = this.transform.localScale.x > 0 ? start.x - 2 : start.x + 2;
            Vector2 end = new Vector2(__x, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            IsHitQiangQainmian = Physics2D.Linecast(start, end, groundLayer);
            return IsHitQiangQainmian;
        }
    }


    [Header("临时 提高硬直")]
    public float TempAddYZ = 1000;

    [Header("一闪用的 碰撞块")]
    public GameObject YishanHitKuai;

    [Header("自身 碰撞块")]
    public GameObject ZishenHitKuai;


    [Header("一闪的 距离")]
    public float YishanDistance = 10;

    float thisChushiX = 0;

    bool IsTanshe = false;
    float vx = 60;
    float DisX = 0;

    [Header("烟尘 ")]
    public ParticleSystem YanChen;

    protected override void ChixuSkillStarting()
    {
        //base.ChixuSkillStarting();
        if (!_isGetStart) return;

        if (!GetComponent<AIBase>().isActioning)
        {
            TheSkillOver();
            return;
        }

        if (_roleDate.isBeHiting || _roleDate.isDie)
        {
            TheSkillOver();
            //print("------> 进来没？？   "+ GetComponent<Rigidbody2D>().velocity);
            if (_roleDate.isDie) GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            return;
        }

        if (_gameBody.IsHitWall || IsHitWall2)
        {
            GetOver();
            return;
        }

        if (DisX>= YishanDistance)
        {
            GetOver();
            return;
        }


        if (!IsStopSelf)
        {
            IsStopSelf = true;
            _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
        }

        if (!IsSkillShowOut)
        {
            IsSkillShowOut = true;

            //print("zd acName:    "+ACName);
            if (ACName != "")
            {
                _gameBody.isAcing = true;
                //_gameBody.GetDB().animation.GotoAndPlayByFrame(ACName, 0, 1);
                _gameBody.GetDB().animation.GotoAndStopByFrame(ACName);
                _gameBody.isAcing = true;
                GetComponent<TempAddValues>().TempAddYZ(TempAddYZ, StartDelayTimes+1.4f);
            }

        }


        if (_isGetStart)
        {
            if (IsStartDelayOver())
            {
                if (!IsTanshe)
                {
                    IsTanshe = true;
                    vx = Mathf.Abs(vx);
                    vx = this.transform.localScale.x > 0 ? vx * -1 : vx;
                    ZishenHitKuai.SetActive(false);
                    if (YishanHitKuai) YishanHitKuai.SetActive(true);
                    _gameBody.GetDB().animation.GotoAndPlayByFrame(ACName,0,1);
                    //这里将 攻击属性 改成了 非碰撞
                    GetComponent<JN_Date>().HitInSpecialEffectsType = 1;

                    _gameBody.IsJiasu = true;

                    GameObject tx = GlobalTools.GetGameObjectInObjPoolByName("TX_Yishan_1");
                    tx.transform.position = this.transform.position;
                    tx.transform.localScale = new Vector3(this.transform.localScale.x, 1, 1);
                    tx.transform.parent = this.transform.parent;
                }

                //if (_gameBody.GetDB().animation.lastAnimationState.isCompleted)
                //{
                //    print("  jiehsu maammamamamamam ");
                //}

                if (_gameBody.GetDB().animation.lastAnimationName == ACName)
                {
                    if (StartParticle != null && !IsStartParticle)
                    {
                        IsStartParticle = true;
                        //print("zd qs 播放 起手特效");
                        StartParticle.gameObject.SetActive(true);
                        StartParticle.Play();

                    }

                    //print("------------------??????   " + ACName + "   sudu   " + GetComponent<Rigidbody2D>().velocity);

                    if (_gameBody.GetDB().animation.isCompleted)
                    {
                        //print("???????  结束   ");
                        //print("???????  结束   ");
                        //print("???????  结束   ");
                        _gameBody.GetDB().animation.Stop();
                    }

                    GetComponent<Rigidbody2D>().velocity = new Vector2(vx, 0);
                    if (YanChen) YanChen.Play();
                }
                else
                {
                    print("跳出了 一闪动作 结束");
                    GetOver();
                }



                //GetComponent<Rigidbody2D>().velocity = new Vector2(vx, 0);
                //if (YanChen) YanChen.Play();

                DisX = Mathf.Abs(this.transform.position.x - thisChushiX);

            }
            
        }

    }

    private void GetOver()
    {
        //throw new NotImplementedException();
        if (YishanHitKuai) YishanHitKuai.SetActive(false);
        GetComponent<JN_Date>().HitInSpecialEffectsType = 5;
        ZishenHitKuai.SetActive(true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (IsOverDelayOver())
        {
            TheSkillOver();
        }
       
    }
}
