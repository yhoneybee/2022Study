using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    /// <summary>
    /// PerlinNoise 맵을 만드는 함수
    /// </summary>
    /// <param name="mapWidth">폭</param>
    /// <param name="mapHeight">높이</param>
    /// <param name="seed">시드</param>
    /// <param name="scale">크기</param>
    /// <param name="octaves">옥타브의 개수</param>
    /// <param name="persistence">지속성</param>
    /// <param name="lacunarity">세심함</param>
    /// <param name="offset">위치</param>
    /// <returns></returns>
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        // scale은 NoiseMap의 배율에 영향을 미치는 변수, 그래서 음수가 되면 안됨 특히 0이 될경우 DivisonByZero를 보게 됨
        if (scale <= 0) scale = 0.00001f;

        // seed는 마인크래프트의 seed와 동일한 역할을 함
        // rand는 System.Random 생성자에 seed를 전달 받았음
        System.Random rand = new System.Random(seed);
        // octaveOffsets은 ppt로 설명한 옥타브들의 위치
        Vector2[] octaveOffsets = new Vector2[octaves];

        // 여기에서는 octaveOffset에 값을 넣는다
        for (int i = 0; i < octaves; i++)
        {
            // seed값이 다를때마다 다른 맵이 보이게 하기 위해 octave의 위치를 랜덤하게 위치시킨다
            // 추가로 offset을 더해줌으로 임의로 상하 좌우로 움직이게 할 수 있다.
            float offsetX = rand.Next(-10000, 10000) + offset.x;
            float offsetY = rand.Next(-10000, 10000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float[,] noiseMap = new float[mapWidth, mapHeight];

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // 진폭
                float amplitude = 1;
                // 빈도수
                float frequency = 1;
                // 현재 x, y에서의 높이
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    // 여기서 폭, 높이의 절반을 빼주는 이유는 scale값을 늘이거나 줄였을때 중앙을 기준으로 줌인, 아웃됬으면 해서
                    // frequency가 곱해지면 tempX, Y의 값이 커지고 x, y자리에 tempX, Y의 값을 넣는다고 가정하면 더 앞에있는것을 끌어다가 오는것이기에 빈도가 같이 커짐
                    // 아까 위에서 구한 octaveOffest을 더해서 적용
                    float tempX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float tempY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    // 파인곳도 만들기 위해 0 ~ 1의 범위를 -1 ~ 1의 범위로 바꿔줌 -> * 2 - 1
                    float perlinValue = Mathf.PerlinNoise(tempX, tempY) * 2 - 1;
                    // amlitude를 곱해서 perlinValue의 값을 더욱 극대화 시킴 ( 더욱 커지거나 작아짐 )
                    noiseHeight += perlinValue * amplitude;

                    // 여기서 곱해주는 이유는 amplitude와 frequency의 0제곱은 1이기 때문에 1이라는 값을 먼저 사용하고 곱해서 제곱을 표현함
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                // noiseHeight를 minNoiseHeight ~ maxNoiseHeight (float)범위안에 가두기 위해
                Mathf.Clamp(noiseHeight, minNoiseHeight, maxNoiseHeight);

                // 적용
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // InverseLerp는 Lerp의 반전이다 그러므로 value의 범위는 min(a,b) < x < max(a,b)이며 반환값은 0 ~ 1사이의 값이다
                // noiseMap에는 0과 1사이의 있는 값이 있어야 하므로 InverseLerp를 사용
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}
