using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space]
    [SerializeField] private float speed = 1f;

    [Space]
    [SerializeField] private float RotationSpeed = 1f;

    Vector3 firstTouchPos = Vector3.zero;
    Vector3 deltaTouchPos = Vector3.zero;
    Vector3 direction = Vector3.zero;

    bool IsWalkable = false;

    Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update(){
        Rotate();
        Move();
    }

    void FixedUpdate()
    {
       TakeInput();
    }
    private void Rotate()     {         
        if (IsWalkable)         
        {             
             //slow version   
            if (direction.magnitude > .5f)             {                 
                var toRotation = Quaternion.LookRotation(transform.position + direction * speed);
                             
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);             
            }                        
        }     
    }

    private void TakeInput()     {         
        if (Input.GetMouseButtonDown(0))         {             
            firstTouchPos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);         
        }         
        if (Input.GetMouseButton(0))         {             
            direction = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y) - firstTouchPos;             
            IsWalkable = true;          
        }         
        if (Input.GetMouseButtonUp(0))         {             
            direction = Vector3.zero;            
            IsWalkable = false;         
        }     
    }

    private void Move(){         
        if (IsWalkable){             
            if (direction.magnitude > .5f){
                //rb.velocity = (direction.normalized) * speed;                 
                body.velocity=direction.normalized * speed;             
            }          
        }     
    }
}
