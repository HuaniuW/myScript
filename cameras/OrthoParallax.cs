using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoParallax : MonoBehaviour
{
    Camera cam;
    public List<JingDate> BGLayers = new List<JingDate>() { };
    public Transform jing;

    bool isGetChuShiPos = true;

    private void Start()
    {
        cam = GetComponent<Camera>();
        //获取景列表
        GetListJing();
        //获取 摄像头 角色 景的初始位置

    }


    void GetListJing()
    {
        foreach(Transform child in jing)
        {
            JingDate layer = new JingDate();
            layer.bgs = child;

            layer.speedOverCam = layer.bgs.transform.position.z / 100;
            float _scale = 1 - Mathf.Abs(layer.speedOverCam);

            layer.bgs.localScale = new Vector3(_scale, _scale, _scale);

            BGLayers.Add(layer);
        }
    }

    void Update()
    {
        if (!isGetChuShiPos) return;


        float camSpeed = cam.velocity.x;
        float camSpeedY = cam.velocity.y;

        //Move the layers based on cams velocity
        if (camSpeed != 0)
            foreach (JingDate layer in BGLayers)
            {
                
                float speed = camSpeed * layer.speedOverCam;
                float speedY = camSpeedY * layer.speedOverCam;
                print(layer.bgs.name+  "   speed   " + layer.speedOverCam);
                if (layer.bgs == null) continue;
                float z = layer.bgs.transform.position.z;
                float y = layer.bgs.transform.position.y;
                float x = layer.bgs.transform.position.x;
                x += speed * Time.deltaTime;
                y += speedY * Time.deltaTime;
                Vector3 newPosition = new Vector3(x, y, z);

                layer.bgs.transform.position = newPosition;
            }
    }

    //Serializable序列化
    //[System.Serializable]
    public struct BGLayer
    {
        public Transform bgs;
        //[Range(-1,1)]
        public float speedOverCamX;
        public float speedOverCamY;
    }

}

public class JingDate {
    public Transform bgs;
    //[Range(-1,1)]
    public float speedOverCam;
}
