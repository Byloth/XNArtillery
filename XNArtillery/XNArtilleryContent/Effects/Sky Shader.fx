float4 topColor;
float4 middleColor;
float4 bottomColor;

float4 PixelShaderFunction(float2 position : TEXCOORD) : COLOR
{
	const float bottomValue = 0.666;

	float y = position.y;
	float4 color = float4(0, 0, 0, 1);

	color.rgb = topColor.rgb * (1 - y) + middleColor.rgb * y;

	if (y >= bottomValue)
	{
		y -= bottomValue;
		y *= 3;

		color.rgb = color.rgb * (1 - y) + bottomColor.rgb * y;
	}

	return color;
}

technique Color
{
    pass Do
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
