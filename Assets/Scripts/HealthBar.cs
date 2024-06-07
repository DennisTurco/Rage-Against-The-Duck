using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] PlayerHealth playerHealth;
    private List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable()
    {
        PlayerHealth.UpdateHealthHeartBar += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerHealth.UpdateHealthHeartBar -= DrawHearts;
    }

    private void Start()
    {
        DrawHearts();
    }

    // if my health is 8, i have to draw 4 hearts
    public void DrawHearts()
    {
        ClearHearts();

        // determine how many hearts to make total
        //based off the max health
        float maxHealthRemainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth / 2) + maxHealthRemainder);
        for  (int i = 0; i < heartsToMake; ++i)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; ++i)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    // method that allows to remove all the heart game object under the HealthBar object
    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }


}
