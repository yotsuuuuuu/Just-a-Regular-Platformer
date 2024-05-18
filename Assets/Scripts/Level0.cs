using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private AudioHandler audioHandler;
    private AudioSource audioSource;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        audioHandler = player.GetComponent<AudioHandler>();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT");
        StartCoroutine(Speak(2f));
    }

    private IEnumerator Speak(float time)
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        audioSource.Play();
        yield return new WaitForSeconds(time);
        audioHandler.Scream();
        rb.isKinematic = true;
        rb.velocity = Vector3.down;
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
