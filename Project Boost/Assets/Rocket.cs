using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

    public enum State { Alive, Transcending, Dead };

    public State state;

    private int currentLvl = 0;

    [SerializeField]
    float mainThrust = 100f;

    [SerializeField]
    float rcsThrust = 100f;

    [SerializeField]
    AudioClip mainEngine;

    [SerializeField]
    AudioClip winSound;

    [SerializeField]
    AudioClip deathSound;

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
                print("Friendly landing pad.");
                break;
            case "Finish":
                state = State.Transcending;
                rigidBody.constraints = RigidbodyConstraints.FreezeAll;
                audioSource.Stop();
                Invoke("LoadNextScene", 2f);
                audioSource.PlayOneShot(winSound);
                break;
            default:
                state = State.Dead;
                audioSource.Stop();
                Invoke("LoadCurrentScene", 2f);
                audioSource.PlayOneShot(deathSound);
                break;
        }
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentLvl);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(currentLvl + 1);
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
    }
}
