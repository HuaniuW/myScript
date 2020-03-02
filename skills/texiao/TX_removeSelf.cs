using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_removeSelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float removeTimes = 0.5f;

    private void OnEnable()
    {
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, removeTimes));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
