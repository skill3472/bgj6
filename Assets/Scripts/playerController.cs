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
    [Space]
    [Header("Shooting")]
    public Transform weapon;
    public int weaponDamage;
    public float fireRate;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += rawInputMovement * movementSpeed * Time.deltaTime;
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

        if(aim.magnitude > 0)
        {
            aim *= crosshairDistanceMultiplier;
            crosshair.transform.localPosition = aim;
            Debug.Log(aim);
        }
    }

    public void OnAimMouse(InputAction.CallbackContext value)
    {
        Vector2 inputAim = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
        Vector3 aim = new Vector3(inputAim.x, inputAim.y, 0);
        crosshair.transform.position = aim;
        //Debug.Log(aim);
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 targetDirection = crosshair.transform.position - transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);

            if (hit.collider != null)
            {
                Debug.DrawLine(transform.position, hit.point, Color.white, 0.2f);
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    GameObject hitEnemy = hit.transform.gameObject;
                    hitEnemy.GetComponent<enemyManager>().GetDamage(weaponDamage);
                }
            }
        }
    }
}
