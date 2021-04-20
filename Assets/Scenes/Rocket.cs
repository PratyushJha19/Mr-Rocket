using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody; // 'Rigidbody' is a variable type provided by unity.
    // 'rigidBody' is the name of a member variable of type 'Rigidbody'.
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); //not explained in the course, yet. 
    }

    // Update is called once per frame
    void Update()
    {
        OnUserInput();
    }
  //"(Input.GetKey(KeyCode.Input))" is the syntax for getting the input from the user
  //"Input. GetKey" will repeatedly return the value which the user holds down the specified key.
  //'KeyCode.' is a command for specifying the key pressed.
  //"rigidBody.AddRelativeForce(Vector3.direction);" syntax for making an object or player move
  //rigidBody is the variable name used here. Any variable name can be used here at its place.
    void OnUserInput()
    {
        if (Input.GetKey(KeyCode.Space))
            print("You pressed space");

       else if (Input.GetKey(KeyCode.A))
            rigidBody.AddRelativeForce(Vector3.left);

       else if (Input.GetKey(KeyCode.D))
            rigidBody.AddRelativeForce(Vector3.right);

       else if (Input.GetKey(KeyCode.W))
            rigidBody.AddRelativeForce(Vector3.up);
        
       else if (Input.GetKey(KeyCode.S))
            rigidBody.AddRelativeForce(Vector3.down);
       OnUserInput2();
    }

    void OnUserInput2()
    {
        if (Input.GetKey(KeyCode.W))
            print("Take Off");
        else if (Input.GetKey(KeyCode.D))
            print("Going Right");
        else if (Input.GetKey(KeyCode.A))
            print("Going Left");
        else if (Input.GetKey(KeyCode.S))
            print("Going Down");
    }
}
