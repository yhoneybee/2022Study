#include <stdio.h>

int number = 6;	// ��� ����
int INF = 10000000; // ���� ��ġ

//��ü �׷��� *��忡 �����ϴ� �ڽ�Ʈ, �ʱ�ȭ
int a[6][6] = 
{
	{0,     2, 5, 1,   INF, INF},
	{2,     0, 3, 2,   INF, INF},
	{5,     3, 0, 3,   1,   5  },
	{1,     2, 3, 0,   1,   INF},
	{INF, INF, 1, 1,   0,   2  },
	{INF, INF, 5, INF, 2,   0  },
};

bool v[6];	// �湮�� ���
int d[6];	// �Ÿ�

// ���� �ּ� �Ÿ��� ������ ���� ��ȯ (�湮���� ���� ���� �� �ּ� �Ÿ��� ��ȯ)
int getSmallIndex()
{
	// min = ����
	// index = 0;
	int min = INF;
	int index = 0;

	// 0���� ��� ����
	for (int i = 0; i < number; i++)
	{
		// i��° �Ÿ��� ������ ������ ���� ��.
		// �׸��� i��°�� �湮�� ��尡 �ƴ� ��
		if (d[i] < min && !v[i]) 
		{
			// min�� i��° �Ÿ��� ����, index = i;
			min = d[i];
			index = i;
		}
	}

	return index;
}

void dijkstra(int start)
{
	// 0���� ����� ��������
	for (int i = 0; i < number; i++)
	{
		// ���� �������� ��� �������� �Ÿ�
		d[i] = a[start][i];
	}

	// start��° ����� �湮 ���θ� true
	v[start] = true;

	// 0���� ����� ���� - 2(4)������
	for (int i = 0; i < number - 2; i++)
	{
		// current = ���� �ּ� �Ÿ��� ������ ������ ��ȯ
		int current = getSmallIndex();

		// �ּ� �Ÿ��� ������ ������ �湮 ���θ� true
		v[current] = true;

		// 0���� 6����
		for (int j = 0; j < 6; j++)
		{
			// j��° ��� �湮 ���ΰ� false���
			if (!v[j])
			{
				// ���� �ּ� �Ÿ��� ������ ������ �Ÿ� + a�� �������� j�� ���� j��° �Ÿ����� �۴ٸ�
				if (d[current] + a[current][j] < d[j])
				{
					// j��° �Ÿ� = ���� �ּ� �Ÿ��� ������ ������ �Ÿ� + a�� �������� j�� ��;
					// (�� ���� ������ ����)
					d[j] = d[current] + a[current][j];
				}
			}
		}
	}
}

int main(void)
{
	dijkstra(0);
	for (int i = 0; i < number; i++)
	{
		printf("%d ", d[i]);
	}
	return 0;
}