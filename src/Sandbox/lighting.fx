float4x4 WorldViewProj : WorldViewProjection;
float3 LightDirection = normalize(float3(0.5, -1, 0));

struct VS_IN
{
	float3 pos : POSITION;
	float3 col : COLOR;
	float3 normal : NORMAL;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float3 col : COLOR;
	float3 normal : NORMAL;
};


PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = mul(WorldViewProj, float4(input.pos.xyz, 1.0));
	output.col.xyz = 0.6 * dot(-LightDirection, input.normal) + 0.4;
	output.normal = input.normal;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target
{
	return float4(input.col.rgb, 1.0);
}

technique10 Render
{
	pass P0
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VS() ) );
		SetPixelShader( CompileShader( ps_4_0, PS() ) );
	}
}
