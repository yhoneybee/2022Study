//**************************************************************//
//  Effect File exported by RenderMonkey 1.6
//
//  - Although many improvements were made to RenderMonkey FX  
//    file export, there are still situations that may cause   
//    compilation problems once the file is exported, such as  
//    occasional naming conflicts for methods, since FX format 
//    does not support any notions of name spaces. You need to 
//    try to create workspaces in such a way as to minimize    
//    potential naming conflicts on export.                    
//    
//  - Note that to minimize resulting name collisions in the FX 
//    file, RenderMonkey will mangle names for passes, shaders  
//    and function names as necessary to reduce name conflicts. 
//**************************************************************//

//--------------------------------------------------------------//
// ToonShader
//--------------------------------------------------------------//
//--------------------------------------------------------------//
// Pass 0
//--------------------------------------------------------------//
string ToonShader_Pass_0_Model : ModelData = "D:\\Program Files (x86)\\AMD\\RenderMonkey 1.82\\Examples\\Media\\Models\\Teapot.3ds";

struct VS_INPUT
{
   float4 mPosition : POSITION;
   float3 mNormal: NORMAL;
};

struct VS_OUTPUT
{
   float4 mPosition : POSITION;
   float3 mDiffuse : TEXCOORD1;
};

float4x4 gWorldViewProjectionMatrix : WorldViewProjection;
float4x4 gInvWorldMatrix : WorldInverse;

float4 gWorldLightPosition
<
   string UIName = "gWorldLightPosition";
   string UIWidget = "Direction";
   bool UIVisible =  false;
   float4 UIMin = float4( -10.00, -10.00, -10.00, -10.00 );
   float4 UIMax = float4( 10.00, 10.00, 10.00, 10.00 );
   bool Normalize =  false;
> = float4( 55.78, 27.33, -504.39, 1.00 );

VS_OUTPUT ToonShader_Pass_0_Vertex_Shader_vs_main( VS_INPUT input )
{
   VS_OUTPUT output;
   output.mPosition = mul(input.mPosition,gWorldViewProjectionMatrix );
   float3 objectLightPosition = mul(gWorldLightPosition,gInvWorldMatrix);
   float3 lightDir = normalize(input.mPosition.xyz - objectLightPosition);
   output.mDiffuse = dot(-lightDir,normalize(input.mNormal));
 
   return output;
 
}
float3 gSurfaceColor
<
   string UIName = "gSurfaceColor";
   string UIWidget = "Numeric";
   bool UIVisible =  false;
   float UIMin = -1.00;
   float UIMax = 1.00;
> = float3( 0.00, 1.00, 0.00 );

struct PS_INPUT
{
   float3 mDiffuse : TEXCOORD1;
};

float4 ToonShader_Pass_0_Pixel_Shader_ps_main(PS_INPUT input) : COLOR
{
   float3 diffuse = saturate(input.mDiffuse); 
   diffuse = ceil(diffuse * 5) / 5.0f;
   
   return float4( gSurfaceColor * diffuse.xyz, 1);
}
//--------------------------------------------------------------//
// Technique Section for ToonShader
//--------------------------------------------------------------//
technique ToonShader
{
   pass Pass_0
   {
      VertexShader = compile vs_2_0 ToonShader_Pass_0_Vertex_Shader_vs_main();
      PixelShader = compile ps_2_0 ToonShader_Pass_0_Pixel_Shader_ps_main();
   }

}

