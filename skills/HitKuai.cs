using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKuai : MonoBehaviour {

    public int teamNum;
    // Use this for initialization
    void Start() {
        //print(this.transform);
       
    }


    //GameObject的位置  攻击属性  相对位置x y  尺寸   是否立即消失

    private void OnEnable()
    {
        //print("???");
        //_isCanHit = true;
        //IsHasChixuHit = false;
    }

    // Update is called once per frame
    void Update() {
    }

    

    GameObject atkObj;
    RoleDate atkObjRoleDate;

    RoleDate BeHitRoleDate;
    GameBody BeHitGameBody;

    GameObject BehitGameObject;

    Rigidbody2D _BeHitRigidbody2D;

    Rigidbody2D _atkRigidbody2D;


    GameObject txObj;
    Transform beHitObj;
    
    public void GetTXObj(GameObject txObj,bool isSkill = false,float atkObjScaleX = 1,GameObject atkObj = null){
        if (txObj != null)
        {
            this.txObj = txObj;
        }
        else
        {
            this.txObj = this.transform.parent.gameObject;
            if (atkObj != null)
            {
                txObj.GetComponent<JN_base>().atkObj = atkObj;
            }
            //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"+this.name+"     "+this.txObj);
        }
        //this.GetComponent<JN_Date>().team = this.txObj.GetComponent<JN_Date>().team;
        
        if (!isSkill)StartCoroutine(ObjectPools.GetInstance().IEDestory2(this.gameObject));
        //atkObj = txObj.GetComponent<JN_base>().atkObj;
        _atkObjScaleX = atkObjScaleX; // txObj.GetComponent<JN_base>().atkObj.transform.localScale.x;
        //if(atkObj.tag == "Player")print("TEAM --------->  " + this.txObj.GetComponent<JN_Date>().team + "   _atkObjScaleX  " + _atkObjScaleX);
    }




    JN_Date jn_date;
    float _atkObjScaleX;

    float txPos = 0;


    Vector2 HitPos = Vector2.zero;
    Vector2 GetHitPos(bool IsHasbody = true)
    {
        
        //if(this.GetComponent<BoxCollider2D>() == null)return BeHitGameBody.transform.position;
        Vector2 p1 = Vector2.zero;
        Vector2 s1 = Vector2.zero;
        Vector2 p2 = Vector2.zero;
        if (this.GetComponent<BoxCollider2D>())
        {
            print(" 55555 ???????1  ");
            p1 = this.GetComponent<BoxCollider2D>().bounds.center;
            s1 = this.GetComponent<BoxCollider2D>().bounds.extents;
        }else if (this.GetComponent<CircleCollider2D>())
        {
            print(" 55555 ???????12222  ");
            p1 = this.GetComponent<CircleCollider2D>().bounds.center;
            s1 = this.GetComponent<CircleCollider2D>().bounds.extents;
        }
        else if(this.GetComponent<CapsuleCollider2D>())
        {
            print("  556666s ");
            p1 = this.GetComponent<CapsuleCollider2D>().bounds.center;
            s1 = this.GetComponent<CapsuleCollider2D>().bounds.extents;
        }



        if (BeHitGameBody && BeHitGameBody.GetComponent<CapsuleCollider2D>()) {
            p2 = BeHitGameBody.GetComponent<CapsuleCollider2D>().bounds.center;
        }
        else if(BeHitGameBody && BeHitGameBody.GetComponent<EdgeCollider2D>())
        {
            p2 = BeHitGameBody.GetComponent<EdgeCollider2D>().bounds.center;
        }
        
        Vector2 s2 = Vector2.zero;
        if (BeHitGameBody && BeHitGameBody.GetComponent<CapsuleCollider2D>())
        {
            s2 = BeHitGameBody.GetComponent<CapsuleCollider2D>().bounds.extents;
        }
        else if (BeHitGameBody && BeHitGameBody.GetComponent<EdgeCollider2D>())
        {
            s2 = BeHitGameBody.GetComponent<EdgeCollider2D>().bounds.extents;
        }


        if (BeHitGameBody && BeHitGameBody.GetComponent<CapsuleCollider2D>())
        {
            p2 = BehitGameObject.GetComponent<CapsuleCollider2D>().bounds.center;
            s2 = BehitGameObject.GetComponent<CapsuleCollider2D>().bounds.extents;
        }else if (BeHitGameBody && BeHitGameBody.GetComponent<EdgeCollider2D>())
        {
            p2 = BehitGameObject.GetComponent<EdgeCollider2D>().bounds.center;
            s2 = BehitGameObject.GetComponent<EdgeCollider2D>().bounds.extents;
        }


        float _x = 0;
        float _y = 0;


        if (p1.x + s1.x < p2.x)
        {
            _x = p1.x + s1.x;
        }
        else
        {
            _x = p2.x;
        }

        if (p1.y >= p2.y + s2.y)
        {
            //print(p1.y+"   u   "+(p2.y+s2.y));
            _y = p2.y + s2.y;
        }
        else if (p1.y <= p2.y - s2.y)
        {
            _y = p2.y - s2.y;
        }
        else
        {
            _y = p1.y;
        }
        HitPos = new Vector2(_x,_y);
        if (!IsHasbody && BehitGameObject.GetComponent<CircleCollider2D>())
        {
            print("  ??????????????????????? ");
            HitPos = beHitObj.position;
        }

        if (!IsHasbody)
        {
            HitPos = beHitObj.position;
            if (beHitObj.GetComponent<TX_RenzheBiao>())
            {
                if (atkObj) beHitObj.GetComponent<TX_RenzheBiao>().GetBehit(atkObj.transform.position);
            }
        }

        print("  -----------jizhong weizhi "+HitPos);
        return HitPos;
    }



    //public bool IsCanChiXuHit = false;
    //bool IsHasChixuHit = false;

    private void OnTriggerStay2D(Collider2D Coll)
    {
        //if (!IsCanChiXuHit) return;
        //if (!IsHasChixuHit)
        //{
            
        //    if (Coll.GetComponent<RoleDate>() == null || Coll.GetComponent<RoleDate>().team == this.teamNum) return;
        //    if (!Coll.GetComponent<RoleDate>().isCanBeHit) return;
        //    IsHasChixuHit = true;
        //    //print(" --------------------------持续碰撞  ");
        //    GetHit(Coll);
        //}
    }


    List<GameObject> skill_list = new List<GameObject> { };

    //被动弹开 用于被动技能 弹开 攻击者  花防 神佑
    void BeiDongTankai(string type = "huafang")
    {
        float GaoTuiliBeishu = 5;
        float DiTuiliBeishu = 2;
        //减慢时间
        float SXTimes = 2;
        //减慢倍数 
        float SXBeishu = 0.2f;

        if (type == "gedang")
        {
            GaoTuiliBeishu = 2;
            DiTuiliBeishu = 1;

            SXTimes = 1f;
            SXBeishu = 0.2f;

        }

        // 弹开攻击者
        ObjV3Zero(atkObj);

        if (__tuiliFX > 0)
        {
            BeHitGameBody.GetComponent<GameBody>().TurnLeft();
            //BeHitGameBody.transform.position = new Vector2(BeHitGameBody.transform.position.x-0.01f, BeHitGameBody.transform.position.y);
            //BeHitGameBody.GetComponent<GameBody>().GetPlayerRigidbody2D().AddForce(new Vector2(-10, 0));
        }
        else
        {
            BeHitGameBody.GetComponent<GameBody>().TurnRight();
        }

        //弹开攻击者

        // 进入 BeHit里面 判断 角色的 硬直 来判断 是否进入
        if (!atkObj || !atkObj.GetComponent<GameBody>())
        {

        }
        else
        {

            print("hf -------------------》技能的 类型  " + jn_date._type + "    jienneg name " + jn_date.name);
            print("hf  硬直是多少  atkyz   " + atkObj.GetComponent<RoleDate>().yingzhi + "    behitYZ  " + beHitObj.GetComponent<RoleDate>().yingzhi);
            if (jn_date._type == "1" || jn_date._type == "2" || jn_date._type == "4")
            {
                print(" atkObj.name    " + atkObj.name);
                //print(" atkObj GamebODY   " + atkObj.GetComponent<GameBody>());
                float tuili = atkObj.transform.position.x > beHitObj.transform.position.x ? 400 + jn_date.FanTuili : -400 - jn_date.FanTuili;
                float YinzhiCha = atkObj.GetComponent<RoleDate>().yingzhi - beHitObj.GetComponent<RoleDate>().yingzhi;
                if (Mathf.Abs(atkObj.GetComponent<RoleDate>().yingzhi - beHitObj.GetComponent<RoleDate>().yingzhi) < 100)
                {
                    if (jn_date.chongjili != 0)
                    {
                        atkObj.GetComponent<GameBody>().HasBeHit(-10);
                    }
                    else
                    {
                        atkObj.GetComponent<GameBody>().HasBeHit();
                    }
                    atkObj.GetComponent<GameBody>().GetTuili(tuili, 1f, true);
                }
                else
                {
                    print(" hf 进来没  推力测试 在哪 ??  " + -tuili);
                    if (YinzhiCha >= 300)
                    {
                        beHitObj.GetComponent<GameBody>().GetTuili(-tuili * GaoTuiliBeishu, 1f, true);
                    }
                    else
                    {
                        beHitObj.GetComponent<GameBody>().GetTuili(-tuili * DiTuiliBeishu, 1f, true);
                    }

                }


                atkObj.GetComponent<GameBody>().GetPause(SXTimes, SXBeishu);
                //_atkRigidbody2D.AddForce(new Vector2(-1000 * _roleScaleX, 0));
                //GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes();

                //if (jn_date.FanTuili != 0) tuili -= jn_date.FanTuili;
                //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> tuili  是多少： " + tuili);

            }


        }
    }


    //无视硬直弹开 攻击者
    void AtkerBeiTankai()
    {
        // 弹开攻击者
        ObjV3Zero(atkObj);

        if (__tuiliFX > 0)
        {
            BeHitGameBody.GetComponent<GameBody>().TurnLeft();
            //BeHitGameBody.transform.position = new Vector2(BeHitGameBody.transform.position.x-0.01f, BeHitGameBody.transform.position.y);
            //BeHitGameBody.GetComponent<GameBody>().GetPlayerRigidbody2D().AddForce(new Vector2(-10, 0));
        }
        else
        {
            BeHitGameBody.GetComponent<GameBody>().TurnRight();
        }

        //弹开攻击者

        // 进入 BeHit里面 判断 角色的 硬直 来判断 是否进入
        if (!atkObj || !atkObj.GetComponent<GameBody>())
        {

        }
        else
        {

            print("hf -------------------》技能的 类型  " + jn_date._type + "    jienneg name " + jn_date.name);
            print("hf  硬直是多少  atkyz   " + atkObj.GetComponent<RoleDate>().yingzhi + "    behitYZ  " + beHitObj.GetComponent<RoleDate>().yingzhi);
            if (jn_date._type == "1" || jn_date._type == "2" || jn_date._type == "4")
            {
                print(" atkObj.name    " + atkObj.name);
                //print(" atkObj GamebODY   " + atkObj.GetComponent<GameBody>());
                float tuili = atkObj.transform.position.x > beHitObj.transform.position.x ? 400 + jn_date.FanTuili : -400 - jn_date.FanTuili;
                float YinzhiCha = atkObj.GetComponent<RoleDate>().yingzhi - beHitObj.GetComponent<RoleDate>().yingzhi;

                if (YinzhiCha >= 300)
                {
                    beHitObj.GetComponent<GameBody>().GetTuili(-tuili * 5, 1f, true);
                }
                else
                {
                    beHitObj.GetComponent<GameBody>().GetTuili(-tuili * 2, 1f, true);
                }


                atkObj.GetComponent<GameBody>().GetPause(2, 0.2f);
                //_atkRigidbody2D.AddForce(new Vector2(-1000 * _roleScaleX, 0));
                //GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes();

                //if (jn_date.FanTuili != 0) tuili -= jn_date.FanTuili;
                //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> tuili  是多少： " + tuili);

            }


        }
    }



    bool GetShenyou()
    {
        print("神佑！！！     +   "+ skill_list.Count);
        if (BeHitGameBody&&BeHitGameBody.tag == "Player" && skill_list.Count != 0)
        {
            foreach (GameObject defSkill in skill_list)
            {
                //获取技能信息
                //GameObject obj = Resources.Load(defSkill) as GameObject;

                if (defSkill.GetComponent<UI_Skill>().GetHZDate().type == "zd") continue;
                if (defSkill.GetComponent<UI_Skill>().GetHZDate().def_effect == "shenyou")
                {
                    //是否有蓝
                    if (BeHitGameBody.GetComponent<RoleDate>().lan < defSkill.GetComponent<UI_Skill>().GetHZDate().xyLan) return false;
                    //是否冷却  还是只能找 玩家装备的技能
                    if (!defSkill.GetComponent<UI_Skill>().IsCDSkillCanBeUse()) return false;

                    //防御几率
                    float jv = defSkill.GetComponent<UI_Skill>().GetHZDate().Chance_of_Passive_Skills;
                    //获取触发几率 
                    float cfjv = GlobalTools.GetRandomDistanceNums(100);
                    if (cfjv > jv) return false;
                    //触发
                    //加血
                    //无伤害
                    //判断 蓝够不够


                    //伤害减免
                    print("进入神佑技能！！！！");
                    //计算 生命值 和伤害
                    print(" 神佑 进来没？？？？？ ");
                    BeHitRoleDate.live = BeHitRoleDate.maxLive * 0.5f;

                    //BeiDongTankai();
                    AtkerBeiTankai();

                    // 减帧数
                    GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes(0.2f, 0.3f);
                    BeHitGameBody.GetComponent<GameBody>().ShowPassiveSkill(defSkill);
                    return true;
                }
            }

        }
        return false;
    }


    //花防
    bool IsGetHuafang()
    {
        if (BeHitGameBody.tag == "Player" && skill_list.Count != 0)
        {
            foreach (GameObject defSkill in skill_list)
            {
                //获取技能信息
                //GameObject obj = Resources.Load(defSkill) as GameObject;

                if (defSkill.GetComponent<UI_Skill>().GetHZDate().type == "zd") continue;
                if (defSkill.GetComponent<UI_Skill>().GetHZDate().def_effect == "wushanghai")
                {
                    //是否有蓝
                    if (BeHitGameBody.GetComponent<RoleDate>().lan < defSkill.GetComponent<UI_Skill>().GetHZDate().xyLan) return false;
                    //是否冷却  还是只能找 玩家装备的技能
                    if (!defSkill.GetComponent<UI_Skill>().IsCDSkillCanBeUse()) return false;

                    //防御几率
                    float jv = defSkill.GetComponent<UI_Skill>().GetHZDate().Chance_of_Passive_Skills;
                    //获取触发几率 
                    float cfjv = GlobalTools.GetRandomDistanceNums(100);
                    if (cfjv > jv) return false;

                    beHitObj.GetComponent<GameBody>().ResetAll();
                    BeHitGameBody.GetComponent<GameBody>().ShowPassiveSkill(defSkill);


                    //print(" 无伤害 播放 被动防御 特效！！！1 ");
                    BeiDongTankai();

                    // 减帧数
                    GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes(0.2f, 0.3f);
                    return true;
                }
            }

        }
        return false;
    }



    //伤害减免 护盾
    void ShanghaiJianmianHD()
    {
        foreach (GameObject defSkill in skill_list)
        {
            //获取技能信息
            //GameObject obj = Resources.Load(defSkill) as GameObject;

            if (defSkill.GetComponent<UI_Skill>().GetHZDate().type == "zd") continue;
           


            if (defSkill.GetComponent<UI_Skill>().GetHZDate().def_effect == "shanghaijianmianDun")
            {

                //是否有蓝
                if (BeHitGameBody.GetComponent<RoleDate>().lan < defSkill.GetComponent<UI_Skill>().GetHZDate().xyLan) return;
                //是否冷却  还是只能找 玩家装备的技能
                if (!defSkill.GetComponent<UI_Skill>().IsCDSkillCanBeUse()) return;

                //防御几率
                float jv = defSkill.GetComponent<UI_Skill>().GetHZDate().Chance_of_Passive_Skills;
                //获取触发几率 
                float cfjv = GlobalTools.GetRandomDistanceNums(100);
                if (cfjv > jv) return;
                //触发
                //加血
                //无伤害
                //判断 蓝够不够



                AtkerBeiTankai();

                //伤害减免
                print("触发伤害 减免 盾**** 的 防御型 技能");
                //专用？ 给一个 判定参数  显示 特效 被攻击 显示 减免特效？
                //关掉原来的 盾圈特效 低亮
                //出一个 盾圈特效 高亮 3秒后结束
                BeHitGameBody.GetComponent<GameBody>().ShowPassiveSkill(defSkill);
                return;
            }

        }
    }




    public void LiziHit(GameObject o)
    {
        print("粒子 击中对象名字   "+o.name);
        //if (o.GetComponent<BoxCollider2D>())
        //{
        //    GetHit(o.GetComponent<BoxCollider2D>());
        //    return;
        //}
        GetHit(o.GetComponent<Collider2D>());
    }


    [Header("是否两边推力 被攻击这在身后 方向就是身后")]
    public bool IsLiangbianTuiLi = false;

    int __tuiliFX = 1;

    void GetHit(Collider2D Coll)
    {
        beHitObj = Coll.transform;
        BeHitGameBody = Coll.GetComponent<GameBody>();
        BehitGameObject = Coll.gameObject;
        BeHitRoleDate = Coll.GetComponent<RoleDate>();
        _BeHitRigidbody2D = Coll.GetComponent<Rigidbody2D>();
        //IsPianyiHitPos = false;
        //print("?????????>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>    "+Coll.GetComponent<GameBody>().GetDB().armature.GetBones());
        //ContactPoint2D contact = Coll.GetContacts(Coll);

        //if (Coll.tag == "Player")
        //{
        //    print("   tag " + Coll.tag);
        //    print("持续 ---------------- 1 " + roleDate);
        //    print("   Coll team  " + roleDate.team);
        //    print("    jn_date.team  " + jn_date.team);
        //}

        if (!_playerUI)
        {
            GameObject _ui = GlobalTools.FindObjByName("PlayerUI");
            if (_ui) _playerUI = _ui.GetComponent<PlayerUI>();
        }



        if (this.txObj == null) this.txObj = this.transform.parent.gameObject;
        //print("parent  "+this.transform.parent);
        //print("parent name  " + this.transform.parent.name);
        //print("parent atkObj!!!  " + this.transform.parent.GetComponent<JN_base>().atkObj);
        //if(roleDate) print("-------------------------------------------------------------this.txObj    " + this.txObj+"       "+this.transform.name+ "   this.teamNum  " + this.teamNum+ "  roleDate.team "+ roleDate.team);
        atkObj = txObj.GetComponent<JN_base>().atkObj;
        if (atkObj) {
            atkObjRoleDate = atkObj.GetComponent<RoleDate>();
            //if (atkObj.GetComponent<RoleDate>().isDie) return;
            //if (atkObj == null) return;
            _atkRigidbody2D = atkObj.GetComponent<Rigidbody2D>();
        }
        
        //print(atkObj.name);
        txPos = -1.2f;

        jn_date = txObj.GetComponent<JN_Date>();



        //推力方向 默认是 左推
        __tuiliFX = -1;
        if (atkObj && beHitObj)
        {
            __tuiliFX = atkObj.transform.position.x > beHitObj.transform.position.x ? -1 : 1;
        }



        if (BeHitRoleDate != null && BeHitRoleDate.team != jn_date.team)
        {
            //print(" >>>>>>  chixu!!!! ");
            //print("击中的2Dbox  "+Coll.GetComponent<BoxCollider2D>().transform.position);
            //if(atkObj.tag == "Player")print("beHit name   "+ beHitObj.name+ "击中************************************BeHitRoleDate isCanBeHit  " + BeHitRoleDate.isCanBeHit);

            if (BeHitRoleDate.isDie) return;
            if(!BeHitRoleDate.isCanBeHit) return;

            print("555555HitPos-----------------zhuanji jinlaimei ???>>------?   " + HitPos);


            //if (!IsHasChixuHit && !BeHitRoleDate.isCanBeHit) return;
            //IsHasChixuHit = true;
            //print(Coll.);
            //ContactPoint contact = Coll.contacts[0];
            //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            //Vector3 pos = contact.point;

            //print("持续 ---------------- 2 ");

            //print(this.GetComponent<BoxCollider2D>().bounds);
            //print(this.GetComponent<BoxCollider2D>().transform.position + "   ???size  " + this.GetComponent<BoxCollider2D>().size);
            //print(gameBody.GetComponent<CapsuleCollider2D>().bounds);
            //print(gameBody.GetComponent<CapsuleCollider2D>().transform.position + "  BEIGONGJI ???size  " + gameBody.GetComponent<CapsuleCollider2D>().size);

            if (!BeHitGameBody)
            {
                HitPos = GetHitPos(false);
            }
            else
            {
                if (BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
                {

                    HitPos = GetHitPos();
                    print("555555HitPos-----------------------?   "+ HitPos);
                }
                else
                {
                    //print("我靠！！！！！！！！！！！！！！！！！！");
                    HitPos = BeHitGameBody.transform.position;
                }
            }

           



            //取到施展攻击角色的方向
            //float _roleScaleX = this.transform.localScale.x > 0?-1:1 ;  //-atkObj.transform.localScale.x;
            float _roleScaleX = -_atkObjScaleX;//atkObj.transform.localScale.x;
            if (_roleScaleX == 0&&atkObj!=null) _roleScaleX = -atkObj.transform.localScale.x;

            //print(this.name+"  技能名字 "+jn_date.name+"  施展攻击方 名字      "+atkObj.name+ "      _roleScaleX     ----    " + _roleScaleX+ "    -atkObj.transform.localScale.x    "+ -atkObj.transform.localScale.x+"  team "+jn_date.team+"     ");
            /**
            if (jn_date._type != "1")
            {
                _roleScaleX = this.transform.localScale.x > 0 ? -1 : 1;
            }*/

            //被击中 变色
            if (atkObj &&atkObj.tag == "Player") {
                //print("变色 ！！！！  -------------------------》  BeHitColorChange  ");
                if(BeHitGameBody) BeHitGameBody.BeHitColorChange();
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_RUN_AC, null), this);
                //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CAMERA_SHOCK, this.GetShock);
                //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.1-2"), this);
            }
            else
            {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_RUN_AC, null), this);
            } 




            //print(_roleScaleX+"   ??   "+this.transform.localScale);

            //这个已经不需要了 
            //if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().GetBeHit(jn_date, _roleScaleX);

            //力作用  这个可以防止 推力重叠 导致人物飞出去
            Vector2 tempV3 = _BeHitRigidbody2D.velocity;
            if(beHitObj.GetComponent<JijiaGamebody>()==null) _BeHitRigidbody2D.velocity = new Vector3(0, tempV3.y);
            //_BeHitRigidbody2D.velocity = new Vector3(0, 0);
            //if (jn_date != null && BeHitGameBody != null && jn_date.HitInSpecialEffectsType != 5)
            //之前是 攻击类型 不能是5 不知道为什么 现在去掉 因为 花防 会被卡住 不能恢复动作  ---应该进入 了 受伤 动作 又进入了 花防 所以导致的 卡住  
            if (atkObj&&jn_date != null && BeHitGameBody != null)
            {
                ObjV3Zero(Coll.gameObject);
                //gameBody.GetPause(0.2f);



                if (BeHitGameBody.IsGedanging&&!BeHitGameBody.IsGeDangAC&&
                    jn_date.HitInSpecialEffectsType != 5&& jn_date.HitInSpecialEffectsType != 11 && jn_date.HitInSpecialEffectsType != 12
                    && (
                    (atkObj.transform.position.x>beHitObj.transform.position.x&& beHitObj.transform.localScale.x<0)||
                    (atkObj.transform.position.x <= beHitObj.transform.position.x && beHitObj.transform.localScale.x > 0)
                    ))
                {
                    BeHitGameBody.GetGedang();
                    BeiDongTankai("gedang");
                    return;
                }


                //print("持续 ---------------- 3 ");
                //List<string> passive_def_skill = gameBody.GetComponent<RoleDate>().passive_def_skill;
                //---------------------------------------被击中 启动被动防御技能
                skill_list = _playerUI.GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().GetHZList();  //GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().HZList;
                if (BeHitGameBody.tag == "Player" && skill_list.Count != 0)
                {
                    if (IsGetHuafang()) return;



                    ShanghaiJianmianHD();

                }


                //身体碰撞类型 检查 是否有碰撞保护
                if (jn_date.HitInSpecialEffectsType == 5)
                {
                    if (!beHitObj.GetComponent<RoleDate>().isBodyCanBeHit) return;
                    beHitObj.GetComponent<GameBody>().StartBodyHitProtect();

                }

                if (jn_date.HitInSpecialEffectsType == 6)
                {
                    beHitObj.GetComponent<GameBody>().StartBodyHitProtect();
                }

                //print("硬直  "+ atkObj.GetComponent<RoleDate>().yingzhi+"   敌人硬直   "+ roleDate.yingzhi+"   方向  "+atkObj.transform.localScale.x);
                //判断是否破防   D 代办事项 
                float beHitXFScale = BeHitRoleDate.beHitXFScale;
                //if (jn_date.atkPower - roleDate.yingzhi > roleDate.yingzhi * 0.5)
                float atkObjYZ = 200;
                if (atkObj != null)
                {
                    if (atkObj.GetComponent<RoleDate>() != null)
                    {
                        atkObjYZ = atkObj.GetComponent<RoleDate>().yingzhi;
                    }
                }

                if (jn_date.JNYingzhi != 0) atkObjYZ = jn_date.JNYingzhi;

                //print(atkObj.name+"-----------------------------------------------------------------------> atkObjYZ  "+ atkObjYZ);
                //int _fx = atkObj.transform.position.x > Coll.transform.position.x ? 1 : -1;

                if (Mathf.Abs(atkObjYZ - BeHitRoleDate.yingzhi) <= 100)
                {
                    //BeHitGameBody.HasBeHit();
                    BeHitAndChongji(0);
                    //atkObjV3Zero(Coll.gameObject);
                    //if (_atkRigidbody2D) _atkRigidbody2D.AddForce(new Vector2(jn_date.moveXSpeed * _roleScaleX, jn_date.moveYSpeed));
                    if (_atkRigidbody2D)
                    {
                        _atkRigidbody2D.velocity = Vector2.zero;
                        //_atkRigidbody2D.AddForce(new Vector2(jn_date.moveXSpeed * -_roleScaleX, jn_date.moveYSpeed));
                        //atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.moveXSpeed * -_roleScaleX, jn_date.moveYSpeed),true);

                        if (atkObj.tag == "Player" && !atkObj.GetComponent<GameBody>().isInAiring&& jn_date.atkDirection == "")
                        {
                            _atkRigidbody2D.AddForce(new Vector2(-400 * _roleScaleX, 0));
                        }
                        else
                        {
                            _atkRigidbody2D.AddForce(new Vector2(jn_date.moveXSpeed * _roleScaleX, jn_date.moveYSpeed));
                        }

                    }

                    _BeHitRigidbody2D.velocity = Vector2.zero;
                    if (jn_date.fasntuili != 0) beHitXFScale = 1;// 有反推力说明是空中向下攻击
                    //print("----------------------------------------------------------------> 冲击力  "+jn_date.chongjili+ "  _roleScaleX   "+ _roleScaleX);
                    if (Coll.tag == "AirEnemy")
                    {
                        //print("tl----------------------------------------》》》   空中怪被攻击 冲击力！ ");
                        //_BeHitRigidbody2D.AddForce(GlobalTools.GetVector2ByPostion(Coll.transform.position, atkObj.transform.position, jn_date.chongjili));
                        beHitObj.GetComponent<GameBody>().GetZongTuili(GlobalTools.GetVector2ByPostion(Coll.transform.position, atkObj.transform.position, jn_date.chongjili),true);
                    }
                    else
                    {
                        //print("   >>>>>>>>>>>地面怪被攻击 冲击力！ "+jn_date.chongjili+"     ");
                        //_BeHitRigidbody2D.AddForce(new Vector2(jn_date.chongjili * -_fx * beHitXFScale, 0));
                        beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * __tuiliFX * beHitXFScale, 0));
                    }

                    if (jn_date.FanTuili != 0)
                    {
                        if(atkObj) atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(-jn_date.FanTuili * __tuiliFX * beHitXFScale, 0));
                    }


                    txPos = 0.8f;
                    //print("sudu-------------------------------------------<>>>>>>>>>> 11111   " + Coll.GetComponent<Rigidbody2D>().velocity.x+"   || "+Coll.name+"   txPos   "+txPos);
                    //if(Coll.tag!="Player") print(Coll.GetComponent<Rigidbody2D>().velocity.x);
                    //print(Coll.tag);


                    if(atkObj && atkObj.tag == "Player"&&jn_date._type =="1"&& !atkObj.GetComponent<GameBody>().isInAiring) {
                        atkObj.GetComponent<GameBody>().IsAtkKuaijiReSet();
                    }


                }
                else if (Mathf.Abs(atkObjYZ - BeHitRoleDate.yingzhi) <= 200)
                {
                    //print("????????????????????????????   这里 硬直 控制 肯定有问题啊  ");
                    //atkObjV3Zero(Coll.gameObject);
                    //print("  >>>>>>>>_roleScaleX   " + _roleScaleX+"  jndate type  "+ jn_date._type+"  collname  "+Coll.name);
                    //这里要判断 谁的硬直大
                    if (!IsLiangbianTuiLi)
                    {
                        if (atkObjYZ > BeHitRoleDate.yingzhi)
                        {
                            //_BeHitRigidbody2D.AddForce(new Vector2(jn_date.chongjili * -atkObj.transform.localScale.x - 100, 0));
                            beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * __tuiliFX - 100, 0));
                            //BeHitGameBody.HasBeHit(jn_date.chongjili);
                            BeHitAndChongji(jn_date.chongjili);
                        }
                        else
                        {
                            //atkObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(jn_date.chongjili * -Coll.transform.localScale.x - 100, 0));
                            if(atkObj&& atkObj.GetComponent<GameBody>()) atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * -__tuiliFX - 100, 0));
                        }
                    }
                    else
                    {

                        //_BeHitRigidbody2D.AddForce(new Vector2(jn_date.chongjili * -_fx - 100, 0));
                        beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * __tuiliFX - 100, 0));
                        //BeHitGameBody.HasBeHit(jn_date.chongjili);
                        BeHitAndChongji(jn_date.chongjili);
                    }

                    if (jn_date.FanTuili != 0)
                    {
                        print("----------------------------------------------------------------> 作用反推力 "+ jn_date.FanTuili);
                        if (atkObj&& atkObj.GetComponent<GameBody>()) atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(-jn_date.FanTuili * __tuiliFX * beHitXFScale, 0));
                    }


                    
                    if (atkObj && (jn_date._type == "1" || jn_date._type == "4"))
                    {
                        ObjV3Zero(atkObj);
                        //print("  /////////////////////////////////>>>>>>>>_roleScaleX   " + _roleScaleX);
                        if (jn_date.atkDirection == "" && _atkRigidbody2D) _atkRigidbody2D.AddForce(new Vector2(200 * -__tuiliFX, 0));
                    }
                    txPos = 0.4f;
                    //print("----------------------------222222222------------------------------------> 冲击力  " + jn_date.chongjili + "  _roleScaleX   " + _roleScaleX);

                    if (atkObj.tag == "Player" && jn_date._type == "1" && !atkObj.GetComponent<GameBody>().isInAiring)
                    {
                        atkObj.GetComponent<GameBody>().IsAtkKuaijiReSet();
                    }
                }
                else
                {
                    //print("  --------硬直差   "+Mathf.Abs(atkObjYZ - BeHitRoleDate.yingzhi)+"  jn_date._type        " + jn_date._type+ "   技能name  " + jn_date.name+ "    atkObjYZ "+ atkObjYZ+ "  BeHitRoleDate.yingzhi  "+ BeHitRoleDate.yingzhi);
                    if (atkObj && (jn_date._type == "1" || jn_date._type == "4"))
                    {

                        if (atkObjYZ> BeHitRoleDate.yingzhi)
                        {
                            BeHitGameBody.SetXSpeedZero();
                            //print(" 冲压！！！！！！！！！！！！！！！！22222222222222222    "+ jn_date.atkDirection+"     name>  "+jn_date.name);
                            if (jn_date.atkDirection == "ya")
                            {
                                //print("冲压！！！！！！！！！！！！！！！！！！！！！！！！！！！  ya！");
                                if (BeHitGameBody) {
                                    
                                    //_BeHitRigidbody2D.AddForce(new Vector2(-_fx  * jn_date.chongjili , 0));
                                    beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(__tuiliFX * jn_date.chongjili, 0));
                                }
                                
                            }
                            else
                            {
                                if (BeHitGameBody)
                                {
                                    print(" chongjili ------------------------> jinlaimei  "+ jn_date.chongjili);
                                    if (_BeHitRigidbody2D) {
                                        //print("/////////>>>>>>>>>>>>>@!  ?????????   ");
                                        //_BeHitRigidbody2D.velocity = new Vector2(20 * -_fx, _BeHitRigidbody2D.velocity.y);
                                        //_BeHitRigidbody2D.AddForce(new Vector2(jn_date.chongjili * -_fx * beHitXFScale, 0));
                                        beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * __tuiliFX * beHitXFScale, 0));
                                    }
                                    
                                }
                            }
                            //BeHitGameBody.HasBeHit(jn_date.chongjili);
                            BeHitAndChongji(jn_date.chongjili);
                            //BeHitGameBody.GetPause(0.2f, 0f);
                        }
                        else
                        {
                            //print(" gongji bei tankai!!  ");
                            if (atkObj && atkObj.GetComponent<GameBody>())
                            {
                                atkObj.GetComponent<GameBody>().SetXSpeedZero();
                                /* if (!atkObj.GetComponent<GameBody>().isAtkFanTui)
                                 {
                                     atkObj.GetComponent<GameBody>().isAtkFanTui = true;
                                 }*/
                                if (jn_date.atkDirection == "" && _atkRigidbody2D) {
                                    float fantuili = -15 * -atkObj.transform.localScale.x;
                                    //print("fantuili:     "+fantuili);
                                    _atkRigidbody2D.velocity = new Vector2(fantuili, _atkRigidbody2D.velocity.y);
                                }
                                
                                // _atkRigidbody2D.AddForce(new Vector2(-600 * _roleScaleX, 0));
                                //GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes(0.1f);
                            }
                        }


                        //if (jn_date.FanTuili != 0)
                        //{
                        //    print("222----------------------------------------------------------------> 作用反推力 " + jn_date.FanTuili);
                        //    if (atkObj) atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(-jn_date.FanTuili * __tuiliFX * beHitXFScale, 0));
                        //}

                        //print("  333333333>>>>>>>>_roleScaleX   " + _roleScaleX+"    atkObjScaleX  "+ atkObj.transform.localScale.x  + "  jndate type  " + jn_date._type + "  collname  " + Coll.name);
                        //被攻击怪硬值过大 被反弹
                        //ObjV3Zero(atkObj);


                    }
                    else
                    {
                        if (atkObjYZ > BeHitRoleDate.yingzhi)
                        {
                            //BeHitGameBody.HasBeHit(jn_date.chongjili);
                            BeHitAndChongji(jn_date.chongjili);
                            beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * __tuiliFX * beHitXFScale, 0));
                        }
                       
                    }
                    
                    //if (atkObj.tag == "Player" && jn_date._type == "1" && !atkObj.GetComponent<GameBody>().isInAiring)
                    //{
                    //    atkObj.GetComponent<GameBody>().IsAtkKuaijiReSet();
                    //}

                    //print("---------------------333333333-------------------------------------------> 冲击力  " + jn_date.chongjili + "  _roleScaleX   " + _roleScaleX);
                }

                //print("持续 ---------------- 5 "+atkObj);
                //硬直时间？
                if (atkObj.tag == "Player") {
                    BeHitGameBody.GetPause(jn_date.yingzhishijian, 0.4f);
                }
                else
                {
                    GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes(0.3f,0.4f);
                    if (BeHitGameBody.isInAiring) {
                        if(jn_date.HitInSpecialEffectsType == 8)
                        {
                            if (GlobalTools.GetRandomNum() > 80)
                            {
                                BeHitGameBody.GetPlayerRigidbody2D().velocity = new Vector2(BeHitGameBody.GetPlayerRigidbody2D().velocity.x, 0); //Vector2.zero;
                            }
                        }
                        else
                        {
                            BeHitGameBody.GetPlayerRigidbody2D().velocity = new Vector2(BeHitGameBody.GetPlayerRigidbody2D().velocity.x, 0); //Vector2.zero;

                        }
                    }
                }

                if (!_playerUI)
                {
                    _playerUI = GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>();
                }
                else
                {
                    //print("已经 生成！！！！！！ ");
                }
             
                
                //附带效果
                //BeHitGameBody.FudaiXiaoguo(jn_date.JiZhongFDXiaoguo);
                //if (Coll.tag != "Player")

                if (jn_date.JiZhongFDXiaoguo!="")
                {
                    if (beHitObj != null && BeHitRoleDate.team != jn_date.team) beHitObj.GetComponent<ChiXuShangHai>().FudaiXiaoguo(jn_date.JiZhongFDXiaoguo);
                }



                //毒伤害
                if (jn_date.DuChixuShanghai!= 0)
                {
                    print(beHitObj.name + "  --beihitteam  " + BeHitRoleDate.team + "  jnteam  " + jn_date.team+"   atkObjname "+atkObj.name);
                    if (beHitObj != null && BeHitRoleDate.team != jn_date.team) beHitObj.GetComponent<ChiXuShangHai>().InDu(jn_date.DuChixuShanghai, jn_date.DuChixuShanghaiTime);
                }

                if (jn_date.HuoChixuShanghai != 0)
                {
                    //print("beHitObj---    " + beHitObj.name);
                    if (beHitObj != null && BeHitRoleDate.team != jn_date.team) beHitObj.GetComponent<ChiXuShangHai>().InHuo(jn_date.HuoChixuShanghai, jn_date.HuoChixuShanghaiTime);
                    //if (beHitObj && beHitObj.GetComponent<JijiaGamebody>()) return;
                }

                if (jn_date.DianShanghai != 0)
                {
                    print(beHitObj.name + "  --beihitteam  " + BeHitRoleDate.team + "  jnteam  " + jn_date.team + "   atkObjname " + atkObj.name);
                    if (beHitObj != null && BeHitRoleDate.team != jn_date.team) beHitObj.GetComponent<ChiXuShangHai>().InDian(jn_date.DianShanghai, jn_date.DianShanghaiMabiTime);
                }
            }
            else
            {
                //没有 atkObj的攻击

                //print("没有 Obj 》》》》》》》》》》》》    "+jn_date.DianShanghai);
                if (beHitObj.GetComponent<ChiXuShangHai>())
                {
                    if (jn_date.HuoChixuShanghai != 0)
                    {
                        if (beHitObj != null && BeHitRoleDate.team != jn_date.team) beHitObj.GetComponent<ChiXuShangHai>().InHuo(jn_date.HuoChixuShanghai, jn_date.HuoChixuShanghaiTime);
                    }

                    if (jn_date.DuChixuShanghai != 0)
                    {
                        print(beHitObj.name + "  --beihitteam  " + BeHitRoleDate.team + "  jnteam  " + jn_date.team + "   atkObjname " + atkObj.name);
                        if (beHitObj != null && BeHitRoleDate.team != jn_date.team) beHitObj.GetComponent<ChiXuShangHai>().InDu(jn_date.DuChixuShanghai, jn_date.DuChixuShanghaiTime);
                    }


                    if (jn_date.DianShanghai != 0)
                    {
                        //print(beHitObj.name + "  --beihitteam  " + BeHitRoleDate.team + "  jnteam  " + jn_date.team + "   atkObjname " + atkObj.name);
                        if (beHitObj != null && BeHitRoleDate.team != jn_date.team) beHitObj.GetComponent<ChiXuShangHai>().InDian(jn_date.DianShanghai, jn_date.DianShanghaiMabiTime);
                    }
                }

            

            }

            //print("sudu-------------------------------------------11111   " + Coll.GetComponent<Rigidbody2D>().velocity.x);
            //print("持续 ---------------- 6 ");
            //记录空中向下攻击的反推力
            if (jn_date.fasntuili != 0 && atkObj.GetComponent<Rigidbody2D>())
            {
                //print("#########??????");
                atkObj.GetComponent<GameBody>().SetYSpeedZero();
                //打到鱼 给反作用力16  不然很多怪一只在天上打 就能打死
                if (BeHitRoleDate.IsAirEnemy)
                {
                    
                    _atkRigidbody2D.velocity = new Vector2(_atkRigidbody2D.velocity.x, 18);
                    //print("  ##############################################--------->  " + _atkRigidbody2D.velocity);
                }
                else
                {
                    _atkRigidbody2D.velocity = new Vector2(_atkRigidbody2D.velocity.x, 12);
                    //print("  ##############################################@@@@@@@@@@@@@@@@@@--------->  " + _atkRigidbody2D.velocity);
                }
                atkObj.GetComponent<GameBody>().ResetShanJin();
            }

            if (atkObj&&atkObj.GetComponent<GameBody>()) atkObj.GetComponent<GameBody>().GetPause();

            //print("持续 ---------------- 7 ");
            //判断作用力与反作用力  硬直判断

            //
            //Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));

            //如果是碰撞就消失 调用消失方法
            if (jn_date != null && jn_date._type == "3")
            {
                //if (gameObject) ObjectPools.GetInstance().DestoryObject2(gameObject);     
                txObj.GetComponent<JN_base>().DisObj();
            }


            //GetBeHit(jn_date, _roleScaleX);
            if (atkObj) {
                //print("  yougongji  duixiang "+atkObj);
                GetBeHit(jn_date, -atkObj.transform.localScale.x);
            }
            else
            {
                //print("    没有攻击 对象 "+this.transform.position.x+"    behitObjX  "+beHitObj.transform.position.x);
                int fxX = 1;
                if (this.transform.position.x > beHitObj.transform.position.x)
                {
                    fxX = 1;
                }
                else
                {
                    fxX = -1;
                }
                GetBeHit(jn_date, fxX);
            }
            

        }
    }


    void BeHitAndChongji(float chongjili)
    {
        if (jn_date.HitInSpecialEffectsType == 8)
        {
            if (GlobalTools.GetRandomNum() > 80)
            {
                BeHitGameBody.HasBeHit(chongjili);
            }
            return;
        }
        BeHitGameBody.HasBeHit(chongjili);
    }



    PlayerUI _playerUI;


    void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll) GetHit(Coll);
    }

    //X方向速度清0
    void ObjV3Zero(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody2D>() == null) return;
        Vector3 v3 = obj.GetComponent<Rigidbody2D>().velocity;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, v3.y);
    }

    bool IsBaoji = false;

    public void GetBeHit(JN_Date jn_date, float sx)
    {
        print(" 55555555555 被击中 特效 类型   --------    "+ jn_date.HitInSpecialEffectsType);
        //print("被击中 !! "+this.transform.name+"   攻击力 "+ jn_date.atkPower+"  被攻击对象防御力 "+ BeHitRoleDate.def);
        if (BeHitRoleDate.isDie||BeHitRoleDate.team == jn_date.team) return;
        //print(" isCanBeHit>2:  "+ BeHitRoleDate.isCanBeHit);
        if (!BeHitRoleDate.isCanBeHit) return;
        float addxue = jn_date.atkPower - BeHitRoleDate.def;
        if (atkObj && atkObj.GetComponent<RoleDate>()&& jn_date.HitInSpecialEffectsType != 5)
        {
            float _atkPower = atkObj.GetComponent<RoleDate>().atk + jn_date.atkPower;
            //print("  攻击力： "+_atkPower+"  被攻击者防御力  "+ BeHitRoleDate.def);
            if (atkObjRoleDate && atkObjRoleDate.BaoJiLv != 0 && GlobalTools.GetRandomNum() <= atkObjRoleDate.BaoJiLv) {
                _atkPower *= atkObjRoleDate.BaoJiShangHaiBeiLv;
                IsBaoji = true;
            } 
            addxue = _atkPower - BeHitRoleDate.def;
        }
        
        addxue = addxue > 0 ? addxue : 1;
        //if (jn_date.HitInSpecialEffectsType == 5) addxue = 50;
        //计算伤害减免比率
        if (BeHitRoleDate.shanghaijianmianLv != 0) addxue *= (100 - BeHitRoleDate.shanghaijianmianLv)*0.01f;
        if (BeHitRoleDate.live - addxue<0)
        {
            if (!GetShenyou())
            {
                BeHitRoleDate.live -= addxue;
            }
        }
        else
        {
            BeHitRoleDate.live -= addxue;
           
        }

        if (BeHitRoleDate.live < 0)
        {
            BeHitRoleDate.live = 0;
        }


        if (BeHitGameBody&& BeHitGameBody.tag != "Player"&& BeHitRoleDate.live != 0)
        {
            //print("*************************************************************BeHitGameBody.tag   "+ BeHitGameBody.tag);
            _playerUI.GetSlowByTimes(0.12f, 0.4f);
        }

        //击退 判断方向
        float _psScaleX = sx;

        //无特效 伤害 毒 火等 避免 光圈显示位置 错误
        if (jn_date.HitInSpecialEffectsType == 21)
        {
            return;
        }

        if (BeHitGameBody&&BeHitGameBody.GetComponent<GameBody>().IsJianmianDun)
        {
            GameObject TX_jianmian = GlobalTools.GetGameObjectInObjPoolByName("BeHit_Jianmian");
            HitTXPos(TX_jianmian);
        }

        //带火 属性的 刀光  防止 光圈导出飞
        if (jn_date.HitInSpecialEffectsType == 11)
        {
            return;
        }

        string tx_1 = "";
        if (BeHitGameBody) tx_1 = BeHitGameBody.GetComponent<RoleDate>().BeHitTX_1;
        print("55555555 ----   tx_1 "+ tx_1);
        if (!IsBaoji && tx_1 != "")
        {
            //print("  >>tx_1 " + tx_1);

            GameObject hitTx_1 = Resources.Load(tx_1) as GameObject;
            hitTx_1 = ObjectPools.GetInstance().SwpanObject2(hitTx_1);
            HitTXPos(hitTx_1);
        }


        //击中特效类型 8 和 9 都是 火焰类的
        if (jn_date.HitInSpecialEffectsType == 8 || jn_date.HitInSpecialEffectsType == 9)
        {

            print("555555  碰到 毒 火？？？ "+ jn_date.HitInSpecialEffectsType);

            //print(BeHitGameBody+"  5555555    " + BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos+"     ");


            //if (!BeHitGameBody)
            //{
            //    HitPos = GetHitPos(false);
            //}
            //else
            //{
            //    if (BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
            //    {

            //        HitPos = GetHitPos();
            //        print("HitPos-----------------------?   " + HitPos);
            //    }
            //    else
            //    {
            //        //print("我靠！！！！！！！！！！！！！！！！！！");
            //        HitPos = BeHitGameBody.transform.position;
            //    }
            //}



            return;
        }


       
        if (!IsBaoji)
        {
            GameObject hitTx_2 = Resources.Load("TX_jizhong_2") as GameObject;
            hitTx_2 = ObjectPools.GetInstance().SwpanObject2(hitTx_2);
            HitTXPos(hitTx_2);
        }



        //print(this.name+ " ??atkSkillName  " + jn_date .atkSkillName+ "  "+"live@@@ "+ beHitObj.GetComponent<RoleDate>().live);

        //判断是否在躲避阶段  无法被攻击
        //判断击中特效播放位置


        //修正击中特效X位置
        if (jn_date.HitInSpecialEffectsX != 0) txPos = jn_date.HitInSpecialEffectsX;

        //判断是否在空中
        //挨打动作  判断是否破硬直
        //判断是否生命被打空

        if (jn_date.HitInSpecialEffectsType == 4)
        {
            //HitTX(_psScaleX, "JZTX_dian", "", 2, false, false, -txPos);
            //被电麻痹了  电特效
            //HitTX2("JZTX_dian");
            return;
        }
        //print("  攻击类型 "+ jn_date.HitInSpecialEffectsType);

        if (jn_date.HitInSpecialEffectsType == 5|| jn_date.HitInSpecialEffectsType == 7|| jn_date.HitInSpecialEffectsType == 8)
        {
            print("5555555555555  被撞击  5  7  8 毒 火 等 ");
            //5是身体碰撞 只显示光圈   7的话 是 毒 持续等 碰撞
            GameObject hitTx_1 = Resources.Load("TX_hitGuangquan") as GameObject;
            hitTx_1 = ObjectPools.GetInstance().SwpanObject2(hitTx_1);
            //GameObject hitTx_1 = GlobalTools.GetGameObjectByName("TX_hitGuangquan");
            HitTXPos(hitTx_1);
            //被撞击者 退后
            float _fanTuili = 1000;
            _fanTuili = jn_date.FanTuili;

            if (atkObj)
            {
                _fanTuili = atkObj.transform.position.x > beHitObj.transform.position.x ? _fanTuili : -_fanTuili;
            }

            
            if (atkObj&&atkObj.GetComponent<RoleDate>())
            {
                if (Mathf.Abs(atkObj.GetComponent<RoleDate>().yingzhi - beHitObj.GetComponent<RoleDate>().yingzhi) <= 100)
                {
                    atkObj.GetComponent<GameBody>().HasBeHit(_fanTuili, false);
                    atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(_fanTuili, 0));
                }
                else if (atkObj.GetComponent<RoleDate>().yingzhi - beHitObj.GetComponent<RoleDate>().yingzhi > 100)
                {
                    beHitObj.GetComponent<GameBody>().HasBeHit(-_fanTuili, false);
                    beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(-_fanTuili, 0), true);
                }
            }

            print("  方向是什么 多少 正反 "+_psScaleX);
            if(jn_date.HitInSpecialEffectsType == 7) HitTX(_psScaleX, "BloodSplatCritical3", "", 2, false, false, -txPos);

            return;
        }


        if (jn_date.HitInSpecialEffectsType == 6)
        {
            print("66666666666  型的攻击被撞击  刺鱼  刺猬等");
            //是身体碰撞 显示光圈 和 攻击特效
            GameObject hitTx_1 = Resources.Load("TX_hitGuangquan") as GameObject;
            hitTx_1 = ObjectPools.GetInstance().SwpanObject2(hitTx_1);
            HitTXPos(hitTx_1);
            //被撞击者 退后
            float _fanTuili = atkObj.GetComponent<JN_Date>().FanTuili; // 1000;
            _fanTuili = atkObj.transform.position.x > beHitObj.transform.position.x ? _fanTuili : -_fanTuili;
            if (Mathf.Abs(atkObj.GetComponent<RoleDate>().yingzhi - beHitObj.GetComponent<RoleDate>().yingzhi) <= 100)
            {

                atkObj.GetComponent<GameBody>().HasBeHit(_fanTuili, false);
                atkObj.GetComponent<GameBody>().BeHitSlowByTimes(1,0);
                atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(_fanTuili, 0),true);
            }
        }

       

       
        HitTX(_psScaleX, "BloodSplatCritical3", "",2,false,false,-txPos);
        if (jn_date.HitInSpecialEffectsType != 3) HitTX(_psScaleX, "jizhong", BeHitRoleDate.beHitVudio, 3, true, true);


        if (IsBaoji)
        {
            IsBaoji = false;
            GameObject hitTx_1 = Resources.Load("TX_hitGuangquanBJ") as GameObject;
            hitTx_1 = ObjectPools.GetInstance().SwpanObject2(hitTx_1);
            HitTXPos(hitTx_1);
            //if (jn_date.HitInSpecialEffectsType != 3) HitTX(_psScaleX, "jizhongBJ", BeHitRoleDate.beHitVudio, 3, true, true);
        }
    }



    void HitTX2(string txName)
    {
        GameObject hitTx = Resources.Load(txName) as GameObject;
        hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);
        hitTx.transform.position = BeHitGameBody.transform.position;
        hitTx.transform.parent = BeHitGameBody.transform;
    }


    protected Vector2 PianyiHitPos = Vector2.zero;
    //bool IsPianyiHitPos = false;
    float PianyiDis = 2;

    void HitTXPos(GameObject hitTx, float hy = 0)
    {
        //print("  hitTXname  "+hitTx.name);
        //这个 偏移 是给 乱刃的 
        if (jn_date &&jn_date.IsHitTXPianyi)
        {
            //IsPianyiHitPos = false;
            PianyiDis = GlobalTools.GetRandomDistanceNums(PianyiDis);
            float pyX = GlobalTools.GetRandomNum() > 50 ? -PianyiDis : PianyiDis;
            PianyiDis = GlobalTools.GetRandomDistanceNums(PianyiDis);
            float pyY = GlobalTools.GetRandomNum() > 50 ? -PianyiDis : PianyiDis;

            if (!BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
            {
                HitPos = new Vector3(BeHitGameBody.transform.position.x - hy * -_atkObjScaleX, BeHitGameBody.transform.position.y, BeHitGameBody.transform.position.z);
            }

            PianyiHitPos = new Vector2(HitPos.x + pyX, HitPos.y + pyY);
            print("5555555 偏移坐标PianyiHitPos-----  " + PianyiHitPos);
        }

        //if (BeHitGameBody) {
        //    if (BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
        //    {
        //        hitTx.transform.position = HitPos;
        //    }
        //    else
        //    {
        //        //大块头怪 击中点位置计算
        //        hitTx.transform.position = new Vector3(BeHitGameBody.transform.position.x - hy * -_atkObjScaleX, BeHitGameBody.transform.position.y, BeHitGameBody.transform.position.z);
        //    }
        //}
        //else
        //{
        //    hitTx.transform.position = HitPos;
        //}

        if (jn_date.IsHitTXPianyi)
        {
            //if(hitTx.name == "jizhong")
            //{
            //    print("击中特效 进入偏移！！！！");
            //    print(" PianyiHitPos  "+ PianyiHitPos);
            //    print(" HitPos  " + HitPos);
            //    //PianyiHitPos = new Vector2(18, 6);
            //}
            hitTx.transform.position = PianyiHitPos;
            return;
        }


        if (BeHitGameBody && !BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos){
            //大块头怪 击中点位置计算
            print(" 555555  --------->>>>    被击中 hitpos ");
            hitTx.transform.position = new Vector3(BeHitGameBody.transform.position.x - hy * -_atkObjScaleX, BeHitGameBody.transform.position.y, BeHitGameBody.transform.position.z);
        }
        else
        {
            print("555555 HitPos-- >>>  "+ HitPos);
            hitTx.transform.position = HitPos;
        }
    
    }


    /// <summary>
    /// 特效
    /// </summary>
    /// <param name="psScaleX">角色的方向</param>
    /// <param name="txName">特效名字（动态取预制资源）</param>
    /// <param name="hitVudio">播放特效声音</param>
    /// <param name="isSJJD">是否随机角度</param>
    /// <param name="isZX">是否需要转向</param>
    /// <param name="hy">特效后移修正</param>
    void HitTX(float psScaleX,string txName,string hitVudio = "",float beishu = 3,bool isSJJD = false,bool isZX = true,float hy = 0)
    {
        //print("hy ------------------------------------------------------>     "+hy);

       
        GameObject hitTx = Resources.Load(txName) as GameObject;
        hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);
        hitTx.name = txName;
        HitTXPos(hitTx,hy);

        //print(hitTx.name + "  1---------------------->>   " + hitTx.transform.localEulerAngles);
        //print("sudu-------------------------------------------   "+ gameBody.GetComponent<Rigidbody2D>().velocity.x);
        //if (BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
        //{
        //    hitTx.transform.position = HitPos;
        //}
        //else
        //{
        //    //大块头怪 击中点位置计算
        //    hitTx.transform.position = new Vector3(BeHitGameBody.transform.position.x - hy * -_atkObjScaleX, BeHitGameBody.transform.position.y, BeHitGameBody.transform.position.z);
        //}
        
        //击中特效缩放
        hitTx.transform.localScale = new Vector3(beishu, beishu, beishu);

        //print(hitTx.transform.localEulerAngles + " 000000000000000000000000000000000 "+hitTx.transform.name);

        if (!isZX) {
            //if(atkObj.transform.position.y-)


            //纵向攻击  打天上 或者向下攻击
            if (jn_date.isAtkZongXiang)
            {
                hitTx.transform.localEulerAngles = new Vector3(-90, hitTx.transform.localEulerAngles.y * -_atkObjScaleX, hitTx.transform.localEulerAngles.z);
                //print("  sx  " + _atkObjScaleX);

                //if (BeHitGameBody)
                //{
                //    if (BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
                //    {
                //        //print(1);
                //        hitTx.transform.position = HitPos;
                //    }
                //    else
                //    {
                //        //print(2);
                //        //hitTx.transform.position = new Vector3(gameBody.transform.position.x - hy * -_atkObjScaleX, gameBody.transform.position.y, gameBody.transform.position.z);
                //        hitTx.transform.position = new Vector3(BeHitGameBody.transform.position.x - hy * -_atkObjScaleX, BeHitGameBody.transform.position.y, BeHitGameBody.transform.position.z);
                //    }
                //}
                //else
                //{
                //    hitTx.transform.position = HitPos;
                //}

                print("   hitpoint!!!!!!!!!! ");
                HitTXPos(hitTx,hy);


            }
            else
            {
                if (atkObj)
                {
                    hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, hitTx.transform.localEulerAngles.y * -_atkObjScaleX, hitTx.transform.localEulerAngles.z);
                }
                else
                {
                    hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, hitTx.transform.localEulerAngles.y * -psScaleX, hitTx.transform.localEulerAngles.z);
                }
                
            }
            
        }
        
        //if (!isZX) hitTx.transform.localEulerAngles = new Vector3(-90, hitTx.transform.localEulerAngles.y * psScaleX, hitTx.transform.localEulerAngles.z);
        //print(hitTx.name + "  2---------------------->>   " + hitTx.transform.localEulerAngles);

        float jd = 0;
       
        if (hitVudio != "")
        {
            hitTx.GetComponent<JZ_audio>().PlayAudio(hitVudio);
        }
        //特效方向 
        if (!isZX) return;
        //print("psScaleX  -----    "+ psScaleX+" _atkScaleX   "+ -_atkObjScaleX);
        if (-_atkObjScaleX > 0)
        {
            if(isSJJD)jd = Random.Range(-5, 10);

            //print("hitTx.transform.localEulerAngles    "+ hitTx.transform.localEulerAngles);
            hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, hitTx.transform.localEulerAngles.y, jd);
            //print(">>>>>>>>>>>>>>>>>>>>>>>>>左    "+ -_atkObjScaleX+"   ??  "+ hitTx.transform.localEulerAngles+"  jd  "+jd);
        }
        else
        {
            if (isSJJD) jd = Random.Range(-5, 10);
            hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, 180, jd);
            //print(">>>>>>>>>>>>>>>>>>>>右     "+ -_atkObjScaleX);
        }

    }

  

    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
    }
    
}
