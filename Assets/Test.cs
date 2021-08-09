using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string moji = "[aaaa][watanabe]力こそパワーだ！";
        //([^)]*)
        Regex regex = new Regex(@"\[([^)]*)\]");
        Match match = regex.Match(moji);
        for( int i = 0; i < match.Groups.Count; i++)
        {
            Debug.Log(match.Groups[i].Value);
        }
        // 1 > match.Groups[1].Value

        string nokori = moji.Replace(match.Groups[0].Value, "");
        Debug.Log(nokori);

        // 2 > nokori
    }
    //​①[ ]でくくられた中の抽出
    //​②[ ] の後の文言の抽出
}
