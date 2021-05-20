using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_hitkuaiShenTi : MonoBehaviour
{
    public bool IsNeedHideHitKuai = false;
    // Start is called before the first frame update
    void Start()
    {
        _roleDate = GetComponent<RoleDate>();
        if (IsNeedHideHitKuai &&HitKuai) HitKuai.SetActive(false);
        ChuCiXiaLuoHit();

    }

    private void OnEnable()
    {
        ChuCiXiaLuoHit();
        IsHitDiMian = false;
    }

    public void ReSetAll()
    {
        IsHitDiMian = false;
    }

    RoleDate _roleDate;

    public GameObject HitKuai;

    public bool IsChuCiXialuoHit = false;
    void ChuCiXiaLuoHit()
    {
        //print("---------------------- ?inAiring    "+ GetComponent<GameBody>().isInAiring);
        if (IsChuCiXialuoHit && !GetComponent<GameBody>().IsGround) ShowHitKuaiInAir();
    }

    bool IsStart = false;
    public void ShowHitKuaiInAir()
    {
        IsHitDiMian = false;
        IsStart = true;
        HitKuai.SetActive(true);
    }

    bool IsHitDiMian = false;

    void BeHitOrDonOnGroundHide()
    {
        //if (GetComponent<GameBody>().isJumping) return;
        //这里是不能 >=0 的  只能是>0 不然有时候   落到地面 速度是0 直接return了
        if (IsStart&&GetComponent<GameBody>().isJumping && GetComponent<GameBody>().GetPlayerRigidbody2D().velocity.y > 0) {
            //IsHitDiMian = false;
            return;
        }
        

        if (_roleDate.isDie||_roleDate.isBeHiting||GetComponent<GameBody>().IsDownOnGround())
        {
            if(IsNeedHideHitKuai && HitKuai) HitKuai.SetActive(false);
        }

        if (GetComponent<GameBody>().IsDownOnGround())
        {
            //print("     --------------------------------------------------------------------------  @@@@@ IsHitDiMian     " + IsHitDiMian);
            if (!IsHitDiMian)
            {
                //print("     --------------------------------------------------------------------------   ????? 没有进来这里     ");
                IsHitDiMian = true;
                IsStart = false;
                //出烟尘 特效  震动
                GameObject yc = Resources.Load("tx_xialuozhendongYC") as GameObject; 
                yanchenHitKuai =  ObjectPools.GetInstance().SwpanObject2(yc);
                //yanchenHitKuai.transform.position = this.transform.position;
                yanchenHitKuai.GetComponent<JN_base>().GetPositionAndTeam(this.transform.position, this.GetComponent<RoleDate>().team, 1, this.gameObject);
                //print("烟尘特效Y----》@@@@@@@@    "+ yanchenHitKuai.transform.position.y);
                //Time.timeScale = 0.1f;
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.6"), this); // removeEventListener(EventTypeName.CAMERA_SHOCK, this.GetShock);
            }
        }
    }

    GameObject yanchenHitKuai;

    // Update is called once per frame
    void Update()
    {
        BeHitOrDonOnGroundHide();
    }
}
