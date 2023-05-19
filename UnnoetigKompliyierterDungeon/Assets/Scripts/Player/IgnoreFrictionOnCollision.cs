using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreFrictionOnCollision : MonoBehaviour
{
    #region Fields

    [SerializeField] private PhysicMaterial originalMaterial;
    [SerializeField] private Collider playerCollider;
    
    [SerializeField] private float ignoreFrictionDuration = 1.0f;

    private float ignoreFrictionTimer = 0.0f;


    #endregion

    private void Start()
    {
        playerCollider = GetComponent<Collider>();
        originalMaterial = playerCollider.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if(ignoreFrictionTimer > 0)
        {
            ignoreFrictionTimer -= Time.deltaTime;
            if(ignoreFrictionTimer <= 0)
            {
                playerCollider.material = originalMaterial;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            if (ignoreFrictionTimer <= 0)
            {
                ignoreFrictionTimer += ignoreFrictionDuration;
                playerCollider.material = originalMaterial;
                playerCollider.material = null;
            }        
        }
    }
}
