using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    Rigidbody rigidbdy;
    AudioSource audioSource;
    [SerializeField] float acceleration = 100f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip engine;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    // Start is called before the first frame update
    void Start()
    {
        rigidbdy = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<CollisionHandler>().isTransitioning)
        {
            Fly();
            Rotate();
        }
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
            Rotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotation(-rotationSpeed);
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    private void Rotation(float rotationThisFrame)
    {
        rigidbdy.freezeRotation = true;
        transform.Rotate(0, 0, rotationThisFrame * Time.deltaTime);
        rigidbdy.freezeRotation = false;
    }

    private void Fly()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbdy.AddRelativeForce(0, acceleration*Time.deltaTime, 0);
            if (!audioSource.isPlaying && !GetComponent<CollisionHandler>().isTransitioning)
            {
                audioSource.PlayOneShot(engine);
            }
            if (!mainBooster.isPlaying)
            {
                mainBooster.Play();
            }
        }
        else
        {
            mainBooster.Stop();
            audioSource.Pause();
        }
    }
}
