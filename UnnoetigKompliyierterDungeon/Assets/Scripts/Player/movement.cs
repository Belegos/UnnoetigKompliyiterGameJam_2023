using Cinemachine;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    #region Fields
    
    [SerializeField] GameObject _player;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float moveSpeed = 1.1f;
    [SerializeField] private float jumpBoost = 3f;
    [SerializeField] private float bounce = 300f;

    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float bufferTime = 0.2f;

    [SerializeField] private float coyoteTimeCounter;
    [SerializeField] private float bufferTimeCounter;

    [SerializeField] private bool hasJumped = false;
    [SerializeField] private bool isGrounded = false;

    private Vector2 _moveValue;

    [SerializeField] public Animator animator;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float velocityMagnitude = _rb.velocity.magnitude;
        float speedNormalized = velocityMagnitude / _rb.drag;
        animator.SetFloat("Speed", speedNormalized * 1.0f);
        
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            hasJumped = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            if(coyoteTimeCounter < 0)
            {
                coyoteTimeCounter = 0;
            }
            
            bufferTimeCounter -= Time.deltaTime;
            if(bufferTimeCounter < 0)
            {
                bufferTimeCounter = 0;
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = new Vector3(_moveValue.x * Time.deltaTime, _rb.velocity.y, _moveValue.y * Time.deltaTime);
        if (_rb.velocity.z > 0)
        {
            _player.transform.Rotate(0, 25 * Time.deltaTime, 0);
            _player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            
        }
        else if (_rb.velocity.z < 0)
        {
            _player.transform.Rotate(0, 25 * Time.deltaTime, 0);
            _player.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if(_rb.velocity.x > 0)
        {
            _player.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);            
        }
        else if(_rb.velocity.x < 0)
        {
            _player.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);            
        }        
    }

    public void UpdateMove(InputAction.CallbackContext ctx)
    {
        _moveValue = ctx.ReadValue<Vector2>();                          
    }

    public void UpdateJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            bufferTimeCounter = bufferTime;

            if(bufferTimeCounter > 0f && coyoteTimeCounter > 0f)
            {
                if (!hasJumped)
                {
                    _rb.AddForce(Vector3.up * jumpBoost, ForceMode.Impulse);
                }
                
                hasJumped = true;
                isGrounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if(collision.gameObject.tag == "Wall")
        {
            
            Vector3 collisionNormal = collision.contacts[0].normal;
            _rb.AddForce(-collisionNormal * bounce * Time.deltaTime, ForceMode.Impulse);
            // _rb.angularVelocity = Vector3.zero;
        }
    }    
}
