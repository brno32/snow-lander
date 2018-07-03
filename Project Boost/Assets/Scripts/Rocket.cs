using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Rocket : MonoBehaviour {

    public float mainThrust = 1000f;
    public float torque = 175f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    public ParticleSystem thrustParticles, winParticles, deathParticles;

    public AudioClip mainEngine, winSound, deathSound;

    private bool collisionsOff = false;

    // Use this for initialization
    void Start () {
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
            //RespondToDebugInput();
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
                PlayWinEffects();
                break;
            default:
                GameMaster.currentGameState = GameMaster.GameState.Dead;
                PlayDeathEffects();
                break;
        }
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true;
        
        // +1 when thrown right. -1 when thrown left
        float zThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        // Result is ordinarily positive, meaning counter-clockwise
        // When pressing right, you want to move clock-wise
        float rotationThisFrame = - torque * zThrow * Time.deltaTime;
        
        transform.Rotate(Vector3.forward * rotationThisFrame);

        rigidBody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {
        float thrustOn = CrossPlatformInputManager.GetAxis("Thrust");

        if (thrustOn > 0)
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            thrustParticles.Stop();
        }
    }

    //private void RespondToDebugInput()
    //{
    //    if (Input.GetKey(KeyCode.L))
    //    {
    //        //sceneLoaderScript.LoadNextScene();
    //    }
    //    if ((Input.GetKey(KeyCode.C)))
    //    {
    //        collisionsOff = !collisionsOff;
    //    }
    //}

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
        thrustParticles.Play();
    }

    public void PlayWinEffects()
    {
        FreezePlayerInput();
        audioSource.Stop();
        thrustParticles.Stop();
        winParticles.Play();
        audioSource.PlayOneShot(winSound);
    }

    public void PlayDeathEffects()
    {
        FreezePlayerInput();
        audioSource.Stop();
        thrustParticles.Stop();
        deathParticles.Play();
        audioSource.PlayOneShot(deathSound);
    }
}
