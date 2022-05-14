using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    float theta = 0.1f;
    Vector3 playerPosition;

    void Start()
    {
        playerPosition = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(playerPosition, new Vector3(0f, 0f, -0.1f), theta);
    }
}
