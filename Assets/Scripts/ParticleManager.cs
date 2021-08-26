using System;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public Particle[] particles;

    public void Spawn(string name,Vector3 pos)
    {
        Particle p = Array.Find(particles, particle => particle.name == name);
        GameObject par = Instantiate(p.prefab, pos, Quaternion.Euler(0, 0, 0));
        Destroy(par, p.time);
    }
}
