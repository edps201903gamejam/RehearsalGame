using UnityEngine;
using UnityEngine.UI;

public class BadgeComponent : MonoBehaviour
{
    [System.Serializable]
    public enum BADGE_TYPE
    {
        Bad = 0,
        Excellent = 1,
    }

    public BADGE_TYPE BadgeType { get { return this.type; } set { this.type = value; } }

    // Serialize cache
    [SerializeField]
    private Image image_ = null;
    [Space(8)]
    [SerializeField]
    private Sprite[] sprites = null;
    [SerializeField]
    private BADGE_TYPE type = BADGE_TYPE.Bad;

	private void Start ()
    {
	}
	
	private void Update ()
    {
        this.image_.sprite = sprites[(int)this.type];
	}
}
