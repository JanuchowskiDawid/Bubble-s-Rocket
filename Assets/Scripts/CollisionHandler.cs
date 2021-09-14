using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayInSec = 1f;
    [SerializeField] AudioClip succes;
    [SerializeField] AudioClip collision;
    
    AudioSource audioSource;

    public bool isTransitioning;
    // Start is called before the first frame update
    void Start()
    {
        isTransitioning = false;
        audioSource = GetComponent<AudioSource>();

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning)
        {
            switch (collision.gameObject.tag)
            {
                case "Start":
                    break;
                case "Meta":
                    LevelCompleted();
                    break;
                default:
                    StartCrash();
                    break;
            }
        }
    }

    private void LevelCompleted()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(succes);
        Invoke("LoadNextLevel", delayInSec);
        isTransitioning = true;
    }

    private void StartCrash()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(collision);
        GetComponent<Flying>().enabled = false;
        Invoke("ReloadLevel", delayInSec);
        isTransitioning = true; 
    }

    private void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene==SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
