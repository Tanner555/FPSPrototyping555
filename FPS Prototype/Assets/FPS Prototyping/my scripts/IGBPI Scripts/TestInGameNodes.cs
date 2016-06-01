using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using NodeBehaviorPrototyping;

namespace IGBPI {
    public class TestInGameNodes : MonoBehaviour {
        private IGBPI_Manager_Master behaviorMaster;
        private IGBPI_UIHandler uihandler;
        private bool runBehaviorUI
        {
            get { return behaviorMaster ? behaviorMaster.isBehaviorUIOn : false; }
        }

        private List<BaseNode> windows = new List<BaseNode>();

        private Vector2 mousePos;

        private BaseNode selectednode;

        private bool makeTransitionMode = false;
        private bool testClickGate = false;

        void OnEnable()
        {
            behaviorMaster = GetComponent<IGBPI_Manager_Master>();
            uihandler = GetComponent<IGBPI_UIHandler>();
            if(!behaviorMaster || !uihandler)
            {
                Debug.LogError("Needed COMPs are missing!");
            }
        }

        //void OnGUI()
        //{
        //    if (runBehaviorUI)
        //    {
        //        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 50, 300, 250));

        //        //the menu background box
        //        GUI.Box(new Rect(0, 0, 300, 250), "");
        //        Event e = Event.current;
        //        mousePos = e.mousePosition;
        //        if (e.button == 1 && e.type == EventType.MouseDown)
        //        {
        //            Debug.Log("E was pressed!");
        //            testClickGate = true;
        //        }

        //        if (testClickGate)
        //            GUI.Label(new Rect(15, 10, 300, 68), "Do you want to travel to ");
        //        //GUI.BeginGroup(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2));
        //        //GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2),"");
        //        //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 150),"Hello");

        //        GUI.EndGroup();
        //    }
        //}

        #region InGameNodesProto

        void OnGUI()
        {
            if (runBehaviorUI)
            {
                GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 50/*Screen.height /2*/, 300/*Screen.width*/, 250/*Screen.height*/));

                //the menu background box
                GUI.Box(new Rect(0, 0, 300, 250), "");
                Event e = Event.current;
                mousePos = e.mousePosition;
                if (e.button == 1 && e.type == EventType.MouseDown)
                {
                    Debug.Log("E was pressed!");
                    testClickGate = true;
                }

                if (testClickGate)
                    GUI.Label(new Rect(15, 10, 300, 68), "Do you want to travel to ");
                //GUI.BeginGroup(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2));
                //GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2),"");
                //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 150),"Hello");

                GUI.EndGroup();
            }

        }

        public static void DrawNodeCurve(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;
            Color shadowCol = new Color(0, 0, 0, 0.06f);

            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
            }

            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
        }

        #endregion
    }
}