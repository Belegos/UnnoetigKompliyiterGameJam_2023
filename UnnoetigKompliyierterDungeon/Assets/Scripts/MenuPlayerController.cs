using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timePassed;
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Slider volumeSlider;
    private float smoothRot;
    private float timer = 3f;
    private Vector3 moveVec;
    private float moveRot;
    private float currentVelocity;
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (moveVec.sqrMagnitude == 0) return;
        moveRot = Mathf.Atan2(moveVec.x, moveVec.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, moveRot, 0);
        smoothRot = Mathf.SmoothDampAngle(transform.eulerAngles.y, smoothRot, ref currentVelocity, smoothTime);

        playerRb.velocity = new Vector3(moveVec.x * moveSpeed * Time.fixedDeltaTime, playerRb.velocity.y, moveVec.y * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
        transform.Rotate(moveVec);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Start"))
        {
            timePassed += Time.deltaTime;
            if(timePassed > timer) 
            {
            // Load game scene

            }
        }
        else if (other.gameObject.CompareTag("Options"))
        {
            timePassed += Time.deltaTime;
            if (timePassed > timer)
            {
                mainMenu.SetActive(false);
                optionsMenu.SetActive(true);
                timePassed = 0;
            }
        }
        else if (other.gameObject.CompareTag("Exit"))
        {
            timePassed += Time.deltaTime;
            if (timePassed > timer)
            {
            EditorApplication.ExitPlaymode();
            }
        }
        else if (other.gameObject.CompareTag("Back"))
        {
            timePassed += Time.deltaTime;
            if (timePassed > timer)
            {
                mainMenu.SetActive(true);
                optionsMenu.SetActive(false);
                timePassed = 0;
            }
        }
        else if (other.gameObject.CompareTag("Plus"))
        {
            volumeSlider.value += 0.01f;
        }
        else
        {
            volumeSlider.value -= 0.01f;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        timePassed = 0;
    }
}
