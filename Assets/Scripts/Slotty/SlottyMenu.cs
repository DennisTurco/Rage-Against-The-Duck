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
    [SerializeField] private float spinDuration = 7.0f; // Durata totale dell'animazione di spin
    [SerializeField] private float initialSpinSpeed = 1000f; // Velocità iniziale di movimento delle immagini in pixel per secondo

    private bool isSpinning = false;
    private RectTransform[] imageTransforms;
    private float slotWidth;
    private int centralIndex;

    private void Start()
    {
        spinButton.onClick.AddListener(SpinSlotMachine);
        UpdateSpinButtonState();
        imageTransforms = new RectTransform[slotImages.Count];
        for (int i = 0; i < slotImages.Count; i++)
        {
            imageTransforms[i] = slotImages[i].GetComponent<RectTransform>();
        }
        slotWidth = imageTransforms[0].rect.width;
        centralIndex = slotImages.Count / 2;
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

        float elapsedTime = 0f;
        float spinSpeed = initialSpinSpeed;

        // Inizializza le immagini con ricompense casuali
        for (int i = 0; i < slotImages.Count; i++)
        {
            SlottyReward randomReward = rewardList[Random.Range(0, rewardList.Count)];
            slotImages[i].sprite = GetSpriteForItem(randomReward.itemName);
            imageTransforms[i].anchoredPosition = new Vector2(i * slotWidth, 0);
        }

        SlottyReward finalReward = rewardList[Random.Range(0, rewardList.Count)];

        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Utilizza Time.unscaledDeltaTime
            float moveAmount = spinSpeed * Time.unscaledDeltaTime; // Utilizza Time.unscaledDeltaTime per calcolare il movimento

            for (int i = 0; i < slotImages.Count; i++)
            {
                imageTransforms[i].anchoredPosition -= new Vector2(moveAmount, 0);

                if (imageTransforms[i].anchoredPosition.x <= -slotWidth)
                {
                    imageTransforms[i].anchoredPosition += new Vector2(slotWidth * slotImages.Count, 0);

                    // Per gli elementi che ritornano a destra, scegli una ricompensa casuale fino alla fine
                    SlottyReward randomReward = (elapsedTime >= spinDuration - 1.0f) ? finalReward : rewardList[Random.Range(0, rewardList.Count)];
                    slotImages[i].sprite = GetSpriteForItem(randomReward.itemName);
                }
            }

            // Rallenta gradualmente la velocità di rotazione
            spinSpeed = Mathf.Lerp(initialSpinSpeed, 0, elapsedTime / spinDuration);

            yield return null; // Aspetta il prossimo frame
        }

        // Assicurati che l'immagine vincente sia centrata
        float finalPositionX = -slotWidth * centralIndex;
        for (int i = 0; i < slotImages.Count; i++)
        {
            imageTransforms[i].anchoredPosition = new Vector2(finalPositionX + (i * slotWidth), 0);
            if (i == centralIndex)
            {
                slotImages[i].sprite = GetSpriteForItem(finalReward.itemName);
            }
        }

        List<SlottyReward> awardedRewards = new List<SlottyReward> { finalReward };
        GivePlayerRewards(awardedRewards);

        isSpinning = false;
        UpdateSpinButtonState(); // Aggiorna lo stato del pulsante
    }

    private void GivePlayerRewards(List<SlottyReward> awardedRewards)
    {
        if (awardedRewards == null || awardedRewards.Count == 0)
        {
            Debug.Log("No rewards to give.");
            return;
        }

        foreach (var reward in awardedRewards)
        {
            Debug.Log($"Awarding reward: {reward.itemName} x {reward.rewardQuantity}");
            switch (reward.itemName)
            {
                case ItemName.Coin:
                    Debug.Log("Player has been scammed by Slotty");
                    break;
                case ItemName.Bomb:
                    ItemBomb.CollectItemBomb(reward.rewardQuantity);
                    Debug.Log("Player won Slotty BOMB jackpot");
                    break;
                case ItemName.Minion:
                    ItemMinion.CollectItemMinion(reward.rewardQuantity);
                    Debug.Log("Player won Slotty MINION jackpot");
                    break;
                case ItemName.FullHeart:
                    ItemHeart.CollectItemHeart(reward.rewardQuantity);
                    Debug.Log("Player won Slotty FULL HEART jackpot");
                    break;
                case ItemName.HalfHeart:
                    // Handle half heart reward here if applicable
                    Debug.Log("Player won Slotty HALF HEART jackpot");
                    break;
                case ItemName.Key:
                    // Handle key reward here if applicable
                    Debug.Log("Player won Slotty KEY jackpot");
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
        Debug.LogError("Sprite not found for item: " + itemName);
        return null;
    }
}
