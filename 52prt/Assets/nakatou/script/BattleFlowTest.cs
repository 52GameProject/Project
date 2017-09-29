using UnityEngine;
using UnityEngine.Collections;
using System;

public class BattleFlowTest : MonoBehaviour
{
    GameObject[] allChara;

    public int Turn = 0;//ターン数カウント

    public GameObject subCamera;

    public bool ConcentrationMode = false;//集中モード


    void Start()
    {
        setAllChara();
    }

    void Update()
    {
        
    }

    //全てのキャラをスピード順に取得
    void setAllChara()
    {
        allChara = new GameObject[FindObjectsOfType<CharaStatus>().Length];
        if (allChara.Length <= 0) return;

        var i = 0;
        // typeで指定した型の全てのオブジェクトを配列で取得.
        foreach (CharaStatus chara in FindObjectsOfType<CharaStatus>())
        {
            allChara[i] = chara.gameObject;
            i++;
        }
        Array.Sort(allChara, (a, b) => b.GetComponent<CharaStatus>().speed - a.GetComponent<CharaStatus>().speed);
    }

    public void TurnEnd()
    {
        Turn++;
        if (Turn == allChara.Length) Turn = 0;
        FindObjectOfType<cameraChanger>().setActiveChara(allChara[Turn]);
        subCamera.SetActive(true);
    }

    public void fpsBtTest()
    {
        subCamera.SetActive(false);
        
        FindObjectOfType<cameraChanger>().setActiveChara(allChara[Turn]);
        ConcentrationMode = true;
    }

    public void moveBtTest()
    {
        FindObjectOfType<Move_System>().Retrieval();
        FindObjectOfType<cameraChanger>().setActiveChara(allChara[Turn]);
    }

    public void MoveChara(Vector3 position)
    {
        Vector3 pos = FindObjectOfType<cameraChanger>().getActiveChara().transform.position;
        pos.x =position.x * 10;
        pos.z =position.z * 10; 
        FindObjectOfType<cameraChanger>().getActiveChara().transform.position = pos;
    }
}
