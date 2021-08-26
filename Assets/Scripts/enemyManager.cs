using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    public Transform player;
    private GameObject gm;
    [Header("Stats")]
    public float health;
    public float range;
    public float damage;
    public float attackRate;
    private float nextAttack;
    private AudioManager am;
    private float lastX;

    // Start is called before the first frame update
    void Start()
    {
        if(gm == null) gm = GameObject.Find("_GM");
        if(player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (am == null) am = gm.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer(player);
        if (transform.position.x < player.position.x) gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        else gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
    }

    public void GetDamage(float value)
    {
        am.Play("Hit");
        health -= value;
        if(health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Enemy died");
        gm.GetComponent<WaveManager>().enemiesAlive.Remove(gameObject);
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
