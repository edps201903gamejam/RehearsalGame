using UnityEngine;
using UnityEngine.UI;

public class BadgeView : MonoBehaviour
{
    [System.Serializable]
    public enum BADGE_TYPE
    {
        Bad = 0,
        Excellent = 1,
    }
    
    // Serialize cache
    [SerializeField]
    private Image image_ = null;
    [Space(8)]
    [SerializeField]
    private Sprite[] sprites = null;
    [SerializeField]
    private BADGE_TYPE badgeType = BADGE_TYPE.Bad;

    public void Set(BADGE_TYPE _type)
    {
        this.badgeType = _type;
        this.image_.sprite = sprites[(int)this.badgeType];
    }

	private void Start ()
    {
	}
}
