#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerInfoViewer))]
public class PlayerInfoViewerEditor : Editor
{
    private PlayerInfoViewer _viewer;

    private void OnEnable()
    {
        _viewer = (PlayerInfoViewer)target;
        EditorApplication.update += Repaint;
    }

    private void OnDisable()
    {
        EditorApplication.update -= Repaint;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("실행 중 GameManager가 존재하면, _allPlayers 정보를 실시간으로 보여줍니다.", MessageType.Info);

        var list = _viewer.GetSnapshot();
        if (list == null || list.Count == 0)
        {
            EditorGUILayout.LabelField("플레이어 정보 없음");
            return;
        }

        foreach (var player in list)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"닉네임: {player.userName}");
            EditorGUILayout.LabelField($"직업: {player.jobType}");
            EditorGUILayout.Toggle("Hostage", player.isHostage);
            EditorGUILayout.Toggle("Dead", player.isDie);
            EditorGUILayout.Toggle("Order", player.isOrder);
            EditorGUILayout.EndVertical();
        }
    }
}
#endif
