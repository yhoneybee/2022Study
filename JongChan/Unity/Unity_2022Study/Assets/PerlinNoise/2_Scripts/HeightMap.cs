using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample_Perlin
{
    public class HeightMap : MonoBehaviour
    {
        public int size;
        public GameObject cube;
        public float scale;
        public float m;
        bool move;
        float height;

        void Start()
        {
            move = true;

            for (int x = 0; x < size; x++)
            {
                for (int z = 0; z < size; z++)
                {
                    var c = Instantiate(cube, new Vector3(x, 0, z), Quaternion.identity);

                    c.transform.parent = transform;
                }
            }
        }

        void Update()
        {
            foreach (Transform child in transform)
            {
                height = Mathf.PerlinNoise(child.transform.position.x / scale, child.transform.position.z / scale);

                child.GetComponent<MeshRenderer>().material.color = new Color(height, height, height, height);
            }

            if (move)
            {
                foreach (Transform child in transform)
                {
                    height = Mathf.PerlinNoise(child.transform.position.x / scale, child.transform.position.z / scale);
                    child.transform.position = new Vector3(child.transform.position.x, Mathf.RoundToInt(height * m),
                        child.transform.position.z);

                    // Mathf.RoundToInt() : 매개변수로 받은 두 정수중 가까운 정수를 반환합니다.
                }
            }
        }
    }
}