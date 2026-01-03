using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "PaperAriplane.aab";
        string apkPath = "PaperAriplane.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "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";
        string keystorePass = "konorwell";
        string keyAlias = "flyali";
        string keyPass = "konorwell";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
