using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    bool canScale = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canScale) {
            return;
        }
        Vector3 vec = new Vector3(Mathf.Sin(Time.time * 2) + 2, transform.localScale.y, Mathf.Sin(Time.time * 2) + 2);
        transform.localScale = vec;
    }

    void OnCollisionEnter(Collision collision)
    {
        canScale = false;
    }
}
