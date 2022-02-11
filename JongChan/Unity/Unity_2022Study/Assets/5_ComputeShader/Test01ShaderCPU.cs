using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01ShaderCPU : MonoBehaviour
{
    //1. 접근한 컴퓨트 셰이더
    public ComputeShader computeShader;
    
    //2. 실행할 커널의 인덱스(쉐이더에 선언된 함수의 인덱스정보)
    private int kernelIndex_FunctionA;
    
    //3. GPU로부터 연산결과를 받아와 저장할 버퍼
    public ComputeBuffer intComputeBuffer;
    
    void Start()
    {
        // 1. 실행할 커널함수의 인덱스를 받아온다.
        kernelIndex_FunctionA = computeShader.FindKernel("GPU_TestFunctionA");
        
        // 2. GPU의 연산결과를 CPU에서 받아오기 위해, 버퍼공간을 할당한다.
        intComputeBuffer = new ComputeBuffer(1024, sizeof(int));
        
        // 3. 해당 커널함수의 실행결과를 저장하는 버퍼 "intBuffer"의 값을, 해당스크립트에서 선언된 intComputeBuffer에 저장한다.
        computeShader.SetBuffer(kernelIndex_FunctionA, "intBuffer", intComputeBuffer);
        
        // 4. CPU스크립트에서 컴퓨트셰이더로 특정 값을 전달하는 방식, 여기선 숫자 1을 전달했다. 
        computeShader.SetInt("intValue", 1);
        
        // 5. 컴퓨트 셰이더를 실행시킨다. 실행할 커널의 인덱스와, 실행할 그룹의 수를 지정한다. 여기선 한개의 그룹실행
        computeShader.Dispatch(kernelIndex_FunctionA, 1024, 1, 1);
        
        // 6. 결과값을 저장할 임시 배열을 만들고
        int[] result = new int[1024];
        
        // 7. 컴퓨트 셰이더 커널의 실행결과를 가져온다
        intComputeBuffer.GetData(result);
        
        // 8. 결과값 확인
        for (int i = 0; i < 1024; i++)
        {
            print(result[i]);
        }
        
        // 9. 사용한 ComputeBuffer는 메모리 할당 헤제가 필요하다
        intComputeBuffer.Release();
    }
}
