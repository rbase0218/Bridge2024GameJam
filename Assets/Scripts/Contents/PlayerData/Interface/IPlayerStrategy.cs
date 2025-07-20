using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStrategy
{
    // Player 클래스에 초기화 하기 위한 데이터
    public void Initialized(List<UserInfo> allPlayers);
    public void Clean();
    
    // 현재 유저를 가져온다.
    public UserInfo GetCurrentPlayer();
    
    // 다음 유저를 가져온다.
    public UserInfo GetNextPlayer();

    // 유저 인덱스를 업데이트한다.
    public void UpdateNextPlayer();
    
    public bool ValidateCurrentAndNextPlayers();

    public bool IsLastPlayer();
}
