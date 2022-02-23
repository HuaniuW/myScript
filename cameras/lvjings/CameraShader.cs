using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraShader : MonoBehaviour
{
    private Material mat;
    private Shader shader;
    private float TimeX = 1.0f;
    [Range(1, 10)]
    public float Distortion = 1.0f;
    [Range(0.5f, 1.0f)]
    public float speed = 0.75f;
    Material material
    {
        get
        {
            if (mat == null)
            {
                mat = new Material(shader);
                //HideAndDontSave：保留对象到新场景 此属性的功能是用来设置是否将Object对象保留到新Scene中，如果使用HideFlags.HideAndDontSave，
                //则Object对象将在新Scene中被保留下来，但不会显示在Hierarchy面板中。所以后面需要用DestroyImmediate()销毁。
                mat.hideFlags = HideFlags.HideAndDontSave;
            }
            return mat;
        }
    }
    // Use this for initialization
    void Start()
    {
        //获取shader
        shader = Shader.Find("CameraFilterPack/TV_Old_Movie");
        print("启动 摄像机 滤镜！！！！！"+(shader == null));
        //判断显卡是否支持图像后期处理效果
        if (!SystemInfo.supportsImageEffects)
        {
            print("显卡 不支持！！！");
            enabled = false; //此为代码的enable
            return;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (shader != null)
        {
            //shader参数控制
            TimeX += Time.deltaTime * speed;
            if (TimeX > 100) TimeX = 0;
            material.SetFloat("_TimeX", TimeX);
            material.SetFloat("_Distortion", Distortion);
            //渲染
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying != true)
        {
            shader = Shader.Find("CameraFilterPack/TV_Old_Movie");
        }
#endif
    }

    void OnDisable()
    {
        if (mat)
        {
            DestroyImmediate(mat);
        }
    }
}