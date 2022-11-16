using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaPos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); 
        transform.position += speed * Time.deltaTime * deltaPos;
    }

    [SerializeField] private float speed = 20.0f;
}
