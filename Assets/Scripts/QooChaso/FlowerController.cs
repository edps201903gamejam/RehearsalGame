// Writer：QooChaso

using UnityEngine;

public class FlowerController : SingletonMonoBehaviour<FlowerController>
{
    [SerializeField]
    private Flower[] flowers = null;

    private void Start()
    {

    }

    public void OnUpdate()
    {
        for (var i = 0; i < this.flowers.Length; ++i)
        {
            this.flowers[i].OnUpdate();
        }
    }

    public bool IsAllWithered()
    {
        for (var i = 0; i < this.flowers.Length; ++i)
        {
            if (this.flowers[i].CurrentState != Flower.State.DIE) { return false; }
        }

        return true;
    }
}
