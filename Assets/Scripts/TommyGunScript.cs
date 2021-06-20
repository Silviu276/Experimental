using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TommyGunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed, bulletDelaySeconds, reloadTimeSeconds;
    public TMP_Text bulletsText;
    private float lastBullet = -1f;
    private int loadedBullets = 50, unloadedBullets = 100;
    private bool reloading = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if ((loadedBullets <= 0 && unloadedBullets > 0) && !reloading)
            {
                StartCoroutine(ReloadTommyGun());
            }
            else if (loadedBullets > 0 && !reloading)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && (loadedBullets < 50 && unloadedBullets > 0))
        {
            StartCoroutine(ReloadTommyGun());
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
            bulletsText.text = $"{loadedBullets} / {unloadedBullets}";
        }
    }

    private IEnumerator ReloadTommyGun()
    {
        reloading = true;
        bulletsText.text = "Reloading...";
        int bulletsDiff = 50 - loadedBullets;
        if (unloadedBullets >= bulletsDiff)
        {
            yield return new WaitForSeconds(reloadTimeSeconds);
            loadedBullets += bulletsDiff;
            unloadedBullets -= bulletsDiff;
            bulletsText.text = $"{loadedBullets} / {unloadedBullets}";
        }
        else
        {
            yield return new WaitForSeconds(reloadTimeSeconds);
            loadedBullets += unloadedBullets;
            unloadedBullets = 0;
            bulletsText.text = $"{loadedBullets} / {unloadedBullets}";
        }
        reloading = false;
    }
}
