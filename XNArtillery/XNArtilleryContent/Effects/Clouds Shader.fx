float acceptedAngle;
float4 sunColor;

sampler2D input;

float4 PixelShaderFunction(float2 position : TEXCOORD) : COLOR
{
	const float pi = 3.1415926535; 
	const float2 center = float2(0.75, 0.5);

	float4 color = tex2D(input, position) * sunColor;
	
	if (color.a != 0)
	{
		float increment = (pi - acceptedAngle);
		float angle = (atan2(position.y - center.y, position.x - center.x) + increment) - (pi / 2);

		if (angle >= pi * 2)
		{
			angle -= pi * 2;

			if (angle >= pi * 2)
			{
				angle -= pi * 2;
			}
		}
		else if (angle < 0)
		{
			angle += pi * 2;

			if (angle < 0)
			{
				angle += pi * 2;
			}
		}

		if (angle > pi)
		{
			color = 0;
		}
		else
		{
			if (angle > (pi / 2))
			{
				color *= (1 - (angle / pi)) * 2;
			}
			else
			{
				color *= (angle / (pi / 2));
			}
		}
	}
	
	return color;
}

technique Shading
{
    pass Do
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}