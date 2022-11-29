using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    [SerializeField] private AudioSource SliceSFX;

    private Rigidbody2D fruitRigidbody2D;
    private Collider2D fruitCollider2D;

    public Rigidbody2D[] slices;
    public ParticleSystem juiceEffect;

    public bool fruitSliced;
    private void Awake()
    {
        fruitRigidbody2D = GetComponent<Rigidbody2D>();
        fruitCollider2D = GetComponent<Collider2D>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore();
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider2D.enabled = false;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);


        slices = sliced.GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D slice in slices)
        {
            slice.velocity = fruitRigidbody2D.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode2D.Impulse);
        }
        
        
        juiceEffect.Play();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }

    private void Update()
    {
        if (fruitSliced == true)
        {
            SliceSFX.enabled = true;
            fruitSliced = false;
        }
    }
}
