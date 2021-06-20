using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    [SerializeField] private bool isPlayerBullet;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void Update()
    {

    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && isPlayerBullet)
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            enemy.health -= damage;
            Destroy(gameObject);
        }
        else if (other.tag == "Player" && !isPlayerBullet)
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            player.health--;
            player.HealthBarUpdate();
            Destroy(gameObject);
        }
    }
}
