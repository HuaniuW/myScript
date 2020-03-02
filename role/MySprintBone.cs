using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class MySprintBone : MonoBehaviour {
    protected UnityArmatureComponent DBBody;
    // Use this for initialization
    void Start () {
        DBBody = GetComponent<GameBody>().GetDB();

        //print("获取骨骼 "+DBBody.armature.GetBone("图层_29_拷贝1").animationPose.rotation);

        //DBBody.armature.GetBone("辫子左").offset.rotation = 0.9f;
        //DBBody.armature.GetBone("图层_29_拷贝1").offset.rotation = 0.9f;

        //print("获取骨骼2222222222 " + DBBody.armature.GetBone("图层_29_拷贝1").animationPose.rotation);

        //print("pos  "+this.transform.position);
        //print("scale "+ this.transform.localScale.x);

        //DBBody.armature.GetBone("辫子左").offset.x = 1;

        Vector3 t1 = new Vector3(18,3,0);
        Vector3 t2 = new Vector3(12, 13, 0);
        //print("?????????????????????      "+(t1-t2));
        //print(DBBody.armature.GetBone("辫子左").global.x);

    }

    //根据 人物移动距离 与上一帧判断  距离大 说明速度快 往上移动 距离小 说明慢往下移动  负数反着移动   Y距离呢？

    /**
    public void GetBoneColorChange(Color color)
    {
        List<DragonBones.Bone> bones = GetDB().armature.GetBones();
        foreach (DragonBones.Bone o in bones)
        {
            //print("name:  " + o.GetType()+o.slot);
            if (o.slot != null)
            {
                //if(o.slot.display!=null &&(o.slot.display as GameObject).GetComponent<Renderer>()!=null&& (o.slot.display as GameObject).GetComponent<Renderer>().material!=null&& (o.slot.display as GameObject).GetComponent<Renderer>().material.color!=null != null) (o.slot.display as GameObject).GetComponent<Renderer>().material.color = Color.red;
                if (o.slot.display != null) (o.slot.display as GameObject).GetComponent<Renderer>().material.color = color;
                //return;
            }
            //print(o.slot._SetColor(DragonBones.ColorTransform));
        }
    }*/

    // Update is called once per frame
    void Update () {
        //print("pos  " + this.transform.position);
        //print("scale " + this.transform.localScale.x);

        //print("获取骨骼y " + DBBody.armature.GetBone("辫子左").animationPose.y);
        //print("获取骨骼----2 " + DBBody.armature.GetBone("辫子左").animationPose.skew);
        //print("获取骨骼----------3 " + DBBody.armature.GetBone("辫子左").animationPose.rotation);

        //print(DBBody.armature.GetBone("辫子左").global);
        //print(DBBody.armature.GetBone("辫子左"));

    }
}
