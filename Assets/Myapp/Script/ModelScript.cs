using UnityEngine;


public class ModelScript : MonoBehaviour
{

    private GameObject _emptyObject; // ��̃I�u�W�F�N�g���Q��
    private float _maxDistance = 0.0f; // ���f������I�u�W�F�N�g���痣���ő勗��
    private float _speed = 1f; // ���f������I�u�W�F�N�g�Ɍ��������x
    [SerializeField]
    InstiatePrehab instiatePrehab;

    public float delay = 2.0f; // �ҋ@���ԁi�b�j
    GameObject gmb;
    void Start()
    {
        GameObject gmb = GameObject.Find("Launcher");
        instiatePrehab = gmb.GetComponent<InstiatePrehab>();
        _emptyObject = instiatePrehab.EmptyObjPrehab;
        //�����f���ړ��֐��Ŏg�p
    }


    void Update()
    {
      //  MoveModel();

    }
    




�@�@�@//���f���ړ��֐�
   �@//�J�����{�̂̈ړ�������΃��f�����Ǐ]���邪�J������]�ɂ͍��킹�Ĉړ����Ȃ�
    private void MoveModel()
    {
        if (instiatePrehab != null)
        {
            Debug.Log("hihi" + instiatePrehab.EmptyObjPrehab.transform.position);
            Debug.Log("hihimodel" + transform.position);
            // ���f������I�u�W�F�N�g�����苗������Ă��邩�m�F
            float distanceX = Mathf.Abs(transform.position.x - _emptyObject.transform.position.x);
            if (distanceX > _maxDistance)
            {
                // ���f������I�u�W�F�N�g�̈ʒu�Ɍ�������X�������Ɉړ�
                float step = _speed * Time.deltaTime;
            float targetX = Mathf.MoveTowards(transform.position.x, _emptyObject.transform.position.x, step);
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
            }
        }
    }
}
