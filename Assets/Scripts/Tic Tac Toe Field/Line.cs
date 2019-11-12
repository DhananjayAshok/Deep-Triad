using System.Collections;
using System.Collections.Generic;

public class Line
{
    private static int count = 1;
    private int lineNo;
    public int[] point0, point1, point2;

    public Line(int[] start, int[] direction) {
        point0 = start;
        point1 = Add(start, direction);
        point2 = Add(start, Scale(direction, 2));
        lineNo = count;
        count++;
    }

    private int[] Add(int[] a, int[] b) {
        return new int[3]{a[0]+b[0], a[1]+b[1], a[2]+b[2]};
    }

    private int[] Scale(int[] a, int c) {
        return new int[3] { c * a[0], c * a[1], c * a[2] };
    }

    public bool onLine(int[] point) {
        if (point == point0 || point == point1 || point == point2)
        {
            return true;
        }
        else {
            return false;
        }
    
    }

    override
    public string ToString() {
        return "Line: " + lineNo+". Start - "+point0[0]+""+point0[1]+""+point0[2]+" Middle - "+point1[0]+""+point1[1]+""+point1[2]+" End - "+point2[0]+""+point2[1]+""+point2[2];
    }

}
