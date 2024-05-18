using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float g; // gravity scale
    [SerializeField] private GameObject target;
    private Rigidbody2D rb;
    private Vector3 displacement = new Vector3(0f, 0f, 0f);
    public float maxFallSpeed = -10.0f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        
        if (rb.velocity.y < maxFallSpeed)
        {

            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }
    
    public void Rotate(float angle)
    {
        transform.RotateAround(target.transform.position, Vector3.forward, angle);
        transform.position += displacement;
        StartCoroutine(gravityWait(.2f));
    }


    IEnumerator gravityWait(float time)
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(time);
        rb.gravityScale = g;
        //Debug.Log("waited");
    }
}
