Shader "Fur10"
{
	Properties
	{
		_FurLength ("Length", Float) = 1
		_FurDarkening ("Darkening", Float) = 0.1
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NoiseTex ("Transparency (R)", 2D) = "white" {}
	}
	
	Category
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		SubShader
		{
			UsePass "FurBase/AMBIENT0"
			UsePass "FurBase/PPL0"
			UsePass "FurBase/SORTING10"
			
			UsePass "FurBase/AMBIENT1"
			UsePass "FurBase/PPL1"
			UsePass "FurBase/AMBIENT2"
			UsePass "FurBase/PPL2"
			UsePass "FurBase/AMBIENT3"
			UsePass "FurBase/PPL3"
			UsePass "FurBase/AMBIENT4"
			UsePass "FurBase/PPL4"
			UsePass "FurBase/AMBIENT5"
			UsePass "FurBase/PPL5"
			UsePass "FurBase/AMBIENT6"
			UsePass "FurBase/PPL6"
			UsePass "FurBase/AMBIENT7"
			UsePass "FurBase/PPL7"
			UsePass "FurBase/AMBIENT8"
			UsePass "FurBase/PPL8"
			UsePass "FurBase/AMBIENT9"
			UsePass "FurBase/PPL9"
			UsePass "FurBase/AMBIENT10"
			UsePass "FurBase/PPL10"
		}
	}
	
	FallBack "Diffuse"
}
