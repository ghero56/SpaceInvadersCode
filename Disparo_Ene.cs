﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo_Ene : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float velocidad = 10;

    public Sprite ExplosionNave1;

    public Sprite ExplosionNave2;

    public AudioSource explosion;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.down * velocidad;

        explosion = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Muros")
            Destroy(gameObject);

        if (col.gameObject.tag == "Player")
            explosion.Play();
            Destroy(gameObject);
    }

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

}