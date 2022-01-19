using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    /// <summary>
    /// PerlinNoise ���� ����� �Լ�
    /// </summary>
    /// <param name="mapWidth">��</param>
    /// <param name="mapHeight">����</param>
    /// <param name="seed">�õ�</param>
    /// <param name="scale">ũ��</param>
    /// <param name="octaves">��Ÿ���� ����</param>
    /// <param name="persistence">���Ӽ�</param>
    /// <param name="lacunarity">������</param>
    /// <param name="offset">��ġ</param>
    /// <returns></returns>
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        // scale�� NoiseMap�� ������ ������ ��ġ�� ����, �׷��� ������ �Ǹ� �ȵ� Ư�� 0�� �ɰ�� DivisonByZero�� ���� ��
        if (scale <= 0) scale = 0.00001f;

        // seed�� ����ũ����Ʈ�� seed�� ������ ������ ��
        // rand�� System.Random �����ڿ� seed�� ���� �޾���
        System.Random rand = new System.Random(seed);
        // octaveOffsets�� ppt�� ������ ��Ÿ����� ��ġ
        Vector2[] octaveOffsets = new Vector2[octaves];

        // ���⿡���� octaveOffset�� ���� �ִ´�
        for (int i = 0; i < octaves; i++)
        {
            // seed���� �ٸ������� �ٸ� ���� ���̰� �ϱ� ���� octave�� ��ġ�� �����ϰ� ��ġ��Ų��
            // �߰��� offset�� ���������� ���Ƿ� ���� �¿�� �����̰� �� �� �ִ�.
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
                // ����
                float amplitude = 1;
                // �󵵼�
                float frequency = 1;
                // ���� x, y������ ����
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    // ���⼭ ��, ������ ������ ���ִ� ������ scale���� ���̰ų� �ٿ����� �߾��� �������� ����, �ƿ������ �ؼ�
                    // frequency�� �������� tempX, Y�� ���� Ŀ���� x, y�ڸ��� tempX, Y�� ���� �ִ´ٰ� �����ϸ� �� �տ��ִ°��� ����ٰ� ���°��̱⿡ �󵵰� ���� Ŀ��
                    // �Ʊ� ������ ���� octaveOffest�� ���ؼ� ����
                    float tempX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float tempY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    // ���ΰ��� ����� ���� 0 ~ 1�� ������ -1 ~ 1�� ������ �ٲ��� -> * 2 - 1
                    float perlinValue = Mathf.PerlinNoise(tempX, tempY) * 2 - 1;
                    // amlitude�� ���ؼ� perlinValue�� ���� ���� �ش�ȭ ��Ŵ ( ���� Ŀ���ų� �۾��� )
                    noiseHeight += perlinValue * amplitude;

                    // ���⼭ �����ִ� ������ amplitude�� frequency�� 0������ 1�̱� ������ 1�̶�� ���� ���� ����ϰ� ���ؼ� ������ ǥ����
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                // noiseHeight�� minNoiseHeight ~ maxNoiseHeight (float)�����ȿ� ���α� ����
                Mathf.Clamp(noiseHeight, minNoiseHeight, maxNoiseHeight);

                // ����
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // InverseLerp�� Lerp�� �����̴� �׷��Ƿ� value�� ������ min(a,b) < x < max(a,b)�̸� ��ȯ���� 0 ~ 1������ ���̴�
                // noiseMap���� 0�� 1������ �ִ� ���� �־�� �ϹǷ� InverseLerp�� ���
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}
