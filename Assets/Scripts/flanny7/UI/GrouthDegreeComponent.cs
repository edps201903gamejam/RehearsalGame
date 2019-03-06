using UnityEngine;
using UnityEngine.UI;

public sealed class GrouthDegreeComponent : MonoBehaviour
{
    public float FillAmount { get { return this.fillAmount; } set { this.fillAmount = value; } }

    [SerializeField]
    private Image coverImage_ = null;
    [SerializeField]
    private Image coloredImage_ = null;
    [SerializeField, Range(0, 1)]
    private float fillAmount = 0.0f;

    public float CoverImageFillAmount
    {
        get { return this.coverImage_.fillAmount; }
        private set { this.coverImage_.fillAmount = value; }
    }

    public Color ColorefImageColor
    {
        get { return this.coloredImage_.color; }
        private set { this.coloredImage_.color = value; }
    }

	private void Start ()
    {
        FyUtility.NullCheck(this.coverImage_, this.gameObject);
    }
	
	private void Update ()
    {
        this.CoverImageFillAmount = 1 - this.fillAmount;

        if (1.0f / 3.0f * 2 < this.fillAmount)
        {
            this.ColorefImageColor = Color.red;
        }
        else if (1.0f / 3.0f < this.fillAmount)
        {
            this.ColorefImageColor = Color.yellow;
        }
        else
        {
            this.ColorefImageColor = Color.green;
        }
    }
}
