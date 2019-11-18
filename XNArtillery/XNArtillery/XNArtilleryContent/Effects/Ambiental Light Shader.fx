float4 ambientalLightColor;

sampler2D input;

float4 PixelShaderFunction(float2 position : TEXCOORD) : COLOR
{
	return tex2D(input, position) * ambientalLightColor;
}

technique Shading
{
    pass Do
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}