/*
*	作成者	：
*	機能	：
*	作成	：
*	更新	：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI
{

    public class StepDecision : MonoBehaviour
    {

        public bool ovLap = false;

        private void Update()
        {
            Vector3 origin = transform.position;
            Vector3 dir = Vector3.right;
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, 1f);

            if (hit.collider != null && hit.collider.tag == "Ground")
            {
                ovLap = true;
            }
            else
            {
                ovLap = false;
            }
            Debug.DrawRay(origin, dir, Color.blue);
        }
    }
}