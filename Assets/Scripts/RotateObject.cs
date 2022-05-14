using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 RotateAmount;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (GroundGenerator.instance.gameOver || !GroundGenerator.instance.gameStarted) {
            return;
        }
        if (gameObject.tag == "Player") {
            //Debug.Log(GroundGenerator.instance.movingSpeed);
            transform.Rotate(RotateAmount * Time.deltaTime * GroundGenerator.instance.movingSpeed / 8);
        } else {
            transform.Rotate(RotateAmount * Time.deltaTime);
        }
    }
}
