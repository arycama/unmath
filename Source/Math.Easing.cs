using UnityEngine;

public static partial class Math
{
    public static float PhysicsMoveTowards(float current, float target, ref float velocity, float acceleration, float deltaTime, float maxDelta = float.PositiveInfinity)
    {
        var isNegative = target < current;
        var c = isNegative ? -Sqrt(2 * acceleration * (current - target)) : Sqrt(2 * acceleration * (target - current));
        var a1 = velocity < c ? acceleration : -acceleration;
        var vs = velocity < c ? velocity : -velocity;

        var sqrtDiscriminant = Sqrt(Max(0, 0.5f * Square(velocity) + a1 * (target - current)));
        var halfwayTime = (sqrtDiscriminant - vs) / acceleration;
       
        if (deltaTime < halfwayTime)
        {
            var result = Displacement(current, velocity, deltaTime, a1);
            velocity = Velocity(velocity, deltaTime, a1);

            // If we're too far after moving, clamp
            if (Abs(result - target) > maxDelta)
            {
                result = target + maxDelta * Sign(result - target);
				velocity = Sqrt(2 * acceleration * Abs(result - target)) * Sign(target - result);
                return result;
            }

            return result;
        }
        else
        {
            // Overshoot check, if we're going to reach the target before deltaTime, simply return the force to reach the target
            var arrivalTime = (2 * sqrtDiscriminant - vs) / acceleration;
            if (arrivalTime <= deltaTime)
            {
                velocity = 0;
                return target;
            }

            var halfwayVelocity = Velocity(velocity, halfwayTime, a1);
            var halfwayPosition = Displacement(current, velocity, halfwayTime, a1);
            var result = Displacement(halfwayPosition, halfwayVelocity, deltaTime - halfwayTime, -a1);

            // If we're too far after moving, clamp
            if (Abs(result - target) > maxDelta)
            {
                result = target + maxDelta * Sign(result - target);
                velocity = Sqrt(2 * acceleration * Abs(result - target)) * Sign(target - result);
				return result;
            }

            velocity = Velocity(halfwayVelocity, deltaTime - halfwayTime, -a1);
			return result;
        }
    }

	public static float PhysicsMoveTowardsAngle(float current, float target, ref float velocity, float acceleration, float deltaTime, float maxDelta = float.PositiveInfinity)
	{
		target = current + Math.DeltaAngle(current, target);
		return PhysicsMoveTowards(current, target, ref velocity, acceleration, deltaTime, maxDelta);
	}
}
