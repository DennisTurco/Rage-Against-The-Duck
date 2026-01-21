using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlottyMenu : MonoBehaviour
{
    [SerializeField] private List<SlottyReward> rewardList;
    [SerializeField] private Button spinButton;
    [SerializeField] private List<Image> slotImages;
    [SerializeField] private int spinCost = 10;
    [SerializeField] private float spinDuration = 7.0f;
    [SerializeField] private float slotSpacing = 20f;
    [SerializeField] private float initialSpinSpeed = 1000f;
    [SerializeField] private TMP_Text spinPriceText;
    [SerializeField] private RectTransform viewport;

    private float halfViewport;
    private float wrapLeft;
    private float wrapRight;
    private float centerX;

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
            imageTransforms[i].anchorMin = imageTransforms[i].anchorMax = new Vector2(0.5f, 0.5f);
            imageTransforms[i].pivot = new Vector2(0.5f, 0.5f);
        }

        slotWidth = imageTransforms[0].rect.width;
        centralIndex = slotImages.Count / 2;

        RectTransform slotsParent = imageTransforms[0].parent as RectTransform;
        Vector3[] w = new Vector3[4];
        viewport.GetWorldCorners(w);
        Vector3 worldCenter = (w[0] + w[2]) * 0.5f;
        centerX = slotsParent.InverseTransformPoint(worldCenter).x;

        ApplyPositions(0f);
        UpdateSpinPriceText();
    }

    private void Update()
    {
        UpdateSpinButtonState();
    }

    private void UpdateSpinButtonState()
    {
        spinButton.interactable = !isSpinning && GameManager.Instance.coins >= spinCost;
        //UpdateSpinPriceText();   if spinCost can change after a bet
    }

    private void SpinSlotMachine()
    {
        if (isSpinning) return;
        if (GameManager.Instance.coins < spinCost) return;

        ItemCoin.UseItemCoin(spinCost);
        StartCoroutine(SpinAnimation());
    }

    private IEnumerator SpinAnimation()
    {
        isSpinning = true;
        UpdateSpinButtonState();

        List<SlottyReward> strip = new List<SlottyReward>(slotImages.Count);
        for (int i = 0; i < slotImages.Count; i++)
        {
            var r = rewardList[Random.Range(0, rewardList.Count)];
            strip.Add(r);
            slotImages[i].sprite = GetSpriteForItem(r.itemName);
        }

        float offsetX = 0f;
        ApplyPositions(offsetX);

        float t = 0f;
        while (t < spinDuration)
        {
            t += Time.unscaledDeltaTime;
            float k = Mathf.Clamp01(t / spinDuration);
            float speed = Mathf.Lerp(initialSpinSpeed, 0f, k * k);

            offsetX -= speed * Time.unscaledDeltaTime;

            float step = slotWidth + slotSpacing;
            while (offsetX <= -step)

            {
                offsetX += step;
                ShiftLeft(strip);
                strip[strip.Count - 1] = rewardList[Random.Range(0, rewardList.Count)];

                for (int i = 0; i < slotImages.Count; i++)
                    slotImages[i].sprite = GetSpriteForItem(strip[i].itemName);
            }


            ApplyPositions(offsetX);
            yield return null;
        }

        float snapT = 0f;
        float snapDur = 0.35f;
        float startOffset = offsetX;

        while (snapT < snapDur)
        {
            snapT += Time.unscaledDeltaTime;
            float u = Mathf.Clamp01(snapT / snapDur);
            float ease = 1f - Mathf.Pow(1f - u, 3f);

            offsetX = Mathf.Lerp(startOffset, 0f, ease);
            ApplyPositions(offsetX);

            yield return null;
        }

        ApplyPositions(0f);

        SlottyReward finalReward = strip[centralIndex];
        GivePlayerRewards(new List<SlottyReward> { finalReward });

        isSpinning = false;
        UpdateSpinButtonState();
    }

    private void GivePlayerRewards(List<SlottyReward> awardedRewards)
    {
        if (awardedRewards == null || awardedRewards.Count == 0) return;

        foreach (var reward in awardedRewards)
        {
            switch (reward.itemName)
            {
                case ItemName.None:
                    Debug.Log("Player recived reward 'none' from Slotty");
                    break;
                case ItemName.Coin:
                    Debug.Log("Player recived reward 'coin' from Slotty");
                    ItemCoin.CollectItemCoin(reward.rewardQuantity);
                    break;
                case ItemName.Bomb:
                    Debug.Log("Player recived reward 'bomb' from Slotty");
                    ItemBomb.CollectItemBomb(reward.rewardQuantity);
                    break;
                case ItemName.MinionOrbiter:
                    Debug.Log("Player recived reward 'minion' from Slotty");
                    GameManager.Instance.SpawnMinion(ItemName.MinionOrbiter);
                    break;
                case ItemName.MinionFollower:
                    Debug.Log("Player recived reward 'minion' from Slotty");
                    GameManager.Instance.SpawnMinion(ItemName.MinionFollower);
                    break;
                case ItemName.FullHeart:
                    Debug.Log("Player recived reward 'fullheart' from Slotty");
                    ItemHeart.CollectItemHeart(reward.rewardQuantity);
                    break;
                case ItemName.HalfHeart:
                    Debug.Log("Player recived reward 'helfheart' from Slotty");
                    ItemHeart.CollectItemHeart(reward.rewardQuantity);
                    break;
                case ItemName.Key:
                    Debug.Log("Player recived reward 'key' from Slotty");
                    ItemKey.CollectItemKey(reward.rewardQuantity);
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
            if (reward.itemName == itemName)
                return reward.itemSprite;

        Debug.LogError("Sprite not found for item: " + itemName);
        return null;
    }
    private void ApplyPositions(float offsetX)
    {
        float cx = centerX;
        float step = slotWidth + slotSpacing;

        for (int i = 0; i < imageTransforms.Length; i++)
            imageTransforms[i].anchoredPosition =
                new Vector2(cx + (i - centralIndex) * step + offsetX, 0f);
    }

    private void UpdateSpinPriceText()
    {
        if (spinPriceText != null)
            spinPriceText.text = spinCost + " coins";
    }


    private void ShiftLeft(List<SlottyReward> strip)
    {
        var first = strip[0];
        strip.RemoveAt(0);
        strip.Add(first);
    }
}
