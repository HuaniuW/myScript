using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_ChufaBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetStart();
    }

    protected virtual void GetStart()
    {

    }

    // Update is called once per frame
    bool IsHitChufa = false;

    protected GameObject HitObj;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //return;
        if (!IsHitChufa && Coll.tag == GlobalTag.Player&&Coll.GetComponent<JijiaGamebody>() == null)
        {
            IsHitChufa = true;
            HitObj = Coll.gameObject;
            Chufa();
            if(IsHitRemoveSelf) RemoveSelf();
        }
    }

    public bool IsHitRemoveSelf = true;

    protected virtual void Chufa()
    {
        print("g怪组over！");
        RemoveSelf();
    }

    protected virtual void ResetAll()
    {
        IsHitChufa = false;
    }

    protected void RemoveSelf()
    {
        StartCoroutine(IEDestoryByEnd(this.gameObject));
    }
    protected IEnumerator IEDestoryByEnd(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(obj, true);
    }
}
