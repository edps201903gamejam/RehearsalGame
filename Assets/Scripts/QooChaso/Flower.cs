// Writer：QooChaso

using UnityEngine;

public class Flower : MonoBehaviour
{
    public enum State
    {
        NONE,
        SPROUT,
        BUD,
        FLOWER,
        DIE
    }

    [SerializeField]
    private Material[] materials = null;
    [SerializeField]
    private float startGrowTime;
    [SerializeField]
    private float endGrowTime;
    [SerializeField]
    private FlowerGrowingView growingView = null;
    [SerializeField]
    private FlowerPointMessageView pointMesageView = null;

    private MeshRenderer msRender;
    public State CurrentState
    {
        get;
        private set;
    }

    private float elapsedTime;
    private float witherElapsedTime;
    private float givenTime;
    private float growTime;
    private float witherParcentage; //100になると花がかれる。GetPointを呼び出すと0になる。
    private bool CanGrow { get { return growTime <= (elapsedTime - givenTime) && hasWater; } }
    private bool hasWater;

    private float GrowingRate { get { return (this.elapsedTime - this.givenTime) / this.growTime; } }

    private void Start()
    {
        msRender = this.GetComponent<MeshRenderer>();
        this.msRender.material = materials[(int)State.NONE];

        CurrentState = State.NONE;

        elapsedTime = 0;
        witherElapsedTime = 0;
        givenTime = 0;
        growTime = GetRandomFloat(startGrowTime, endGrowTime);
        witherParcentage = 0;
        hasWater = true;
    }

    public void OnUpdate()
    {
        elapsedTime += Time.deltaTime;
        witherElapsedTime += Time.deltaTime;
        if (CanGrow) { Grow(); }
        
        this.growingView.Set(this.GrowingRate);

        //枯れる処理
        if(1 <= witherElapsedTime)
        {
            witherParcentage += 10; //1秒ごとに何%上昇か
            witherElapsedTime = 0.0f;
        }
        if(100 <= witherParcentage){ Wither(); }
    }

    public int GetPoint()
    {
        if (CurrentState == State.FLOWER)
        {
            hasWater = true;
            ChangeState(State.FLOWER);
            return 3;
        }

        if (hasWater) { return 0; }

        var point = 0;
        switch (CurrentState)
        {
            case State.NONE: break;

            case State.SPROUT:
            case State.BUD:
                point = 1;
                break;

            default: return 0;
        }

        // 水について
        hasWater = true;

        // 成長について
        growTime = GetRandomFloat(startGrowTime, endGrowTime);
        givenTime = this.elapsedTime;

        //枯れゲージを初期化
        witherParcentage = 0;

        this.pointMesageView.Set(FlowerPointMessageView.State.None);
        return point;
    }

    private void Grow()
    {
        if (!hasWater) { return; }

        hasWater = false;
        ChangeState(CurrentState);

        if (CurrentState == State.FLOWER)
        {
            this.pointMesageView.Set(FlowerPointMessageView.State.CanGetFlower);
        }
        else if (CurrentState == State.SPROUT ||
                 CurrentState == State.BUD)
        {
            this.pointMesageView.Set(FlowerPointMessageView.State.CanSetWater);
        }
        else
        {
            this.pointMesageView.Set(FlowerPointMessageView.State.None);
        }
    }

    private void ChangeState(State current)
    {
        switch (current)
        {
            case State.NONE:
                CurrentState = State.SPROUT;
                this.msRender.material = materials[(int)State.SPROUT];
                break;
            case State.SPROUT:
                CurrentState = State.BUD;
                this.msRender.material = materials[(int)State.BUD];
                break;
            case State.BUD:
                CurrentState = State.FLOWER;
                this.msRender.material = materials[(int)State.FLOWER];
                break;
            case State.FLOWER:
                CurrentState = State.NONE;
                this.msRender.material = materials[(int)State.NONE];
                break;
        }
    }

    private void Wither()
    {
        CurrentState = State.DIE;
        this.msRender.material = materials[(int)State.DIE];
        this.pointMesageView.Set(FlowerPointMessageView.State.None);
        this.growingView.Set(0);
    }

    private float GetRandomFloat(float start, float fin)
    {
        return Random.Range(start, fin);
    }

}
