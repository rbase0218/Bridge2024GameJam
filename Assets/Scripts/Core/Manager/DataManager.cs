using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    #region # [ Datas ] #
    
    // 게임 카테고리 ▶ 주제
    public readonly string[] categoryArray = new string[]{ "동물", "음식", "영화 제목", "직업", "나라 이름", "운동", "악기" };
    // 게임 카테고리 ▶ 세부 주제
    public readonly string[][] wordArray = new string[][]
    {
        new string[] { "사자", "호랑이", "코끼리", "기린", "표범", "하이에나", "코알라", "캥거루", "바다표범", "북극곰" },
        new string[] { "피자", "초콜릿", "라면", "햄버거", "스파게티", "초밥", "김밥", "샐러드", "스테이크", "치킨" },
        new string[] { "인셉션", "타이타닉", "어벤져스", "다크나이트", "포레스트 검프", "매트릭스", "해리 포터", "반지의 제왕", "스타워즈", "ET" },
        new string[] { "의사", "변호사", "요리사", "경찰관", "소방관", "간호사", "교사", "그래픽 디자이너", "게임 기획자", "프로그래머" },
        new string[] { "한국", "일본", "중국", "미국", "캐나다", "브라질", "아르헨티나", "영국", "프랑스", "독일" },
        new string[] { "축구", "농구", "테니스", "배드민턴", "골프", "야구", "배구", "핸드볼", "럭비", "족구" },
        new string[] { "피아노", "바이올린", "첼로", "장구", "가야금", "드럼", "기타", "단소", "리코더", "색소폰" }
    };
    
    // JobReveal Screen에서 필요한 Texts
    public readonly Dictionary<EJobType, (string, string)> jobInfoTexts = new Dictionary<EJobType, (string,string)>
    {
        { EJobType.Actor, ("최후 찬스 발동!", "그러나 암살자가\n본인을 드러내 암구호를 외친다면,\n암살자가 승리합니다.") },
        { EJobType.VIP, ("다음 라운드 진행", "앞으로 해당 귀빈은\n발언권은 잃지만,\n투표 권력은 유지합니다.") },
        { EJobType.Assassin, ("최후 찬스 발동!", "과연\n암살자가 암구호를\n파악했을까요?") }
    };
    
    
    #endregion
    
    #region # [ For Debug ] #
    
    // 대체 닉네임
    public readonly string[] exampleNicknameArray = { "정혜원", "김지은", "김수민", "이시원", "류병현", "독고순옥"  };
    
    #endregion

    // 각자 필요한 초상화 데이터
    public Sprite ActorSprite, VipSprite, AssSprite;
    public Sprite Hostage, NobleA, NobleB, Black;
    
    // BG
    public Sprite ActorBG, VipBG, AssBG;
    
    public void Init()
    {
        ActorSprite = Load<Sprite>(@"Sprite/UI/Frame/ActorFrame");
        VipSprite = Load<Sprite>(@"Sprite/UI/Frame/NobleFrame_A");
        AssSprite = Load<Sprite>(@"Sprite/UI/Frame/AssFrame");
        Hostage = Load<Sprite>(@"Sprite/UI/Frame/HostageFrame");
        NobleA = Load<Sprite>(@"Sprite/UI/Frame/NobleFrame_A");
        NobleB = Load<Sprite>(@"Sprite/UI/Frame/NobleFrame_B");
        Black = Load<Sprite>(@"Sprite/UI/Frame/BlackFrame");
        
        ActorBG = Load<Sprite>(@"Sprite/Verify/Play_Verify_ActorBG");
        VipBG = Load<Sprite>(@"Sprite/Verify/Play_Verify_VipBG");
        AssBG = Load<Sprite>(@"Sprite/Verify/Play_Verify_AccBG");
    }

    public Sprite GetFrameSprite(EJobType jobType)
    {
        switch (jobType)
        {
            case EJobType.Actor:
                return ActorSprite;
            case EJobType.VIP:
                return VipSprite;
            case EJobType.Assassin:
                return AssSprite;
            default:
                return null;
        }
    }

    public Sprite GetFrameBGSprite(EJobType jobType)
    {
        switch (jobType)
        {
            case EJobType.Actor:
                return ActorBG;
            case EJobType.VIP:
                return VipBG;
            case EJobType.Assassin:
                return AssBG;
            default:
                return null;
        }
    }

    private T Load<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    public string GetJobText(EJobType jobType)
    {
        switch (jobType)
        {
            case EJobType.Actor:
                return Global.ActorJobText;
            case EJobType.VIP:
                return Global.VipJobText;
            case EJobType.Assassin:
                return Global.AssJobText;
        }
        return "";
    }
}
