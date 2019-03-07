using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public enum State
    {
        Initial,
        Play,
        Result,

        Length,
    }

    private StateManager<State> stateManager = null;
    private InitialAction initialAction = null;
    private PlayAction playAction = null;
    private ResultAction resultAction = null;
    private State currentState = State.Length;

    public void ChangeState(State _state, StateAction _this)
    {
        if (!this.HasStateActionInstance(_this)) { return; }

        this.stateManager.SetState(_state);
        this.currentState = _state;
    }

    private void Start()
    {
        this.stateManager = new StateManager<State>();
        this.initialAction = new InitialAction(this);
        this.playAction = new PlayAction(this);
        this.resultAction = new ResultAction(this);
    }

    private void Update()
    {
        this.stateManager.Update();
    }

    /// <summary>
    /// 対象がGamaManaerが持つStateActionのInstanceであるか
    /// </summary>
    /// <param name="_this">対象</param>
    /// <returns></returns>
    private bool HasStateActionInstance(StateAction _this)
    {
        return (!Object.ReferenceEquals(this.initialAction, _this) &&
                !Object.ReferenceEquals(this.playAction, _this) &&
                !Object.ReferenceEquals(this.resultAction, _this));
    }
}
