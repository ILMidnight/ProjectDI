using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public Player player;

    Rigidbody body;
    RaycastHit hit;
    LayerMask EnvironmentsMask;

    bool nowGrounded;

    Vector3 velocity, desiredVelocity;

    private void Awake()
    {
        body = gameObject.AddComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeRotation;
        EnvironmentsMask = ~LayerMask.NameToLayer("Environments");
    }

    private void Update()
    {
        CheckState();

        
        desiredVelocity = new Vector3(player.movementInput.x, 0f, player.movementInput.y) * 10;
        
        
    }

    private void FixedUpdate()
    {
        float maxSpeedChange = 1000 * Time.deltaTime;
        velocity = body.velocity;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        //velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        if (player.jumpInput)
        {
            player.jumpInput = false;
            Jump();
        }

        body.velocity = velocity;

        nowGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void EvaluateCollision(Collision collision)
    {
        for (int i = collision.contacts.Length - 1; i >= 0; i--)
        {
            Vector3 normal = collision.contacts[i].normal;

            nowGrounded |= normal.y >= 0.9f;
        }
    }

    private void Jump()
    {
        if (nowGrounded)
        {
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * 2);
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed; // (-2f * 중력값 * 원하는 높이) 의 제곱근(Mathf.Sqrt) == 중력을 극복하기 위한 수직 속도
        }
    }

    private void CheckState()
    {        
        //nowGrounded = Physics.Raycast(transform.position + (Vector3.up * .2f), -transform.up, out hit, EnvironmentsMask);
    }

    
}
