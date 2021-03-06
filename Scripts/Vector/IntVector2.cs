﻿/*
 * Original author Kyle Misner
 * GitHub: https://github.com/ismakefire
 * 
 * An integer version of Unity's Vector2 class.
 */

[System.Serializable]
public struct IntVector2
{
    /// <summary>
    ///   <para>X component of the vector.</para>
    /// </summary>
    public int x;

    /// <summary>
    ///   <para>Y component of the vector.</para>
    /// </summary>
    public int y;

    /// <summary>
    /// Shorthand for writing new IntVector2(0, 0).
    /// </summary>
    public static readonly IntVector2 zero = new IntVector2(0, 0);

    /// <summary>
    /// Shorthand for writing new IntVector2(1, 1).
    /// </summary>
    public static readonly IntVector2 one = new IntVector2(1, 1);

    /// <summary>
    /// Constructs a new vector with given x, y components.
    /// </summary>
    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static IntVector2 operator +(IntVector2 left, IntVector2 right)
    {
        left.x += right.x;
        left.y += right.y;
        return left;
    }

    public static IntVector2 operator -(IntVector2 left, IntVector2 right)
    {
        left.x -= right.x;
        left.y -= right.y;
        return left;
    }

    public static IntVector2 operator -(IntVector2 value)
    {
        value.x = -value.x;
        value.y = -value.y;
        return value;
    }

    public static IntVector2 operator *(IntVector2 left, int rightScale)
    {
        left.x *= rightScale;
        left.y *= rightScale;
        return left;
    }

    public static IntVector2 operator *(int leftScale, IntVector2 right)
    {

        // This assumes the communitative property, a * b = b * a.
        right.x *= leftScale;
        right.y *= leftScale;
        return right;
    }

    public static IntVector2 operator /(IntVector2 left, int rightScale)
    {
        left.x /= rightScale;
        left.y /= rightScale;
        return left;
    }

    public static IntVector2 operator %(IntVector2 left, int rightScale)
    {
        left.x %= rightScale;
        left.y %= rightScale;
        return left;
    }

    public static bool operator ==(IntVector2 left, IntVector2 right)
    {
        return EqualsInternal(left, right);
    }

    public static bool operator !=(IntVector2 left, IntVector2 right)
    {
        return (EqualsInternal(left, right) == false);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is IntVector2))
        {
            return false;
        }

        IntVector2 left = this;
        IntVector2 right = (IntVector2)obj;

        return EqualsInternal(left, right);
    }

    /// <summary>
    /// Serves as a hash function for a IntVector2.
    /// 
    /// Switching to an approch used by unity, which beats out our old
    /// implementation across the board. Leaving in our old explaination.
    /// 
    /// Old explaination:
    /// Why this implementaiton over others?
    /// 
    /// So the other implementations I've seen and considered are either as fast as possilbe or an even distribution.
    /// 
    /// First let's consider our fast implementations. Modern CPUs care more about operation alignment then the
    /// performance of a single operation, so it's likely that XOR and Multiplication are each faster then a pair of
    /// operations. Assuming this, I ran some some tests and found Subtraction and Multiplication beats out XOR and
    /// Addition for small values and positive values. Multiplication has a pretty big flaw with data sets with a
    /// large number of zeros. So ultimatedly, Subtraction ended up being my favorite choice.
    /// 
    /// Next, when looking at even distributions we're clearly gonna do great for chess boards, but what about
    /// completely noisey values or byte aligned values? For implementations of the form x ^ (y [left] N), N = 16 feels
    /// like an easy choise, but you're assuming the complexity of y comes from it's bottom half, which might not be
    /// the case if we're converting from floats (a common case) or byte packed values (32 bit colors).
    /// 
    /// So for me, N = 13 strikes a fair balance between (x ^ (y [left] 16)) low order perfection and a rotating shift
    /// implementation of the form (x ^ (y [left] N) ^ (y [right] N)), while also avoiding common byte alignments of 8 bits.
    /// 
    /// Finally, combining the solutions into a form such as the following: x - (y [left] 13), sounds reasonable but
    /// would be missleading without data supporting it. So I'm sticking with what's a more readable choice unless
    /// I find further data to do otherwise.
    /// 
    /// Note: I have replaced all uses of the "left bit-shift operator" and the "right bit-shift operator" have been replaces
    /// with [left] and [right] respectively.
    /// 
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
        // Old: return x ^ (y << 13);
        return x.GetHashCode() ^ (y.GetHashCode() << 2);
    }

    private static bool EqualsInternal(IntVector2 left, IntVector2 right)
    {
        return (left.x == right.x && left.y == right.y);
    }

    /// <summary>
    /// Returns a nicely formatted string for this vector.
    /// </summary>
    public override string ToString()
    {
        return string.Format("({0:F1}, {1:F1})", x, y);
    }

    /// <summary>
    /// Returns a nicely formatted string for this vector.
    /// </summary>
    public string ToString(string format)
    {
        return string.Format("({0}, {1})", x.ToString(format), y.ToString(format));
    }
}
