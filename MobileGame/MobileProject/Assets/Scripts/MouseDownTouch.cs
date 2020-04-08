using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
//	Allows "OnMouseDown()" events to work on touch devices.
//From: http://wiki.unity3d.com/index.php/OnMouseDown?_ga=2.40046434.1765837225.1586277858-1854003708.1571771899
/// </summary>
public class MouseDownTouch : MonoBehaviour
{
    private void Update()
    {
        var hit = new RaycastHit();

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates.
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(ray, out hit))
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                }
            }
        }
    }
}
