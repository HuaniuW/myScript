using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生成地图
public class GenerateMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //创建一个大关卡的地图  map1_1,map1_2...
    public void createCustomAllMap() {
        //有几个小地图组成？ 最少10个 +随机
        int n = (int)UnityEngine.Random.Range(0, 6);
        int mapsNum = 10 + n;
        //查找 是否有特别地图 以及特别地图位置编号
    }


}
