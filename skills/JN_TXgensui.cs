using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_TXgensui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_obj != null)
        {
            this.transform.position = new Vector2(_obj.transform.position.x, this.transform.position.y);
        }
    }

    GameObject _obj;
    public void GetGenSuiObj(GameObject obj)
    {
        _obj = obj;
    }
}
