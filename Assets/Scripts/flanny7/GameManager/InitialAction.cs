using UnityEngine;

public sealed class InitialAction : StateAction
{
    private GamaManager gm = null;

    public InitialAction(GamaManager _gamaManager)
    {
        this.gm = _gamaManager;
    }

    public override void Start()
    {
    }

    public override void Update()
    {
        this.gm.ChangeState(GamaManager.State.Play, this);
    }

    public override void End()
    {
    }
}