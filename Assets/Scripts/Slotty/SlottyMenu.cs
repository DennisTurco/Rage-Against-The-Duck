using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlottyMenu : MonoBehaviour
{
    [SerializeField] private List<SlottyReward> rewardList;
    [SerializeField] private Button spinButton;
    [SerializeField] private List<Image> slotImages; // Gli elementi della slot machine da animare
    [SerializeField] private int spinCost = 10; // Costo di uno spin

    private bool isSpinning = false;

    private void Start()
    {
        spinButton.onClick.AddListener(SpinSlotMachine);
        UpdateSpinButtonState();
    }

    private void Update()
    {
        // Abilita il pulsante solo se il giocatore ha abbastanza monete e non sta girando
        UpdateSpinButtonState();
    }

    private void UpdateSpinButtonState()
    {
        spinButton.interactable = !isSpinning && GameManager.Instance.coins >= spinCost;
    }

    private void SpinSlotMachine()
    {
        if (isSpinning) return;

        int playerCoins = GameManager.Instance.coins;
        if (playerCoins >= spinCost)
        {
            ItemCoin.UseItemCoin(spinCost);
            StartCoroutine(SpinAnimation());
        }
        else
        {
            Debug.Log("Not enough coins to spin.");
        }
    }

    private IEnumerator SpinAnimation()
    {
        isSpinning = true;
        UpdateSpinButtonState(); // Aggiorna lo stato del pulsante

        // Simula il giro della slot machine
        for (int i = 0; i < slotImages.Count; i++)
        {
            SlottyReward randomReward = rewardList[Random.Range(0, rewardList.Count)];
            slotImages[i].sprite = GetSpriteForItem(randomReward.itemName);
        }

        // Simula un breve ritardo prima di mostrare i risultati
        yield return new WaitForSeconds(1.5f);

        List<SlottyReward> awardedRewards = DetermineRewards();
        GivePlayerRewards(awardedRewards);

        isSpinning = false;
        UpdateSpinButtonState(); // Aggiorna lo stato del pulsante
    }

    private List<SlottyReward> DetermineRewards()
    {
        List<SlottyReward> awardedRewards = new List<SlottyReward>();

        foreach (var reward in rewardList)
        {
            float randomProbability = Random.value;

            if (randomProbability <= reward.probability)
            {
                awardedRewards.Add(reward);
            }
        }

        return awardedRewards;
    }

    private void GivePlayerRewards(List<SlottyReward> awardedRewards)
    {
        if (awardedRewards == null || awardedRewards.Count == 0) return;

        foreach (var reward in awardedRewards)
        {
            switch (reward.itemName)
            {
                case ItemName.Coin:
                    ItemCoin.CollectItemCoin(reward.rewardQuantity);
                    break;
                case ItemName.Bomb:
                    ItemBomb.CollectItemBomb(reward.rewardQuantity);
                    break;
                case ItemName.Minion:
                    ItemMinion.CollectItemMinion(reward.rewardQuantity);
                    break;
                case ItemName.FullHeart:
                    ItemHeart.CollectItemHeart(reward.rewardQuantity);
                    break;
                case ItemName.HalfHeart:
                    break;
                case ItemName.Key:
                    break;
                default:
                    Debug.LogError("Reward type not recognized: " + reward.itemName);
                    break;
            }
        }
    }

    private Sprite GetSpriteForItem(ItemName itemName)
    {
        foreach (var reward in rewardList)
        {
            if (reward.itemName == itemName)
            {
                // Assume che gli sprite siano associati ai nomi degli oggetti nella tua logica di gioco
                return reward.itemSprite;
            }
        }
        return null;
    }
}
