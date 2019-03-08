using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreTextView : MonoBehaviour
{
    [SerializeField]
    private Text flowerText = null;
    [SerializeField]
    private Text giftWaterText = null;

    private int flowerCount = 0;
    private int giftWaterCount = 0;

    public void IncreaseFlowerCount()
    {
        ++this.flowerCount;
        this.flowerText.text = string.Format("× {0}", this.flowerCount);
    }

    public void IncreaseGiftWaterCount()
    {
        ++this.giftWaterCount;
        this.giftWaterText.text = string.Format("× {0}", this.giftWaterCount);
    }
}