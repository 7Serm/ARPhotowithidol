using UnityEngine;
using UnityEngine.UI;


public class ModelScript : MonoBehaviour
{

    private GameObject _emptyObject; // カメラの先にある目標となるオブジェクトを参照
    private float _maxDistance = 0.1f; // モデルが目標オブジェクトから離れる最大距離
    private float _speed = 5f; // モデルが目標オブジェクトに向かう速度
    private InstiatePrehab instiatePrehab;

    private Camera _mainCamera;　//メインカメラの参照
    private Animator _animator;　//モデルが持つアニメーターコンポーネント用

    private UIManager _uiManager;
    void Start()
    {
        GameObject gmb = GameObject.Find("Launcher");
        instiatePrehab = gmb.GetComponent<InstiatePrehab>();
        _emptyObject = instiatePrehab.EmptyObjPrehab;
        //↑モデル移動関数で使用

        GameObject _canvas = GameObject.Find("Canvas");
        _uiManager = _canvas.GetComponent<UIManager>();

        _mainCamera = Camera.main;
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (_uiManager.MoveActive)
        {
            MoveModel();
        }
        if (_uiManager.LookAtActive)
        {
            ModelLookatCamera();
        }
        //UpdateAnimatorParameters();
    }





    //モデル移動関数
    //移動する方向に向かって回転までする
    private void MoveModel()
    {
        if (instiatePrehab != null && _emptyObject != null)
        {
            // X軸とZ軸の距離を計算
            float distanceX = Mathf.Abs(transform.position.x - _emptyObject.transform.position.x);
            float distanceZ = Mathf.Abs(transform.position.z - _emptyObject.transform.position.z);

            // X軸またはZ軸の距離が_maxDistanceを超えている場合
            if (distanceX > _maxDistance || distanceZ > _maxDistance)
            {
                // モデルを空オブジェクトの最新の位置に向かってX軸とZ軸方向に移動
                Vector3 targetPosition = new(_emptyObject.transform.position.x, transform.position.y, _emptyObject.transform.position.z);
                Vector3 direction = targetPosition - transform.position;
                // 回転を設定
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
                }

                // 移動を設定
                float step = _speed * Time.deltaTime;
                transform.position = Vector3.Slerp(transform.position, targetPosition, step);
                
            }
        }
    }


    //スマホカメラの方向を常に見続ける
    private void ModelLookatCamera()
    {
        {
            
        }
        if (_mainCamera != null)
        {
            Vector3 direction = _mainCamera.transform.position - transform.position;
            direction.y = 0; // Y軸の回転を無視する場合はコメントを外す
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }


    //X方向への差が多き場合に移動するためその時にアニメーションの歩きを再生するよう遷移させる
    private void UpdateAnimatorParameters()
    {
        if (_animator != null)
        {
            float distanceX = Mathf.Abs(transform.position.x - _emptyObject.transform.position.x);
            // 例えば、モデルが移動しているかどうかを判定してアニメーターに伝える
            if (distanceX > _maxDistance)
            {
                _animator.SetBool("isMoving", true);
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }              
             
        }
    }

   
}