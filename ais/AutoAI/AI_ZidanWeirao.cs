using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZidanWeirao : AI_SkillBase
{

    protected override void ACSkillShowOut()
    {
        print(" dongzuo name shijian **********************ac    " + TXName);
        if(!roleDate) roleDate = GetComponent<RoleDate>();

        GetTheStart2();
    }


    RoleDate roleDate;

    [Header("生成子弹的 名字")]
    public string BulletName = "TX_Zidan1_XZ";

    List<GameObject> BulletsList = new List<GameObject>() { };
    [Header("显示 子弹特效时候的 音效")]
    public AudioSource ShowBulletsAudio;
    [Header("显示 围绕子弹的 个数")]
    public int BulletNums = 4;
    [Header("子弹距离中心点距离")]
    public float R = 3;

    void ShowBullets()
    {
        if (ShowBulletsAudio && !ShowBulletsAudio.isPlaying) ShowBulletsAudio.Play();
        float radian = 3.14f * 2 / BulletNums;
        float r = R;

        Vector2 v2 = new Vector2(this.transform.position.x, this.transform.position.y);

        for (int i = 0; i < BulletNums; i++)
        {
            GameObject _bullet = GlobalTools.GetGameObjectByName(BulletName);
            //_bullet.transform.position = new Vector2(1000, 1000);
            if (_bullet)
            {
                _bullet.GetComponent<JN_Date>().team = roleDate.team;
                BulletsList.Add(_bullet);
                //_bullet.transform.position = GetDateByName.GetInstance().GetTransformByName("pos" + i, this).transform.position;

                _bullet.transform.position = GetXYPosByRadian(i, radian * i, r) + v2;
                _bullet.transform.parent = this.gameObject.transform.parent;
                if (_bullet.GetComponent<JN_ZidanWeirao>())
                {
                    _bullet.GetComponent<JN_ZidanWeirao>().GetTheCenterObj(this.gameObject);
                    _bullet.GetComponent<JN_ZidanWeirao>().GetTargetObj(GetComponent<AIAirBase>().thePlayer);
                }
            }
        }
    }



    void RemoveAllBullets()
    {
        for (int i = BulletsList.Count - 1; i >= 0; i--)
        {
            if (BulletsList[i]) BulletsList[i].SetActive(false);
            BulletsList.Remove(BulletsList[i]);
        }

    }


    Vector2 GetXYPosByRadian(int i, float radian, float r)
    {
        float _x = Mathf.Cos(radian) * r;
        float _y = Mathf.Sin(radian) * r;
        return new Vector2(_x, _y);
    }



    bool IsShowBullet = false;
    void StartLive()
    {
        if (!IsShowBullet && roleDate.live <= roleDate.maxLive * 0.5f)
        {
            IsShowBullet = true;
            //GetStart();
            GetTheStart2();
        }
    }


    private void GetTheStart2()
    {
        BulletsList.Clear();

        ShowBullets();
    }

    float timeNums = 0;
    void ShowOver()
    {
        if (roleDate.isDie) RemoveAllBullets();

        if (!IsShowBullet) return;
        timeNums += Time.deltaTime;
        if (timeNums > 3)
        {
            timeNums = 0;
            for (int i = BulletsList.Count - 1; i >= 0; i--)
            {
                GameObject o = BulletsList[i];
                if (!o.activeSelf) BulletsList.Remove(o);
            }
            if (BulletsList.Count == 0)
            {
                IsShowBullet = false;
            }
        }
    }



}
