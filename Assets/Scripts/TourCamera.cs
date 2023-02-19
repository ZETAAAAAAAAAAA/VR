using UnityEngine;

public class TourCamera : MonoBehaviour
{
    public Transform target;
    public float xSpeed = 200, ySpeed = 200, mSpeed = 10;
    public float yMinLimit = 5, yMaxLimit = 50;
    public float distance = 50, minDistance = 2, maxDistance = 100;
    public bool needDamping = true;
    float damping = 5f;
    public float x = 0f;
    public float y = 0f;
 
 
    private Vector3 m_mouseMovePos;
    private Camera camera;
 
 
    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            //鼠标点击右键，围绕target旋转移动相机，改变视野
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 
 
                //y = ClampAngle(y, yMinLimit, yMaxLimit);
            }
            distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);  //
            Vector3 disVector = new Vector3(0f, 0f, -distance);
            Vector3 position = rotation * disVector + target.position;
            if (needDamping)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
                transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);
            }
            else
            {
                transform.rotation = rotation;
                transform.position = position;
            }

            //切换物体
            if (Input.GetMouseButtonDown(2))
            {
                //从摄像机发出到点击坐标的射线
                //由于使用了射线检测，所以鼠标一定要点击到带碰撞体的物体上，点击位置为空时，无效果。
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    GameObject go = hitInfo.collider.gameObject; 
                    target = go.GetComponent<Transform>();
                    //m_mouseMovePos = hitInfo.point;
                }
            }
        }
    }
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
