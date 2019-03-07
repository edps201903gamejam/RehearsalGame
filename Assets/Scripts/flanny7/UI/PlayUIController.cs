using UnityEngine;

public class PlayUIController : MonoBehaviour
{
    public WateringCanView WateringCan { get { return this.wateringCan; } set { this.wateringCan = value; } }

    [SerializeField]
    private WateringCanView wateringCan = null;

    private void Start()
    {
        FyUtility.NullCheck(this.wateringCan, this.gameObject);
    }

    public void OnUpdate()
    {

    }
}
