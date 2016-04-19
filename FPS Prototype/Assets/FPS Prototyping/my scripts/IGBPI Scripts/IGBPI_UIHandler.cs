using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using S3;
using System.Collections.Generic;
using UnityEditor;
using RTSPrototype;
using System;

namespace IGBPI
{
    //Previously named BehaviorUIManager
    public class IGBPI_UIHandler : MonoBehaviour 
    {
        public GameObject behaviorUI;
        private GameManager_Master gameManagerMaster;
        private IGBPI_Manager_Master behaviorManager;
        public GameObject UI_Panel_Prefab;
        public Transform contentTransform;
        public List<UIPanelMenuInfo> UI_Panel_Members { get { return uipanelmembers; } }
        private List<UIPanelMenuInfo> uipanelmembers;
        public IGBPI_UI_Panel UIPanelSelection
        {
            get { return uipanelselection; }
            set { uipanelselection = value; }
        }

        public IGBPI_UI_Panel PreviousPanelSelection
        {
            get { return previouspanelselection; }
        }

        private IGBPI_UI_Panel uipanelselection = null;
        private IGBPI_UI_Panel previouspanelselection = null;
        //Testing reflection
        //List<MethodInfo> _findIntMethodValues;
        //List<FieldInfo> _findIntFieldValues;

        void Start()
        {
            SetInitialReferences();
            behaviorManager.EventAddDropdownInstance += PrepDropInstanceForAdding;
            behaviorManager.EventRemoveDropdownInstance += PrepDropInstanceForRemoving;
            behaviorManager.EventToggleBehaviorUI += ToggleBehaviorUI;
            behaviorManager.EventUIPanelSelectionChanged += SelectUIPanel;
            uipanelmembers = new List<UIPanelMenuInfo>();
            #region Testing Reflection
            //reflection
            //_findIntMethodValues = new List<MethodInfo>();
            //_findIntFieldValues = new List<FieldInfo>();
            //Debug.Log(typeof(GameObject).Name);
            //foreach (var _field in this.GetType().GetFields())
            //{
            //    if(_field.FieldType.Name == typeof(GameObject).Name)
            //    {
            //        Debug.Log(_field.Name);
            //        Debug.Log(typeof(GameObject).Name);
            //    }
            //}
            //var _methods = this.GetType().GetMethods();
            //var _fields = this.GetType().GetFields();
            //string _gameobject = "string";
            //string _name = this.name;
            ////Debug.Log(_gameobject.GetType().AssemblyQualifiedName);
            //var _gtype = Type.GetType(_gameobject.GetType().AssemblyQualifiedName,false);
            //var _ntype = Type.GetType(_name, false);
            //var _assemName = Assembly.CreateQualifiedName(_gameobject, _gameobject.GetType().AssemblyQualifiedName);
            ////var _printassem = Type.ReflectionOnlyGetType(_gameobject.GetType().AssemblyQualifiedName, false, true);
            //var _printassem = Type.ReflectionOnlyGetType(_assemName, false, true);
            //Debug.Log(_printassem.Name);
            //Type.ReflectionOnlyGetType(_name, false, true);
            //Debug.Log(_gtype != null ? _gtype.Name : "none5");
            //Debug.Log(_ntype != null ? _ntype.Name : "none5");

            //foreach (var _method in _methods)
            //{
            //    if (_method.ReturnType == typeof(GameObject))
            //    {
            //        var _print = _method.Name;
            //        Debug.Log(_print);
            //        _findIntMethodValues.Add(_method);
            //    }
            //}

            //foreach (var _field in _fields)
            //{
            //    if (_field.FieldType == typeof(GameObject))
            //    {
            //        var _print = _field.Name;
            //        Debug.Log(_print);
            //        _findIntFieldValues.Add(_field);
            //    }
            //}
            #endregion
        }

        void OnDisable()
        {
            behaviorManager.EventAddDropdownInstance -= PrepDropInstanceForAdding;
            behaviorManager.EventRemoveDropdownInstance -= PrepDropInstanceForRemoving;
            behaviorManager.EventToggleBehaviorUI -= ToggleBehaviorUI;
            behaviorManager.EventUIPanelSelectionChanged -= SelectUIPanel;
        }

        #region ButtonCalls
        public void CallAddDropdown()
        {
            if(behaviorManager != null)
            {
                UIPanelMenuInfo _ddata = !UIPanelSelection ? _ddata = new UIPanelMenuInfo(null, null) : new UIPanelMenuInfo(UIPanelSelection.gameObject, UIPanelSelection);
                behaviorManager.CallEventAddDropdownInstance(_ddata);
            }
        }

        public void CallRemoveDropdown()
        {
            if (behaviorManager != null && UIPanelSelection != null)
            {
                var _ddata = new UIPanelMenuInfo(UIPanelSelection.gameObject,UIPanelSelection);
                behaviorManager.CallEventRemoveDropdownInstance(_ddata);
            }
        }

        public void CallToggleBehavior()
        {
            if(behaviorManager != null)
            {
                behaviorManager.CallEventToggleBehaviorUI();
            }
        }
        #endregion

        //Behavior Events
        void PrepDropInstanceForAdding(UIPanelMenuInfo _info)
        {
            AddDropdownInstance(_info);
        }

        void PrepDropInstanceForRemoving(UIPanelMenuInfo _info)
        {
            if(_info.myGameObject != null)
            DeregisterDropdownMenu(_info);
        }

        void AddDropdownInstance(UIPanelMenuInfo _info)
        {
            if (!contentTransform || !UI_Panel_Prefab)
                return;
        
             var _optionData = new List<Dropdown.OptionData>();
            var _dropdownInstance = Instantiate(UI_Panel_Prefab);
            _dropdownInstance.transform.SetParent(contentTransform, false);
            var _rect = _dropdownInstance.GetComponent<RectTransform>();
            _rect.localPosition = new Vector3(546.95f, -60f, 0f);
            _rect.pivot = new Vector2(0.5f,0.5f);

            var _mydropdownpanel = _dropdownInstance.GetComponent<IGBPI_UI_Panel>();

            if (_mydropdownpanel != null && _mydropdownpanel.AllMenusAreValid)
            {
                RegisterDropdownMenu(new UIPanelMenuInfo(_dropdownInstance, _mydropdownpanel));
            }
            else
            {
                Debug.LogError("DeRegistering Panel because all requirements were not met!");
                behaviorManager.CallEventRemoveDropdownInstance(_info);
            }

        }

        void SelectUIPanel(UIPanelMenuInfo _info)
        {
            if(_info.myGameObject && _info.myPanel && _info.myPanel.AllMenusAreValid)
            {
                previouspanelselection = UIPanelSelection;
                UIPanelSelection = _info.myPanel;
            }
        }

        void ToggleBehaviorUI(bool _set)
        {
            if (behaviorUI != null)
            {
                behaviorUI.SetActive(_set);
            }
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GameObject.FindObjectOfType<GameManager_Master>();
            behaviorManager = GameObject.FindObjectOfType<IGBPI_Manager_Master>();
            if (!gameManagerMaster)
            {
                Debug.LogError("No gameManagerMaster");
            }
            if (!behaviorManager)
            {
                Debug.LogError("No behavior master!");
            }
        }

        void RegisterDropdownMenu(UIPanelMenuInfo _info)
        {
            uipanelmembers.Add(_info);
        }

        void DeregisterDropdownMenu(UIPanelMenuInfo _info)
        {
            if (UIPanelSelection == _info.myPanel)
                UIPanelSelection = null;

            uipanelmembers.Remove(_info);
            Destroy(_info.myGameObject);
        }


    }

    [System.Serializable]
    public struct UIPanelMenuInfo
    {
        private GameObject _myGameObject;
        private IGBPI_UI_Panel _myUIPanel;
        public GameObject myGameObject { get { return _myGameObject; } }
        public IGBPI_UI_Panel myPanel { get { return _myUIPanel; } }
        
        public UIPanelMenuInfo(GameObject _gameobject, IGBPI_UI_Panel _dropdownpanel)
        {
            _myGameObject = _gameobject;
            _myUIPanel = _dropdownpanel;
        }
    }
}
