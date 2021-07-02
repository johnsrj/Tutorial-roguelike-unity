using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float lifetime;
    [SerializeField] public float speed;
    

    [SerializeField] private float damage;


    private void Awake()
    {
        StartCoroutine("DeactivateAfterSeconds");
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    IEnumerator DeactivateAfterSeconds()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
