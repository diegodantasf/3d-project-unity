using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class Player : MonoBehaviour
{
    public float gravity = 20.0f;
    public float jumpHeight = 2.5f;

    Rigidbody r;
    bool grounded = false;
    Vector3 defaultScale;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        //r.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        r.freezeRotation = true;
        r.useGravity = false;
        defaultScale = transform.localScale;
    }

    void Update()
    {
        if (GroundGenerator.instance.gameOver || !GroundGenerator.instance.gameStarted) {
            return;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            r.velocity = new Vector3(r.velocity.x, CalculateJumpVerticalSpeed(), r.velocity.z);
        }

        // Move to the left
        if (Input.GetKey(KeyCode.A)) {
            r.velocity = new Vector3(-2f, r.velocity.y, r.velocity.z);            
        } 

        // Move to the right
        if (Input.GetKey(KeyCode.D)) {
            r.velocity = new Vector3(2f, r.velocity.y, r.velocity.z);
        }
        
        //Crouch
        if (Input.GetKey(KeyCode.S))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(defaultScale.x, defaultScale.y * 0.4f, defaultScale.z), Time.deltaTime * 7);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, Time.deltaTime * 7);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // We apply gravity manually for more tuning control
        r.AddForce(new Vector3(0, -gravity * r.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Finish") {
            //print("GameOver!");
            GroundGenerator.instance.gameOver = true;
        }

        if (collision.gameObject.tag == "Boost") {
            GroundGenerator.instance.score += 10;
            GroundGenerator.instance.movingSpeed += 4;
            StartCoroutine(slowDown());
        }

    }

    IEnumerator slowDown() {
        yield return new WaitForSeconds(4f);
        GroundGenerator.instance.movingSpeed -= 4;
    }
}