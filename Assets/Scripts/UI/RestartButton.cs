using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WaveTestTask.UI
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(RestartScene);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RestartScene);
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}