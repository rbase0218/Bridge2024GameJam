using UnityEditor;
using UnityEngine;
using System.IO;
using System.Net;
using Debug = UnityEngine.Debug;

public class EDMManagerTool : EditorWindow
{
    [MenuItem("Tools/EDM4U Helper")]
    public static void ShowWindow()
    {
        GetWindow<EDMManagerTool>("EDM4U Helper");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("1. Delete EDM4U"))
        {
            RemoveEDM4U();
        }

        if (GUILayout.Button("2. Reinstall EDM4U and Force Resolve"))
        {
            InstallAndResolve();
        }
    }

    public static void RemoveEDM4U()
    {
        string edmPath = Path.Combine(Application.dataPath, "ExternalDependencyManager");
        if (Directory.Exists(edmPath))
        {
            Directory.Delete(edmPath, true);
            File.Delete(edmPath + ".meta");
            AssetDatabase.Refresh();
            Debug.Log("[EDM] ExternalDependencyManager folder deleted.");
        }
        else
        {
            Debug.LogWarning("[EDM] ExternalDependencyManager folder not found.");
        }
    }

    public static void InstallAndResolve()
    {
        const string url = "https://github.com/googlesamples/unity-jar-resolver/releases/download/v1.2.176/external-dependency-manager-1.2.176.unitypackage";
        string unitypackagePath = Path.Combine(Path.GetTempPath(), "EDM4U.unitypackage");

        using (var client = new WebClient())
        {
            Debug.Log("[EDM] Downloading EDM4U .unitypackage...");
            client.DownloadFile(url, unitypackagePath);
        }

        AssetDatabase.ImportPackage(unitypackagePath, false);
        Debug.Log("[EDM] EDM4U reinstalled via .unitypackage.");

        EditorApplication.delayCall += RunForceResolve;
    }



    private static void RunForceResolve()
    {
        var edmType = System.Type.GetType("GooglePlayServices.PlayServicesResolver, Google.IOSResolver");
        if (edmType != null)
        {
            var method = edmType.GetMethod("MenuForceResolve", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            if (method != null)
            {
                method.Invoke(null, null);
                Debug.Log("[EDM] Force Resolve executed.");
            }
            else
            {
                Debug.LogWarning("[EDM] Could not find ForceResolve method.");
            }
        }
        else
        {
            Debug.LogWarning("[EDM] PlayServicesResolver type not found. Is EDM4U properly installed?");
        }
    }
}
