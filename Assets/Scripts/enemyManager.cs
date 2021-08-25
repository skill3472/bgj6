using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    public Transform player;
    [Header("Stats")]
    public int health;
    public float range;
    public float damage;
    public float attackRate;
    private float nextAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer(player);
    }

    public void GetDamage(int value)
    {
        health -= value;
        if(health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }

    private void CheckForPlayer(Transform checkedPlayer)
    {
        if(Vector2.Distance(transform.position, checkedPlayer.position) < range)
        {
            Debug.Log("Range is good, casting...");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, checkedPlayer.position - transform.position);
            Debug.DrawRay(transform.position, checkedPlayer.position - transform.position, Color.cyan, attackRate);
            if(hit.collider.gameObject.CompareTag("Player") && Time.time > nextAttack)
            {
                Debug.Log("Attacking the player...");
                nextAttack = Time.time + attackRate;
                hit.collider.gameObject.GetComponent<playerController>().DamagePlayer(damage);
            }
        }
    }
}
