using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using TinCan;
using UnityEngine;
using Xasu;
using Xasu.HighLevel;
using Xasu.Util;

public class GameAnalyticsController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await Task.Yield();
        CompletableTracker.Instance.Initialized("Tattered-wings", CompletableTracker.CompletableType.Game);
    }


    private void OnApplicationQuit()
    {
        CompletableTracker.Instance.Completed("Tattered-wings", CompletableTracker.CompletableType.Game);
    }

}
