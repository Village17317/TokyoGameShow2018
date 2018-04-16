/*
*	作成者	：青木仁志
*	機能	：
*	作成	：
*	更新	：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI {
	
	public class NpcController : Village.Inheritor {

        [SerializeField]
        private bool jmp = false, clm = false, stp = false;

        [SerializeField]
        private StepDecision jumpCol, climbCol, stopCol;

		private void Awake()
		{
			
		}
	
		private void Start ()
		{
			
		}
	
		private void Update ()
		{
            jmp = jumpCol.ovLap;
            clm = climbCol.ovLap;
            stp = stopCol.ovLap;
        }
	}
}
