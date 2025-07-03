using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[ExecuteAlways]
public class PlayerInfoViewer : MonoBehaviour
{
    // Editor에서도 확인 가능하게
    [SerializeField, HideInInspector]
    private List<UserInfo> _snapshot = new();

    // 필드 캐시
    private static readonly BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;
    private FieldInfo _fi_gamePlayers;
    private FieldInfo _fi_allPlayers;

    private void OnEnable()
    {
        CacheFields();
    }

    private void Update()
    {
        if (Application.isPlaying)
            UpdateSnapshot();
    }

    private void CacheFields()
    {
        _fi_gamePlayers = typeof(GameManager).GetField("_gamePlayers", Flags);
        var gamePlayersType = typeof(GamePlayers);
        _fi_allPlayers = gamePlayersType.GetField("_allPlayers", Flags);
    }

    private void UpdateSnapshot()
    {
        _snapshot.Clear();

        GameManager gm = Object.FindObjectOfType<GameManager>();
        if (gm == null || _fi_gamePlayers == null || _fi_allPlayers == null)
            return;

        object gamePlayers = _fi_gamePlayers.GetValue(gm);
        if (gamePlayers == null)
            return;

        var list = _fi_allPlayers.GetValue(gamePlayers) as List<UserInfo>;
        if (list != null)
            _snapshot.AddRange(list);
    }

    public IReadOnlyList<UserInfo> GetSnapshot() => _snapshot;
}
