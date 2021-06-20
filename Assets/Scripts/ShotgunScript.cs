using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShotgunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed, bulletDelaySeconds, reloadTimeSeconds;
    public TMP_Text bulletsText;
    private float lastBullet = -1f;
    private int loadedBullets = 5, unloadedBullets = 10;
    private bool reloading = false;

    private void Start()
    {
        bulletsText.text = $"{loadedBullets} / {unloadedBullets}";
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if ((loadedBullets <= 0 && unloadedBullets > 0) && !reloading)
            {
                StartCoroutine(ReloadShotgun());
            }
            else if (loadedBullets > 0 && !reloading)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && (loadedBullets < 5 && unloadedBullets > 0))
        {
            StartCoroutine(ReloadShotgun());
        }
    }

    private void Shoot()
    {
        if (Time.time - lastBullet > bulletDelaySeconds && loadedBullets > 0)
        {
            GameObject topBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            GameObject midBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            GameObject downBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            topBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.parent.localScale.x * 2f * bulletSpeed, 2.5f), ForceMode2D.Impulse);
            midBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.parent.localScale.x * 2f * bulletSpeed, 0f), ForceMode2D.Impulse);
            downBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.parent.localScale.x * 2f * bulletSpeed, -2.5f), ForceMode2D.Impulse);

            lastBullet = Time.time;
            loadedBullets--;
            bulletsText.text = $"{loadedBullets} / {unloadedBullets}";
        }
    }

    private IEnumerator ReloadShotgun()
    {
        reloading = true;
        bulletsText.text = "Reloading...";
        int bulletsDiff = 5 - loadedBullets;
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
