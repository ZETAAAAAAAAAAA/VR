using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockModify : MonoBehaviour
{
    public GameObject cube;
    public List<GameObject> cubes;
    public GameObject FindCubes(int i)
    {
        return cubes[i];
    }

    public int i=0;
    // Start is called before the first frame update
    
    public Vector3 CubeControl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 res= new Vector3(0,0,0);
        //Èç¹ûÃüÖÐ
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Plane")
            {
                res = hit.point;
                Vector3 p = Findlocation(res);
                CreatCube(p);
            }
            else
            {
                GameObject.DestroyImmediate(hit.collider.gameObject);
            }
            //print(hit.collider.gameObject.name);
        }
        
        return res;
    }
    public Vector3 Findlocation(Vector3 v)
    {
        var a = v.x;
        var a0 = a / 10;
        var a1 = Mathf.Floor(a0);
        var a2 = Mathf.Ceil(a0);
        var nx = 5 * (a1 + a2);
        var b = v.z;
        var b0 = b / 10;
        var b1 = Mathf.Floor(b0);
        var b2 = Mathf.Ceil(b0);
        var nz = 5 * (b1 + b2);
        Vector3 res = new Vector3(nx, 4.5f, nz);
        return res;
    }

    public void CreatCube(Vector3 v)
    {
        cube = FindCubes(i);
        GameObject p = GameObject.Instantiate(cube);
        p.transform.position = v;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CubeControl();
        }
    }
}
