using UnityEngine;

public class fighter : MonoBehaviour
{
    int count = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (FindObjectOfType<cameraChanger>().getActiveChara() == this.gameObject)
        {
            transform.rotation = Camera.main.gameObject.transform.rotation;

            if (Input.GetKeyDown(KeyCode.Return) && FindObjectOfType<BattleFlowTest>().ConcentrationMode)
            {
                count++;
                if (count == 2)
                {
                    GetComponent<Animator>().SetBool("punch", true);
                    count = 0;
                }
            }
        }
    }
}
