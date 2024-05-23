using UnityEngine;


public class ModelScript : MonoBehaviour
{

    private GameObject _emptyObject; // 空のオブジェクトを参照
    private float _maxDistance = 0.0f; // モデルが空オブジェクトから離れる最大距離
    private float _speed = 1f; // モデルが空オブジェクトに向かう速度
    [SerializeField]
    InstiatePrehab instiatePrehab;

    public float delay = 2.0f; // 待機時間（秒）
    GameObject gmb;
    void Start()
    {
        GameObject gmb = GameObject.Find("Launcher");
        instiatePrehab = gmb.GetComponent<InstiatePrehab>();
        _emptyObject = instiatePrehab.EmptyObjPrehab;
        //↑モデル移動関数で使用
    }


    void Update()
    {
      //  MoveModel();

    }
    




　　　//モデル移動関数
   　//カメラ本体の移動があればモデルも追従するがカメラ回転には合わせて移動しない
    private void MoveModel()
    {
        if (instiatePrehab != null)
        {
            Debug.Log("hihi" + instiatePrehab.EmptyObjPrehab.transform.position);
            Debug.Log("hihimodel" + transform.position);
            // モデルが空オブジェクトから一定距離離れているか確認
            float distanceX = Mathf.Abs(transform.position.x - _emptyObject.transform.position.x);
            if (distanceX > _maxDistance)
            {
                // モデルを空オブジェクトの位置に向かってX軸方向に移動
                float step = _speed * Time.deltaTime;
            float targetX = Mathf.MoveTowards(transform.position.x, _emptyObject.transform.position.x, step);
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
            }
        }
    }
}
