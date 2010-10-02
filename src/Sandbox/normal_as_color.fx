float4x4 WorldViewProj : WorldViewProjection;

struct VS_IN
{
	float3 pos : POSITION;
	float3 normal : NORMAL;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float3 normal : NORMAL;
};


PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = mul(WorldViewProj, float4(input.pos.xyz, 1.0));
	output.normal = input.normal;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target
{
	return float4(input.normal.xyz * 0.5 + 0.5, 1.0);
	return float4(abs(input.normal.xyz), 1.0);
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
