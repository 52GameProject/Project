using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveBase : MonoBehaviour {
    // 目的地
    public GameObject target_square;
    // 移動するルートを登録
    private List<GameObject> route_;
    // 動いているか
    private bool moving_ = false;
    // 今いるマスを取得する用
    private GameObject now_pos;
    // 経路探索用の数
    private int cost = 99;
    // 実際のコスト
    public int first_cost = 3;
    // 現在使ったコスト
    private int now_cost = 0;
    // 移動時のListの番号用
    private int move_list_num = 0;
    // 移動のLerp用
    private Vector3 start_pos;
    private Vector3 goal_pos;
    // Lerpのタイマー
    private float move_timer = 0.0f;
    // 移動速度
    public float move_speed = 2.0f;
    // コストが余っている状態か
    private bool cost_remainder = false;

    // Use this for initialization
    void Awake () {
        route_ = new List<GameObject>();
        RaycastHit hit;
        if (Physics.SphereCast(transform.position - new Vector3(0, -0.5f, 0), 0.2f, Vector3.down, out hit, 5.0f))
        {
            now_pos = hit.transform.gameObject;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // 移動
        if (moving_) Move();
        else if (Input.GetKeyDown(KeyCode.LeftShift)) SetNextGoal(target_square);
    }

    public void SetNextGoal(GameObject next)
    {
        if (next == now_pos)
        {
            cost_remainder = true;
            return;
        }
        Retrieval();
        target_square = next;
        Search();
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject g in obj)
        {
            g.GetComponent<Square_Info>().DecisionEnd();
        }
        cost_remainder = false;
    }

    void Retrieval()
    {
        Square_Info si_;
        si_ = now_pos.GetComponent<Square_Info>();
        si_.Decision();
        GameObject[] nears_ = si_.GetNear();
        foreach (GameObject n in nears_)
        {
            Square_Info n_si_ = n.GetComponent<Square_Info>();
            if (cost - n_si_.GetCost() >= 0 && n_si_.CanSetCost(cost, now_pos) && first_cost >= n_si_.GetCost())
            {
                n_si_.Decision();

                Retrieval(n, cost);
            }
        }

    }

    void Retrieval(GameObject near, int cost_)
    {
        Square_Info si_;
        si_ = near.GetComponent<Square_Info>();
        GameObject[] nears_ = si_.GetNear();
        cost_ -= si_.GetCost();
        foreach (GameObject n in nears_)
        {
            Square_Info n_si_ = n.GetComponent<Square_Info>();
            if (cost_ - n_si_.GetCost() >= 0 && n_si_.CanSetCost(cost_, near)&&first_cost>= n_si_.GetCost())
            {
                n_si_.Decision();
                Retrieval(n, cost_);
            }
        }
    }

    void Search()
    {
        route_.Add(target_square);
        while (route_[route_.Count - 1] != now_pos)
        {
            //if(route_[route_.Count - 1].GetComponent<Square_Info>().SearchRoute().GetComponent<Square_Info>().GetCost()<=first_cost)
            route_.Add(route_[route_.Count - 1].GetComponent<Square_Info>().SearchRoute());
        }
        if (route_.Count > 0)
        {
            route_.Reverse();
            SetPos(move_list_num);
            moving_ = true;
            now_cost += route_[move_list_num + 1].GetComponent<Square_Info>().GetCost();
        }
    }

    void SetPos(int num)
    {
        start_pos = route_[num].transform.position;
        goal_pos = route_[num + 1].transform.position;
        start_pos.y = transform.position.y;
        goal_pos.y = transform.position.y;
    }

    void Move()
    {
        move_timer += move_speed * Time.deltaTime;
        transform.position = Vector3.Lerp(start_pos, goal_pos, move_timer);
        if (move_timer > 1.0f)
        {
            move_list_num++;
            if (move_list_num < route_.Count - 1 && now_cost + route_[move_list_num + 1].GetComponent<Square_Info>().GetCost() <= first_cost)
            {
                SetPos(move_list_num);
                now_cost += route_[move_list_num + 1].GetComponent<Square_Info>().GetCost();
            }
            else
            {
                moving_ = false;
                RaycastHit hit;
                if (Physics.SphereCast(transform.position - new Vector3(0, -0.5f, 0), 0.2f, Vector3.down, out hit, 5.0f))
                {
                    now_pos = hit.transform.gameObject;
                }
                
                move_list_num = 0;
                route_.Clear();
                if (now_cost < first_cost)
                {
                    cost_remainder = true;
                }
                else
                {
                    now_cost = 0;
                }
            }
            move_timer = 0;
        }
    }

    public bool IsRemainder()
    {
        return cost_remainder;
    }
}
