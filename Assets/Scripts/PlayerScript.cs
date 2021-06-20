using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int health = 9;
    public GameObject healthBar;
    [SerializeField] private GameObject[] healthPoints;

    private void Start()
    {
        HealthInit();
    }

    public void HealthBarUpdate()
    {

        for(int i = 0; i < 10; i++)
        {
            if (i <= health)
                healthPoints[i].SetActive(true);
            else
                healthPoints[i].SetActive(false);
        }
    }

    private void HealthInit()
    {
        for (int i = 0; i < 10; i++)
        {
            healthPoints[i] = healthBar.transform.Find($"HP{i}").gameObject;
            //Debug.Log(healthBar.transform.Find($"HP{i}").gameObject.name);
        }
    }
}
