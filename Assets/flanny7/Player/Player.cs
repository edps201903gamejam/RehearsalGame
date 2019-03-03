﻿using UnityEngine;

public class Player : MonoBehaviour
{
    // Todo: tagの名前を入力する
    private const string DEVIL_TAG = "Devil";
    private const string WATER_SPOT_TAG = "WaterSpot";
    private const string FLOWER_POT_TAG = "FlowerPot";

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

	private void Start ()
    {
        // cache
        this.transform_ = this.transform;
        this.animator_ = GetComponent<Animator>();

        // null check
        if (this.transform_ == null) { this.LogError("transform_ is null."); }
        if (this.animator_ == null) { this.LogError("animator_ is null."); }

        // init
        this.animState = ANIMATION_STATE.Wait;
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

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="_other">衝突対象</param>
    private void OnTriggerEnter(Collider _other)
    {
        var target = _other.gameObject;

        switch (target.tag)
        {
            case DEVIL_TAG:
                break;
                
            case WATER_SPOT_TAG:
                break;

            case FLOWER_POT_TAG:
                break;
        }
    }

    /// <summary>
    /// エラーログをイイ感じに表示する
    /// </summary>
    /// <param name="_msg">ログに出力させる文字</param>
    private void LogError(string _msg)
    {
        Debug.LogError("<color=red>" + _msg + "</color>", this.gameObject); Debug.Break();
    }
}
