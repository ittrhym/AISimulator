using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour
{

    private class Node
    {
        public string Name { get; private set; }
        public bool Available { get; private set; }
        public bool Completed { get; private set; }
        public List<Node> Next;
        public GameObject GameObject;

        public Node (string name, Texture2D sprite, Node[] next)
        {
            this.Name = name;
            this.Available = false;
            this.Completed = false;
            this.Next = new List<Node>();
            foreach (Node n in next)
            {
                this.Next.Add(n);
            }
            this.GameObject = new GameObject(this.Name);
            SpriteRenderer renderer = this.GameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(
                sprite,
                new Rect(0, 0, sprite.width, sprite.height),
                new Vector2(0, 0)
            );
        }
        public Node (string name, Texture2D sprite) : this(name, sprite, new Node[] {}) {}

        public bool HasNext ()
        {
            return(this.Next.Count > 0);
        }
        public int Depth ()
        {
            int depth = 1;
            if (this.HasNext())
            {
                int greatest = 0;
                foreach (Node node in this.Next)
                {
                    int tmp = node.Depth();
                    if (tmp > greatest) {
                        greatest = tmp;
                    }
                }
                depth += greatest;
            }
            return depth;
        }
        public bool HasNode(int n)
        {
            if (n <= 0)
            {
                return true;
            }
            foreach (Node node in this.Next)
            {
                if (node.HasNode(n-1))
                {
                    return true;
                }
            }
            return false;
        }
        public List<Node> Get (int index)
        {
            List<Node> nodes = new List<Node>();
            if (this.HasNode(index))
            {
                foreach (Node node in this.Next)
                {
                    foreach (Node n in node.Get(index-1))
                    {
                        nodes.Add(n);
                    }
                }
            }
            return nodes;
        }
    }

    public Texture2D _sprite;
    public GameObject _label;

    private Node[] progress;

    void Awake()
    {
    }

    void Start()
    {
        int longestDepth = 0;
        int nodeCount = 0;
        int viewportWidth = 1920;
        int viewportHeight = 1080;
        this.progress = new[] {
            new Node("cooling1", this._sprite, new[] {new Node("cooling2", this._sprite, new[] {new Node("cooling3", this._sprite)})}),
            new Node("power1", this._sprite, new[] {new Node("power2", this._sprite, new[] {new Node("power3", this._sprite)})}),
            new Node("hardware1", this._sprite, new[] {new Node("hardware2", this._sprite, new[] {new Node("hardware3", this._sprite)})})
        };
        foreach (Node node in this.progress)
        {
            int depth = node.Depth();
            if (depth > longestDepth)
            {
                longestDepth = depth;
            }
        }
        print(longestDepth);
        for (int i = 0; i < longestDepth; i++)
        {
            print(i);
            int distance;
            int y = 0;
            List<Node> nodes = new List<Node>();
            foreach (Node root in this.progress)
            {
                print("Hello");
                foreach (Node node in root.Get(i))
                {
                    nodes.Add(node);
                }
            }
            distance = viewportHeight/(nodes.Count+2);
            foreach (Node node in nodes)
            {
                y += distance;
                print(y);
                node.GameObject.transform.position = new Vector3(node.GameObject.transform.position.x, y, 0);
            }
        }
    }

    void Update()
    {
    }
}
