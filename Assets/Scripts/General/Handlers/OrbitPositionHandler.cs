using System.Collections.Generic;
using UnityEngine;

public class OrbitPositionHandler
{
    private Dictionary<int, Vector2[]> orbiterPositionDictionary;
    private Vector2[] orbitPositions2, orbitPositions3, orbitPositions4, orbitPositions5;
    private Vector2[] currentOrbitPositions;
    private int numberOfOrbiters;

    public OrbitPositionHandler(int _o)
    {
        orbiterPositionDictionary = new Dictionary<int, Vector2[]>();

        orbitPositions2 = new Vector2[2];
        orbitPositions2[0] = new Vector2(0f,5f);
        orbitPositions2[1] = new Vector2(0f,-5f);
        orbiterPositionDictionary.Add(2,orbitPositions2);

        orbitPositions3 = new Vector2[3];
        orbitPositions3[0] = new Vector2(0f,5f);
        orbitPositions3[1] = new Vector2(-4f,-2.5f);
        orbitPositions3[2] = new Vector2(4f,-2.5f);
        orbiterPositionDictionary.Add(3,orbitPositions3);

        orbitPositions4 = new Vector2[4];
        orbitPositions4[0] = new Vector2(0f,5f);
        orbitPositions4[1] = new Vector2(-5f,0f);
        orbitPositions4[2] = new Vector2(0f,-5f);
        orbitPositions4[3] = new Vector2(5f,0f);
        orbiterPositionDictionary.Add(4,orbitPositions4);

        orbitPositions5 = new Vector2[5];
        orbitPositions5[0] = new Vector2(0f,5f);
        orbitPositions5[1] = new Vector2(-5f,1.5f);
        orbitPositions5[2] = new Vector2(-3f,-4.5f);
        orbitPositions5[3] = new Vector2(3f,-4.5f);
        orbitPositions5[4] = new Vector2(5f, 1.5f);
        orbiterPositionDictionary.Add(5,orbitPositions5);

        numberOfOrbiters = _o;
        currentOrbitPositions = orbiterPositionDictionary[numberOfOrbiters];
    }
    public void UpdateOrbitCount(){numberOfOrbiters++;}
    public Vector2[] GetOrbitPositions(){return currentOrbitPositions;}
}
