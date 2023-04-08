using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MeshGeneration : MonoBehaviour
{
    private List<Vector3> vertices;
    private List<int> triangles;

    public SquareGrid squareGrid;

    /// <summary>
    /// Generates a mesh from a 2D array of integers and a square size.
    /// </summary>
    /// <param name="map">2D array of integers.</param>
    /// <param name="squareSize">Size of the squares.</param>
    public void GenerateMesh(int[,] map, float squareSize)
    {
        squareGrid = new SquareGrid(map, squareSize);

        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int x = 0; x < squareGrid.squares.GetLength(0); x++)
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++)
            {
                TriangulateSquare(squareGrid.squares[x, y]);
            }

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        MeshCollider mesh_collider = GetComponent<MeshCollider>();
        mesh_collider.sharedMesh = mesh;
    }

    /// <summary>
    /// This method triangulates a square based on its configuration.
    /// </summary>
    /// <param name="square">The square to be triangulated.</param>
    private void TriangulateSquare(Square square)
    {
        switch (square.configuration)
        {
            case 0:
                break;
            case 1:
                MeshFromPoints(square.centreBottom, square.buttomLeft, square.centreLeft);
                MeshFromPoints(square.centreBottom, square.centreLeft, square.WcentreLeft, square.WcentreBottom);
                break;
            case 2:
                MeshFromPoints(square.centreRight, square.buttomRight, square.centreBottom);
                MeshFromPoints(square.centreRight, square.centreBottom, square.WcentreBottom, square.WcentreRight);
                break;
            case 4:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight);
                MeshFromPoints(square.centreTop, square.centreRight, square.WcentreRight, square.WcentreTop);
                break;
            case 8:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreLeft);
                MeshFromPoints(square.centreLeft, square.centreTop, square.WcentreTop, square.WcentreLeft);
                break;


            // 2 points:

            case 3:
                MeshFromPoints(square.centreRight, square.buttomRight, square.buttomLeft, square.centreLeft);
                MeshFromPoints(square.centreRight, square.centreLeft, square.WcentreLeft, square.WcentreRight);
                break;
            case 6:
                MeshFromPoints(square.centreTop, square.topRight, square.buttomRight, square.centreBottom);
                MeshFromPoints(square.centreTop, square.centreBottom, square.WcentreBottom, square.WcentreTop);
                break;
            case 9:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreBottom, square.buttomLeft);
                MeshFromPoints(square.centreBottom, square.centreTop, square.WcentreTop, square.WcentreBottom);
                break;
            case 12:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreLeft);
                MeshFromPoints(square.centreLeft, square.centreRight, square.WcentreRight, square.WcentreLeft);
                break;
            case 5:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight, square.centreBottom, square.buttomLeft, square.centreLeft);
                MeshFromPoints(square.centreTop, square.centreLeft, square.WcentreRight, square.WcentreTop);
                MeshFromPoints(square.centreBottom, square.centreRight, square.WcentreRight, square.WcentreBottom);
                break;
            case 10:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.buttomRight, square.centreBottom, square.centreLeft);
                MeshFromPoints(square.centreRight, square.centreTop, square.WcentreTop, square.WcentreRight);
                MeshFromPoints(square.centreLeft, square.centreBottom, square.WcentreBottom, square.WcentreLeft);
                break;


            // 3 points:

            case 7:
                MeshFromPoints(square.centreTop, square.topRight, square.buttomRight, square.buttomLeft, square.centreLeft);
                MeshFromPoints(square.centreTop, square.centreLeft, square.WcentreLeft, square.WcentreTop);
                break;
            case 11:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.buttomRight, square.buttomLeft);
                MeshFromPoints(square.centreRight, square.centreTop, square.WcentreTop, square.WcentreRight);
                break;
            case 13:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreBottom, square.buttomLeft);
                MeshFromPoints(square.centreBottom, square.centreRight, square.WcentreRight, square.WcentreBottom);
                break;
            case 14:
                MeshFromPoints(square.topLeft, square.topRight, square.buttomRight, square.centreBottom, square.centreLeft);
                MeshFromPoints(square.centreLeft, square.centreBottom, square.WcentreBottom, square.WcentreLeft);
                break;


            // 4 points:

            case 15:
                MeshFromPoints(square.topLeft, square.topRight, square.buttomRight, square.buttomLeft);
                break;
        }
    }

    /// <summary>
    /// Creates a mesh from a given set of points.
    /// </summary>
    /// <param name="points">The points to create the mesh from.</param>
    private void MeshFromPoints(params Node[] points)
    {
        AssignVertices(points);

        if (points.Length >= 3)
            CreateTringle(points[0], points[1], points[2]);
        if (points.Length >= 4)
            CreateTringle(points[0], points[2], points[3]);
        if (points.Length >= 5)
            CreateTringle(points[0], points[3], points[4]);
        if (points.Length >= 6)
            CreateTringle(points[0], points[4], points[5]);
    }

    /// <summary>
    /// Assigns a vertex index to each node in the given array and adds the node's position to the vertices list.
    /// </summary>
    /// <param name="points">The array of nodes to assign vertex indices to.</param>
    private void AssignVertices(Node[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {

            if (points[i].vertexIndex == -1)
            {
                points[i].vertexIndex = vertices.Count;
                vertices.Add(points[i].position);
            }
        }
    }

    /// <summary>
    /// Creates a triangle from three nodes and adds it to the triangles list.
    /// </summary>
    /// <param name="a">The first node of the triangle.</param>
    /// <param name="b">The second node of the triangle.</param>
    /// <param name="c">The third node of the triangle.</param>
    private void CreateTringle(Node a, Node b, Node c)
    {
        triangles.Add(a.vertexIndex);
        triangles.Add(b.vertexIndex);
        triangles.Add(c.vertexIndex);
    }

    public class SquareGrid
    {
        public Square[,] squares;
        public float scale = 6f;

        /// <summary>
        /// Constructor for the SquareGrid class.
        /// </summary>
        /// <param name="map">A 2D array of integers representing the map.</param>
        /// <param name="squareSize">The size of each square in the grid.</param>
        /// <returns>
        /// A new SquareGrid object.
        /// </returns>
        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, Mathf.PerlinNoise(x / scale, y / scale), -mapHeight / 2 + y * squareSize + squareSize);
                    controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1, squareSize);
                }

            squares = new Square[nodeCountX - 1, nodeCountY - 1];
            for (int x = 0; x < nodeCountX - 1; x++)
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1], controlNodes[x + 1, y], controlNodes[x, y]);
                }
        }
    }

    public class Square
    {
        public ControlNode topLeft, topRight, buttomRight, buttomLeft;
        public Node centreTop, centreRight, centreBottom, centreLeft;
        public Node WcentreTop, WcentreRight, WcentreBottom, WcentreLeft;
        public int configuration;

        /// <summary>
        /// Constructor for the Square class. 
        /// </summary>
        /// <param name="_topLeft">The top left ControlNode of the Square.</param>
        /// <param name="_topRight">The top right ControlNode of the Square.</param>
        /// <param name="_buttomRight">The bottom right ControlNode of the Square.</param>
        /// <param name="_buttomLeft">The bottom left ControlNode of the Square.</param>
        /// <returns>
        /// A new instance of the Square class.
        /// </returns>
        public Square(ControlNode _topLeft, ControlNode _topRight, ControlNode _buttomRight, ControlNode _buttomLeft)
        {
            topLeft = _topLeft;
            topRight = _topRight;
            buttomRight = _buttomRight;
            buttomLeft = _buttomLeft;

            centreTop = topLeft.right;
            centreRight = buttomRight.above;
            centreBottom = buttomLeft.right;
            centreLeft = buttomLeft.above;

            WcentreTop = topLeft.wallRight;
            WcentreRight = buttomRight.wallAbove;
            WcentreBottom = buttomLeft.wallRight;
            WcentreLeft = buttomLeft.wallAbove;

            if (topLeft.active)
                configuration += 8;
            if (topRight.active)
                configuration += 4;
            if (buttomRight.active)
                configuration += 2;
            if (buttomLeft.active)
                configuration += 1;
        }
    }

    public class Node
    {
        public Vector3 position;
        public int vertexIndex = -1;

        /// <summary>
        /// Constructor for Node class, sets the position of the node.
        /// </summary>
        /// <param name="_pos">The position of the node.</param>
        /// <returns>
        /// A new Node object with the given position.
        /// </returns>
        public Node(Vector3 _pos)
        {
            position = _pos;
        }
    }

    public class ControlNode : Node
    {
        public bool active;
        public Node above, right, wallAbove, wallRight;

        /// <summary>
        /// Constructor for ControlNode class.
        /// </summary>
        /// <param name="_pos">Position of the ControlNode.</param>
        /// <param name="_active">Boolean value to determine if the ControlNode is active.</param>
        /// <param name="squareSize">Size of the square.</param>
        /// <returns>
        /// A new ControlNode object.
        /// </returns>
        public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos)
        {
            active = _active;
            above = new Node(position + Vector3.forward * squareSize / 2f);
            right = new Node(position + Vector3.right * squareSize / 2f);
            wallAbove = new Node(position + Vector3.forward * squareSize / 2f - new Vector3(0, position.y + 0.5f, 0));
            wallRight = new Node(position + Vector3.right * squareSize / 2f - new Vector3(0, position.y + 0.5f, 0));
        }
    }
}