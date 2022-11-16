using Unity.VisualScripting;

public class CellChangedEventArgs
{
    public CellChangedEventArgs(int i, int j, int value)
    {
        this.i = i;
        this.j = j;
        this.value = value;
    }

    public int i;
    public int j;
    public int value;
}