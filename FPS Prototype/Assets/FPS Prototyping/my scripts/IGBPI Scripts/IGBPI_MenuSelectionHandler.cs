using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using S3;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using RTSPrototype;
using System.Reflection;
using System;

namespace IGBPI
{
    //previously named IGBPI_MenuSelection_Manager
    public class IGBPI_MenuSelectionHandler : MonoBehaviour
    {
        private IGBPI_Manager_Master behaviorManagerMaster;
        private IGBPI_UIHandler behaviorUIManager;
        //String Lists
        [SerializeField]
        private List<string> ComparisionTypesStringList;

        void OnEnable()
        {
            SetInitalReferences();
            //behaviorManagerMaster.EventDropdownValueChanged += OnDropdownChangeValue;
            //behaviorManagerMaster.EventResetPanelUIMenu += ResetUIPanelMenu;
            //behaviorManagerMaster.EventResetAllPaneUIMenus += ResetAllUIPanelMenus;
            
        }

        void OnDisable()
        {
            //behaviorManagerMaster.EventDropdownValueChanged -= OnDropdownChangeValue;
            //behaviorManagerMaster.EventResetPanelUIMenu -= ResetUIPanelMenu;
            //behaviorManagerMaster.EventResetAllPaneUIMenus -= ResetAllUIPanelMenus;
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        //void OnDropdownChangeValue(UIPanelMenuInfo _info, Dropdown _dropdown, int _value)
        //{

        //}

        //void ResetUIPanelMenu(UIPanelMenuInfo _info)
        //{
        //    //foreach (var _menu in _info.myPanel.Dropdown_Menus)
        //    //{
        //    //    _menu.interactable = false;
        //    //}
        //    //_info.myPanel.ComparisonType.interactable = true;
        //    //_info.myPanel.ComparisonType.AddOptions(ComparisionTypesStringList);
        //    //foreach (var _opt in _info.myPanel.ComparisonType.options)
        //    //{
        //    //    Debug.Log(_opt.text);
        //    //}
        //}

        void ResetAllUIPanelMenus()
        {

        }

        void SetInitalReferences()
        {
            behaviorManagerMaster = GetComponent<IGBPI_Manager_Master>();
            behaviorUIManager = GetComponent<IGBPI_UIHandler>();
            if (!behaviorManagerMaster || !behaviorUIManager)
                Debug.LogError("Cannot find managers on gobject!");


        }

    }
}