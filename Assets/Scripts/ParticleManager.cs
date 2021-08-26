using System;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public Particle[] particles;

    public void Spawn(string name,Vector3 pos, Quaternion rot)
    {
        Particle p = Array.Find(particles, particle => particle.name == name);
        GameObject par = Instantiate(p.prefab, pos, rot);
        Destroy(par, p.time);
    }
}
