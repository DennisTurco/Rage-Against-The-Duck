using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlottyMenu : MonoBehaviour
{
    [SerializeField] private List<SlottyReward> rewardList;
    [SerializeField] private Button spinButton;
    [SerializeField] private List<Image> slotImages; // Slot machine elements to animate
    [SerializeField] private int spinCost = 10; // Spin cost
    [SerializeField] private float spinDuration = 7.0f; // Total animation duration
    [SerializeField] private float initialSpinSpeed = 1000f; // Initial movement speed of images in pixels per second

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
        Debug.Log("Slot machine initialized.");
    }

    private void Update()
    {
        // Enable the button only if the player has enough coins and is not spinning
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
            foreach (var image in slotImages)
            {
                image.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f); // Ancoraggio minimo
                image.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f); // Ancoraggio massimo
            }
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
        UpdateSpinButtonState(); // Update button state

        float elapsedTime = 0f;
        float spinSpeed = initialSpinSpeed;

        // Initialize images with random rewards
        for (int i = 0; i < slotImages.Count; i++)
        {
            SlottyReward randomReward = rewardList[Random.Range(0, rewardList.Count)];
            slotImages[i].sprite = GetSpriteForItem(randomReward.itemName);
            imageTransforms[i].anchoredPosition = new Vector2((i - centralIndex) * slotWidth, 0);
            Debug.Log($"Image {i} initialized with reward: {randomReward.itemName}");
        }

        SlottyReward finalReward = rewardList[Random.Range(0, rewardList.Count)];
        Debug.Log($"Final reward set to: {finalReward.itemName}");

        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Use Time.unscaledDeltaTime
            float moveAmount = spinSpeed * Time.unscaledDeltaTime; // Calculate movement using Time.unscaledDeltaTime

            for (int i = 0; i < slotImages.Count; i++)
            {
                imageTransforms[i].anchoredPosition -= new Vector2(moveAmount, 0);

                if (imageTransforms[i].anchoredPosition.x <= -slotWidth)
                {
                    imageTransforms[i].anchoredPosition += new Vector2(slotWidth * slotImages.Count, 0);

                    // For items returning to the right, choose a random reward until the end
                    SlottyReward randomReward = (elapsedTime >= spinDuration - 1.0f) ? finalReward : rewardList[Random.Range(0, rewardList.Count)];
                    slotImages[i].sprite = GetSpriteForItem(randomReward.itemName);
                    Debug.Log($"Image {i} reinitialized with reward: {randomReward.itemName}");
                }
            }

            // Gradually slow down the rotation speed
            spinSpeed = Mathf.Lerp(initialSpinSpeed, 0, elapsedTime / spinDuration);

            yield return null; // Wait for the next frame
        }

        // Smoothly move the winning image to the center
        float targetPositionX = -slotWidth * centralIndex;
        for (int i = 0; i < slotImages.Count; i++)
        {
            float currentPositionX = imageTransforms[i].anchoredPosition.x;
            float newPositionX = Mathf.Lerp(currentPositionX, targetPositionX + (i * slotWidth), 0.5f); // Adjust the interpolation factor as needed
            imageTransforms[i].anchoredPosition = new Vector2(newPositionX, 0);
            if (i == centralIndex)
            {
                slotImages[i].sprite = GetSpriteForItem(finalReward.itemName);
                Debug.Log($"Winning image centered: {finalReward.itemName}");
                //for (int j = 0 ; j < slotImages.Count; ++j)
                //{
                //    if (j == 0 || j == slotImages.Count - 1)
                //    {
                //        continue;
                //    }
                //    slotImages[j].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f); // Ancoraggio minimo
                //    slotImages[j].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f); // Ancoraggio massimo
                //}
            }
        }

        List<SlottyReward> awardedRewards = new List<SlottyReward> { finalReward };
        GivePlayerRewards(awardedRewards);

        isSpinning = false;
        UpdateSpinButtonState(); // Update button state
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
                // Assume that sprites are associated with item names in your game logic
                return reward.itemSprite;
            }
        }
        Debug.LogError("Sprite not found for item: " + itemName);
        return null;
    }
}
