using UnityEngine;

public class PathNode 
{
    // ���������� ����� �� �����.
    public Tile Position { get; set; }
    // ����� ���� �� ������ (G).
    public int PathLengthFromStart { get; set; }
    // �����, �� ������� ������ � ��� �����.
    public PathNode CameFrom { get; set; }
    // ��������� ���������� �� ���� (H).
    public int HeuristicEstimatePathLength { get; set; }
    // ��������� ������ ���������� �� ���� (F).
    public int EstimateFullPathLength
    {
        get
        {
            return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
        }
    }
}
