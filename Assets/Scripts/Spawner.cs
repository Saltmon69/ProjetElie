using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class Spawner : MonoBehaviour
{
    private Collider2D spawnArea;

    public GameObject[] fruitPrefabs;

    [SerializeField] float minSpawnDelay = 0.25f;
    [SerializeField] float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        
        while (enabled)
        {
            GameObject prefab = fruitPrefabs[UnityEngine.Random.Range(0, fruitPrefabs.Length)];

            Vector2 position = new Vector3();
            position.x = UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);

            Quaternion rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifetime);

            float force = UnityEngine.Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody2D>().AddForce(fruit.transform.up*force, ForceMode2D.Impulse);
            
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
