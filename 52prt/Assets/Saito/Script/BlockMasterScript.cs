//ブロック全体に何かしたいときはここに記述します。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMasterScript : MonoBehaviour {

    /// <summary>
    /// 床の色付けスイッチ(全体)rr
    /// これがfalseだとMapHiLightのs_mapHilightがtrueでも光らないので注意
    /// </summary>
    public bool masterMapHighLightSwitch;



	void Start () {
        masterMapHighLightSwitch = true;
	}
	
	void Update () {
		
	}
}
