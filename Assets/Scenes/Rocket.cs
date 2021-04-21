using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rigidBody;
 //'Rigidbody' is a variable type provided by unity. As it is a component added by us in the unity Engine
 // 'rigidBody' is the name of a member variable of type 'Rigidbody'.

    // Start is called before the first frame update
    void Start()
    {//Here we are telling machine to take value from Component Added in the engine for 'rigidBody'
     //'GetComponent<Rigidbody>();'is the syntax for the statement above
     //'GetComponent<>();' is a function for taking a value from a component added to the game object
     // Returns the component of Type if the game object has one attached, null if it doesn't.
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        {
            rigidBody.AddRelativeForce(Vector3.up);
            AudioOnUSerInput();
        }

        else if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.forward);

        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.back);

        else if (Input.GetKey(KeyCode.W))
        {   
            rigidBody.AddRelativeForce(Vector3.up);   
            if (!audioSource.isPlaying) //here '!' means not. Here not can also be shown as '(audio.isPlaying == false)'.
                audioSource.Play();
        }

        else if (Input.GetKey(KeyCode.S))
            rigidBody.AddRelativeForce(Vector3.down);

        else
            audioSource.Stop();

        OnUserInput2();
    }
    void AudioOnUSerInput() 
    {
        if (!audioSource.isPlaying) //here '!' means not. Here not can also be shown as '(audio.isPlaying == false)'.
            audioSource.Play();
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
