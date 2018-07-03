using UnityEngine;

public class Rocket : MonoBehaviour {

    public float mainThrust = 1000f;
    public float torque = 175f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    public enum GameState { Alive, Transcending, Dead };
    static public GameState currentGameState;

    public ParticleSystem mainEngineParticles, winParticles, deathParticles;

    public AudioClip mainEngine, winSound, deathSound;

    private bool collisionsOff = false;

    // Use this for initialization
    void Start () {
        currentGameState = GameState.Alive;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.currentGameState != GameMaster.GameState.Alive)
        {
            return;
        }

        RespondToThrustInput();
        RespondToRotateInput();

        if (Debug.isDebugBuild)
        {
            RespondToDebugInput();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameMaster.currentGameState != GameMaster.GameState.Alive)
        {
            return;
        }

        if (collisionsOff)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                GameMaster.currentGameState = GameMaster.GameState.Transcending;
                HandleWinEffects();
                break;
            default:
                GameMaster.currentGameState = GameMaster.GameState.Dead;
                HandleLoseEffects();
                break;
        }
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true;
        float rotationThisFrame = torque * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            // Do nothing if both keys pressed at once
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {
        print("Thrust printed");
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
            print("Thrust called");
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void RespondToDebugInput()
    {
        if (Input.GetKey(KeyCode.L))
        {
            //sceneLoaderScript.LoadNextScene();
        }
        if ((Input.GetKey(KeyCode.C)))
        {
            collisionsOff = !collisionsOff;
        }
    }

    public void FreezePlayerInput()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void ApplyThrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;

        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    public void HandleWinEffects()
    {
        audioSource.Stop();
        FreezePlayerInput();
        mainEngineParticles.Stop();
        winParticles.Play();
        audioSource.PlayOneShot(winSound);
    }

    public void HandleLoseEffects()
    {
        FreezePlayerInput();
        audioSource.Stop();
        mainEngineParticles.Stop();
        deathParticles.Play();
        audioSource.PlayOneShot(deathSound);
    }
}
