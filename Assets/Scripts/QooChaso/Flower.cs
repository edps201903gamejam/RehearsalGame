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

    private float givenTime;
    private float growTime;
    private bool CanGrow { get { return growTime <= (elapsedTime - givenTime) && hasWater; } }

    private bool hasWater;

    private void Start()
    {
        msRender = this.GetComponent<MeshRenderer>();
        this.msRender.material = materials[(int)State.NONE];

        currentState = State.NONE;
        elapsedTime = 0;
        givenTime = 0;
        growTime = GetRandomFloat(startGrowTime, endGrowTime);
        hasWater = true;
    }

    public void OnUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (CanGrow) { Grow(); }
    }

    public int GetPoint()
    {
        if (currentState == State.FLOWER)
        {
            hasWater = true;
            ChangeState(State.NONE);
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
        return point;
    }

    private void Grow()
    {
        if (!hasWater) { return; }

        hasWater = false;
        ChangeState(currentState);
    }

    private void ChangeState(State type)
    {
        switch (type)
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
            case State.FLOWER: break;
            case State.DIE:
                currentState = State.DIE;
                this.msRender.material = materials[(int)State.DIE];
                break;
        }
    }

    private float GetRandomFloat(float start, float fin)
    {
        return Random.Range(start, fin);
    }

}
