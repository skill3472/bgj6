using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{

    [Header("Stats")]
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(int value)
    {
        health -= value;
        if(health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}
