using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Ray2D
{
	public Float2 origin, direction;

	public Ray2D(Float2 origin, Float2 direction)
	{
		this.origin = origin;
		this.direction = direction;
	}
}
