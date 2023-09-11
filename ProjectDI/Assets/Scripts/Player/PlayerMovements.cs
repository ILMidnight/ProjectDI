using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public Player player;

    Rigidbody body;
    RaycastHit hit;
    LayerMask EnvironmentsMask;

    bool nowGrounded;

    Vector3 velocity;

    private void Awake()
    {
        body = gameObject.AddComponent<Rigidbody>();
        EnvironmentsMask = ~LayerMask.NameToLayer("Environments");
    }

    private void Update()
    {
        CheckState();

        Vector3 desiredVelocity = new Vector3(player.movementInput.x, 0, 0) * 10;
        
        float maxSpeedChange = 10 * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        Vector3 displacement = velocity * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + displacement;

        player.playerModel.GetComponent<Renderer>().material.color = nowGrounded ? Color.black : Color.white;

        transform.localPosition = newPosition;
    }

    private void CheckState()
    {
        nowGrounded = Physics.Raycast(transform.position + (Vector3.up * .2f), -transform.up, out hit, .21f, EnvironmentsMask);
    }
}
