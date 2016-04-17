using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace NodeBehaviorPrototyping
{
    public class BaseInputNode : BaseNode
    {
        public virtual string getResult()
        {
            return "None";
        }

        public override void DrawCurves()
        {
            
        }


    }
}