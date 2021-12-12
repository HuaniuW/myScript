using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_Zidan_XZ : MonoBehaviour ,ISkill
{

    [Header("生成子弹的 名字")]
    public string BulletName = "TX_Zidan1_XZ";

    public void GetStart(GameObject gameObj = null)
    {
        BulletsList.Clear();
        ShowBullets();
    }

    RoleDate roleDate;

    // Start is called before the first frame update
    void Start()
    {
        roleDate = GetComponent<RoleDate>();
        
    }

    // Update is called once per frame
    void Update()
    {
        StartLive();
        ShowOver();
    }

    bool IsShowBullet = false;
    void StartLive()
    {
        if (!IsShowBullet&&roleDate.live<=roleDate.maxLive*0.5f)
        {
            IsShowBullet = true;
            GetStart();
        }
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
            for (int i= BulletsList.Count - 1; i >= 0; i--)
            {
                GameObject o = BulletsList[i];
                if (!o.activeSelf) BulletsList.Remove(o);
            }
            if(BulletsList.Count == 0)
            {
                IsShowBullet = false;
            }
        }
    }


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
        float radian = 3.14f*2 / BulletNums;
        float r = R;

        Vector2 v2 = new Vector2(this.transform.position.x,this.transform.position.y);

        for (int i=0;i< BulletNums; i++)
        {
            GameObject _bullet = GlobalTools.GetGameObjectByName(BulletName);
            //_bullet.transform.position = new Vector2(1000, 1000);
            if (_bullet) {
                _bullet.GetComponent<JN_Date>().team = roleDate.team;
                BulletsList.Add(_bullet);
                //_bullet.transform.position = GetDateByName.GetInstance().GetTransformByName("pos" + i, this).transform.position;

                _bullet.transform.position = GetXYPosByRadian(i, radian * i, r)+v2;
                if (_bullet.GetComponent<JN_ZidanWeirao>())
                {
                    _bullet.GetComponent<JN_ZidanWeirao>().GetTheCenterObj(this.gameObject);
                }
            }
        }
    }

    void RemoveAllBullets()
    {
        for (int i = BulletsList.Count-1;i>=0;i--)
        {
            if (BulletsList[i]) BulletsList[i].SetActive(false);
            BulletsList.Remove(BulletsList[i]);
        }

    }


    Vector2 GetXYPosByRadian(int i,float radian, float r)
    {
        float _x = Mathf.Cos(radian) * r;
        float _y = Mathf.Sin(radian) * r;
        return new Vector2(_x,_y);
    }


    public bool IsGetOver()
    {
        throw new System.NotImplementedException();
    }

    public void ReSetAll()
    {
        throw new System.NotImplementedException();
    }


}
