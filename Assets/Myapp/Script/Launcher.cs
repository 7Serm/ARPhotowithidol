using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject _prefab;
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
#else
        if (Input.touchCount > 0)
#endif
        {
            //�J�����̑O�ɃX�|�[������ 
            var pos = Camera.main.transform.position;
            var forw = Camera.main.transform.forward;
            var thing = Instantiate(_prefab, pos + (forw * 0.1f), Quaternion.identity);

            //�������Z���ǉ�����Ă���ꍇ�͔��˂���
            if (thing.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(forw * 200.0f);
            }
        }
    }
}
