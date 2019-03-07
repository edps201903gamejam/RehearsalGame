using UnityEngine;

public sealed class PlayAction : StateAction
{
    private GamaManager gm = null;

    public PlayAction(GamaManager _gamaManager)
    {
        this.gm = _gamaManager;
    }

    public override void Start()
    {
    }

    public override void Update()
    {
        this.gm.ChangeState(GamaManager.State.Result, this);
    }

    public override void End()
    {
    }
}