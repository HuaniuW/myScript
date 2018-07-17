using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_positionAndTeam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void GetPositionAndTeam(Vector3 _position, float team)
    {
        this.transform.position = _position;
        //GetComponent<JN_base>().hitKuai.GetComponent<AtkAttributesVO>().team = team;
        //GetComponent<JN_base>().hitKuai.transform.position = _position;
    }
}
