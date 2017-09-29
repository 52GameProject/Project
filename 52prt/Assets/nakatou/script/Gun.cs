using UnityEngine;

public class Gun: MonoBehaviour
{
    public GameObject Muzzle;
    public ParticleSystem particle;
    public ParticleSystem particle2;

    public bool nowShot = false;

    int count = 0;

	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (FindObjectOfType<cameraChanger>().getActiveChara() == gameObject)
        {
            transform.rotation = Camera.main.gameObject.transform.rotation;

            if (Input.GetKeyDown(KeyCode.Return) && !nowShot && FindObjectOfType<BattleFlowTest>().ConcentrationMode)
            {
                count++;
                if (count == 2)
                {
                    shot();
                    nowShot = true;
                    count = 0;
                }
            }
        }
    }

    void shot()
    {
        particle.Play();
        particle2.Play();
    }
}
