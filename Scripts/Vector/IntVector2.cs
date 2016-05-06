/*
 * Original author Kyle Misner
 * GitHub: https://github.com/ismakefire
 * 
 * An integer version of Unity's Vector2 class.
 */

[System.Serializable]
public struct IntVector2 {
	
	public int x;
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
	public IntVector2 (int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public static IntVector2 operator +(IntVector2 left, IntVector2 right) {
		left.x += right.x;
		left.y += right.y;
		return left;
	}
	
	public static IntVector2 operator -(IntVector2 left, IntVector2 right) {
		left.x -= right.x;
		left.y -= right.y;
		return left;
	}
	
	public static IntVector2 operator -(IntVector2 value) {
		value.x = -value.x;
		value.y = -value.y;
		return value;
	}
	
	public static IntVector2 operator *(IntVector2 left, int rightScale) {
		left.x *= rightScale;
		left.y *= rightScale;
		return left;
	}
	
	public static IntVector2 operator *(int leftScale, IntVector2 right) {
		
		// This assumes the communitative property, a * b = b * a.
		right.x *= leftScale;
		right.y *= leftScale;
		return right;
	}
	
	public static IntVector2 operator /(IntVector2 left, int rightScale) {
		left.x /= rightScale;
		left.y /= rightScale;
		return left;
	}
	
	public static IntVector2 operator %(IntVector2 left, int rightScale) {
		left.x %= rightScale;
		left.y %= rightScale;
		return left;
	}
	
	public static bool operator ==(IntVector2 left, IntVector2 right) {
		return (left.x == right.x && left.y == right.y);
	}
	
	public static bool operator !=(IntVector2 left, IntVector2 right) {
		return (left.x != right.x || left.y != right.y);
	}

	public override bool Equals(object o) {

		if ( !(o is IntVector2) ) {
			return false;
		}

		return (this == (IntVector2)o);
	}
	
	/// <summary>
	/// Returns a nicely formatted string for this vector.
	/// </summary>
	public override string ToString()
	{
		return string.Format("({0}, {1})", x, y);
	}
}