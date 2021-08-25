using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public int weaponDamage;
    public float fireRate;
    private float nextFire;
    [Space]
    [Header("Stats")]
    public float health;
    public float healthRegen;
    public float maxHealth;
    private float nextRegen;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rawInputMovement * movementSpeed * Time.deltaTime;
        HealthRegenCheck();
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
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        if (Time.time > nextFire)
        {
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
        if (Time.time > nextRegen && health <= maxHealth - 5)
        {
            nextRegen = Time.time + nextRegen;
            health += 5;
        } else if(Time.time > nextRegen && health < maxHealth && health > maxHealth - 5)
        {
            nextRegen = Time.time + nextRegen;
            health = maxHealth;
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
