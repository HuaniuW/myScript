using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetDrawTest();

    }

    class AfterImage
    {
        public Mesh mesh;
        public Material material;
        public Matrix4x4 matrix;
        public float showStartTime;
        public float duration;  // 残影镜像存在时间
        public float alpha;
        public bool needRemove = false;
    }


    private List<AfterImage> _imageList = new List<AfterImage>();
    private Shader _shaderAfterImage;
    private float _duration; // 残影特效持续时间
    private float _interval; // 间隔
    private float _fadeTime; // 淡出时间


    // Update is called once per frame
    void Update () {
		
	}

    void Awake()
    {
        _shaderAfterImage = Shader.Find("Shader/AfterImage");
        print(">>>>>>>>>>>>>>>>>>>>>>>>>>      "+_shaderAfterImage);
    }


    void GetDrawTest()
    {
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        CombineInstance[] combineInstances = new CombineInstance[renderers.Length + meshRenderers.Length];

        //print(renderers[0].name);

        Transform t = transform;
        Material mat = null;
        for (int i = 0; i < renderers.Length; ++i)
        {
            var item = renderers[i];
            t = item.transform;
            mat = new Material(item.material);
            mat.shader = _shaderAfterImage;

            var mesh = new Mesh();
            item.BakeMesh(mesh);
            combineInstances[i] = new CombineInstance
            {
                mesh = mesh,
                subMeshIndex = 0,
            };
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances, true, false);

        _imageList.Add(new AfterImage
        {
            mesh = combinedMesh,
            material = mat,
            matrix = t.localToWorldMatrix,
            showStartTime = Time.realtimeSinceStartup,
            duration = _fadeTime,
        });
    }

    private void DrawMesh(Mesh trailMesh, Material trailMaterial)
    {
        Graphics.DrawMesh(trailMesh, Matrix4x4.identity, trailMaterial, gameObject.layer);
    }

    void LateUpdate()
    {
        bool hasRemove = false;
        foreach (var item in _imageList)
        {
            print(">>>>>>>??????   "+item);
            print(item.mesh);
            print(item.matrix);
            print("gameObject.layer  " + gameObject.name+"     "+ gameObject.layer);
            float time = Time.realtimeSinceStartup - item.showStartTime;

            if (time > item.duration)
            {
                item.needRemove = true;
                hasRemove = true;
                continue;
            }

            if (item.material.HasProperty("_Color"))
            {
                item.alpha = Mathf.Max(0, 1 - time / item.duration);
                Color color = Color.white;
                color.a = item.alpha;
                item.material.SetColor("_Color", color);
            }

            


            Graphics.DrawMesh(item.mesh, item.matrix, item.material, gameObject.layer);
        }

        if (hasRemove)
        {
            _imageList.RemoveAll(x => x.needRemove);
        }
    }
    
    
}
