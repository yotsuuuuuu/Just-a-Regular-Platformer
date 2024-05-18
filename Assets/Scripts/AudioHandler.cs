using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{

    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip[] screams;
    [SerializeField] private AudioClip[] ouche;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    public void Scream()
    {
        source.PlayOneShot(screams[Random.Range(0, screams.Length)]);
    }

    public void Ouche()
    {
        source.PlayOneShot(ouche[Random.Range(0, ouche.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
