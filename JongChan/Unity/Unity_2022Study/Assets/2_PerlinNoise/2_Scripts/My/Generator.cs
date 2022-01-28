using UnityEngine;

namespace MyCode
{
    public class Generator : MonoBehaviour
    {
        // #pragma warning disable 78
	
        public GameObject dirtPrefab; 
        public GameObject grassPrefab;
        public int Seed;
	
        public int minX = -16;
        public int maxX = 16;
        public int minY = -10;
        public int maxY = 10;
	
        PerlinNoise noise;
	
        void Start () 
        {
            noise = new PerlinNoise(Seed);
            Regenerate();
        }
	
        private void Regenerate()
        {
		
            float width = dirtPrefab.transform.lossyScale.x;
            float height = dirtPrefab.transform.lossyScale.y;
		
            for (int i = minX; i < maxX; i++)//columns x values
            {
                int columnHeight = 2 + noise.getNoise(i - minX, maxY - minY - 2);
			
                for(int j = minY; j < minY + columnHeight; j++)//rows (y values)
                {
                    GameObject block = (j == minY + columnHeight - 1) ? grassPrefab : dirtPrefab;
                    Instantiate(block, new Vector2(i * width, j * height), Quaternion.identity).transform.parent = transform;
                }
            }
        }
    }
}