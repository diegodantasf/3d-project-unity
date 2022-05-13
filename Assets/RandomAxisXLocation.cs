using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomAxisXLocation : MonoBehaviour
{
    public float low = -2.5f;
    public float high = 2.5f;
    // Start is called before the first frame update
    void Start() {
        transform.Translate(new Vector3(NextFloat(low, high), 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float NextFloat(float min, float max) {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}
