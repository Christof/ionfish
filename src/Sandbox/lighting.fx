float4x4 WorldViewProj : WorldViewProjection;
float4x4 World : World;
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
	output.col.rgb = input.col.rgb;
	output.normal = mul(World, float4(input.normal.xyz, 1.0)).xyz;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target
{
	input.col.rgb *= 0.6 * dot(-LightDirection, input.normal) + 0.4;
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
