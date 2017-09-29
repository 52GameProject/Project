using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_Info : MonoBehaviour {
    public int move_cost = 1;
    public GameObject[] near_squares;
    private bool isDecisioned = false;
    private int max_cost = -1;
    private GameObject before_point;

    //追加
    private MapHighLight m_highLight;
    private BlockMasterScript blockMas;
    //ここまで

    // Use this for initialization
    void Start () {

        //追加
        m_highLight = this.gameObject.GetComponent<MapHighLight>();
        blockMas = this.gameObject.GetComponent<MapHighLight>().blockMas;
        //ここまで
    }

    // Update is called once per frame
    void Update () {
		
	}

    public int GetCost()
    {
        return move_cost;
    }

    public GameObject[] GetNear()
    {
        return near_squares;
    }

    public void Decision()
    {
        if (isDecisioned) return;
        isDecisioned = true;
        //ここを追加 by齋藤
        m_highLight.s_mapHighLight = true;
        blockMas.masterMapHighLightSwitch = true;
        //ここまで

        //GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 1);
    }

    public void DecisionEnd()
    {
        isDecisioned = false;
        //追加
        m_highLight.s_mapHighLight = false;
        //ここまで

        //GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
        max_cost = -1;
    }

    public bool IsDecision()
    {
        return isDecisioned;
    }

    public bool CanSetCost(int c,GameObject b)
    {
        if (c > max_cost)
        {
            max_cost = c;
            before_point = b;
            return true;
        }
        return false;
    }

    public GameObject SearchRoute()
    {
        return before_point;
    }
}
