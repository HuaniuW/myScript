using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticlesScale : MonoBehaviour {

    // Use this for initialization
    ParticleSystem[] particles;
    int particlesLength;
    void Start () {
        particles = GetComponentsInChildren<ParticleSystem>(true);
        particlesLength = particles.Length;
        //print("length   "+ particlesLength);
        if (particlesLength == 0)
        {
            return;
        }

    }
	

    public void SetParticlesScale(float _sx)
    {
        print(transform.localEulerAngles);
        if (_sx>0)
        {
            //transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.localRotation = new Quaternion(0, 0, 0, 1);
        }
        else
        {
            //改变 物体的 rotation  可以用localEulerAngles
            //transform.localEulerAngles = new Vector3(0, 180, 0);
            this.transform.localRotation = new Quaternion(0, 180, 0, 1);
        }

        return;
        for (int idx = 0; idx < particlesLength; idx++)
        {
            var particle = particles[idx];
            if (particle == null) continue;
            var v3 = particle.transform.localScale;
            particle.transform.localScale = new Vector3(1,  _sx, 1);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
