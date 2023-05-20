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
    private float timer = 2f;
    private float moveRot;
    private float currentVelocity;
    private Vector3 moveVec;
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
        Debug.Log(Screen.resolutions[i]);

        }
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
        timePassed += Time.deltaTime;

        if (other.gameObject.CompareTag("Start"))
        {
            if(timePassed > timer) 
            {
                SceneManager.LoadSceneAsync(1);
            }
        }
        else if (other.gameObject.CompareTag("Options"))
        {
            if (timePassed > timer)
            {
                mainMenu.SetActive(false);
                optionsMenu.SetActive(true);
                timePassed = 0;
            }
        }
        else if (other.gameObject.CompareTag("Exit"))
        {
            if (timePassed > timer)
            {
            Application.Quit();
            }
        }
        else if (other.gameObject.CompareTag("Back"))
        {
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
        else if(other.gameObject.CompareTag("Minus"))
        {
            volumeSlider.value -= 0.01f;
        }
        else if (other.gameObject.CompareTag("1080p"))
        {
            if(timePassed > timer)
            {

            Screen.SetResolution(1920, 1080, true);
                Debug.Log(Screen.width + " " + Screen.height);
                Debug.Log(Screen.currentResolution.ToString());
            }
        }
        else if (other.gameObject.CompareTag("720p"))
        {
            if(timePassed > timer)
            {
            Screen.SetResolution(1280, 720, false);

                Debug.Log(Screen.width + " " + Screen.height);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        timePassed = 0;
    }
}
