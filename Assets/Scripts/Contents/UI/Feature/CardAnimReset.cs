using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

public class CardAnimReset : MonoBehaviour
{
    private MMF_Player _player;

    private void Awake()
    {
        _player = GetComponentInChildren<MMF_Player>(true);
    }

    public void ResetCardAnim()
    {
        _player.ResetFeedbacks();
    }
}