using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : PoolObject
{
    public string crystalPref;
    public float speed = 0.2f;
    bool destroyTile = false;

    public Crystal activeCrystal;

    public void GenerateCrystal(Vector3 position)
    {
        activeCrystal = PoolManager.instance.GetObject(crystalPref, position, Quaternion.identity).GetComponent<Crystal>();
        activeCrystal.tile = this;
    }

    public void DestoryTile()
    {
        destroyTile = true;
        DestroyCrystal();
    }

    public void Update()
    {
        if(destroyTile)
            transform.Translate(-Vector3.up * speed);

        if (transform.position.y < -10)
        {
            ReturnToPool();
        }
    }

    public void DestroyCrystal()
    {
        if (activeCrystal != null)
        {
            activeCrystal.ReturnToPool();
            activeCrystal = null;
        }
    }

    public override void ReturnToPool()
    {
        destroyTile = false;
        DestroyCrystal();

        base.ReturnToPool();
    }
}
