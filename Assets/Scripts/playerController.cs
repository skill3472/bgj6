using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    private Vector3 rawInputMovement;
    public GameObject crosshair;
    public float crosshairDistanceMultiplier;
    public Rigidbody2D rb;
    [Space]
    [Header("Shooting")]
    public Transform weapon;
    public float weaponDamage;
    public float fireRate;
    private float nextFire;
    [Space]
    [Header("Stats")]
    public float health;
    public float healthRegen;
    public float maxHealth;
    private float nextRegen;
    [Space]
    public SpriteRenderer model;
    public Slider hpSlider;
    private AudioManager am;


    // Start is called before the first frame update
    void Start()
    {
        if (am == null) am = GameObject.Find("_GM").GetComponent<AudioManager>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = health;
        hpSlider.maxValue = maxHealth;
        rb.velocity = rawInputMovement * movementSpeed * Time.deltaTime;
        HealthRegenCheck();
        if (crosshair.transform.localPosition.x > 0)
            model.flipX = true;
        else if(crosshair.transform.localPosition.x < 0)
            model.flipX = false;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    public void OnAim(InputAction.CallbackContext value)
    {
        Vector2 inputAim = value.ReadValue<Vector2>();
        Vector3 aim = new Vector3(inputAim.x, inputAim.y, 0);

        if (aim.magnitude > 0)
        {
            aim *= crosshairDistanceMultiplier;
            crosshair.transform.localPosition = aim;
            //Debug.Log(aim);
        }
    }

    public void OnAimMouse(InputAction.CallbackContext value)
    {
        Vector2 inputAim = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
        Vector2 aim = new Vector3(inputAim.x, inputAim.y, 0);
        crosshair.transform.position = aim;
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        if (Time.time > nextFire)
        {
            am.Play("Shoot");
            nextFire = Time.time + fireRate;
            Vector3 targetDirection = crosshair.transform.position - transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);

            if (hit.collider != null)
            {

                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red, 0.2f);
                    GameObject hitEnemy = hit.transform.gameObject;
                    hitEnemy.GetComponent<enemyManager>().GetDamage(weaponDamage);
                } else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.white, 0.2f);
                }
            }
        }
    }

    private void HealthRegenCheck()
    {
        nextRegen -= Time.deltaTime;
        if (nextRegen <= 0 && health <= maxHealth - 5)
        {
            health += 5;
            nextRegen = healthRegen;
        } else if(nextRegen <= 0 && health < maxHealth && health > maxHealth - 5)
        {
            health = maxHealth;
            nextRegen = healthRegen;
        }
    }

    public void DamagePlayer(float value)
    {
        health -= value;
        if(health <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        //Insert some particles, sounds (?) and future death screen here
        Destroy(gameObject);
    }
}
