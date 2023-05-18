using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    #region Fields
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float movespeed = 5f;
    Vector3 _moveValue;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = new Vector3((_moveValue.x * Time.deltaTime * movespeed), _rb.velocity.y, (_moveValue.y * Time.deltaTime * movespeed));
    }

    public void UpdateMove(InputAction.CallbackContext ctx)
    {
        _moveValue = ctx.ReadValue<Vector2>();
    }
}
