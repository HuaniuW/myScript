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

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //return;
        if (!IsHitChufa && Coll.tag == GlobalTag.Player&&Coll.GetComponent<JijiaGamebody>() == null)
        {
            IsHitChufa = true;

            Chufa();
            if(IsHitRemoveSelf) RemoveSelf();
        }
    }

    public bool IsHitRemoveSelf = true;

    protected virtual void Chufa()
    {

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
