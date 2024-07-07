using System.Collections.Generic;
using UnityEngine;

public class SpawnBlood : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private List<Sprite> bloodList = new List<Sprite>();

    private Sprite GetBloodSprite()
    {
        int random = Random.Range(0, bloodList.Count);        
        return bloodList[random];
    }

    public void InstantiateBloodObject(Vector3 spawnPosition)
    {
        Sprite sprite = GetBloodSprite();
        
        GameObject selected = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
        if (spawnParent != null) selected.transform.parent = spawnParent;
        
        selected.GetComponent<SpriteRenderer>().sprite = sprite;
    }

}
