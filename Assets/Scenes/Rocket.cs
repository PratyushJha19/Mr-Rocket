using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnUserInput();
    }
    void OnUserInput()
    {
        if (Input.GetKey(KeyCode.Space))
            print("You pressed space");

        else if (Input.GetKey(KeyCode.A))
            print("Rotating Right");

        else if (Input.GetKey(KeyCode.D))
            print("Rotating left");
    }
}
