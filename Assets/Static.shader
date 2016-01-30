// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:7137,x:32719,y:32712,varname:node_7137,prsc:2|custl-2523-OUT;n:type:ShaderForge.SFN_Noise,id:8967,x:31929,y:33039,varname:node_8967,prsc:2|XY-130-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3234,x:31712,y:33036,varname:node_3234,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:8907,x:31929,y:33200,ptovrint:False,ptlb:Static Size,ptin:_StaticSize,varname:node_8907,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.3;n:type:ShaderForge.SFN_Distance,id:8319,x:32145,y:33079,varname:node_8319,prsc:2|A-8967-OUT,B-8907-OUT;n:type:ShaderForge.SFN_Time,id:9403,x:32145,y:33216,varname:node_9403,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2523,x:32382,y:33093,varname:node_2523,prsc:2|A-3089-OUT,B-8319-OUT;n:type:ShaderForge.SFN_Sign,id:3089,x:32348,y:33216,varname:node_3089,prsc:2|IN-9403-T;n:type:ShaderForge.SFN_Panner,id:130,x:31765,y:33226,varname:node_130,prsc:2,spu:0.01,spv:0|UVIN-3234-UVOUT;proporder:8907;pass:END;sub:END;*/

Shader "Unlit/NewUnlitShader" {
    Properties {
        _StaticSize ("Static Size", Float ) = 0.3
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform float _StaticSize;
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
////// Lighting:
                float4 node_9403 = _Time + _TimeEditor;
                float4 node_4583 = _Time + _TimeEditor;
                float2 node_130 = (i.uv0+node_4583.g*float2(0.01,0));
                float2 node_8967_skew = node_130 + 0.2127+node_130.x*0.3713*node_130.y;
                float2 node_8967_rnd = 4.789*sin(489.123*(node_8967_skew));
                float node_8967 = frac(node_8967_rnd.x*node_8967_rnd.y*(1+node_8967_skew.x));
                float node_2523 = (sign(node_9403.g)*distance(node_8967,_StaticSize));
                float3 finalColor = float3(node_2523,node_2523,node_2523);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
