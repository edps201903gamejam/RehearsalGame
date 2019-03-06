using UnityEngine;

public class ResultTester : MonoBehaviour
{
    [System.Serializable]
    private struct Param
    {
        public int FlowerCount;
        public int DieCount;
        public BadgeComponent.BADGE_TYPE BadgeType;
    }

    [SerializeField]
    private KeyCode openKeyCode = KeyCode.Z;
    [SerializeField]
    private KeyCode closeKeyCode = KeyCode.X;
    [SerializeField]
    private ResultUIManager uIManager = null;
    [SerializeField]
    private Param param;

    private void Start()
    {
        FyUtility.NullCheck(this.uIManager, this.gameObject);
    }

    private void Update ()
    {
        if (Input.GetKeyDown(this.openKeyCode)) { this.uIManager.Open(this.param.FlowerCount, this.param.DieCount, this.param.BadgeType); }
        if (Input.GetKeyDown(this.closeKeyCode)) { this.uIManager.Close(); }
    }
}
