using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public string[] sceneAddresses;
    public TMP_Text progressText; // Loading progress dikhane ke liye UI Text
    public Slider progressBar; // Loading progress dikhane ke liye UI Slider

    private int totalScenesToLoad;
    private int scenesLoaded;

    void Start()
    {
        totalScenesToLoad = sceneAddresses.Length;
        scenesLoaded = 0;

        // Sabhi scenes ko load karna shuru karo
        foreach (var sceneAddress in sceneAddresses)
        {
            Addressables.LoadSceneAsync(sceneAddress, LoadSceneMode.Additive).Completed += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> handle)
    {
        scenesLoaded++;

        // Progress update karo
        float progress = (float)scenesLoaded / totalScenesToLoad;
        progressText.text = $"Loading... {progress * 100:F0}%";
        progressBar.value = progress;

        // Check karo ki sabhi scenes load ho gaye hain ya nahi
        if (scenesLoaded == totalScenesToLoad)
        {
            Debug.Log("Sabhi scenes load ho gaye hain.");
            // Yahan aap next scene mein ja sakte hain ya loading screen band kar sakte hain
        }
    }
}