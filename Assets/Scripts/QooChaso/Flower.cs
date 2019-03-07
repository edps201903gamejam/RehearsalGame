// Writer：QooChaso

using UnityEngine;

public class Flower : MonoBehaviour
{
    private enum State
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

    private MeshRenderer msRender;
    private State currentState;

    private float elapsedTime;
    private float witherElapsedTime;
    private float givenTime;
    private float growTime;
    private float witherParcentage; //100になると花がかれる。GetPointを呼び出すと0になる。
    private bool CanGrow { get { return growTime <= (elapsedTime - givenTime) && hasWater; } }
    private bool hasWater;

    private void Start()
    {
        msRender = this.GetComponent<MeshRenderer>();
        this.msRender.material = materials[(int)State.NONE];

        currentState = State.NONE;

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

        //枯れる処理
        if(witherElapsedTime >= 1)
        {
            witherParcentage += 10; //1秒ごとに何%上昇か
            witherElapsedTime = 0.0f;
        }
        if(witherParcentage >= 100){ Wither(); }
    }

    public int GetPoint()
    {
        if (currentState == State.FLOWER)
        {
            hasWater = true;
            ChangeState(State.FLOWER);
            return 3;
        }

        if (hasWater) { return 0; }

        var point = 0;
        switch (currentState)
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
        return point;
    }

    private void Grow()
    {
        if (!hasWater) { return; }

        hasWater = false;
        ChangeState(currentState);
    }

    private void ChangeState(State current)
    {
        switch (current)
        {
            case State.NONE:
                currentState = State.SPROUT;
                this.msRender.material = materials[(int)State.SPROUT];
                break;
            case State.SPROUT:
                currentState = State.BUD;
                this.msRender.material = materials[(int)State.BUD];
                break;
            case State.BUD:
                currentState = State.FLOWER;
                this.msRender.material = materials[(int)State.FLOWER];
                break;
            case State.FLOWER:
                currentState = State.NONE;
                this.msRender.material = materials[(int)State.NONE];
                break;
            //case State.DIE:
                //currentState = State.DIE;
                //this.msRender.material = materials[(int)State.DIE];
                //break;
        }
    }

    private void Wither()
    {
        currentState = State.DIE;
        this.msRender.material = materials[(int)State.DIE];
    }

    private float GetRandomFloat(float start, float fin)
    {
        return Random.Range(start, fin);
    }

}
