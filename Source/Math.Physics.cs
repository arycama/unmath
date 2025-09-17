using static Float3;

public static partial class Math
{
    public static Float3 Gravity => UnityEngine.Physics.gravity;

    public static float Velocity(float v0, float a, float t)
    {
        return v0 + a * t;
    }

    public static float Displacement(float x0, float v0, float t, float a)
    {
        return x0 + v0 * t + 0.5f * a * Square(t);
    }

    public static float Acceleration(float distance, float time)
    {
        return 4 * distance / Square(time);
    }

    /// <summary>
    /// Calculates the distance required to stop based on current velocity and max acceleration
    /// </summary>
    public static float StoppingDistance(float velocity, float acceleration)
    {
        return Square(velocity) / (2 * acceleration);
    }

    public static bool Raycast(Float3 position, Float3 direction, float maxDistance)
    {
        return UnityEngine.Physics.Raycast(position, direction, maxDistance);
    }

    public static float LinearDrag(float acceleration, float velocity, float deltaTime)
    {
        // Terminal velocity for linear drag is acceleration/drag. Solving for drag gives drag = acceleration/velocity, however due to discrete timesteps, it will never reach maxVelocity.
        // Drag is applied like so: vNew = (v + a * dt) * (1 - d * dt). Solving for d when vNew == v, eg v=(v+at)(1-dt) gives d = a/(v+at)
        return acceleration / (velocity + acceleration * deltaTime);
    }

    public static float Acceleration(float velocity, float drag, float deltaTime)
    {
        // Rearrange LinearDragCoefficient to solve for acceleration. This gives an acceleration value required to reach a specific speed given a drag coefficient and timestep
        return -(drag * velocity) / (drag * deltaTime - 1);
    }

    /// <summary>
    /// Calculates force to achieve a target velocity, given a current velocity, drag and deltaTime
    /// </summary>
    //public static float Force(float currentVelocity, float targetVelocity, float drag, float deltaTime)
    //{
    //    // Without drag, would simply be (target - current) / dt. However, drag complicates things as we need to apply more force to overcome the extra drag that will be introduced from the increase of velocity.
    //    // Drag is applied like so: vNew = (v + a * dt) * (1 - d * dt). Since we're solving for a final target velocity, we can simply solve the equation for a, which will give us the required force:
    //    return (targetVelocity - currentVelocity * (1 - drag * deltaTime)) / (deltaTime * (1 - drag * deltaTime));
    //}

    /// <summary> Calculates force to accelerate from one value to another </summary>
    public static float Force(float velocity, float current, float target, float acceleration, float deltaTime)
    {
        var x = deltaTime;
        var delta = target - current;

        var t0 = (Sqrt(0.5f * Square(velocity) + acceleration * delta) - velocity) / acceleration;
        var t1 = (Sqrt(0.5f * Square(velocity) - acceleration * delta) + velocity) / acceleration;
        var t3 = 2 * t0 + velocity / acceleration;
        var t4 = 2 * t1 - velocity / acceleration;
        var t2 = delta < 0 ? (t1 < 0 ? t3 : t4) : (t0 < 0 ? t4 : t3);

        if(t2 <= deltaTime)
        {
            // Overshoot check, if we're going to reach the target before deltaTime, simply return the force to reach the target
            return (0 - velocity) / deltaTime;
        }

        if (delta < 0 && t1 < 0 || delta >= 0 && t0 >= 0)
        {
            if (x < t0)
            {
                return +acceleration;
            }
            else
            {
                return -acceleration + 2 * acceleration * t0 / x;
            }
        }

        if (delta < 0 && t1 >= 0 || delta >= 0 && t0 < 0)
        {
            if (x < t1)
            {
                return -acceleration;
            }
            else
            {
                return +acceleration - 2 * acceleration * t1 / x;
            }
        }

        return 0;

        //var a = acceleration;
        //var d = target - current;
        //var v0 = velocity;
        //var x = deltaTime;

        //var t0 = (Sqrt(0.5f * Square(v0) + a * d) - v0) / a;
        //var t1 = (Sqrt(0.5f * Square(v0) - a * d) + v0) / a;

        //var p0 = x < t0 ? Displacement(0, v0, x, +a) : Displacement(Displacement(0, v0, t0, +a), Velocity(v0, t0, +a), x - t0, -a);
        //var p1 = x < t1 ? Displacement(0, v0, x, -a) : Displacement(Displacement(0, v0, t1, -a), Velocity(v0, t1, -a), x - t1, +a);

        //var p2 = t1 < 0 ? p0 : p1;
        //var p3 = t0 < 0 ? p1 : p0;

        //var p4 = d < 0 ? p2 : p3;

        //// Calculate acceleration required to get to our next target in a single frame
        //var result = 2 * (p4 - velocity * deltaTime) / Square(deltaTime);

        ////var result = (p4 - velocity * deltaTime) / Square(deltaTime);
        //return result;
    }

    public static float AccelerateTowards(ref float velocity, float current, float target, float acceleration, float deltaTime)
    {
        var force = Force(velocity, current, target, acceleration, deltaTime);
        velocity += force * deltaTime;
        return current + velocity * deltaTime;
    }

    public static Float2 Force(Float2 currentVelocity, Float2 targetVelocity, float acceleration, float deltaTime, float drag)
    {
        return (targetVelocity - currentVelocity * (1 - drag * deltaTime)) / (deltaTime * (1 - drag * deltaTime));
    }

    public static float Force(float currentVelocity, float targetVelocity, float acceleration, float deltaTime)
    {
        var deltaV = targetVelocity - currentVelocity;
        var magnitude = Magnitude(deltaV);
        var time = Max(deltaTime, magnitude / acceleration);
        return magnitude > 0 ? deltaV / time : 0;
    }

    public static Float2 Force(Float2 currentVelocity, Float2 targetVelocity, float acceleration, float deltaTime)
    {
        var deltaV = targetVelocity - currentVelocity;
        var magnitude = Float2.Magnitude(deltaV);
        var time = Max(deltaTime, magnitude / acceleration);
        return magnitude > 0 ? deltaV / time : Float2.Zero;
    }

	/// <summary> Force required to achieve target velocity from current, limited to a max acceleration </summary>
	public static Float3 Force(Float3 currentVelocity, Float3 targetVelocity, float acceleration, float deltaTime)
    {
        var deltaV = targetVelocity - currentVelocity;
        var magnitude = Float3.Magnitude(deltaV);
        var time = Max(deltaTime, magnitude / acceleration);
        return magnitude > 0 ? deltaV / time : Zero;
    }

	/// <summary> Accelerates a velocity towards a target value, clapmed by a max acceleration </summary>
	public static Float3 AccelerateTowards(Float3 current, Float3 target, float maxAcceleration, float deltaTime)
	{
		return current + Force(current, target, maxAcceleration, deltaTime) * deltaTime;
	}
}
