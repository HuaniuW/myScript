using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DieZibao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _roleDate = GetComponent<RoleDate>();
    }

    // Update is called once per frame
    void Update()
    {
        DieZibao();
    }

    [Header("自爆生成 特效名字")]
    public string TX_ZiBao = "TX_DianranHuoXiaoGuo2";
    bool IsZibao = false;
    RoleDate _roleDate;
    void DieZibao()
    {
        if (!IsZibao &&_roleDate.isDie)
        {
            IsZibao = true;
            GameObject TX_Baozha = GlobalTools.GetGameObjectInObjPoolByName(TX_ZiBao);
            TX_Baozha.name = TX_ZiBao;
            TX_Baozha.transform.position = this.gameObject.transform.position;
            TX_Baozha.transform.parent = this.gameObject.transform.parent;
            TX_Baozha.GetComponent<ParticleSystem>().Play();
        }
    }

}
