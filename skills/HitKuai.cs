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
    }

    // Update is called once per frame
    void Update() {
    }

    GameObject atkObj;

    RoleDate roleDate;
    GameBody gameBody;
    Rigidbody2D _rigidbody2D;

    Rigidbody2D _atkRigidbody2D;


    GameObject txObj;
    Transform beHitObj;
    public void GetTXObj(GameObject txObj,bool isSkill = false,float atkObjScaleX = 1){


        if (txObj != null)
        {
            this.txObj = txObj;
        }
        else
        {
            this.txObj = this.transform.parent.gameObject;
            //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"+this.name+"     "+this.txObj);
        }
        if(!isSkill)StartCoroutine(ObjectPools.GetInstance().IEDestory2(this.gameObject));
        _atkObjScaleX = txObj.GetComponent<JN_base>().atkObj.transform.localScale.x;
    }




    JN_Date jn_date;
    float _atkObjScaleX;

    float txPos = 0;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        beHitObj = Coll.transform;
        gameBody = Coll.GetComponent<GameBody>();
        roleDate = Coll.GetComponent<RoleDate>();
        _rigidbody2D = Coll.GetComponent<Rigidbody2D>();

        //print("?????????>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>    "+Coll.GetComponent<GameBody>().GetDB().armature.GetBones());
        //ContactPoint2D contact = Coll.GetContacts(Coll);


        if (this.txObj == null) this.txObj = this.transform.parent.gameObject;
        //if(roleDate) print("-------------------------------------------------------------this.txObj    " + this.txObj+"       "+this.transform.name+ "   this.teamNum  " + this.teamNum+ "  roleDate.team "+ roleDate.team);
        atkObj = txObj.GetComponent<JN_base>().atkObj;
        _atkRigidbody2D = atkObj.GetComponent<Rigidbody2D>();
        //print(atkObj.name);
        txPos = -1.2f;

        jn_date = txObj.GetComponent<JN_Date>();

        if (roleDate != null&& roleDate.team != jn_date.team)
        {
            //print("击中的2Dbox  "+Coll.GetComponent<BoxCollider2D>().transform.position);
            
            if (roleDate.isDie) return;
            if (!roleDate.isCanBeHit) return;
            //取到施展攻击角色的方向
            //float _roleScaleX = this.transform.localScale.x > 0?-1:1 ;  //-atkObj.transform.localScale.x;
            float _roleScaleX = -_atkObjScaleX;//atkObj.transform.localScale.x;
            if (_roleScaleX == 0) _roleScaleX = -atkObj.transform.localScale.x;
            //print("施展攻击方 名字      "+atkObj.name+ "      _roleScaleX     ----    " + _roleScaleX+ "    -atkObj.transform.localScale.x    "+ -atkObj.transform.localScale.x+"  team "+jn_date.team+"  被攻击放队伍   "+roleDate.team);
                /**
                if (jn_date._type != "1")
                {
                    _roleScaleX = this.transform.localScale.x > 0 ? -1 : 1;
                }*/

                //被击中 变色
            if (gameBody.tag!="Player")gameBody.BeHitColorChange();




            //print(_roleScaleX+"   ??   "+this.transform.localScale);

            //这个已经不需要了 
            //if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().GetBeHit(jn_date, _roleScaleX);

            //力作用  这个可以防止 推力重叠 导致人物飞出去
            Vector2 tempV3 = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector3(0,tempV3.y);
            if (jn_date != null &&gameBody != null)
            {
                ObjV3Zero(Coll.gameObject);
                //gameBody.GetPause(0.2f);


                //List<string> passive_def_skill = gameBody.GetComponent<RoleDate>().passive_def_skill;
                //---------------------------------------被击中 启动被动防御技能
                List<GameObject> skill_list = GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().skill_bar.GetComponent<UI_ShowPanel>().HZList;
                if (gameBody.tag == "Player" && skill_list.Count != 0)
                {
                   foreach(GameObject defSkill in skill_list)
                    {
                        //获取技能信息
                        //GameObject obj = Resources.Load(defSkill) as GameObject;

                        if (defSkill.GetComponent<UI_Skill>().GetHZDate().type == "zd") continue;
                        //是否冷却  还是只能找 玩家装备的技能
                        if (!defSkill.GetComponent<UI_Skill>().IsCDSkillCanBeUse()) continue;
                        
                        float jv = defSkill.GetComponent<UI_Skill>().GetHZDate().Chance_of_Passive_Skills;
                        //获取触发几率 
                        float cfjv = GlobalTools.GetRandomDistanceNums(100);
                        if(cfjv > jv) continue;
                        //触发
                        //加血
                        //无伤害
                        if (defSkill.GetComponent<UI_Skill>().GetHZDate().def_effect == "wushanghai") {
                            //defSkill.GetComponent<UI_Skill>().isCanBeUseSkill();
                            //defSkill.GetComponent<UI_Skill>().Play_Def_Skill_Effect();

                            gameBody.GetComponent<GameBody>().ShowPassiveSkill(defSkill);
                            //print(" 无伤害 播放 被动防御 特效！！！1 ");
                            // 弹开攻击者
                            ObjV3Zero(atkObj);
                            _atkRigidbody2D.AddForce(new Vector2(-500 * _roleScaleX, 0));
                            // 进入 BeHit里面 判断 角色的 硬直 来判断 是否进入
                            atkObj.GetComponent<GameBody>().HasBeHit();
                            // 减帧数
                            GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().GetSlowByTimes(0.5f);
                            return;
                        }
                        
                        //无被攻击动作
                        //反震 这个是另外的 特效攻击
                        //是否触发

                        //触发效果
                        //触发后 特效显示 
                    }

                }


                //判断是否破防   D 代办事项 
                float beHitXFScale = roleDate.beHitXFScale;
                if (jn_date.atkPower - roleDate.yingzhi > roleDate.yingzhi * 0.5)
                {
                    gameBody.HasBeHit();
                    //atkObjV3Zero(Coll.gameObject);
                    if(_atkRigidbody2D) _atkRigidbody2D.AddForce(new Vector2(jn_date.moveXSpeed * _roleScaleX, jn_date.moveYSpeed));
                    Coll.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    if (jn_date.fasntuili != 0) beHitXFScale = 1;// 有反推力说明是空中向下攻击
                    //print("----------------------------------------------------------------> 冲击力  "+jn_date.chongjili+ "  _roleScaleX   "+ _roleScaleX);
                    if(Coll.tag == "AirEnemy")
                    {
                        Coll.GetComponent<Rigidbody2D>().AddForce(GlobalTools.GetVector2ByPostion(Coll.transform.position,atkObj.transform.position,jn_date.chongjili));
                    }
                    else
                    {
                        Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(jn_date.chongjili * _roleScaleX * beHitXFScale, 0));
                    }
                    
                    txPos = 0.8f;
                    //print("sudu-------------------------------------------<>>>>>>>>>> 11111   " + Coll.GetComponent<Rigidbody2D>().velocity.x+"   || "+Coll.name+"   txPos   "+txPos);
                    //if(Coll.tag!="Player") print(Coll.GetComponent<Rigidbody2D>().velocity.x);
                    //print(Coll.tag);
                    
                }
                else if (jn_date.atkPower - roleDate.yingzhi > 0)
                {
                    //atkObjV3Zero(Coll.gameObject);

                    Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(jn_date.chongjili * _roleScaleX - 100, 0));
                    gameBody.HasBeHit(jn_date.chongjili);
                    if (atkObj && jn_date._type == "1")
                    {
                        ObjV3Zero(atkObj);
                        if (jn_date.atkDirection=="" && _atkRigidbody2D) _atkRigidbody2D.AddForce(new Vector2(-200 * _roleScaleX, 0));
                    }
                    txPos = 0.4f;
                }
                else
                {
                    if (atkObj && jn_date._type == "1")
                    {
                        //被攻击怪硬值过大 被反弹
                        //ObjV3Zero(atkObj);
                        atkObj.GetComponent<GameBody>().SetXSpeedZero();
                        /* if (!atkObj.GetComponent<GameBody>().isAtkFanTui)
                         {
                             atkObj.GetComponent<GameBody>().isAtkFanTui = true;
                         }*/
                        if (jn_date.atkDirection == "" && _atkRigidbody2D) _atkRigidbody2D.velocity = new Vector2(-12 * _roleScaleX, _atkRigidbody2D.velocity.y);// _atkRigidbody2D.AddForce(new Vector2(-600 * _roleScaleX, 0));
                    }
                }


                gameBody.GetPause(jn_date.yingzhishijian);

                //if (Coll.tag != "Player")
            }

            //print("sudu-------------------------------------------11111   " + Coll.GetComponent<Rigidbody2D>().velocity.x);

            //记录空中向下攻击的反推力
            if (jn_date.fasntuili != 0&&atkObj.GetComponent<Rigidbody2D>())
            {
                atkObj.GetComponent<GameBody>().SetYSpeedZero();
                _atkRigidbody2D.velocity = new Vector2(_atkRigidbody2D.velocity.x, 16);
            }

            if(atkObj.GetComponent<GameBody>()) atkObj.GetComponent<GameBody>().GetPause();


            //判断作用力与反作用力  硬直判断

            //
            //Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));

            //如果是碰撞就消失 调用消失方法
            if (jn_date != null && jn_date._type == "3")
            {
                //if (gameObject) ObjectPools.GetInstance().DestoryObject2(gameObject);     
                txObj.GetComponent<JN_base>().DisObj();
            }

            GetBeHit(jn_date, _roleScaleX);

        }
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
        if (roleDate.isDie) return;
        if (!roleDate.isCanBeHit) return;
        float addxue = jn_date.atkPower - roleDate.def;
        if (atkObj.GetComponent<RoleDate>())
        {
            addxue = atkObj.GetComponent<RoleDate>().atk + jn_date.atkPower - roleDate.def;
        }
        addxue = addxue > 0 ? addxue : 1;
        roleDate.live -= addxue;
        if (roleDate.live < 0) roleDate.live = 0;
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
        HitTX(_psScaleX, "BloodSplatCritical3", "",2,false,false,-txPos);
        if(jn_date.HitInSpecialEffectsType != 3)HitTX(_psScaleX,"jizhong",roleDate.beHitVudio,4,true,true);
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

        //print(hitTx.name + "  1---------------------->>   " + hitTx.transform.localEulerAngles);
        //print("sudu-------------------------------------------   "+ gameBody.GetComponent<Rigidbody2D>().velocity.x);
        hitTx.transform.position = new Vector3(gameBody.transform.position.x-hy*psScaleX, gameBody.transform.position.y, gameBody.transform.position.z) ;
        //击中特效缩放
        hitTx.transform.localScale = new Vector3(beishu, beishu, beishu);

        //print(hitTx.transform.localEulerAngles + " 000000000000000000000000000000000 "+hitTx.transform.name);

        if (!isZX) {
            //if(atkObj.transform.position.y-)
            if (jn_date.isAtkZongXiang)
            {
                hitTx.transform.localEulerAngles = new Vector3(-90, hitTx.transform.localEulerAngles.y * psScaleX, hitTx.transform.localEulerAngles.z);
                hitTx.transform.position = new Vector3(gameBody.transform.position.x - hy * psScaleX-0.5f, gameBody.transform.position.y, gameBody.transform.position.z);
            }
            else
            {
                hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, hitTx.transform.localEulerAngles.y * psScaleX, hitTx.transform.localEulerAngles.z);
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
        if (psScaleX>0)
        {
            if(isSJJD)jd = Random.Range(-5, 10);
            hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, hitTx.transform.localEulerAngles.y, jd);
            //print(">>>>>>>>>>>>>>>>>>>>>>>>>左");
        }
        else
        {
            if (isSJJD) jd = Random.Range(-5, 10);
            hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, 180, jd);
        }

    }

  

    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");
    }
}
