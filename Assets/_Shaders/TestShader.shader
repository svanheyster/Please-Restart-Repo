Shader "TestShader" { // defines the name of the shader 
	Properties{
		_Red("Red", Range(-1,1)) = 0.5
		_Green("Green", Range(-1,1)) = 0.5
		_Blue("Blue", Range(-1,1)) = 0.5
	}
   	SubShader { 
      Pass { 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag
         
         #include "UnityCG.cginc" 
         
         uniform float _Red;
         uniform float _Green;
         uniform float _Blue;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;  
            float3 normal : NORMAL;
            float4 texcoord : TEXCOORD0;   
            fixed4 color : COLOR; 
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 col : TEXCOORD0;
            float3 positionWorld : TEXCOORD1;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            float3 elmibys = float3(_Red, _Green, _Blue);
 
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
            output.positionWorld = mul((float3x3)_Object2World, input.normal);
            //output.col = float4((elmibys + output.positionWorld), 1.0); // set the output color
            output.col = float4((elmibys + input.normal), 1.0); // set the output color

            // other possibilities to play with:

            // output.col = input.vertex;
            // output.col = input.tangent;
            // output.col = float4(input.normal, 1.0);
            // output.col = input.texcoord;
            // output.col = input.texcoord1;
            // output.col = input.color;

            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
            return input.col; 
         }
 
         ENDCG  
      }
   }
}