using UnityEngine;

public class Knife : MonoBehaviour
{
    public GameObject knife;
    int count = 0;

    void Start()
    {

    }

    void Update()
    {
        if (FindObjectOfType<cameraChanger>().getActiveChara() == gameObject)
        {
            transform.rotation = Camera.main.gameObject.transform.rotation;

            if (Input.GetKeyDown(KeyCode.Return) && FindObjectOfType<BattleFlowTest>().ConcentrationMode)
            {
                count++;
                if (count == 2)
                {
                    knife.GetComponent<Animator>().SetBool("act", true);
                    count = 0;
                }
            }
        }
    }
}
