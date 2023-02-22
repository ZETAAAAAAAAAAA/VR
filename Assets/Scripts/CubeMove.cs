using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject targetObject;
    private Vector3 mousePos;
    private bool isDrag;
    private GameObject go;
    private Vector3 oriScreenPos;
    public Camera theCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //射线由主摄像机发出，射向屏幕点击的点
            Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
            //射线撞击点
            RaycastHit hit;
            //如果射线撞击到碰撞体，且碰撞体的标签是我们设置需要拖拽的物体，那么进行主逻辑

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name != "Plane")
                {
                    //res = hit.point;
                    //targetObject = hit.collider.gameObject;
                    //记录下当前鼠标位置
                    mousePos = Input.mousePosition;
                    isDrag = true;
                    go = hit.collider.gameObject;
                    //记录下拖拽物的原始屏幕空间位置
                    oriScreenPos = theCamera.WorldToScreenPoint(go.transform.position);

                }
            }
        }
        //左键一直处于按下状态，即为拖拽过程
        if (Input.GetMouseButton(1))
        {
            //如果拖拽状态处于true，且有拖拽物
            if (isDrag && go)
            {
                //获取屏幕空间鼠标增量，并加上拖拽物原始位置（屏幕空间计算）
                Vector3 newPos = oriScreenPos + Input.mousePosition - mousePos;
                //将屏幕空间坐标转换为世界空间
                Vector3 newWorldPos = theCamera.ScreenToWorldPoint(newPos);
                //将世界空间位置赋予拖拽物
                go.transform.position = newWorldPos;
            }
        }
        //松开左键
        if (Input.GetMouseButtonUp(1))
        {
            isDrag = false;
            go = null;
        }
    }
}
