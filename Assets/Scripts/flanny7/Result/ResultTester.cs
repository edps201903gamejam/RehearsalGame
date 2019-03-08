using UnityEngine;

public class ResultTester : MonoBehaviour
{
    [System.Serializable]
    private struct Param
    {
        public int FlowerCount;
        public int DieCount;
        public BadgeView.BADGE_TYPE BadgeType;
    }

    [SerializeField]
    private KeyCode openKeyCode = KeyCode.Z;
    [SerializeField]
    private KeyCode closeKeyCode = KeyCode.X;
    [SerializeField]
    private ResultUIController uIManager = null;
    [SerializeField]
    private Param param;

    private void Start()
    {
        FyUtility.NullCheck(this.uIManager, this.gameObject);
    }

    private void Update ()
    {
    }
}
