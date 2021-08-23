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
        Debug.Log(aim);
    }
}
