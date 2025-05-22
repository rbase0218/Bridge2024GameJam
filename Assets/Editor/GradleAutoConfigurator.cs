using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class GradleAutoConfigurator : EditorWindow
{
    private static readonly string gradleWrapperPath = "Assets/Plugins/Android/gradle/wrapper/gradle-wrapper.properties";
    private static readonly string mainTemplatePath = "Assets/Plugins/Android/mainTemplate.gradle";
    private static readonly string launcherGradlePath = "Assets/Plugins/Android/launcherTemplate.gradle";

    [MenuItem("Tools/🔧 Android Gradle 설정 자동화")]
    static void ConfigureGradle()
    {
        UpdateMainTemplateGradle();
        UpdateGradleWrapperProperties();
        UpdateLauncherGradle();
        EditorUtility.DisplayDialog("완료!", "Gradle 및 AGP 설정이 적용되었습니다.\n다시 빌드해보세요!", "OK");
    }

    static void UpdateMainTemplateGradle()
    {
        if (!File.Exists(mainTemplatePath))
        {
            Debug.LogWarning("mainTemplate.gradle 파일이 없습니다. 먼저 'Custom Main Gradle Template'을 활성화하세요.");
            return;
        }

        string content = File.ReadAllText(mainTemplatePath);
        content = Regex.Replace(content,
            @"id\s+'com\.android\.application'\s+version\s+'.*?'\s+apply\s+false",
            "id 'com.android.application' version '8.0.2' apply false");
        content = Regex.Replace(content,
            @"id\s+'com\.android\.library'\s+version\s+'.*?'\s+apply\s+false",
            "id 'com.android.library' version '8.0.2' apply false");

        File.WriteAllText(mainTemplatePath, content);
        Debug.Log("✅ mainTemplate.gradle 수정 완료");
    }

    static void UpdateGradleWrapperProperties()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(gradleWrapperPath));

        string gradleContent = "distributionUrl=https\\://services.gradle.org/distributions/gradle-8.0-all.zip";
        File.WriteAllText(gradleWrapperPath, gradleContent);
        Debug.Log("✅ gradle-wrapper.properties 생성/수정 완료");
    }

    static void UpdateLauncherGradle()
    {
        if (!File.Exists(launcherGradlePath)) return;

        string content = File.ReadAllText(launcherGradlePath);

        // 예시로 compileSdkVersion 같은 것도 설정 강제 가능
        content = Regex.Replace(content,
            @"compileSdkVersion\s+\d+",
            "compileSdkVersion 34");
        content = Regex.Replace(content,
            @"targetSdkVersion\s+\d+",
            "targetSdkVersion 34");
        content = Regex.Replace(content,
            @"minSdkVersion\s+\d+",
            "minSdkVersion 23");

        File.WriteAllText(launcherGradlePath, content);
        Debug.Log("✅ launcherTemplate.gradle 수정 완료");
    }
}
