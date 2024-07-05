using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 게임의 인원 수
    public readonly string[] personArray = new string[]{ "3", "4", "5", "6" };
    // 대체 닉네임
    public readonly string[] randNicknameArray = { "정혜원", "김지은", "김수민", "이시원", "류병현", "독고순옥"  };
    
    // 게임 카테고리 Database
    public Dictionary<string, string[]> categoryDic = new Dictionary<string, string[]>();
    public readonly string[] categoryArray = new string[]{ "동", "해", "물", "과" };
    public readonly string[][] wordArray = new string[][]
    {
        new string[] { "사과", "오징어", "배", "감자", "귤" },
        new string[] { "피자", "떡볶이", "탕수육", "와플", "한라봉"},
        new string[] { "인간", "사자", "호랑이", "치타", "표범"},
        new string[] { "브릿지", "다리", "한강대교", "마포대교", "잠실대교"}
    };
    
    public void Init()
    {
        categoryDic = new Dictionary<string, string[]>();
        
        for (int i = 0; i < categoryArray.Length; ++i)
        {
            categoryDic?.Add(categoryArray[i], wordArray[i]);
        }
    }
}
