using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinScreen : MonoBehaviour
{
    public GameObject winMenuUI;
    private AudioSource winSource;
    //public static bool GamePause = false;
    // Start is called before the first frame update
    void Start()
    {
        winMenuUI.SetActive(false);
        Debug.Log("false");
        winSource = GetComponent<AudioSource>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex == 10)
        {
            winMenuUI.SetActive(true);
            winSource.Play();
            Time.timeScale = 0f;
            //GamePause = true;

        }



    }
    public void tryAgain()
    {
        winMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        //GamePause = false;
        SceneManager.LoadSceneAsync(2);

    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
