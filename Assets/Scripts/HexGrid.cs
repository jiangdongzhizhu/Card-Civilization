using System.Collections.Generic;
using UnityEngine;

public struct HexGridElement<T>
{
    public Vector2Int coordinates;
    public T value;

    public static implicit operator Vector2Int(HexGridElement<T> element)
        => element.coordinates;

    public static implicit operator T(HexGridElement<T> element) => element.value;
}

public class HexGrid<T> where T : new()
{
    private Dictionary<Vector2Int, HexGridElement<T>> dict = new Dictionary<Vector2Int, HexGridElement<T>>();
    private int radius;

    private static readonly Vector2Int[] axialDirectionVectors = new Vector2Int[]
    {
        new Vector2Int(+1, 0), new Vector2Int(+1, -1), new Vector2Int(0, -1),
        new Vector2Int(-1, 0), new Vector2Int(-1, +1), new Vector2Int(0, +1),
    };

    public HexGridElement<T> this[Vector2Int coordinates] => dict[coordinates];

    public HexGrid(int radius)
    {
        this.radius = radius;
        InitializeDict(radius);
    }

    private void InitializeDict(int radius)
    {
        for (int q = -radius; q <= radius; q++)
        {
            for (int r = -radius; r <= radius; r++)
            {
                Vector2Int coordinates = new Vector2Int(q, r);
                int s = -coordinates.x - coordinates.y;
                if (Mathf.Abs(s) > radius) continue;
                HexGridElement<T> hexGridElement = new HexGridElement<T>()
                {
                    coordinates = coordinates,
                    value = new T()
                };
                dict.Add(coordinates, hexGridElement);
            }
        }
    }

    public IEnumerable<HexGridElement<T>> GetAllElements()
    {
        foreach (var element in dict.Values)
        {
            yield return element;
        }
    }

    public static Vector3Int ExpandCoordinates(Vector2Int coordinates)
    {
        int s = -coordinates.x - coordinates.y;
        return new Vector3Int(coordinates.x, coordinates.y, s);
    }

    public bool IsCoordinatesValid(Vector2Int coordinates)
    {
        int distance = Distance(coordinates, Vector2Int.zero);
        return distance <= radius;
    }

    public static int Distance(Vector2Int a, Vector2Int b)
    {
        Vector3Int fullA = ExpandCoordinates(a);
        Vector3Int fullB = ExpandCoordinates(b);
        var delta = fullA - fullB;
        int distance = (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) + Mathf.Abs(delta.z)) / 2;
        return distance;
    }

    public IEnumerable<HexGridElement<T>> SingleRing(Vector2Int center, int radius)
    {
        Vector2Int coordinates = center + axialDirectionVectors[4] * radius;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < radius; j++)
            {
                if (IsCoordinatesValid(coordinates))
                {
                    yield return this[coordinates];
                }
                coordinates += axialDirectionVectors[i];
            }
        }
    }

    public IEnumerable<HexGridElement<T>> Area(Vector2Int center, int radius, bool includingCenter = true)
    {
        if (includingCenter) yield return this[center];
        for (int i = 1; i <= radius; i++)
        {
            foreach (var item in SingleRing(center, i))
            {
                yield return item;
            }
        }
    }

    public Vector2 RealPosition(Vector2Int coordinates, float width, float height)
    {
        float q = coordinates.x;
        float r = coordinates.y;
        float x = width * q + width / 2f * r;
        float y = -0.75f * height * r;
        return new Vector2(x, y);
    }
}