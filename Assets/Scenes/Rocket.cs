using System.Collections;
using System.Collections.Generic;
using UnityEngine;// It provides methods, variables, and variable type In unity Engine, so we dont need to define those things as it is already there. 
using UnityEngine.SceneManagement;//'UnityEngine.SceneManagement' is for managing scenes

public class Rocket : MonoBehaviour
{   //Member Variables Declared Below
    //The [SerializeField] attribute is used to mark non-public fields as serializable: so that Unity can save and load those values
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rcsThrust = 10f; //"[SerializeFlield]" is to load values in the engine with the name of variable declared
    [SerializeField] float LevelLoadTime = 2f;

    [SerializeField] AudioClip mainEngine; //AudioClip is a Variable type Given by unity which itself takes audio in the engine not values
    [SerializeField] AudioClip deathAudio;
    [SerializeField] AudioClip levelclear;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    AudioSource audioSource;// It is same as Rigidbody 
    Rigidbody rigidBody;
    ParticleSystem RocketJetParticles;
    bool collisionDisabled = false; 
    
 //'Rigidbody' is a variable type provided by unity. As it is a component added by us in the unity Engine
 // 'rigidBody' is the name of a member variable of type 'Rigidbody'.
  enum State { Alive, Dying, Transcending}
    State state = State.Alive;
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
        if (state == State.Alive)
        {
            OnUserInput();
            PlayerRotation();
        }
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        { collisionDisabled = !collisionDisabled; }
                                              //For toggling the collision of game object
    }

  //"(Input.GetKey(KeyCode.Input))" is the syntax for getting the input from the user
  //"Input. GetKey" will repeatedly return the value which the user holds down the specified key.
  //'KeyCode.' is a command for specifying the key pressed.
  //"rigidBody.AddRelativeForce(Vector3.direction);" syntax for making an object or player move
  //rigidBody is the variable name used here. Any variable name can be used here at its place.
    void OnUserInput() 
    { 
        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying) //here '!' means not. Here not can also be shown as '(audio.isPlaying == false)'.
            {
                audioSource.PlayOneShot(mainEngine); //To remember - for playing an audio added in the engine inspector use '.PlayOneshot(variableName)'
            }
            mainEngineParticles.Play();
        }
        else if (Input.GetKey(KeyCode.S))
            rigidBody.AddRelativeForce(Vector3.down * mainThrust);

        else if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeForce(Vector3.right * mainThrust);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        else if (Input.GetKey(KeyCode.A))
            audioSource.Play();

        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeForce(Vector3.left * mainThrust);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.L)) 
        {
            ChangingScene();
        }

        else
        {            
            audioSource.Stop();
        }
        PlayerRotation();
    }
         
    void PlayerRotation()
    {
        rigidBody.freezeRotation = true; // for taking manual rotation control
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.back * rcsThrust);

        else if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.forward * rcsThrust);
        rigidBody.freezeRotation = false; // giving rotation control back to physics
    }

    // 'Collison' is a variable type provided by unity for collision of any game object
    void OnCollisionEnter(Collision collision)// Method to respond on collision provided by Unity
    {// For Tagging we give the game object a Tag name, and to make it respond on an input we use switch statement

        if (state != State.Alive || collisionDisabled)                // '!=' - it is 'not equal to'
        { return; }
                                 // '||' Means 'or'   //'return' is a command telling the method not to give output further than that if the condition follows
        switch (collision.gameObject.tag)  //Tagging Game Objects,Syntax-'(variableName.gameObject.tag)'
        {
            case "Friendly": 
                break;
            case "Friendly Obstacle":
                break;
            case "Finish 1":
                GoToLevel2();
                break;
            case "Finish 2":
                GoToLevel3();
                break;
            case "Finish 3":
                GoToLevel4();
                break;
            case "Finish 4":
                GoToLevel5();
                break;
            case "Finish 5":
                GoToLevel1();
                break;
            default:
                DeathSequence();
                break;
        }
    }

    void DeathSequence()
    {
        print("Dead");
        state = State.Dying;
        deathParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(deathAudio);
        Invoke("ChangingScene2", LevelLoadTime);
    }

    void GoToLevel2()
    {
        state = State.Transcending;//'Invoke' is a key word to execute a method after some time        
        audioSource.Stop();
        audioSource.PlayOneShot(levelclear);
        successParticles.Play();
        Invoke("ChangingScene", 2f);//Syntax to use invoke 'Invoke("MethodName", Time);' (Time should be in seconds and the value should be a float value)
        print("Finish");
    }

    void ChangingScene2()
    {        
        SceneManager.LoadScene(0);
    }

    void ChangingScene()//"buildIndex"Returns the index of the Scene in the Build Settings.
    {//"GetActiveScene" Gets the currently active Scene.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) { nextSceneIndex = 0; }// Loop Back to Scene 0 or Level 1
        SceneManager.LoadScene(nextSceneIndex);
        //"sceneCountInBuildSettings" Last Scene in Build Settings.
        //'SceneManager' is to use the 'UnityEngine.SceneManagement' package, which is for managemnt of the scenes added to builded scenes
        //'.LoadScene()' is a command given to the system to load the scene number specified in the bracket
        // syntax to change scene in unity 'SceneManager.LoadScene();'
    }
    void GoToLevel3()
    {
        state = State.Transcending;//'Invoke' is a key word to execute a method after some time        
        audioSource.Stop();
        audioSource.PlayOneShot(levelclear);
        successParticles.Play();
        Invoke("ChangingScene3", 2f);//Syntax to use invoke 'Invoke("MethodName", Time);' (Time should be in seconds and the value should be a float value)
        print("Finish");
    }

    void ChangingScene3()
    {
        SceneManager.LoadScene(2);
    }

    void GoToLevel4()
    {
        state = State.Transcending;//'Invoke' is a key word to execute a method after some time        
        audioSource.Stop();
        audioSource.PlayOneShot(levelclear);
        successParticles.Play();
        Invoke("ChangingScene4", 2f);//Syntax to use invoke 'Invoke("MethodName", Time);' (Time should be in seconds and the value should be a float value)
        print("Finish");
    }
    void ChangingScene4()
    {
        SceneManager.LoadScene(3);
    }

    void GoToLevel5()
    {
        state = State.Transcending;//'Invoke' is a key word to execute a method after some time        
        audioSource.Stop();
        audioSource.PlayOneShot(levelclear);
        successParticles.Play();
        Invoke("ChangingScene5", 2f);//Syntax to use invoke 'Invoke("MethodName", Time);' (Time should be in seconds and the value should be a float value)
        print("Finish");
    }

    void ChangingScene5()
    {
        SceneManager.LoadScene(4);
    }

    void GoToLevel1() 
    {
        state = State.Transcending;//'Invoke' is a key word to execute a method after some time        
        audioSource.Stop();
        audioSource.PlayOneShot(levelclear);
        successParticles.Play();
        Invoke("ChangingScene2", 2f);//Syntax to use invoke 'Invoke("MethodName", Time);' (Time should be in seconds and the value should be a float value)
        print("Finish");
    }
}
