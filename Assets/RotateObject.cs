using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 RotateAmount;
    
    // Start is called before the first frame update
    void Start() {
        RotateAmount = new Vector3(60, 60, 60);
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
}
