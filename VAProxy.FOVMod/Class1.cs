using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ChangeFOVMod
{
    [BepInPlugin("vainstar.FOVMod", "Change FOV Mod", "1.0.0")]
    public class ChangeFOVMod : BaseUnityPlugin
    {
        private const string TargetSceneName = "BirdCage";

        private const float DefaultFOV = 0f;
        private float currentFOV = DefaultFOV;

        private const string FOVKey = "CustomFOV";

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            currentFOV = PlayerPrefs.GetFloat(FOVKey, DefaultFOV);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == TargetSceneName)
            {
                StartCoroutine(SetFOVCoroutine());
            }
        }

        private IEnumerator SetFOVCoroutine()
        {
            while (true)
            {
                var camera = FindObjectOfType<Invector.vCamera.vThirdPersonCamera>();
                if (camera != null)
                {

                    SetCustomFOV(camera, currentFOV);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                IncreaseFOV();
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                DecreaseFOV();
            }
        }

        private void IncreaseFOV()
        {
            currentFOV += 5f;
            ClampFOV();

            PlayerPrefs.SetFloat(FOVKey, currentFOV);
        }

        private void DecreaseFOV()
        {
            currentFOV -= 5f;
            ClampFOV();

            PlayerPrefs.SetFloat(FOVKey, currentFOV);
        }

        private void ClampFOV()
        {

            currentFOV = Mathf.Clamp(currentFOV, 0f, 100f);
        }

        private void SetCustomFOV(Invector.vCamera.vThirdPersonCamera camera, float fov)
        {

            camera.healFOV = fov;
        }
    }
}
