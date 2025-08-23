using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq;

public class PlayersScrollView : UIBase
{
    private enum ScrollViews
    {
        ScrollView
    }

    private enum Layouts
    {
        Content
    }

    private GameObject[] _playerObjects = new GameObject[10];

    // Init에 실행되지 않는 메서드
    public void Binding()
    {
        Bind<ScrollRect>(typeof(ScrollViews));
        Bind<VerticalLayoutGroup>(typeof(Layouts));
        
        InitializePlayerObjects();
    }

    private void InitializePlayerObjects()
    {
        var scrollRect = Get<ScrollRect>((int)ScrollViews.ScrollView);
        var viewport = scrollRect.viewport;
        var content = viewport.GetComponentInChildren<VerticalLayoutGroup>();
        
        for (int i = 0; i < _playerObjects.Length; ++i)
        {
            _playerObjects[i] = Utils.FindChild(content.gameObject, $"Player_{i + 1}", true);
        }
    }

    public void RefreshPlayerList()
    {
        // Managers.Game이 초기화되었는지 확인
        if (Managers.Game == null)
        {
            Debug.LogWarning("Managers.Game is null. Skipping RefreshPlayerList.");
            return;
        }

        // Managers.Game에서 모든 플레이어 정보를 가져옴
        var allPlayers = Managers.Game.GetAllPlayers();
        if (allPlayers == null)
        {
            Debug.LogWarning("GetAllPlayers returned null. Skipping RefreshPlayerList.");
            return;
        }
        
        var playerNames = allPlayers.Select(p => p.userName).ToArray();
        
        // 플레이어 수만큼 GameObject 활성화
        for (int i = 0; i < _playerObjects.Length; ++i)
        {
            if (_playerObjects[i] != null)
            {
                if (i < playerNames.Length)
                {
                    _playerObjects[i].SetActive(true);
                    
                    // Bar 안의 NameText를 찾아서 플레이어 이름으로 업데이트
                    var bar = Utils.FindChild(_playerObjects[i], "Bar", true);
                    if (bar != null)
                    {
                        var nameText = Utils.FindChild(bar, "NameText", true)?.GetComponent<TMP_Text>();
                        if (nameText != null)
                        {
                            nameText.text = playerNames[i];
                        }
                    }
                    
                    // Picture Image 색깔 업데이트 (죽은 플레이어는 빨간색)
                    var picture = Utils.FindChild(_playerObjects[i], "Picture", true)?.GetComponent<Image>();
                    if (picture != null)
                    {
                        var playerInfo = allPlayers[i];
                        if (playerInfo.isDie)
                        {
                            picture.color = Color.red; // 죽은 플레이어는 빨간색
                        }
                        else
                        {
                            picture.color = Color.white; // 살아있는 플레이어는 흰색
                        }
                    }
                }
                else
                {
                    _playerObjects[i].SetActive(false);
                }
            }
        }

        // Content 높이 조정
        AdjustContentHeight(playerNames.Length);

    }

    private void AdjustContentHeight(int playerCount)
    {
        if (playerCount <= 0) return;

        var scrollRect = Get<ScrollRect>((int)ScrollViews.ScrollView);
        var viewport = scrollRect.viewport;
        var content = viewport.GetComponentInChildren<VerticalLayoutGroup>();
        
        if (content != null)
        {
            var contentRect = content.GetComponent<RectTransform>();
            
            // 고정된 Player 높이와 간격으로 계산
            float playerHeight = 100f; // 고정된 Player 높이
            float spacing = 120f; // 6명일 때 1200이 되도록 계산: (100×6) + (120×5) = 600 + 600 = 1200
            
            // Content 높이 = (플레이어 높이 × 플레이어 수) + (간격 × (플레이어 수 - 1))
            float totalHeight = (playerHeight * playerCount) + (spacing * (playerCount - 1));
            
            // 최소 높이 보장 (Viewport 높이보다 작으면 Viewport 높이로 설정)
            var viewportRect = viewport.GetComponent<RectTransform>();
            float minHeight = viewportRect.rect.height;
            
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, Mathf.Max(totalHeight, minHeight));
        }
    }
}
