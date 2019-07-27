// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ew3/common" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		LOD 600
		Pass {
			Name "DEBUG_Disabled"
			// for shader debugging, uncomment following line.
			//Name "DEBUG"
			
			Cull Back
			Blend Off
			
			CGPROGRAM
			#pragma vertex vs_debug
			#pragma fragment fs_debug
			#pragma glsl_no_auto_normalization
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			
			struct appdata_debug
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
				float3 normal : NORMAL;
			};
			
			struct v2f_debug
			{
				float4 pos : POSITION;
				float4 uvs : TEXCOORD;
			};
			
			v2f_debug vs_debug(appdata_debug i)
			{
				v2f_debug o; 
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uvs.xy = i.texcoord;
				o.uvs.zw = normalize(mul((float3x3)UNITY_MATRIX_MV, i.normal)).xy;
				return o;
			}
			float4 fs_debug(v2f_debug i) : COLOR
			{
				float3 normal = float3(i.uvs.zw, sqrt(dot(i.uvs.zw, i.uvs.zw)));
				return float4(normal * 0.5 + 0.5, 1);
				return tex2D(_MainTex, i.uvs.xy);
			}
			
			ENDCG
		}
	}
	SubShader {
		LOD 200
		Pass {
			Name "BASE"
			
			Lighting Off
			SetTexture [_MainTex] {
				Combine texture
			}
		}
	}	
	SubShader {
		LOD 100
		Pass {
			Name "LOW"
			
			Lighting Off
			Fog {Mode Off}
			SetTexture [_MainTex] {
				Combine texture
			}
		}
	}
}
