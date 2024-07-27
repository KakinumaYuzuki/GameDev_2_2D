using UnityEngine;

public class AimPointer : MonoBehaviour
{
    private Vector3 _pos;
    
    void Update()
    {
        _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _pos.z = 0;
        transform.position = _pos;
    }
}
