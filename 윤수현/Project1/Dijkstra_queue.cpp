#include <iostream>
#include <vector>
#include <queue>

using namespace std;

int number = 6;	// 노드의 개수
int INF = 10000000;

vector<pair<int, int>>a[7];	// 인접리스트
int d[7];	// 최소 비용

void dijkstra(int start)
{
	d[start] = 0;
	priority_queue<pair<int, int>>pq;	// 우선 순위 큐
	pq.push(make_pair(start, 0));
	while (!pq.empty())
	{
		int current = pq.top().first;
		int distance = -pq.top().second;
		pq.pop();
		if (d[current] < distance) continue;	// 만약 전노드의 거리가 현재 리턴한 거리 보다 작으면 돌아감
		for (int i = 0; i < a[current].size(); i++)	// a인접리스트의 current번째 배열의 크기만큼 반복함
		{
			int next = a[current][i].first;	// next는 a인접리스트의 current번째 배열의 i번째 first값임
			int nextDistance = distance + a[current][i].second;	// nextDistance는 자기 자신 노드의 거리 + a인접리스트의 current번째 배열의 i번째 second값
			if (nextDistance < d[next])	// 만약 nextDistance의 값이 거리를 저장하는 배열의[next]번째 값보다 작으면
			{
				d[next] = nextDistance;	// 값을 업데이트함
				pq.push(make_pair(next, -nextDistance));	// 우선순위 큐에 값을 넣어줌
			}
		}
	}
}

int main(void)
{
	// 모든 거리들을 최대 값으로 초기화
	for (int i = 1; i <= number; i++)
	{
		d[i] = INF;
	}

	// 그래프 값을 넣어줌
	a[1].push_back(make_pair(2, 2));
	a[1].push_back(make_pair(3, 5));
	a[1].push_back(make_pair(4, 1));

	a[2].push_back(make_pair(1, 2));
	a[2].push_back(make_pair(3, 3));
	a[2].push_back(make_pair(4, 2));

	a[3].push_back(make_pair(1, 5));
	a[3].push_back(make_pair(2, 3));
	a[3].push_back(make_pair(4, 3));
	a[3].push_back(make_pair(5, 1));
	a[3].push_back(make_pair(6, 5));

	a[4].push_back(make_pair(1, 1));
	a[4].push_back(make_pair(2, 2));
	a[4].push_back(make_pair(3, 3));
	a[4].push_back(make_pair(5, 1));

	a[5].push_back(make_pair(3, 1));
	a[5].push_back(make_pair(4, 1));
	a[5].push_back(make_pair(6, 2));

	a[6].push_back(make_pair(3, 5));
	a[6].push_back(make_pair(5, 2));

	dijkstra(1);

	// 최소 코스트 탐색 값 출력
	for (int i = 1; i <= number; i++)
	{
		printf("%d ", d[i]);
	}

	return 0;
}