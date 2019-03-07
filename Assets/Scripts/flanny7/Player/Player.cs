using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Todo: tagの名前を入力する
    private const string DEVIL_TAG = "devil";
    private const string WATER_SPOT_TAG = "water";
    private const string FLOWER_TAG = "flower";

    [System.Serializable]
    public struct KeyConfig
    {
        public KeyCode Up;
        public KeyCode Down;
        public KeyCode Left;
        public KeyCode Right;
        public KeyCode Retrun;
    }

    public enum ANIMATION_STATE
    {
        Wait,
        Run,
    }

    // Serialize
    [SerializeField]
    private KeyConfig keyConfig;
    [Space(8)]
    [SerializeField]
    private float moveSpeed = 5.0f;

    // Cache
    private Transform transform_ = null;
    private Animator animator_ = null;

    // Private
    private ANIMATION_STATE animState = ANIMATION_STATE.Wait;
    private HashSet<GameObject> triggered = null;

	private void Start ()
    {
        // cache
        this.transform_ = this.transform;
        this.animator_ = GetComponent<Animator>();

        // null check
        if (this.transform_ == null) { FyUtility.LogError("transform_ is null."); }
        if (this.animator_ == null) { FyUtility.LogError("animator_ is null."); }

        // init
        this.animState = ANIMATION_STATE.Wait;
        this.triggered = new HashSet<GameObject>();
    }
	
	private void Update ()
    {
        this.Move();
	}

    /// <summary>
    /// 移動する
    /// </summary>
    private void Move()
    {
        // Inputの取得
        var isUp = Input.GetKey(this.keyConfig.Up);
        var isDown = Input.GetKey(this.keyConfig.Down);
        var isLeft = Input.GetKey(this.keyConfig.Left);
        var isRight = Input.GetKey(this.keyConfig.Right);

        // 移動
        var moveVec = Vector3.zero;
        if (isUp) { moveVec.z += 1; }
        if (isDown) { moveVec.z -= 1; }
        if (isLeft) { moveVec.x -= 1; }
        if (isRight) { moveVec.x += 1; }

        var isRun = (isUp || isDown || isLeft || isRight);
        if (isRun) { this.Translate(moveVec, this.moveSpeed); }

        // アニメの更新
        var isChangeAnim =
            ((this.animState == ANIMATION_STATE.Wait) && isRun) ||
            ((this.animState == ANIMATION_STATE.Run) && !isRun);
        
        if (isChangeAnim)
        {
            if (isRun) { this.ChangeAnimation(ANIMATION_STATE.Run); }
            else { this.ChangeAnimation(ANIMATION_STATE.Wait); }
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="_distance">移動距離</param>
    /// <param name="_speed">移動速度</param>
    private void Translate(Vector3 _distance, float _speed = 1.0f)
    {
        var beforPos = this.transform_.position;
        var afterPos = this.transform_.position;

        afterPos.x += _distance.x * _speed * Time.deltaTime;
        afterPos.y += _distance.y * _speed * Time.deltaTime;
        afterPos.z += _distance.z * _speed * Time.deltaTime;

        this.transform_.position = afterPos;

        // 向き
        var dx = afterPos.x - beforPos.x;
        var dz = afterPos.z - beforPos.z;
        var deg = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        var ro = this.transform_.localEulerAngles;
        ro.y = deg;
        this.transform_.localEulerAngles = ro;
    }

    /// <summary>
    /// アニメーションを変更する
    /// </summary>
    /// <param name="_animState">アニメーションの状態</param>
    private void ChangeAnimation(ANIMATION_STATE _animState)
    {
        switch (_animState)
        {
            case ANIMATION_STATE.Wait:
                this.animator_.SetBool("isRun", false);
                break;

            case ANIMATION_STATE.Run:
                this.animator_.SetBool("isRun", true);
                break;
        }

        this.animState = _animState;
    }

    private void OnTriggerEnter(Collider _other)
    {
        this.triggered.Add(_other.gameObject);
    } 

    private void OnTriggerExit(Collider _other)
    {
        this.triggered.Remove(_other.gameObject);
    }

    private bool IsTrigger(string _tag)
    {
        foreach (var c in this.triggered)
        {
            if (c.tag == _tag) { return true; }
        }

        return false;
    }
}
