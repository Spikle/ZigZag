using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : PoolObject
{
    public int scorePlus = 1;
    public Tile tile;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<SphereController>().TakeCrystal(scorePlus);
            tile.DestroyCrystal();
        }
    }

}
