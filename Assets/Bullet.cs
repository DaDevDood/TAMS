using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;
    public ParticleSystem bloodEffect;
    public ParticleSystem HitObstacleEffect;
    //public BaseEnemyAI baseEnemyAI;

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    void Update()
    {
        transform.Translate(-Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*if (other.gameObject.TryGetComponent<BaseEnemyAI>(out BaseEnemyAI enemyComponent))
        {
            Debug.Log("Enemy took damage");
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            enemyComponent.TakeDamage(10);
            Destroy(gameObject);
        }

        Instantiate(HitObstacleEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        */
        
        
    }
}
