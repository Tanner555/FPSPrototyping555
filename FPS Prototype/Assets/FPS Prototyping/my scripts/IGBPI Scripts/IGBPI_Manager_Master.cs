using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using S3;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using RTSPrototype;

namespace IGBPI
{
    public class IGBPI_Manager_Master : MonoBehaviour
    {
        public bool isBehaviorUIOn { get { return _isBehaviorUIOn; } }
        private bool _isBehaviorUIOn = false;

        public delegate void GeneralEventHandle();
        //public event GeneralEventHandle EventResetAllPaneUIMenus;

        //public delegate void DropdownInfoEventHandler(UIPanelMenuInfo _info);
        //public event DropdownInfoEventHandler EventAddDropdownInstance;
        //public event DropdownInfoEventHandler EventRemoveDropdownInstance;
        //public event DropdownInfoEventHandler EventUIPanelSelectionChanged;
        //public event DropdownInfoEventHandler EventResetPanelUIMenu;

        //public delegate void PanelDropdownMenuValueChanged(UIPanelMenuInfo _info, Dropdown _dropdown, int _value);
        //public event PanelDropdownMenuValueChanged EventDropdownValueChanged;

        public delegate void ToggleEventHandler(bool _set);
        public event ToggleEventHandler EventToggleBehaviorUI;

        //public void CallEventAddDropdownInstance(UIPanelMenuInfo _info)
        //{
        //    if(EventAddDropdownInstance != null)
        //    {
        //        EventAddDropdownInstance(_info);
        //    }
        //}

        //public void CallEventRemoveDropdownInstance(UIPanelMenuInfo _info)
        //{
        //    if (EventRemoveDropdownInstance != null)
        //    {
        //        EventRemoveDropdownInstance(_info);
        //    }
        //}

        //public void CallEventUIPanelSelectionChanged(UIPanelMenuInfo _info)
        //{
        //    if(EventUIPanelSelectionChanged != null)
        //    {
        //        EventUIPanelSelectionChanged(_info);
        //    }
        //}

        public void CallEventToggleBehaviorUI()
        {
            _isBehaviorUIOn = !_isBehaviorUIOn;
            if (EventToggleBehaviorUI != null)
            {
                EventToggleBehaviorUI(_isBehaviorUIOn);
            }
           
        }

        //public void CallEventDropdownValueChanged(UIPanelMenuInfo _info, Dropdown _dropdown, int _value)
        //{
        //    if(EventDropdownValueChanged != null)
        //    {
        //        EventDropdownValueChanged(_info, _dropdown, _value);
        //    }
        //}

        //public void CallEventResetPanelUIMenu(UIPanelMenuInfo _info)
        //{
        //    if (EventResetPanelUIMenu != null)
        //    {
        //        EventResetPanelUIMenu(_info);
        //    }
        //}

        //public void CallEventResetAllPanelUIMenus()
        //{
        //    if(EventResetAllPaneUIMenus != null)
        //    {
        //        EventResetAllPaneUIMenus();
        //    }
        //}


    }
}