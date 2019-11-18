float lightAngle;

float4 ambientalLightColor;
float4 lightColor;

sampler2D input;

float4 PixelShaderFunction(float2 position : TEXCOORD) : COLOR
{
	const float pi = 3.1415926535;
	const float twoSquareRootDecimalPartOnTwo = 0.207106781185; 
	const float center = 0.5;
	
	bool isLighting = false;

	float x = position.x - center;
	float y = position.y - center;

	float limitTangent = tan(lightAngle + (pi / 2));
	float pointTangent = (1 / x) * y;

	float distanceX = x * 2;
	float distanceY = y * 2;

	float lightIntensity = saturate(sqrt(distanceX * distanceX + distanceY * distanceY) - twoSquareRootDecimalPartOnTwo);

	float4 color;

	if (lightAngle >= (3 * pi) / 2)
	{
		if (x >= 0)
		{
			if (pointTangent <= limitTangent)
			{
				isLighting = true;
			}
		}
		else
		{
			if (pointTangent >= limitTangent)
			{
				isLighting = true;
			}
		}
	}
	else if (lightAngle >= pi)
	{
		if (x <= 0)
		{
			if (pointTangent >= limitTangent)
			{
				isLighting = true;
			}
		}
		else
		{
			if (pointTangent <= limitTangent)
			{
				isLighting = true;
			}
		}
	}
	else if (lightAngle >= pi / 2)
	{
		if (x <= 0)
		{
			if (pointTangent <= limitTangent)
			{
				isLighting = true;
			}
		}
		else
		{
			if (pointTangent >= limitTangent)
			{
				isLighting = true;
			}
		}
	}
	else
	{
		if (x >= 0)
		{
			if (pointTangent >= limitTangent)
			{
				isLighting = true;
			}
		}
		else
		{
			if (pointTangent <= limitTangent)
			{
				isLighting = true;
			}
		}

		
	}

	if (isLighting == true)
	{
		color = (tex2D(input, position) * ambientalLightColor * (1 - lightIntensity)) + (tex2D(input, position) * lightColor * lightIntensity);
	}
	else
	{
		color = tex2D(input, position) * ambientalLightColor;
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