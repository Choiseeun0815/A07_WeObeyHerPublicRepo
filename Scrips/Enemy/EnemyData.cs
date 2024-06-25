using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    // Enemy Move 

    public float moveSpeed = 3.0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 dir)
    {
        if(rb == null) { return; }

        rb.velocity = dir * moveSpeed;
    }
}
