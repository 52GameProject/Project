using UnityEngine;

public class trajectPar : MonoBehaviour
{

    void OnParticleCollision(GameObject obj)
    {
        Debug.Log("name " + obj.name);
        FindObjectOfType<Gun>().nowShot = false;
        FindObjectOfType<BattleFlowTest>().TurnEnd();
    }
}
