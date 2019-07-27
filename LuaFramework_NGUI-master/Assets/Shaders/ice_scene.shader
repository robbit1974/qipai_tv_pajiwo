// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ew3/frozen sea" {
	Properties {
		_NormalTex ("Normal Map", 2D) = "normal" {}
		_Reflect("Reflection (RGB)", Cube) = "white" {}
		_MainColor("Main Color", COLOR) = (0,0,0,1)
		_MainColor2("Main Color2", COLOR) = (0,0,0,1)
		_MoveSpeed("Move Speed", Vector) = (0,0,0,0)
	}
	SubShader {
		LOD 600
		UsePass "ew3/common/DEBUG"
	}
	
	SubShader {
		LOD 300
		Tags {"Queue"="Transparent"}
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Back
			
			CGPROGRAM
			#pragma vertex vs300
			#pragma fragment fs300
			#pragma glsl_no_auto_normalization
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			
			sampler2D _NormalTex;
			float4 _NormalTex_ST;
			samplerCUBE _Reflect;
			float4 _MainColor;
			float4 _MoveSpeed;
			struct appdata300{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
				float3 normal : NORMAL;
			};
			struct v2f300 {
				float4 pos : POSITION;
				float4 uvs : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
				
			};
			v2f300 vs300(appdata300 i){
				v2f300 o;
				o.pos = UnityObjectToClipPos(i.vertex);
				float2 normaluv = i.texcoord * _NormalTex_ST.xy + _NormalTex_ST.zw;
				o.uvs.xy = normaluv + _MoveSpeed.xy * _Time.w;
				o.uvs.zw = normaluv + _MoveSpeed.zw * _Time.w;
				float4 world = mul(unity_ObjectToWorld, i.vertex);
				o.worldPos.xyz = world.xyz / world.w;
				float4 view = mul(UNITY_MATRIX_MV, i.vertex);
				o.worldPos.w = -view.z * unity_FogParams.z + unity_FogParams.w; // always linear fog
				return o;
			}
			float4 fs300(v2f300 i) : COLOR {
				float3 view = normalize(i.worldPos.xyz - _WorldSpaceCameraPos.xyz);
				float3 n1 = normalize(tex2D(_NormalTex, i.uvs.zw).xyz * 2 - 1);
				float3 n2 = normalize(tex2D(_NormalTex, i.uvs.xy).xzy * 2 - 1);
				float3 n = normalize((n1 + n2));
				float3 ref = reflect(view, n);
				float4 r = texCUBE(_Reflect, ref);
				
				float d = dot(-view, float3(0, 1, 0)); 
				r = lerp(r, _MainColor, d);
				r.a = saturate(i.worldPos.w);
				
				#if defined(SHADER_API_MOBILE) || defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
				r.xyz = lerp(unity_FogColor.xyz, r.xyz, r.a);
				#endif
				
				r.a *= _MainColor.a;
				
				return r;
			}
			
			ENDCG
		}
	}
	
	SubShader {
		LOD 100
		Tags {"Queue"="Transparent"}
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Back
			
			CGPROGRAM
			#pragma vertex vs100
			#pragma fragment fs100
			#pragma glsl_no_auto_normalization
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			
			sampler2D _NormalTex;
			float4 _NormalTex_ST;
			samplerCUBE _Reflect;
			float4 _MainColor;
			float4 _MainColor2;
			float4 _MoveSpeed;
			struct appdata100{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
				float3 normal : NORMAL;
			};
			struct v2f100 {
				float4 pos : POSITION;
				float4 uvs : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
				
			};
			v2f100 vs100(appdata100 i){
				v2f100 o = (v2f100)0;
				o.pos = UnityObjectToClipPos(i.vertex);
				float2 normaluv = i.texcoord * _NormalTex_ST.xy + _NormalTex_ST.zw;
				o.uvs = normaluv.xyxy + _MoveSpeed * _Time.w;
				float4 world = mul(unity_ObjectToWorld, i.vertex);
				o.worldPos.xyz = world.xyz / world.w;
				float4 view = mul(UNITY_MATRIX_MV, i.vertex);
				o.worldPos.w = -view.z * unity_FogParams.z + unity_FogParams.w; // always linear fog
				return o;
			}
			float4 fs100(v2f100 i) : COLOR {
				float3 n1 = tex2D(_NormalTex, i.uvs.xy).xyz * 2 - 1;
				float3 n2 = tex2D(_NormalTex, i.uvs.zw).xyz * 2 - 1;
				float3 n = normalize(n1 + n2);
				float4 r = _MainColor2;
				r.xyz *= n.y * 0.5 + 0.5;
				r.a = saturate(i.worldPos.w);
				
				#if defined(SHADER_API_MOBILE) || defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
				r.xyz = lerp(unity_FogColor.xyz, r.xyz, r.a);
				#endif
				
				r.a *= _MainColor.a;
				
				return r;
			}
			
			ENDCG
		}
	}
}
