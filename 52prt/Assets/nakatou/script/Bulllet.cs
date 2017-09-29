using UnityEngine;
using System.Collections;

public class Bulllet : MonoBehaviour
{
	public GameObject particle;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall" || other.gameObject.tag == "enemy")
        {
            //GetComponent<MeshRenderer>().enabled = false;

            foreach (ContactPoint point in other.contacts)
            {
                GameObject par = Instantiate(particle, point.point, Quaternion.identity) as GameObject;
                par.GetComponent<ParticleSystem>().Play();

                Destroy(par, 0.5f);
                Destroy(this);
            }
        }
    }
}
