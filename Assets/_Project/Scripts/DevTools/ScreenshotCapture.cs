using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JackODrain.DevTools
{
    public sealed class ScreenshotCapture : MonoBehaviour
    {
        private const string EditorCaptureFolder = "docs/media/capturas";
        private const string BuildCaptureFolder = "capturas";

        [SerializeField] private Key _captureKey = Key.F12;

        [Tooltip("Output size multiplier. 1 keeps the Game view resolution.")]
        [SerializeField, Range(1, 4)] private int _sizeMultiplier = 1;

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current[_captureKey].wasPressedThisFrame)
                Capture();
        }

        private void Capture()
        {
            string folder = CaptureFolder();
            Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
            ScreenCapture.CaptureScreenshot(path, _sizeMultiplier);
            Debug.Log($"Screenshot saved to {path}");
        }

        private static string CaptureFolder()
        {
            if (!Application.isEditor)
                return Path.Combine(Application.persistentDataPath, BuildCaptureFolder);

            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            return Path.Combine(projectRoot, EditorCaptureFolder);
        }
    }
}
