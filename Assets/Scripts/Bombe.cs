using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombe : MonoBehaviour
{
    public AudioSource ExploVFX;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ExploVFX.enabled = true;
            FindObjectOfType<GameManager>().Explode();

        }
    }
}
