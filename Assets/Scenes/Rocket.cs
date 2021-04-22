using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //Member Variables Declared Below
    //The [SerializeField] attribute is used to mark non-public fields as serializable: so that Unity can save and load those values
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rcsThrust = 100f; //"[SerializeFlield]" is to load values in the engine with the name of variable declared
    AudioSource audioSource;// It is same as Rigidbody
    Rigidbody rigidBody; 
 //'Rigidbody' is a variable type provided by unity. As it is a component added by us in the unity Engine
 // 'rigidBody' is the name of a member variable of type 'Rigidbody'.

    // Start is called before the first frame update
    void Start()
    {//Here we are telling machine to take value from Component Added in the engine for 'rigidBody'
     //'GetComponent<>();' is a function for taking a value from a component added to the game object
     // Returns the component of Type if the game object has one attached, null if it doesn't.
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //'GetComponent<Rigidbody>();'is the syntax for the statement above
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
        float movementOfFrame = mainThrust * Time.deltaTime; 
      if (Input.GetKey(KeyCode.W))
      {
            rigidBody.AddRelativeForce(Vector3.up * movementOfFrame);
            if (!audioSource.isPlaying) //here '!' means not. Here not can also be shown as '(audio.isPlaying == false)'.
                audioSource.Play();
      }

        else if (Input.GetKey(KeyCode.S))
            rigidBody.AddRelativeForce(Vector3.down * movementOfFrame);

        else if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeForce(Vector3.left * movementOfFrame);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeForce(Vector3.right * movementOfFrame);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        else
            audioSource.Stop();
        
        PlayerRotation();
    }
         
    void PlayerRotation()
    {
        float rotationOfFrame = rcsThrust * Time.deltaTime;
        rigidBody.freezeRotation = true; // for taking manual rotation control
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.forward * rotationOfFrame);

        else if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.back * rotationOfFrame);
        rigidBody.freezeRotation = false; // giving rotation control back to physics
    }
    // 'Collison' is a variable type provided by unity for collision of any game object
    void OnCollisionEnter(Collision collision)//Tagging Game Objects,Syntax-'(variableName.gameObject.tag)'
    {// For Tagging we give the game object a Tag name, and to make it respond on an input we use switch statement
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Friendly Obstacle":
                print("OK");
                break;
            default:
                print("Dead");
                break;
        }
    }
}
