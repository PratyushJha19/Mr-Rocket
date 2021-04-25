using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{// vector3 is for the position of the game object under the transform component
    [SerializeField] Vector3 positionVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    [Range(0, 2)] [SerializeField] float positionFactor;
    Vector3 startingPosition;
        // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;// here 'transform' is the component with the game object in the engine
        //And 'position' is the one of the part of transform component in the unity engine 
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return;}
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float sinWave = Mathf.Sin(cycles * tau);
        positionFactor = sinWave / 2f + 0.5f;

        Vector3 offset = positionVector * positionFactor;
        transform.position = startingPosition + offset;
    }
}
