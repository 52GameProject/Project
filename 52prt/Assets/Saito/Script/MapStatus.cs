//ブロック個別に何かしたいときはここに記述します。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatus : MonoBehaviour
{

    //ID番号、ここに該当する数値を入れると残りの内容が反映される
    //Excel依存なので数値を変えたい場合はExcelを変えてください
    [SerializeField]
    private int mapID;
    //地形の名前
    [SerializeField]
    private string mapName;
    /// <summary>
    ///地形のコスト 
    /// </summary>
    public int mapCost;
    /// <summary>
    /// 入れるかどうか(柱などの景観マス)
    /// </summary>
    public int mapInvasion;
    /// <summary>
    /// 回避率の数値
    /// </summary>
    public int mapEvasionRate;

    /// <summary>
    /// 高低差
    /// </summary>
    public float mapHeight;

    //プレイヤーのオブジェクト
    //tagで判断しているのでtagさえ対応させればなんでも取れます
    [SerializeField]
    private GameObject playerObj;

    //ワープ先
    //mapIDが13のときのみ使用
    [SerializeField]
    private GameObject warpPoint;

    //ワープ先の座標の格納先
    [SerializeField]
    private Vector3 warpPointVector;

    void Start()
    {
        Entity_MapStatus mapStatus = Resources.Load("Data/MapStatus") as Entity_MapStatus;

        //Excel内のデータを反映
        mapName = mapStatus.param[mapID].name;
        mapCost = mapStatus.param[mapID].cost;
        mapInvasion = mapStatus.param[mapID].invasion;
        mapEvasionRate = mapStatus.param[mapID].evasionRate;
        mapHeight = mapStatus.param[mapID].height;

        if (warpPoint != null)
        {
            warpPointVector = warpPoint.transform.localPosition;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerObj = collision.gameObject;
            MapStatusBuff();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerObj = null;
        }
    }

    /// <summary>
    /// マップ内の効果
    /// マップ内のダメージはターンの最後に食らわせる
    /// 特殊な床の上に載っている情報だけ保持して、ターン管理でダメ入れる方がいいかも？
    /// </summary>
    public void MapStatusBuff()
    {
        if (playerObj != null)
        {
            //毒ガスマスの処理(スリップダメージ)
            if (mapID == 8)
            {
                Debug.Log("POISON!");
            }
            //マンホールの処理(ワープ)
            if (mapID == 13 && warpPoint != null)
            {
                playerObj.transform.position = warpPointVector;
            }
        }
    }
}
