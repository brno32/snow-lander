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

    [Header("Controller Parameters")]
    public float mainThrust = 1000f;
    public float torque = 175f;

    [Header("Particle Effects")]
    public ParticleSystem thrustParticles;
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
    }

    private void CheckRocketOnScreen()
    {
        elapsedTime += Time.deltaTime;

        // Let the rocket load before ending the game because it's not on-screen
        if (!renderer.isVisible && elapsedTime > 2f)
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

        // Result is ordinarily positive, meaning counter-clockwise
        // When pressing right, you want to move clock-wise
        float rotationThisFrame = -torque * zThrow * Time.deltaTime;
        
        transform.Rotate(Vector3.forward * rotationThisFrame);

        rigidBody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {
        float thrustInput = CrossPlatformInputManager.GetAxis("Thrust");

        if (thrustInput > 0)
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            thrustParticles.Stop();
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
        thrustParticles.Play();
    }

    public void PlayWinEffects()
    {
        FreezeRocket();  // Don't allow ragdoll effects on victory
        audioSource.Stop();
        thrustParticles.Stop();
        winParticles.Play();
        audioSource.PlayOneShot(winSound);
    }

    public void PlayDeathEffects()
    {
        audioSource.Stop();
        thrustParticles.Stop();
        deathParticles.Play();
        audioSource.PlayOneShot(deathSound);
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
