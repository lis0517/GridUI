using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IntVector2
{
    public int x;
    public int y;

    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public IntVector2(float x, float y)
    {
        this.x = (int)x;
        this.y = (int)y;
    }

    public IntVector2(Vector2 v)
    {
        x = (int)v.x;
        y = (int)v.y;
    }

    public void Set(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return ($"{x}, {y}");
    }

    #region < Predefined >

    public static IntVector2 one
    {
        get { return new IntVector2(1, 1); }
    }

    public static IntVector2 negativeOne
    {
        get { return new IntVector2(-1, -1); }
    }

    public static IntVector2 zero
    {
        get { return new IntVector2(0, 0); }
    }
    public static IntVector2 up
    {
        get { return new IntVector2(0, 1); }
    }
    public static IntVector2 down
    {
        get { return new IntVector2(0, -1); }
    }
    public static IntVector2 left
    {
        get { return new IntVector2(-1, 0); }
    }
    public static IntVector2 right
    {
        get { return new IntVector2(1, 0); }
    }

    #endregion

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.x + b.x, a.y + b.y);
    }
    public static IntVector2 operator +(IntVector2 a, int b)
    {
        return new IntVector2(a.x + b, a.y + b);
    }
    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.x - b.x, a.y - b.y);
    }
    public static IntVector2 operator -(IntVector2 a, int b)
    {
        return new IntVector2(a.x - b, a.y - b);
    }
    public static IntVector2 operator *(IntVector2 a, int b)
    {
        return new IntVector2(a.x * b, a.y * b);
    }
    public static IntVector2 operator /(IntVector2 a, int b)
    {
        return new IntVector2(a.x / b, a.y / b);
    }

    public static float Slope(IntVector2 a)
    {
        return ((float)a.y / (float)a.x);
    }

    public static bool operator ==(IntVector2 a, IntVector2 b)
    {
        if ((a.x == b.x) && (a.y == b.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(IntVector2 a, IntVector2 b)
    {
        if ((a.x != b.x) || (a.y != b.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool Equals(object o)
    {
        return true;
    }
    public override int GetHashCode()
    {
        return 0;
    }
}
