using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChangeFOVMod
{
    [BepInPlugin("vainstar.FOVMod", "Change FOV Mod", "1.0.0")]
    public class ChangeFOVMod : BaseUnityPlugin
    {
        private const string TargetSceneName = "BirdCage";
        private const float DefaultFOV = 0f;
        private const string FOVKey = "CustomFOV";

        private float currentFOV = DefaultFOV;
        private Invector.vCamera.vThirdPersonCamera cachedCamera;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            currentFOV = PlayerPrefs.GetFloat(FOVKey, DefaultFOV);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == TargetSceneName)
            {
                cachedCamera = FindObjectOfType<Invector.vCamera.vThirdPersonCamera>();
                if (cachedCamera != null)
                {
                    SetCustomFOV(currentFOV);
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                AdjustFOV(5f);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                AdjustFOV(-5f);
            }
        }

        private void AdjustFOV(float delta)
        {
            currentFOV = Mathf.Clamp(currentFOV + delta, 0f, 100f);
            PlayerPrefs.SetFloat(FOVKey, currentFOV);
            PlayerPrefs.Save();
            SetCustomFOV(currentFOV);
        }

        private void SetCustomFOV(float fov)
        {
            if (cachedCamera != null)
            {
                cachedCamera.healFOV = fov;
            }
        }
    }
}
