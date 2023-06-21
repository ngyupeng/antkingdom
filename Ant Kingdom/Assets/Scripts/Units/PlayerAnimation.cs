using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public string[] staticDirections = {"Worker NW", "Worker SW", "Worker SE", "Worker NE"};
    public string[] walkingDirections = {"Worker NW Walking", "Worker SW Walking", "Worker SE Walking", "Worker NE Walking"};
    private Rigidbody2D rb;

    int lastDirection;
 
    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    
    }

    public void SetDirection(Vector2 _direction)
    { 
        string[] directionArray = null;

        if(_direction.magnitude < 0.01)
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray =  walkingDirections;
            lastDirection = DirectionToIndex(_direction);
        }
        
        anim.Play(directionArray[lastDirection]);
    }

    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;
        float step = 360/4;
        // float offset = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, norDir);
        // angle += offset;
        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }

    void FixedUpdate()
    {
        Vector2 direction = rb.velocity;
        SetDirection(direction);
    }


}
