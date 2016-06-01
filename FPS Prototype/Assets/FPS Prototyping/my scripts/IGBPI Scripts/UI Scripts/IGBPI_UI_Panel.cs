using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using RTSPrototype;
using System;

namespace IGBPI {
    public class IGBPI_UI_Panel : MonoBehaviour {
        #region MenuTypes
        //Conditional Dropdown Menus
        [SerializeField] //Bool, String, or GameObject
        private Dropdown comparisontype;
        //[SerializeField] //First Class to Compare
        //private Dropdown compare_class_01;
        //[SerializeField] //Second Class to Compare
        //private Dropdown compare_class_02;
        //[SerializeField]//First Class Attribute
        //private Dropdown class_01_attribute;
        //[SerializeField]//Second Class Attribute
        //private Dropdown class_02_attribute;
        //[SerializeField] //Greater, Less, or Equal
        //private Dropdown conditionaltype;
        //Action Dropdown Menus
        //[SerializeField] //ClassToCarryOutEvent
        //private Dropdown classtocarryoutevent;
        [SerializeField] //EventFromClassToExecute
        private Dropdown eventfromclasstoexecute;
        //Getters
        public Dropdown ComparisonType { get { return comparisontype; } }
        //public Dropdown Compare_Class_01 { get { return compare_class_01; } }
        //public Dropdown Compare_Class_02 { get { return compare_class_02; } }
        //public Dropdown Class_01_Attribute { get { return class_01_attribute; } }
        //public Dropdown Class_02_Attribute { get { return class_02_attribute; } }
        //public Dropdown ConditionalType { get { return conditionaltype; } }
        //public Dropdown ClassToCarryOutEvent { get { return classtocarryoutevent; } }
        public Dropdown EventFromClassToExecute { get { return eventfromclasstoexecute; } }
        #endregion
        #region UIVariables

        //public bool AllMenusAreValid
        //{
        //    get
        //    {
        //        return (comparisontype && /*compare_class_01 && compare_class_02 && class_01_attribute & class_02_attribute && conditionaltype && classtocarryoutevent &&*/ eventfromclasstoexecute);
        //    }
        //}

        //public bool IsUISelection
        //{
        //    get
        //    {
        //        return behaviorUIManager ? behaviorUIManager.UIPanelSelection == this : false;
        //    }
        //}
        //public List<Dropdown> Dropdown_Menus { get { return UI_Dropdown_Menus; } }
        //private List<Dropdown> UI_Dropdown_Menus;
        private IGBPI_UIHandler behaviorUIManager;
        private IGBPI_Manager_Master behaviorManagerMaster;
        private IGBPI_MenuSelectionHandler behaviorMenuManager;

        #endregion

        // Use this for initialization
        void OnEnable() {
            SetupInitialReferences();
            //behaviorManagerMaster.EventUIPanelSelectionChanged += ChangeUIPanelVisuals;
            //behaviorManagerMaster.EventResetAllPaneUIMenus += ResetUIMenus;
            //behaviorManagerMaster.EventResetPanelUIMenu += ResetUIMenusIfRequired;
        }

        void OnDisable()
        {
            //behaviorManagerMaster.EventUIPanelSelectionChanged -= ChangeUIPanelVisuals;
            //behaviorManagerMaster.EventResetAllPaneUIMenus -= ResetUIMenus;
            //behaviorManagerMaster.EventResetPanelUIMenu -= ResetUIMenusIfRequired;
        }

        void Start()
        {
            //var _info = new UIPanelMenuInfo(this.gameObject, this);
            //behaviorManagerMaster.CallEventResetPanelUIMenu(_info);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    if (behaviorManagerMaster && behaviorUIManager && !IsUISelection && AllMenusAreValid) {
        //        var _info = new UIPanelMenuInfo(this.gameObject, this);                behaviorManagerMaster.CallEventUIPanelSelectionChanged(_info);
        //    }
        //}

        //void OnMenuSelectionChange(Dropdown _menu)
        //{
        //    var _info = new UIPanelMenuInfo(this.gameObject, this);
        //    behaviorManagerMaster.CallEventDropdownValueChanged(_info, _menu, _menu.value);
        //}

        //void ResetUIMenusIfRequired(UIPanelMenuInfo _info)
        //{
        //    if (_info.myPanel == this)
        //        ResetUIMenus();
        //}

        //void ResetUIMenus()
        //{
        //    if (AllMenusAreValid)
        //    {
        //        foreach (var _menu in Dropdown_Menus)
        //        {
        //            //_menu.ClearOptions();
        //            _menu.onValueChanged.RemoveAllListeners();
        //            _menu.onValueChanged.AddListener(delegate { OnMenuSelectionChange(_menu); });
                    
        //        }
        //    }
        //}
        
        //void ChangeUIPanelVisuals(UIPanelMenuInfo _info)
        //{
        //    if (_info.myPanel == this)
        //    {
        //        if (GetComponent<Image>())
        //            GetComponent<Image>().color = Color.blue;
        //    }else if(behaviorUIManager.PreviousPanelSelection == this)
        //    {
        //        if (GetComponent<Image>())
        //            GetComponent<Image>().color = Color.white;
        //    }
        //}

        void SetupInitialReferences()
        {
            behaviorUIManager = GameObject.FindObjectOfType<IGBPI_UIHandler>();
            behaviorManagerMaster = GameObject.FindObjectOfType<IGBPI_Manager_Master>();
            behaviorMenuManager = GameObject.FindObjectOfType<IGBPI_MenuSelectionHandler>();
            if (!behaviorManagerMaster || !behaviorUIManager || !behaviorMenuManager)
                Debug.LogError("No manager or master could be found!");

            //UI_Dropdown_Menus = new List<Dropdown>();
            //if (AllMenusAreValid)
            //{
            //    UI_Dropdown_Menus.Add(comparisontype);
            //    //UI_Dropdown_Menus.Add(compare_class_01);
            //    //UI_Dropdown_Menus.Add(compare_class_02);
            //    //UI_Dropdown_Menus.Add(class_01_attribute);
            //    //UI_Dropdown_Menus.Add(class_02_attribute);
            //    //UI_Dropdown_Menus.Add(conditionaltype);
            //    //UI_Dropdown_Menus.Add(classtocarryoutevent);
            //    UI_Dropdown_Menus.Add(eventfromclasstoexecute);
            //}
            //else
            //{
            //    Debug.LogError("Please drag dropdown instance into dropdown slots!");
            //}

        }

    }
}