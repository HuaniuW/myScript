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

    RoleDate BeHitRoleDate;
    GameBody BeHitGameBody;
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
    Vector2 GetHitPos()
    {
        Vector2 p1 = this.GetComponent<BoxCollider2D>().bounds.center;
        Vector2 s1 = this.GetComponent<BoxCollider2D>().bounds.extents;
        Vector2 p2 = BeHitGameBody.GetComponent<CapsuleCollider2D>().bounds.center;
        Vector2 s2 = BeHitGameBody.GetComponent<CapsuleCollider2D>().bounds.extents;

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


    [Header("是否两边推力 被攻击这在身后 方向就是身后")]
    public bool IsLiangbianTuiLi = false;

    void GetHit(Collider2D Coll)
    {
        beHitObj = Coll.transform;
        BeHitGameBody = Coll.GetComponent<GameBody>();
        BeHitRoleDate = Coll.GetComponent<RoleDate>();
        _BeHitRigidbody2D = Coll.GetComponent<Rigidbody2D>();

        //print("?????????>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>    "+Coll.GetComponent<GameBody>().GetDB().armature.GetBones());
        //ContactPoint2D contact = Coll.GetContacts(Coll);

        //if (Coll.tag == "Player")
        //{
        //    print("   tag " + Coll.tag);
        //    print("持续 ---------------- 1 " + roleDate);
        //    print("   Coll team  " + roleDate.team);
        //    print("    jn_date.team  " + jn_date.team);
        //}

      



        if (this.txObj == null) this.txObj = this.transform.parent.gameObject;
        //if(roleDate) print("-------------------------------------------------------------this.txObj    " + this.txObj+"       "+this.transform.name+ "   this.teamNum  " + this.teamNum+ "  roleDate.team "+ roleDate.team);
        atkObj = txObj.GetComponent<JN_base>().atkObj;
        //if (atkObj.GetComponent<RoleDate>().isDie) return;
        //if (atkObj == null) return;
        _atkRigidbody2D = atkObj.GetComponent<Rigidbody2D>();
        //print(atkObj.name);
        txPos = -1.2f;

        jn_date = txObj.GetComponent<JN_Date>();



        //推力方向 默认是 左推
        int __tuiliFX = -1;
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

            if (BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
            {

                HitPos = GetHitPos();
                //print("HitPos-----------------------?   "+ HitPos);
            }
            else
            {
                //print("我靠！！！！！！！！！！！！！！！！！！");
                HitPos = BeHitGameBody.transform.position;
            }



            //取到施展攻击角色的方向
            //float _roleScaleX = this.transform.localScale.x > 0?-1:1 ;  //-atkObj.transform.localScale.x;
            float _roleScaleX = -_atkObjScaleX;//atkObj.transform.localScale.x;
            if (_roleScaleX == 0) _roleScaleX = -atkObj.transform.localScale.x;

            //print(this.name+"  技能名字 "+jn_date.name+"  施展攻击方 名字      "+atkObj.name+ "      _roleScaleX     ----    " + _roleScaleX+ "    -atkObj.transform.localScale.x    "+ -atkObj.transform.localScale.x+"  team "+jn_date.team+"     ");
            /**
            if (jn_date._type != "1")
            {
                _roleScaleX = this.transform.localScale.x > 0 ? -1 : 1;
            }*/

            //被击中 变色
            if (atkObj.tag == "Player") {
                //print("变色 ！！！！  -------------------------》  BeHitColorChange  ");
                BeHitGameBody.BeHitColorChange();
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
            _BeHitRigidbody2D.velocity = new Vector3(0, tempV3.y);
            if (jn_date != null && BeHitGameBody != null)
            {
                ObjV3Zero(Coll.gameObject);
                //gameBody.GetPause(0.2f);

                //print("持续 ---------------- 3 ");
                //List<string> passive_def_skill = gameBody.GetComponent<RoleDate>().passive_def_skill;
                //---------------------------------------被击中 启动被动防御技能
                List<GameObject> skill_list = GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().HZList;
                if (BeHitGameBody.tag == "Player" && skill_list.Count != 0)
                {
                    foreach (GameObject defSkill in skill_list)
                    {
                        //获取技能信息
                        //GameObject obj = Resources.Load(defSkill) as GameObject;

                        if (defSkill.GetComponent<UI_Skill>().GetHZDate().type == "zd") continue;
                        //是否有蓝
                        if (BeHitGameBody.GetComponent<RoleDate>().lan < defSkill.GetComponent<UI_Skill>().GetHZDate().xyLan) continue;
                        //是否冷却  还是只能找 玩家装备的技能
                        if (!defSkill.GetComponent<UI_Skill>().IsCDSkillCanBeUse()) continue;

                        //防御几率
                        float jv = defSkill.GetComponent<UI_Skill>().GetHZDate().Chance_of_Passive_Skills;
                        //获取触发几率 
                        float cfjv = GlobalTools.GetRandomDistanceNums(100);
                        if (cfjv > jv) continue;
                        //触发
                        //加血
                        //无伤害
                        //判断 蓝够不够

                        if (defSkill.GetComponent<UI_Skill>().GetHZDate().def_effect == "wushanghai")
                        {
                            //defSkill.GetComponent<UI_Skill>().isCanBeUseSkill();
                            //defSkill.GetComponent<UI_Skill>().Play_Def_Skill_Effect();

                            BeHitGameBody.GetComponent<GameBody>().ShowPassiveSkill(defSkill);
                            //print(" 无伤害 播放 被动防御 特效！！！1 ");
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

                                //print("-------------------》技能的 类型  "+jn_date._type+"    jienneg name "+jn_date.name);
                                if (jn_date._type == "1"|| jn_date._type == "4")
                                {
                                    print(" atkObj.name    " + atkObj.name);
                                    //print(" atkObj GamebODY   " + atkObj.GetComponent<GameBody>());
                                    if (jn_date.chongjili != 0) {
                                        atkObj.GetComponent<GameBody>().HasBeHit(-10);
                                    }
                                    else
                                    {
                                        atkObj.GetComponent<GameBody>().HasBeHit();
                                    } 
                                    atkObj.GetComponent<GameBody>().GetPause(2, 0.2f);
                                    //_atkRigidbody2D.AddForce(new Vector2(-1000 * _roleScaleX, 0));
                                    //GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes();
                                    float tuili = atkObj.transform.position.x > beHitObj.transform.position.x ? 400+ jn_date.FanTuili : -400- jn_date.FanTuili;
                                    //if (jn_date.FanTuili != 0) tuili -= jn_date.FanTuili;
                                    //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> tuili  是多少： " + tuili);
                                    atkObj.GetComponent<GameBody>().GetTuili(tuili, 1f, true);
                                }
                              

                            }

                            // 减帧数
                            GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes(0.2f,0.3f);
                            return;
                        }

                        //无被攻击动作
                        //反震 这个是另外的 特效攻击
                        //是否触发

                        //触发效果
                        //触发后 特效显示 
                    }

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

                //print("-----------------------------------------------------------------------> atkObjYZ  "+ atkObjYZ);
                //int _fx = atkObj.transform.position.x > Coll.transform.position.x ? 1 : -1;

                if (Mathf.Abs(atkObjYZ - BeHitRoleDate.yingzhi) <= 100)
                {
                    BeHitGameBody.HasBeHit();
                    //atkObjV3Zero(Coll.gameObject);
                    //if (_atkRigidbody2D) _atkRigidbody2D.AddForce(new Vector2(jn_date.moveXSpeed * _roleScaleX, jn_date.moveYSpeed));
                    if (_atkRigidbody2D)
                    {
                        //_atkRigidbody2D.velocity = Vector2.zero;
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
                        //print("   空中怪被攻击 冲击力！ ");
                        //_BeHitRigidbody2D.AddForce(GlobalTools.GetVector2ByPostion(Coll.transform.position, atkObj.transform.position, jn_date.chongjili));
                        beHitObj.GetComponent<GameBody>().GetZongTuili(GlobalTools.GetVector2ByPostion(Coll.transform.position, atkObj.transform.position, jn_date.chongjili));
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


                    if(atkObj.tag == "Player"&&jn_date._type =="1"&& !atkObj.GetComponent<GameBody>().isInAiring) {
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
                        }
                        else
                        {
                            //atkObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(jn_date.chongjili * -Coll.transform.localScale.x - 100, 0));
                            atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * -__tuiliFX - 100, 0));
                        }
                    }
                    else
                    {

                        //_BeHitRigidbody2D.AddForce(new Vector2(jn_date.chongjili * -_fx - 100, 0));
                        beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * __tuiliFX - 100, 0));
                    }

                    if (jn_date.FanTuili != 0)
                    {
                        print("----------------------------------------------------------------> 作用反推力 "+ jn_date.FanTuili);
                        if (atkObj) atkObj.GetComponent<GameBody>().GetZongTuili(new Vector2(-jn_date.FanTuili * __tuiliFX * beHitXFScale, 0));
                    }


                    BeHitGameBody.HasBeHit(jn_date.chongjili);
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
                                    //print("  ------------------------> jinlaimei  ");
                                    if (_BeHitRigidbody2D) {
                                        //print("/////////>>>>>>>>>>>>>@!  ?????????   ");
                                        //_BeHitRigidbody2D.velocity = new Vector2(20 * -_fx, _BeHitRigidbody2D.velocity.y);
                                        //_BeHitRigidbody2D.AddForce(new Vector2(jn_date.chongjili * -_fx * beHitXFScale, 0));
                                        beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * __tuiliFX * beHitXFScale, 0));
                                    }
                                    
                                }
                            }
                            BeHitGameBody.HasBeHit(jn_date.chongjili);
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
                            BeHitGameBody.HasBeHit(jn_date.chongjili);
                            beHitObj.GetComponent<GameBody>().GetZongTuili(new Vector2(jn_date.chongjili * __tuiliFX * beHitXFScale, 0));
                        }
                       
                    }
                    
                    //if (atkObj.tag == "Player" && jn_date._type == "1" && !atkObj.GetComponent<GameBody>().isInAiring)
                    //{
                    //    atkObj.GetComponent<GameBody>().IsAtkKuaijiReSet();
                    //}

                    //print("---------------------333333333-------------------------------------------> 冲击力  " + jn_date.chongjili + "  _roleScaleX   " + _roleScaleX);
                }

                //print("持续 ---------------- 5 ");
                //硬直时间？
                if (atkObj.tag == "Player") {
                    BeHitGameBody.GetPause(jn_date.yingzhishijian, 0.4f);
                }
                else
                {
                    GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes(0.3f,0.4f);
                    if (BeHitGameBody.isInAiring) BeHitGameBody.GetPlayerRigidbody2D().velocity = new Vector2(BeHitGameBody.GetPlayerRigidbody2D().velocity.x,0); //Vector2.zero;
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
                BeHitGameBody.FudaiXiaoguo(jn_date.JiZhongFDXiaoguo);
                //if (Coll.tag != "Player")
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

            }

            if (atkObj.GetComponent<GameBody>()) atkObj.GetComponent<GameBody>().GetPause();

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
            GetBeHit(jn_date, -atkObj.transform.localScale.x);

        }
    }


    PlayerUI _playerUI;



    void OnTriggerEnter2D(Collider2D Coll)
    {
        GetHit(Coll);
    }

    //X方向速度清0
    void ObjV3Zero(GameObject obj)
    {
        Vector3 v3 = obj.GetComponent<Rigidbody2D>().velocity;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, v3.y);
    }

  

    public void GetBeHit(JN_Date jn_date, float sx)
    {
        //print("被击中 !! "+this.transform.name+"   攻击力 "+ jn_date.atkPower+"  我的防御力 "+this.GetComponent<RoleDate>().def);
        if (BeHitRoleDate.isDie) return;
        //print(" isCanBeHit>2:  "+ BeHitRoleDate.isCanBeHit);
        if (!BeHitRoleDate.isCanBeHit) return;
        float addxue = jn_date.atkPower - BeHitRoleDate.def;
        if (atkObj.GetComponent<RoleDate>())
        {
            addxue = atkObj.GetComponent<RoleDate>().atk + jn_date.atkPower - BeHitRoleDate.def;
        }
        addxue = addxue > 0 ? addxue : 1;
        //计算伤害减免比率
        if (BeHitRoleDate.shanghaijianmianLv != 0) addxue *= (1 - BeHitRoleDate.shanghaijianmianLv);
        BeHitRoleDate.live -= addxue;
        if (BeHitRoleDate.live < 0) BeHitRoleDate.live = 0;

        if (BeHitGameBody.tag != "Player"&& BeHitRoleDate.live != 0)
        {
            //print("*************************************************************BeHitGameBody.tag   "+ BeHitGameBody.tag);
            _playerUI.GetSlowByTimes(0.12f, 0.4f);
        }

        string tx_1 = BeHitGameBody.GetComponent<RoleDate>().BeHitTX_1;
        if (tx_1 != "")
        {
            GameObject hitTx_1 = Resources.Load(tx_1) as GameObject;
            hitTx_1 = ObjectPools.GetInstance().SwpanObject2(hitTx_1);
            HitTXPos(hitTx_1);
        }


        //print("live "+ roleDate.live);

        //判断是否在躲避阶段  无法被攻击
        //判断击中特效播放位置
        //击退 判断方向
        float _psScaleX = sx;

        //修正击中特效X位置
        if (jn_date.HitInSpecialEffectsX != 0) txPos = jn_date.HitInSpecialEffectsX;

        //判断是否在空中
        //挨打动作  判断是否破硬直
        //判断是否生命被打空

        if (jn_date.HitInSpecialEffectsType == 4) {
            //HitTX(_psScaleX, "JZTX_dian", "", 2, false, false, -txPos);
            //被电麻痹了  电特效
            HitTX2("JZTX_dian");
            return;
        }
        
        HitTX(_psScaleX, "BloodSplatCritical3", "",2,false,false,-txPos);
        if(jn_date.HitInSpecialEffectsType != 3)HitTX(_psScaleX,"jizhong", BeHitRoleDate.beHitVudio,4,true,true);
    }



    void HitTX2(string txName)
    {
        GameObject hitTx = Resources.Load(txName) as GameObject;
        hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);
        hitTx.transform.position = BeHitGameBody.transform.position;
        hitTx.transform.parent = BeHitGameBody.transform;
    }


    void HitTXPos(GameObject hitTx, float hy = 0)
    {
        if (BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
        {
            hitTx.transform.position = HitPos;
        }
        else
        {
            //大块头怪 击中点位置计算
            hitTx.transform.position = new Vector3(BeHitGameBody.transform.position.x - hy * -_atkObjScaleX, BeHitGameBody.transform.position.y, BeHitGameBody.transform.position.z);
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
                if (BeHitGameBody.GetComponent<GameBody>().IsNeedHitPos)
                {
                    //print(1);
                    hitTx.transform.position = HitPos;
                }
                else
                {
                    //print(2);
                    //hitTx.transform.position = new Vector3(gameBody.transform.position.x - hy * -_atkObjScaleX, gameBody.transform.position.y, gameBody.transform.position.z);
                    hitTx.transform.position = new Vector3(BeHitGameBody.transform.position.x - hy * -_atkObjScaleX , BeHitGameBody.transform.position.y, BeHitGameBody.transform.position.z);
                }

                
            }
            else
            {
                hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, hitTx.transform.localEulerAngles.y * -_atkObjScaleX, hitTx.transform.localEulerAngles.z);
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
