using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_System : MonoBehaviour {
    // 移動コスト
    private int cost;
    // 初期コスト
    public int first_cost = 3;
    // 今いるマスを取得する用
    private GameObject now_pos;
    // 移動するルートを登録
    List<GameObject> route_;
    // 移動のLerp用
    private Vector3 start_pos;
    private Vector3 goal_pos;
    // 動いているか
    private bool moving_ = false;
    // Lerpのタイマー
    private float move_timer = 0.0f;
    // 移動時のListの番号用
    private int move_list_num = 0;
    // 移動速度
    public float move_speed = 2.0f;

    //ここを追加 by齋藤
    BlockMasterScript blockMas;
    //ここまで


    // Use this for initialization
    void Start () {
        cost = first_cost;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position - new Vector3(0,-0.5f,0), 0.2f, Vector3.down, out hit, 5.0f))
        {
            now_pos = hit.transform.gameObject;
        }
        route_ = new List<GameObject>();

        blockMas = FindObjectOfType<BlockMasterScript>();
    }
	
	// Update is called once per frame
	void Update () {
        // 移動可能な範囲を検索する
        if(Input.GetKeyDown(KeyCode.Space)&&!moving_){
            //Retrieval();
        }

        // クリックしたマスが移動可能なマスであれば移動を開始する
        if (Input.GetMouseButtonDown(0) && !moving_)
        {
            
            Ray ray = GameObject.Find("2Dcam").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if(hit.transform.tag == "Floor"&&hit.transform.GetComponent<Square_Info>().IsDecision())
                {
                    //ここを追加 by齋藤
                    blockMas.masterMapHighLightSwitch = false;
                    //ここまで

                    Search(hit.transform.gameObject);
                    GameObject[] obj = GameObject.FindGameObjectsWithTag("Floor");
                    foreach (GameObject g in obj)
                    {
                        g.GetComponent<Square_Info>().DecisionEnd();
                    }
                    //Debug.Log(hit.transform.GetComponent<Square_Info>().SearchRoute().name);
                }
            }
        }

        // 移動
        if (moving_) Move();
    }

    public void Retrieval()
    {
        Square_Info si_;
        si_ = now_pos.GetComponent<Square_Info>();
        si_.Decision();
        GameObject[] nears_ = si_.GetNear();
        foreach (GameObject n in nears_)
        {
            Square_Info n_si_ = n.GetComponent<Square_Info>();
            if (cost - n_si_.GetCost() >= 0 && n_si_.CanSetCost(cost,now_pos))
            {
                n_si_.Decision();
                
                Retrieval(n, cost);
                
            }
        }
        
    }

    void Retrieval(GameObject near,int cost_)
    {
        
        Square_Info si_;
        si_ = near.GetComponent<Square_Info>();
        GameObject[] nears_ = si_.GetNear();
        cost_ -= si_.GetCost();
        foreach (GameObject n in nears_)
        {
            Square_Info n_si_ = n.GetComponent<Square_Info>();
            if (cost_ - n_si_.GetCost() >= 0 && n_si_.CanSetCost(cost_,near))
            {
                n_si_.Decision();
                Retrieval(n,cost_);
            }
        }
    }

    void Search(GameObject hit)
    {
        route_.Add(hit);
        while (route_[route_.Count - 1] != now_pos)
        {
            route_.Add(route_[route_.Count - 1].GetComponent<Square_Info>().SearchRoute());
        }
        if (route_.Count > 0)
        {
            route_.Reverse();
            SetPos(move_list_num);
            moving_ = true;
        }
    }

    void Move()
    {
        move_timer += move_speed * Time.deltaTime;
        transform.position = Vector3.Lerp(start_pos, goal_pos, move_timer);
        if(move_timer > 1.0f)
        {
            move_list_num++;
            if (move_list_num < route_.Count - 1)
            {
                SetPos(move_list_num);
            }
            else
            {
                moving_ = false;
                FindObjectOfType<BattleFlowTest>().MoveChara(this.transform.position);//0927 追加　中東
                route_.Clear();
                RaycastHit hit;
                if (Physics.SphereCast(transform.position - new Vector3(0, -0.5f, 0), 0.2f, Vector3.down, out hit, 5.0f))
                {
                    now_pos = hit.transform.gameObject;
                }
                cost = first_cost;
                move_list_num = 0;
            }
            move_timer = 0;
        }
    }

    void SetPos(int num)
    {
        start_pos = route_[num].transform.position;
        goal_pos = route_[num + 1].transform.position;
        start_pos.y = transform.position.y;
        goal_pos.y = transform.position.y;
    }
}
