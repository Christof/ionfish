float4x4 WorldViewProj : WorldViewProjection;
Texture2D Texture;

SamplerState TextureSampler
{
	Filter = MIN_MAG_MIP_LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};

struct VS_IN
{
	float3 pos : POSITION;
	float3 col : COLOR0;
	float3 normal : NORMAL;
	float2 tex : TEXCOORD;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float3 col : COLOR0;
	float2 tex : TEXCOORD;
};

PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = mul(WorldViewProj, float4(input.pos.xyz, 1.0));
	output.col = input.col;
	output.tex = input.tex;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target
{
	return Texture.Sample(TextureSampler, input.tex);
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
