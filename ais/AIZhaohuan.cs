using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZhaohuan : MonoBehaviour
{
    //召唤
    // Start is called before the first frame update
    RoleDate _roledate;

    void Start()
    {
        _roledate = GetComponent<RoleDate>();
    }

    [Header("召唤 生命 比例")]
    public float TheLiveBili = 0.5f;


    bool IsHasZhohuan = false;
    public void LiveInZhaohuan()
    {

        if (!IsHasZhohuan && _roledate.live > _roledate.maxLive * TheLiveBili) return;
        if (!IsHasZhohuan)
        {
            IsHasZhohuan = true;
            ZhaoHuan();
        }
    }

    public Transform ZhaohuanOutPos;
    public string ZhaohuanName = "";
    void ZhaoHuan()
    {
        print("zhaohuan    "+ ZhaohuanName);
        if (ZhaohuanName!="")
        {
            GameObject guai = GlobalTools.GetGameObjectByName(ZhaohuanName);
            guai.transform.position = ZhaohuanOutPos.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LiveInZhaohuan();
    }
}
