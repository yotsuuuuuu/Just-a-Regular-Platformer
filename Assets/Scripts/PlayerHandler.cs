using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private AudioHandler audioHandler;

    private enum MovementState { idle, sideways, onHead, fallingSideways, fallingUpright, fallingHead, death}
    private MovementState state = 0;

    private bool isFlipped = false;
    private bool isDead = false;
    private bool screamed = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioHandler = GetComponent<AudioHandler>();
        anim.SetInteger("state", (int)state);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Flip();
            UpdateAnimation();
        }
        
    }

    // flips the x axis of the sprite depending on the orientation
    private void Flip()
    {
        if (0 == RoundAngle(270) && !isFlipped) // if the player has the right side as the floor. so turned right
        {
            Vector3 localScale = transform.localScale;
            isFlipped = true;
            localScale.x *= -1f;
            transform.localScale = localScale;
            Debug.Log("FLIPPED FLOOR RIGHT");
            
        }else if ((180 == RoundAngle(270) || -180 == RoundAngle(270)) && isFlipped) // if the player has the left side as the floor. so turned left
        {
            Vector3 localScale = transform.localScale;
            isFlipped = false;
            localScale.x *= -1f;
            transform.localScale = localScale;
            Debug.Log("FLIPPED FLOOR LEFT");

        }

    }

    private void UpdateAnimation()
    {                   
        // if the player is moving
        if (rb.velocity.y < -.1f)
        {
            if (0 == RoundAngle(270) || 180 == RoundAngle(270) || -180 == RoundAngle(270)) // either of these angles means sideways either left or right
            {
                state = MovementState.fallingSideways;

            }else if (-90 == RoundAngle(270)) // player is standing
            {
                state = MovementState.fallingUpright;

            }else if (90 == RoundAngle(270)) //player is upside down
            {
                state = MovementState.fallingHead;
            }

            if (rb.velocity.y < -5f && !screamed)
            {
                if (Random.Range(0, 11) <= 7)
                {
                    audioHandler.Scream();
                }
                screamed = true;
            }

        }
        else // if the player is not moving
        {
            //Debug.Log(Mathf.DeltaAngle(transform.eulerAngles.z, 270));

            if (0 == RoundAngle(270) || 180 == RoundAngle(270) || -180 == RoundAngle(270)) //sideways
            {
                state = MovementState.sideways;
            }
            else if (-90 == RoundAngle(270)) // standing
            {
                state = MovementState.idle;
            }
            else if (90 == RoundAngle(270))// upside down
            {
                state = MovementState.onHead;
            }


            if (screamed)
            {
                screamed = false;
            }
        }


        anim.SetInteger("state",(int)state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        rb.isKinematic = true;
        isDead = true;
        anim.SetInteger("state",6);
        audioHandler.Ouche();
        //rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        rb.isKinematic = false;
        isDead = false;
    }

    //round the difference between the current angle and the given angle to the nearest integer
    public float RoundAngle(int angle)
    {
        return Mathf.Round(Mathf.DeltaAngle(transform.eulerAngles.z, angle));
    }

}
