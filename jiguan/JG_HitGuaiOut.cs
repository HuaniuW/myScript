using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_HitGuaiOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("怪物列表")]
    public List<string> GuaiList = new List<string> { };

    //怪物类型 空怪 数量   如果是 斧头精英  距离

    //间隔距离   数量 和距离
    [Header("距离中心的 间距")]
    public float ToCenterDis = 2;

    [Header("怪物X间距")]
    public float JianJuX = 1;

    [Header("怪物出现间距Y")]
    public float posY = 8;


    bool IsHasOutGuai = false;

    void GuaiOut()
    {
        if (GuaiList.Count == 0) return;
        float _x = 0;
        float _y = this.transform.position.y+posY;
        for (int i = 0;i< GuaiList.Count; i++)
        {
            if (i % 2 == 0)
            {
                _x = this.transform.position.x + ToCenterDis+ GlobalTools.GetRandomDistanceNums(3);
            }
            else
            {
                _x = this.transform.position.x - ToCenterDis- GlobalTools.GetRandomDistanceNums(3);
            }
            _y += GlobalTools.GetRandomDistanceNums(2);
            Vector2 pos = new Vector2(_x,_y);
            GameObject guai = GlobalTools.GetGameObjectByName(GuaiList[i]);
            guai.transform.position = pos;

            if (GlobalTools.FindObjByName("MainCamera")!=null)
            {
                GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GuaiList.Add(guai);
            }
            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsHasOutGuai && collision.transform.tag == "Player")
        {
            IsHasOutGuai = true;
            GuaiOut();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
