using System.Collections.Generic;
using System.Linq;
using UnityEngine; 
public class Player : MonoBehaviour {
    private const string DEVIL_TAG = "devil";
    private const string WATER_SPOT_TAG = "water";
    private const string FLOWER_TAG = "flower";     private static readonly float FLOAT_MAX_VALUE = float.MaxValue;      [System.Serializable]     public struct KeyConfig     {         public KeyCode Up;         public KeyCode Down;         public KeyCode Left;         public KeyCode Right;         public KeyCode Retrun;     }      public enum ANIMATION_STATE     {         Wait,         Run,         Knockback,     }      // Serialize     [SerializeField]     private KeyConfig keyConfig;     [Space(8)]     [SerializeField]     private float moveSpeed = 5.0f;     [SerializeField]     private float knockbackTime = 3.0f;     [SerializeField]     private AudioClip giftWater;     [SerializeField]     private AudioClip getWater;      // Cache     private Transform transform_ = null;     private Animator animator_ = null;     private AudioSource audioSource_ = null;      // Private     private ANIMATION_STATE animState = ANIMATION_STATE.Wait;     private TriggerUtility trigger = null;
    private WateringCan wateringCan;      private float elapsedTime = 0.0f;      /// knockback      private float knockbackStartTime = 0.0f;     private bool isKnockback = false;      private void Start ()     {         // cache         this.transform_ = this.transform;         this.animator_ = GetComponent<Animator>();         this.audioSource_ = GetComponent<AudioSource>();          // null check         if (this.transform_ == null) { FyUtility.LogError("transform_ is null."); }         if (this.animator_ == null) { FyUtility.LogError("animator_ is null."); }          // init         this.animState = ANIMATION_STATE.Wait;         this.trigger = new TriggerUtility();
        this.wateringCan = new WateringCan();         this.elapsedTime = 0.0f;          this.knockbackStartTime = 0.0f;         this.isKnockback = false;     }         private void Update ()     {         this.elapsedTime += Time.deltaTime;
        this.UpdateStatus();

        if (this.isKnockback) { return; }

        this.Move();
        if (Input.GetKeyDown(this.keyConfig.Retrun)) { this.Action(); }     }

    private void Action()
    {
        var isInFlower = this.trigger.ContainWithTag(FLOWER_TAG);
        var isInWater = this.trigger.ContainWithTag(WATER_SPOT_TAG);
        if (isInWater && isInFlower) { Debug.LogWarning("waterとflowerが重なっています"); }

        if (isInFlower) { GiftWater(this.trigger.FindGameObjectWithTag(FLOWER_TAG)); }         else if (isInWater) { GetWater(); }
    }

    /// <summary>     /// 移動する     /// </summary>     private void Move()     {         // Inputの取得         var isUp = Input.GetKey(this.keyConfig.Up);         var isDown = Input.GetKey(this.keyConfig.Down);         var isLeft = Input.GetKey(this.keyConfig.Left);         var isRight = Input.GetKey(this.keyConfig.Right);          var isMove = (isUp || isDown || isLeft || isRight);         if (!isMove)
        {
            // アニメ
            if (this.animState != ANIMATION_STATE.Wait)
            {
                this.ChangeAnimation(ANIMATION_STATE.Wait);
            }
            return;
        }          // 移動         var moveVec = Vector3.zero;         if (isUp) { moveVec.z += 1; }         if (isDown) { moveVec.z -= 1; }         if (isLeft) { moveVec.x -= 1; }         if (isRight) { moveVec.x += 1; }         this.Translate(moveVec, this.moveSpeed);                  // アニメ         if (this.animState != ANIMATION_STATE.Run)         {
            this.ChangeAnimation(ANIMATION_STATE.Run);         }     }      /// <summary>     /// 移動処理     /// </summary>     /// <param name="_distance">移動距離</param>     /// <param name="_speed">移動速度</param>     private void Translate(Vector3 _distance, float _speed = 1.0f)     {         var beforPos = this.transform_.position;         var afterPos = this.transform_.position;          afterPos.x += _distance.x * _speed * Time.deltaTime;         afterPos.y += _distance.y * _speed * Time.deltaTime;         afterPos.z += _distance.z * _speed * Time.deltaTime;          this.transform_.position = afterPos;          // 向き         var dx = afterPos.x - beforPos.x;         var dz = afterPos.z - beforPos.z;         var deg = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;          var ro = this.transform_.localEulerAngles;         ro.y = deg;         this.transform_.localEulerAngles = ro;     }      /// <summary>     /// アニメーションを変更する     /// </summary>     /// <param name="_animState">アニメーションの状態</param>     private void ChangeAnimation(ANIMATION_STATE _animState)     {         switch (_animState)         {             case ANIMATION_STATE.Wait:                 this.animator_.SetBool("isRun", false);                 break;              case ANIMATION_STATE.Run:                 this.animator_.SetBool("isRun", true);                 break;              case ANIMATION_STATE.Knockback:                 this.animator_.SetBool("isRun", false);                 break;         }          this.animState = _animState;     }      private void UpdateStatus()
    {         // Update Knockback         if (this.isKnockback)
        {
            var knockbackElapsedTime = this.elapsedTime - this.knockbackStartTime;
            this.isKnockback = knockbackElapsedTime <= this.knockbackTime;
            if (!this.isKnockback) { this.knockbackStartTime = float.MaxValue; }
        }
        else
        {
            if (this.trigger.ContainWithTag(FLOWER_TAG))
            {
                var knockbackStartTime = this.trigger.FindItemWithTag(FLOWER_TAG).enterTime;
                var isKnockbackStart = knockbackStartTime != -1;
                if (isKnockbackStart)
                {
                    this.isKnockback = true;
                    this.knockbackStartTime = knockbackStartTime;
                    this.ChangeAnimation(ANIMATION_STATE.Knockback);
                }
            }
        }
    }

    private bool GiftWater(GameObject target)
    {
        if (!wateringCan.isWaterd) { return false; }

        var flower = target.GetComponent<Flower>();
        if (flower == null) { return false; }

        wateringCan.RemovedWater();
        var point = flower.GetPoint();

        audioSource_.clip = giftWater;
        audioSource_.Play();

        return true;
    }

    private bool GetWater()
    {
        if (wateringCan.isWaterd) { return false; }

        wateringCan.ToMaxWaterGase();

        audioSource_.clip = getWater;
        audioSource_.Play();

        return true;
    }

    // Trigger
    private void OnTriggerEnter(Collider _other)     {
        var target = _other.gameObject;
        this.trigger.Add(target, this.elapsedTime + Time.deltaTime);     }

    private void OnTriggerExit(Collider _other)
    {
        var target = _other.gameObject;
        Debug.Log(this.trigger.Remove(target));
    } }  