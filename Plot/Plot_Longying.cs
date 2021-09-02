using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Plot_Longying : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LongFly();
        JishiDis();
    }

    float jishi = 0;
    void JishiDis()
    {
        if (!IsLongyingStart) return;
        jishi += Time.deltaTime;
        if(jishi>=5){
            DestroyImmediate(Long, true);
            DestroyImmediate(this.gameObject, true);

        }
    }



    bool IsPlayerIn = false;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //return;
        if (!IsPlayerIn && Coll.tag == GlobalTag.Player)
        {
            IsPlayerIn = true;
            print(" --longying ");

            LongyingStart();
            //GetYuanColor();
            //DistorySelf();

            Long.GetComponent<GameBody>().GetBoneColorChange(_ycolor);

        }
    }


    protected UnityArmatureComponent DBBody;
    public UnityArmatureComponent GetDB()
    {
        if (!DBBody) DBBody = Long.GetComponentInChildren<UnityArmatureComponent>();
        return DBBody;
    }

    bool IsLongyingStart = false;
    [Header("龙")]
    public GameObject Long;

    //播放遇到Boss的音效
    void LongyingStart()
    {
        //Long.GetComponent<>
        GetDB();
        //DBBody.animation.GotoAndPlayByFrame("run_3");
        Long.GetComponent<GameBody>().RunRight(0);
        IsLongyingStart = true;
        __x = Long.transform.position.x;
        __y = Long.transform.position.y;
        if (Longhou) Longhou.Play();

        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-1"), this);

        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, this.name), this);
    }

    [Header("龙吼")]
    public AudioSource Longhou;

    float __x;
    float __y;
    float _speedX = 0.8f;
    float _speedY = 0.4f;


    void LongFly()
    {
        if (!IsLongyingStart) return;
        __x += _speedX;
        __y += _speedY;
        Long.transform.position = new Vector3(__x, __y, Long.transform.position.z);
    }

    Color _ycolor = Color.black;
    


    public void DistorySelf()
    {
        StartCoroutine(IEDestoryByEnd(this.gameObject));
    }
    public IEnumerator IEDestoryByEnd(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(obj, true);
    }
}
