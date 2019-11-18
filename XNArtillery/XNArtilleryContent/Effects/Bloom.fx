#define SAMPLES 15

float Weights[SAMPLES];
float2 HorizontalOffsets[SAMPLES];
float2 VerticalOffsets[SAMPLES];

sampler2D input;
sampler2D originalInput;

float4 Saturate(float2 texCoord : TEXCOORD) : COLOR
{
	const float saturationLevel = 0.25;

	float4 color = tex2D(input, texCoord);

    return saturate((color - saturationLevel) / (1 - saturationLevel));
}

float4 HorizontalBlur(float2 texCoord : TEXCOORD) : COLOR
{	
	float4 color = 0;

	for (int i = 0; i < SAMPLES; i += 1)
    {
        color += tex2D(input, texCoord + HorizontalOffsets[i]) * Weights[i];
    }

    return color;
}

float4 VerticalBlur(float2 texCoord : TEXCOORD) : COLOR
{	
	float4 color = 0;

	for (int i = 0; i < SAMPLES; i += 1)
    {
        color += tex2D(input, texCoord + VerticalOffsets[i]) * Weights[i];
    }

    return color;
}

float4 AdjustSaturation(float4 color)
{
	const float r = 0.3;
	const float g = 0.59;
	const float b = 0.11;
	const float saturation = 1;

    float grey = dot(color, float3(r, g, b));

    return lerp(grey, color, saturation);
}

float4 Combine(float2 texCoord : TEXCOORD) : COLOR
{
	const float bloomIntensity = 1;

    float4 bloommedColor = tex2D(input, texCoord);
    float4 originalColor = tex2D(originalInput, texCoord);
    
    bloommedColor = AdjustSaturation(bloommedColor) * bloomIntensity;
    originalColor = AdjustSaturation(originalColor) * 0.5;
    
    originalColor *= (1 - saturate(bloommedColor));
    
    return (originalColor + bloommedColor);
}

technique Saturation
{
    pass Do
    {
        PixelShader = compile ps_2_0 Saturate();
    }
}

technique HorizontalBlurring
{
	pass Do
	{
		PixelShader = compile ps_2_0 HorizontalBlur();
	}
}

technique VerticalBlurring
{
	pass Do
	{
		PixelShader = compile ps_2_0 VerticalBlur();
	}
}

technique Combining
{
	pass Do
	{
		PixelShader = compile ps_2_0 Combine();
	}
}