using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControllers
{
    public class SceneSwitch : MonoBehaviour
    {
        private string _nameScene;
        
        public static SceneSwitch Instance;

        private void OnEnable()
        {
            SceneDataLoader.OnInitGlobalControllers += Init;
        }

        private void OnDisable()
        {
            SceneDataLoader.OnInitGlobalControllers -= Init;
        }

        private void Init()
        {
            Instance = this;
        }
        
        public void ChangeScene(string nameScene)
        {
            _nameScene = nameScene;
            LoadingScreenController.Instance.StartAnimationFade(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadSceneAsync(_nameScene);
        }
    }
}
