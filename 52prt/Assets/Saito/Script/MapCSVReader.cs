using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapCSVReader : MonoBehaviour {

    public string CSVFile;

    public string PrefabName;

    void Start () {
    int i = 0, j;
    //CSVの読み込み(リソースフォルダに入れておく)
        TextAsset csv = Resources.Load("CSV/"+CSVFile) as TextAsset;

    //CSVが読み込めなかった場合実行しない
        if (csv != null)
    {

        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() >- 1)
        {
            i++;
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            for (j = 0; j < values.Length; j++)
                {
                Debug.Log(values[j]);
                //オブジェクトを生成
                    switch (int.Parse(values[j]))
                {
                        case 0: //何も生成しない
                            break;
                        case 1:  //テスト用のオブジェクト生成
                         //プレハブを取得
                            GameObject prefab = (GameObject)Resources.Load(PrefabName);
                        //プレハブからインスタンスを生成
                       Instantiate(prefab, transform.position +new Vector3(1 * j, transform.position.y, 1 - i), Quaternion.identity);
                        break;


                        //default上のcaseに当てはまらなかった場合
                            Debug.Log(null);
                        break;
                }

            }
        }
    }
    else
    {
        Debug.Log("ファイルが見つかりません");
        }
    }
}