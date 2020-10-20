using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    void Update()
    {
        //点击输出物品信息
        MousePick();
    }
    void MousePick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //out put here
                //to UI
                Debug.Log(hit.transform.name);
            }
        }
    }
}
