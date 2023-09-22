using System.Collections.Generic;
using UnityEngine;

public class SpawnBlood : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<Sprite> bloodList = new List<Sprite>();

    private Sprite GetBloodSprite()
    {
        int random = Random.Range(0, bloodList.Count);

        Debug.Log(random);
        
        return bloodList[random];
    }

    public void InstantiateBloodObject(Vector3 spawnPosition)
    {
        Sprite sprite = GetBloodSprite();

        GameObject selected = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
        selected.GetComponent<SpriteRenderer>().sprite = sprite;
    }

}
