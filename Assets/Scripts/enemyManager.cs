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
    private ParticleManager pm;
    private Rigidbody2D rb;

    [SerializeField] private List<Sprite> standingSprites;
    [SerializeField] private List<Sprite> movementSprites;
    private List<Sprite> currentList;
    private int framesCount = 0;
    private int animationFrame = 0;
    private float animationSpeed = 0.25f;
    private float nextFrame = 0;

    enum MovementState
    {
        Standing,
        Moving,
    }

    private MovementState movementState = MovementState.Standing;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null) rb = gameObject.GetComponent<Rigidbody2D>();
        if(gm == null) gm = GameObject.Find("_GM");
        if(player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (am == null) am = gm.GetComponent<AudioManager>();
        if (pm == null) pm = gm.GetComponent<ParticleManager>();
        currentList = standingSprites;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer(player);
        if (transform.position.x < player.position.x) gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        else gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;

        if (rb.velocity.sqrMagnitude > 0.1f && movementState == MovementState.Standing)
        {
            SwitchMovementState(MovementState.Moving);
        }
        else if (rb.velocity.sqrMagnitude <= 0.1f && movementState == MovementState.Moving)
        {
            SwitchMovementState(MovementState.Standing);
        }
    }

    public void GetDamage(float value)
    {
        am.Play("Hit");
        pm.Spawn("Spark", transform.position, Quaternion.Euler(90,0,0));
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

    private void SwitchMovementState(MovementState newMovementState)
    {
        animationFrame = 0;
        nextFrame = Time.time + animationSpeed;
        print("switch state!");
        switch (newMovementState)
        {
            case MovementState.Moving:
                {
                    currentList = movementSprites;
                    break;
                }
            case MovementState.Standing:
                {
                    currentList = standingSprites;
                    break;
                }
        }
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = currentList[animationFrame];
        framesCount = currentList.Count;

        movementState = newMovementState;
    }

    private void AnimateModel()
    {
        if (Time.time > nextFrame)
        {
            print("next frame!");
            nextFrame = Time.time + animationSpeed;
            animationFrame += 1;
            if (animationFrame >= framesCount)
            {
                animationFrame = 0;
            }
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = currentList[animationFrame];
        }
    }
}
