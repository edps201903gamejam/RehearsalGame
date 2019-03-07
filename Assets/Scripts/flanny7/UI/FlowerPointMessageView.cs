using UnityEngine;

public class FlowerPointMessageView : MonoBehaviour
{
    [System.Serializable]
    public enum State
    {
        None,
        CanSetWater,
        CanGetFlower,
    }

    [SerializeField]
    private Sprite[] spriteCatalog = null;
    [Space(8)]
    [SerializeField]
    private State currentState = State.None;
    private SpriteRenderer spRender_ = null;

    private void Start()
    {
        this.spRender_ = this.GetComponent<SpriteRenderer>();
        this.spRender_.sprite = this.spriteCatalog[(int)this.currentState];
    }

    public void Set(State _state)
    {
        this.currentState = _state;
        this.spRender_.sprite = this.spriteCatalog[(int)this.currentState];
    }
}
