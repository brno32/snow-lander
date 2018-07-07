using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Rocket : MonoBehaviour {

    // EXTERNAL COMPONENTS
    Rigidbody rigidBody;
    AudioSource audioSource;
    new Renderer renderer;
    public GameObject mobileUI;

    [Header("Controller Parameters")]
    public float mainThrust = 1000f;
    public float torque = 175f;

    [Header("Particle Effects")]
    public ParticleSystem thrustFlameParticles;
    public ParticleSystem thrustSmokeParticles;
    public ParticleSystem winParticles;
    public ParticleSystem deathParticles;

    [Header("Sound Effects")]
    public AudioClip mainEngine;
    public AudioClip winSound;
    public AudioClip deathSound;

    // PRIVATE VARIABLES
    private float elapsedTime = 0;

    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
    }
    
    void Update()
    {
        if (!RocketIsAlive())
        {
            return;
        }

        RespondToThrustInput();
        RespondToRotateInput();
        CheckRocketOnScreen();
        CheckDifficulty();
    }

    void CheckDifficulty()
    {
        if (DifficultyTracker.isEasy)
        {
            rigidBody.useGravity = false;
            rigidBody.drag = 1f;
        }
        else
        {
            rigidBody.useGravity = true;
            rigidBody.drag = .25f;
        }
    }

    private void CheckRocketOnScreen()
    {
        elapsedTime += Time.deltaTime;

        // Let the rocket render before ending the game because it's not on-screen
        if (!renderer.isVisible && elapsedTime > 1f)
        {
            GameMaster.currentGameState = GameMaster.GameState.Dead;
            PlayDeathEffects();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!RocketIsAlive())
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                GameMaster.ChangeGameState(GameMaster.GameState.Transcending);
                PlayWinEffects();
                break;
            default:
                GameMaster.ChangeGameState(GameMaster.GameState.Dead);
                PlayDeathEffects();
                break;
        }
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true;
        
        // Z direction is an arrow pointing into the screen
        // +1 when thrown right. -1 when thrown left
        float zThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        // MOBILE INPUT
        if (mobileUI.activeSelf)
        {
            if (CrossPlatformInputManager.GetButton("Left") && 
                CrossPlatformInputManager.GetButton("Right"))
            {
                zThrow = 0f;
            }
            else if(CrossPlatformInputManager.GetButton("Left"))
            {
                zThrow = -1f;
            }
            else if (CrossPlatformInputManager.GetButton("Right"))
            {
                zThrow = 1f;
            }
        }

        // In this orientation, a positive vector means a counter-clockwise rotation
        // Pressing right should rotate clock-wise, so flip vector with negative sign
        float deltaRotate = -torque * zThrow * Time.deltaTime;
        
        transform.Rotate(Vector3.forward * deltaRotate);

        rigidBody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {
        float thrustInput = CrossPlatformInputManager.GetAxis("Thrust");
        
        bool thrustMobileInput = CrossPlatformInputManager.GetButton("Thrust");

        if (thrustInput > 0 || thrustMobileInput)
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            thrustFlameParticles.Stop();
            thrustSmokeParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        float deltaThrust = mainThrust * Time.deltaTime;

        rigidBody.AddRelativeForce(Vector3.up * deltaThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        thrustFlameParticles.Play();
        thrustSmokeParticles.Play();
    }

    public void PlayWinEffects()
    {
        FreezeRocket();  // Don't allow ragdoll effects on victory
        audioSource.Stop();
        thrustFlameParticles.Stop();
        thrustSmokeParticles.Stop();
        winParticles.Play();
        audioSource.PlayOneShot(winSound);
    }

    public void PlayDeathEffects()
    {
        audioSource.Stop();
        thrustFlameParticles.Stop();
        thrustSmokeParticles.Stop();
        deathParticles.Play();
        audioSource.PlayOneShot(deathSound);
    }

    public void PauseEffects()
    {
        audioSource.Stop();
        thrustFlameParticles.Stop();
        thrustSmokeParticles.Stop();
    }

    public void FreezeRocket()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private static bool RocketIsAlive()
    {
        return GameMaster.currentGameState == GameMaster.GameState.Alive;
    }
}
