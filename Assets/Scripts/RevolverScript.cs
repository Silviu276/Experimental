using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RevolverScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed, bulletDelaySeconds, reloadTimeSeconds;
    public TMP_Text bulletsText;
    private float lastBullet = -1f;
    private int loadedBullets = 6;
    private bool reloading = false;

    private void Start()
    {
        bulletsText.text = $"{loadedBullets} / inf";
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (loadedBullets <= 0 && !reloading)
            {
                StartCoroutine(ReloadRevolver());
            }
            else if (loadedBullets > 0 && !reloading)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && loadedBullets < 6)
        {
            StartCoroutine(ReloadRevolver());
        }
    }

    private void Shoot()
    {
        if (Time.time - lastBullet > bulletDelaySeconds && loadedBullets > 0)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.parent.localScale.x * 2f * bulletSpeed, 0f), ForceMode2D.Impulse);

            lastBullet = Time.time;
            loadedBullets--;
            bulletsText.text = $"{loadedBullets} / inf";
        }
    }

    private IEnumerator ReloadRevolver()
    {
        bulletsText.text = "Reloading...";
        reloading = true;
        yield return new WaitForSeconds(reloadTimeSeconds);
        loadedBullets = 6;
        bulletsText.text = $"{loadedBullets} / inf";
        reloading = false;
    }
}
