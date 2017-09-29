using UnityEngine;

public class cameraChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject activeChara;
    int no =0;

    float rotation_y = 0.0f;
    float rotation_x = 0.0f;
    void Start()
    {
       
    }

    void Update()
    {
        transform.position = activeChara.transform.position + activeChara.GetComponent<CharaStatus>().cameraPosOffset;

        rotation_y += Input.GetAxis("Horizontal");
        rotation_x += Input.GetAxis("Vertical");
        transform.eulerAngles = new Vector3(-rotation_x, rotation_y, 0);
    }

    public void setActiveChara(GameObject obj)
    {
        activeChara = obj;
    }

    public GameObject getActiveChara()
    {
        return activeChara;
    }
}
