using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace S3
{
    public class GameManager_GoToMenuScene : MonoBehaviour
    {
        private GameManager_Master gameManagerMaster;

        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.GoToMenuSceneEvent += GoToMenuScene;
        }

        void OnDisable()
        {
            gameManagerMaster.GoToMenuSceneEvent -= GoToMenuScene;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void GoToMenuScene()
        {
            SceneManager.LoadScene(0);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}