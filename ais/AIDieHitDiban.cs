using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDieHitDiban : MonoBehaviour
{
    // Start is called before the first frame update
    RoleDate _roleDate;
    GameBody _gameBody;


    void Start()
    {
        _roleDate = GetComponent<RoleDate>();
        _gameBody = GetComponent<GameBody>();
    }

    // Update is called once per frame
    void Update()
    {
        DieHitDiban();
    }

    bool IsShowSkill = false;

    public string TX_atkzhengDiban = "tx_xialuozhendongYCDu";

    public string TX_atkzhengDiban2 = "TX_DuyanDa";

    void DieHitDiban()
    {
        if (_roleDate.isDie)
        {
            if (!IsShowSkill && _gameBody.IsGround)
            {
                IsShowSkill = true;
                GameObject dukuai = GlobalTools.GetGameObjectByName(TX_atkzhengDiban);
                Vector2 v2 = this.transform.position;
                dukuai.transform.position = new Vector2(v2.x, v2.y - 1.4f);
                dukuai.GetComponent<JN_base>().GetAtkObjIn(this.gameObject);

                GameObject dukuai2 = GlobalTools.GetGameObjectByName(TX_atkzhengDiban2);
                dukuai2.transform.position = new Vector2(v2.x, v2.y - 1.4f);
                dukuai2.GetComponent<JN_Date>().team = this.GetComponent<JN_Date>().team;

            }
        }
    }
}
