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
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Slider volumeSlider;
    private float timer = 3f;
    private Vector3 moveVec;
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
        playerRb.velocity = new Vector3(moveVec.x * moveSpeed * Time.fixedDeltaTime, playerRb.velocity.y, moveVec.y * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
    }

    private void OnTriggerStay(Collider other)
    {
        //switch (other.gameObject.CompareTag(""))
        //{
        //    default:
        //        break;
        //}
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
                //GameObject.FindWithTag("OptionsMenu").SetActive(true);
                //GameObject.FindWithTag("MainMenu").SetActive(false);
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
