using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class SideSlideObject : MonoBehaviour
{
    public float TranslationFactor = 1;
    public float MaximumFactor = 5;
    Vector3 curDirection;

    Rigidbody r;

    // Start is called before the first frame update
    void Start() {
        r = GetComponent<Rigidbody>();
        r.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        r.freezeRotation = true;
        r.useGravity = false;

        System.Random random = new System.Random();
        int randomNumber = random.Next();
        if (randomNumber % 2 == 0) {
            curDirection = Vector3.left;
        } else {
            curDirection = Vector3.right;
        }
    }

    // Update is called once per frame
    void Update() {
        if (GroundGenerator.instance.gameOver || !GroundGenerator.instance.gameStarted) {
            return;
        }
        r.velocity = curDirection * Math.Min(MaximumFactor, TranslationFactor);
        TranslationFactor += Time.deltaTime / 10;
        //transform.Translate(curDirection * TranslationFactor * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (curDirection ==  Vector3.left) {
            curDirection =  Vector3.right;
        } else {
            curDirection = Vector3.left;
        }
    }
}
