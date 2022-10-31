using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public float damage = 1.0f;
    public float speed = 1.0f;
    public float lifespan = 6.0f;
    public Vector3 targetPosition;
    public bool hasTargetPosition;

    private void Start()
    {
        ApplyForce();
        Destroy(gameObject, lifespan);
    }

    void ApplyForce()
    {
        Vector3 impulseDirection;
        if (hasTargetPosition)
        {
            impulseDirection = Vector3.Normalize(targetPosition - transform.position);
        }
        else
        {
            impulseDirection = transform.forward;
        }
        
        _rigidbody.AddForce(impulseDirection * speed, ForceMode.VelocityChange);
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        hasTargetPosition = true;
    }

    public void SetDirection(Vector3 direction)
    {
        transform.forward = direction;
    }

    public virtual void OnImpact(Collision collision) {}

    private void OnCollisionEnter(Collision collision)
    {
        DamageManager damageManager = collision.gameObject.GetComponent<DamageManager>();

        // Check if target has a DamageManager
        if (damageManager != null)
        {
            // If it does, damage the target
            damageManager.ProcessDamage(damage);
        }

        OnImpact(collision);

        Destroy(gameObject);
    }



}