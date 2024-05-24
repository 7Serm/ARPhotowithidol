using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstiatePrehab : MonoBehaviour
{
    private RaycastHit _hit;
    private int _count = 0;

    [SerializeField]
    private GameObject _modelObject;
    [SerializeField] GameObject _emptyobjprehab;
    [SerializeField] GameObject _motherobj;

    public Text textx;
    public Text texty;
    public Text textz;

    private GameObject emptyobj;

    public GameObject EmptyObjPrehab
    {
        get => emptyobj;
    }
    // Update is called once per frame
    void Update()
    {
        ///ƒ‚ƒfƒ‹‚Ì¶¬
        if(Input.touchCount > 0 && _count == 0)
        {
            Touch touch = Input.touches[0];
            if(touch.phase == TouchPhase.Began )
            {
                var _ray = Camera.main.ScreenPointToRay( touch.position );
                if(Physics.Raycast( _ray, out _hit ))
                {
                    Instantiate(_modelObject,_hit.point,Quaternion.Euler(0,180,0));
                    _count++;
                    emptyobj = Instantiate(_emptyobjprehab, _hit.point, Quaternion.Euler(0, 180, 0));
                    emptyobj.transform.SetParent(Camera.main.transform);
                   
                }

            }
        }

        if(emptyobj != null)
        {
            textx.text = emptyobj.transform.position.x.ToString();
            texty.text = emptyobj.transform.position.y.ToString();
            textz.text = emptyobj.transform.position.z.ToString();
        }
        
    }
}
