using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Loading Next scene");

            if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).IsValid())
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
