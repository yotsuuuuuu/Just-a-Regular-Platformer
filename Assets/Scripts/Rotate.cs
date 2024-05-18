using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float angle;
    private float rotationInput;
    private float rotationCD = 0f;
    private float coolDown = .5f;
    [SerializeField] private PlayerRotate player;

    private bool isrotating = false;
    //values for lerp
    float _intervaule = 0.0f;
    float _target = 1.0f;
    float _currentAngle;
    Rigidbody2D body;
    private float speed = .2f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // looking for left or right key input 
        if (Input.anyKeyDown)
        {
            rotationInput = Input.GetAxis("Horizontal");
        }
        
        if (rotationInput > 0 && rotationCD <= 0 && !isrotating)
        {
            angle = -90.0f;
            rotationCD = coolDown;
            isrotating = true;
            _currentAngle = body.rotation;
            _target = _currentAngle + angle;
        }
        else if (rotationInput < 0 && rotationCD <= 0 && !isrotating)
        {
            angle = 90.0f;
            rotationCD = coolDown;
            isrotating = true;
            _currentAngle = body.rotation;
            _target = _currentAngle + angle;
        }
        else
        {
            rotationInput = 0.0f;
        }
        // looking for up or donw key input 
        if (Input.anyKeyDown)
        {
            rotationInput = Input.GetAxis("Vertical");
        }

        if (rotationInput > 0 && rotationCD <= 0 && !isrotating)
        {
            angle = -180.0f;
            rotationCD = coolDown;
            isrotating = true;
            _currentAngle = body.rotation;
            _target = _currentAngle + angle;
        }
        else if (rotationInput < 0 && rotationCD <= 0 && !isrotating)
        {
            angle = 180.0f;
            rotationCD = coolDown;
            isrotating = true;
            _currentAngle = body.rotation;
            _target = _currentAngle + angle;
        }
        else
        {
            rotationInput = 0.0f;
        }



        Debug.Log(rotationCD);

        if (rotationCD > 0)// cool down update
        {
            rotationCD -= Time.deltaTime;
        }
        
        
    }
    private void FixedUpdate()
    {
        
        if (isrotating)
        {


            float resultAngle = Mathf.Lerp(0, angle, speed);

            transform.RotateAround(transform.position, Vector3.forward, resultAngle);
            player.Rotate(resultAngle);
            
            _intervaule += resultAngle;
            
           
            //Debug.Log("current angel: " + _currentAngle+ " target angle: " + _target +"\n");
            //Debug.Log("Angle rotating by");
            //Debug.Log(resultAngle);
            

            if (Mathf.Abs(_intervaule) >= Mathf.Abs (angle)) 
            {
                isrotating = false;
                _intervaule = 0.0f;
            }
        }

    }
}
