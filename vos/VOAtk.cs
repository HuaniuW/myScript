using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOAtk : MonoBehaviour {

    //名字 x移动力量 y移动力量 特效出现帧 特效名字 特效相对x位置 特效相对y位置 攻击完后延迟帧数 
    //static public string[] atk_1 = { "atk_1", "100", "100", "10", "tx_1", "10", "10", "15" };

    public string atkName;
    public float xF;
    public float yF;
    //特效显示帧
    public float showTXFrame;
    //特效名
    public string txName;
    public float ox;
    public float oy;
    //动作结束延迟时间
    public float yanchi;

    public float atkDistance = 0;
    public float atkDistanceY = 0;

    public bool isYanchi = false;

    public int yanchiNum = 0;

    public int bianzhaochidu = 0;

    public string skillBeginEffect;

    public float fantan;

    public float qishouYC = 0;

    public float chongjili = 0;


    public float hitKuaiOX = 0;
    public float hitKuaiOY = 0;

    
    public float hitKuaiSX = 0;
    public float hitKuaiSY = 0;

    public float TXSX = 0;
    public float TXOX = 0;
    public float TXOY = 0;
    public float atkPower = 0;






    // Use this for initialization
    void Start () {
        //print("");
	}

    public void GetVO(Dictionary<string,string> dict)
    {

        //print("   容错------------------------------------ "+(dict == null));

        //if (dict == null) {
        //    Time.timeScale = 0.2f;
        //}

        if (dict == null) return;
        //print(" ......>??  "+dict.Count +"  ??===   "+ dict["atkName"]);
        if(dict["atkName"]!=null) this.atkName = dict["atkName"];
        this.xF = float.Parse(dict["xF"]);
        this.yF = float.Parse(dict["yF"]);
        this.showTXFrame = float.Parse(dict["showTXFrame"]);
        this.txName = dict["txName"];
        this.ox = float.Parse(dict["ox"]);
        this.oy = float.Parse(dict["oy"]);
        this.yanchi = float.Parse(dict["yanchi"]);
        if (dict.ContainsKey("atkDistance")) this.atkDistance = float.Parse(dict["atkDistance"]);
        if (dict.ContainsKey("isYanchi")) {
            this.isYanchi = dict["isYanchi"] == "true" ? true : false;
        }
        if (dict.ContainsKey("yanchiNum")) this.yanchiNum = int.Parse(dict["yanchiNum"]);
        if(dict.ContainsKey("bianzhaochidu"))this.bianzhaochidu = int.Parse(dict["bianzhaochidu"]);
        if (dict.ContainsKey("skillBeginEffect")) {
            this.skillBeginEffect = dict["skillBeginEffect"];
        }
        if (dict.ContainsKey("fantan"))
        {
            this.fantan = float.Parse(dict["fantan"]);
        }

        if (dict.ContainsKey("qishouYC"))
        {
            this.qishouYC = float.Parse(dict["qishouYC"]);
        }

        if (dict.ContainsKey("atkDistanceY"))
        {
            this.atkDistanceY = float.Parse(dict["atkDistanceY"]);
        }
        else
        {
            this.atkDistanceY = 0;
        }

        if (dict.ContainsKey("chongjili"))
        {
            this.chongjili = float.Parse(dict["chongjili"]);
        }

        if (dict.ContainsKey("hitKuaiOX"))
        {
            this.hitKuaiOX = float.Parse(dict["hitKuaiOX"]);
        }

        if (dict.ContainsKey("hitKuaiOY"))
        {
            this.hitKuaiOY = float.Parse(dict["hitKuaiOY"]);
        }

        if (dict.ContainsKey("hitKuaiSX"))
        {
            this.hitKuaiSX = float.Parse(dict["hitKuaiSX"]);
        }

        if (dict.ContainsKey("hitKuaiSY"))
        {
            this.hitKuaiSY = float.Parse(dict["hitKuaiSY"]);
        }

        if (dict.ContainsKey("TXOX"))
        {
            this.TXOX = float.Parse(dict["TXOX"]);
        }

        if (dict.ContainsKey("TXOY"))
        {
            this.TXOY = float.Parse(dict["TXOY"]);
        }


        if (dict.ContainsKey("atkPower"))
        {
            this.atkPower = float.Parse(dict["atkPower"]);
        }
    }
	
	
}
