using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    public float mainThrust = 100f;
    public float rcsThrust = 100f;

    public AudioClip mainEngine;
    public AudioClip winSound;
    public AudioClip deathSound;

    public ParticleSystem mainEngineParticles;
    public ParticleSystem winParticles;
    public ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    public enum State { Alive, Transcending, Dead };

    public State state;

    private int currentLvl = 0; 

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentLvl = SceneManager.GetActiveScene().buildIndex;
        state = State.Alive;
    }
	
	// Update is called once per frame
	void Update () {

        if (state != State.Alive)
        {
            return;
        }
        
        RespondToThrustInput();
        RespondToRotateInput();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartWinSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartWinSequence()
    {
        state = State.Transcending;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        audioSource.Stop();
        winParticles.Play();
        Invoke("LoadNextScene", 2f);
        audioSource.PlayOneShot(winSound);
    }

    private void StartDeathSequence()
    {
        state = State.Dead;
        audioSource.Stop();
        deathParticles.Play();
        Invoke("LoadCurrentScene", 2f);
        audioSource.PlayOneShot(deathSound);
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentLvl);
    }

    private void LoadNextScene()
    {
        if (currentLvl < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentLvl + 1);
        }
        else
        {
            print("No more levels. You win!");
        }
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;

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
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
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
}
