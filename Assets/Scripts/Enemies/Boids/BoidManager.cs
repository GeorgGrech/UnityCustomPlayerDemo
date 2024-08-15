using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {

    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;

    public List<GameObject> boidsTargets;
    private List<Boid> boids;

    //public WaveManager waveManager;

    void Start () {
        /*boids = FindObjectsOfType<Boid> ();
        foreach (Boid b in boids) {
            
            b.Initialize (settings, boidsTargets[Random.Range(0, boidsTargets.Count)].transform);
        }*/

    }

    void Update () {
        if (boids != null && boids.Count>0) {

            int numBoids = boids.Count;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < boids.Count; i++) {
                boidData[i].position = boids[i].position;
                boidData[i].direction = boids[i].forward;
            }

            var boidBuffer = new ComputeBuffer (numBoids, BoidData.Size);
            boidBuffer.SetData (boidData);

            compute.SetBuffer (0, "boids", boidBuffer);
            compute.SetInt ("numBoids", boids.Count);
            compute.SetFloat ("viewRadius", settings.perceptionRadius);
            compute.SetFloat ("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt (numBoids / (float) threadGroupSize);
            compute.Dispatch (0, threadGroups, 1, 1);

            boidBuffer.GetData (boidData);

            for (int i = 0; i < boids.Count; i++)
            {
                if (boids[i] != null)
                {
                    boids[i].avgFlockHeading = boidData[i].flockHeading;
                    boids[i].centreOfFlockmates = boidData[i].flockCentre;
                    boids[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
                    boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;
                    boids[i].UpdateBoid();
                }
            }

            boidBuffer.Release ();
        }
    }

    public struct BoidData {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size {
            get {
                return sizeof (float) * 3 * 5 + sizeof (int);
            }
        }
    }

    public void InitialiseBoid(Boid b)
    {
        b.Initialize(settings, boidsTargets[Random.Range(0, boidsTargets.Count)].transform);
    }

    public void UpdateBoidList(List<Boid> newBoidsList)
    {
        boids = newBoidsList;
    }
}