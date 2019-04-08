
public struct Coord {

    public int Y { get; set; }

    public int X { get; set; }

    public Coord(int x, int y) : this() {
        Y = y;
        X = x;
    }

    //public override bool Equals(object other) {
    //    Coord temp = (Coord)other;
    //    return X == temp.X && Y == temp.Y;
    //}
}
