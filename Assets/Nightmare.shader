// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:7137,x:32719,y:32712,varname:node_7137,prsc:2|clip-802-OUT;n:type:ShaderForge.SFN_Slider,id:4355,x:32070,y:33162,ptovrint:False,ptlb:node_4355,ptin:_node_4355,varname:node_4355,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1.6,cur:-1.6,max:0.49;n:type:ShaderForge.SFN_Tex2d,id:8083,x:32133,y:32970,ptovrint:False,ptlb:node_8083,ptin:_node_8083,varname:node_8083,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6f01a8563d9bf8d4793bb883d5a6f877,ntxv:0,isnm:False|UVIN-5996-UVOUT;n:type:ShaderForge.SFN_Add,id:802,x:32444,y:32883,varname:node_802,prsc:2|A-8083-B,B-4355-OUT,C-557-RGB;n:type:ShaderForge.SFN_Panner,id:5996,x:31935,y:32970,varname:node_5996,prsc:2,spu:0.05,spv:0.05|UVIN-5550-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5550,x:31747,y:32970,varname:node_5550,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:557,x:32133,y:32790,ptovrint:False,ptlb:node_8083_copy,ptin:_node_8083_copy,varname:_node_8083_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6f01a8563d9bf8d4793bb883d5a6f877,ntxv:0,isnm:False|UVIN-2600-UVOUT;n:type:ShaderForge.SFN_Panner,id:2600,x:31935,y:32790,varname:node_2600,prsc:2,spu:0,spv:-0.05|UVIN-9850-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9850,x:31747,y:32790,varname:node_9850,prsc:2,uv:0;proporder:4355-8083-557;pass:END;sub:END;*/

Shader "Unlit/NewUnlitShader" {
    Properties {
        _node_4355 ("node_4355", Range(-1.6, 0.49)) = -1.6
        _node_8083 ("node_8083", 2D) = "white" {}
        _node_8083_copy ("node_8083_copy", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _node_4355;
            uniform sampler2D _node_8083; uniform float4 _node_8083_ST;
            uniform sampler2D _node_8083_copy; uniform float4 _node_8083_copy_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_1668 = _Time + _TimeEditor;
                float2 node_5996 = (i.uv0+node_1668.g*float2(0.05,0.05));
                float4 _node_8083_var = tex2D(_node_8083,TRANSFORM_TEX(node_5996, _node_8083));
                float2 node_2600 = (i.uv0+node_1668.g*float2(0,-0.05));
                float4 _node_8083_copy_var = tex2D(_node_8083_copy,TRANSFORM_TEX(node_2600, _node_8083_copy));
                clip((_node_8083_var.b+_node_4355+_node_8083_copy_var.rgb) - 0.5);
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _node_4355;
            uniform sampler2D _node_8083; uniform float4 _node_8083_ST;
            uniform sampler2D _node_8083_copy; uniform float4 _node_8083_copy_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_4649 = _Time + _TimeEditor;
                float2 node_5996 = (i.uv0+node_4649.g*float2(0.05,0.05));
                float4 _node_8083_var = tex2D(_node_8083,TRANSFORM_TEX(node_5996, _node_8083));
                float2 node_2600 = (i.uv0+node_4649.g*float2(0,-0.05));
                float4 _node_8083_copy_var = tex2D(_node_8083_copy,TRANSFORM_TEX(node_2600, _node_8083_copy));
                clip((_node_8083_var.b+_node_4355+_node_8083_copy_var.rgb) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
