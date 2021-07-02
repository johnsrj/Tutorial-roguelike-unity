using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private InputActions m_InputActions;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private Transform spawnPoint;
    
    [SerializeField] private int poolSize;
    private Queue<GameObject> projectilePool;
    
    void Awake()
    {
        m_InputActions = new InputActions();
        m_InputActions.Player.Shoot.performed += _ => ShootProjectile();
        
        projectilePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject newProjectile = Instantiate(shootPrefab, spawnPoint.position, transform.rotation);
            Projectile projectileComponent = newProjectile.GetComponent<Projectile>();

            projectilePool.Enqueue(newProjectile);
            newProjectile.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        m_InputActions.Enable();
    }

    private void OnDisable()
    {
        m_InputActions.Disable();
    }

    void Update()
    {
        AdjustRotation();
    }

    private void AdjustRotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = rotation;
    }

    private void ShootProjectile()
    {
        GameObject projectile = projectilePool.Dequeue();

        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = transform.rotation;
        
        projectile.SetActive(true);
        projectilePool.Enqueue(projectile);
    }
}
