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
            if (index <= 0)
            {
                nodes.Add(this);
                return nodes;
            }
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
        public IEnumerable<Node> Children ()
        {
            List<Node> nodes = new List<Node>();
            yield return this;
            if (this.HasNext())
            {
                foreach (Node node in this.Next)
                {
                    foreach (Node n in node.Children())
                    {
                        yield return n;
                    }
                }
            }
        }
    }

    public Texture2D _sprite;
    public GameObject _label;
    public Material _lineMaterial;

    private Node[] progress;

    void Awake()
    {
    }

    void Start()
    {
        int longestDepth = 0;
        this.progress = new[] {
            new Node("main", this._sprite, new[] {
                new Node("Power", this._sprite, new[] {
                    new Node("Renewable Power", this._sprite, new[] {
                        new Node("Improved Renewables", this._sprite, new[] {
                            new Node("Nuclear Power", this._sprite, new[] {
                                new Node("Spent Fuel Recycling", this._sprite, new[] {
                                    new Node("Nuclear Fusion", this._sprite)
                                })
                            })
                        })
                    }),
                    new Node("Coal Power", this._sprite, new[] {
                        new Node("Improved Powerplants", this._sprite, new[] {
                            new Node("Improved Mining", this._sprite, new[] {
                                new Node("Cyclone Furnace", this._sprite, new[] {
                                    new Node("Cogeneration", this._sprite)
                                })
                            })
                        })
                    })
                }),
                new Node("Cooling", this._sprite, new[] {
                    new Node("Water Recycling", this._sprite, new[] {
                        new Node("Lake Cooling", this._sprite)
                    }),
                    new Node("New Water Chillers", this._sprite)
                }),
                new Node("Technology", this._sprite, new[] {
                    new Node("Faster Chips", this._sprite)
                })
            })
        };

        // Find longest path
        foreach (Node node in this.progress)
        {
            int depth = node.Depth();
            if (depth > longestDepth)
            {
                longestDepth = depth;
            }
        }

        // Space out nodes into a tree
        int x = 0;
        int horizontal = Screen.width/(longestDepth+2);
        // Iterate through each tier
        for (int i = 0; i < longestDepth; i++)
        {
            x += horizontal;
            int vertical;
            int y = 0;
            List<Node> nodes = new List<Node>();
            // Collect all nodes on the same tier
            foreach (Node root in this.progress)
            {
                foreach (Node child in root.Get(i))
                {
                    nodes.Add(child);
                }
            }
            // Vertically center all nodes
            vertical = Screen.height/(nodes.Count+2);
            foreach (Node node in nodes)
            {
                y += vertical;
                Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                position.z = 1;
                node.GameObject.transform.position = position;
            }
        }
        // Connect all nodes with lines by iterating through each path
        foreach (Node root in this.progress)
        {
            foreach (Node child in root.Children())
            {
                foreach (Node nextChild in child.Next)
                {
                    GameObject line = new GameObject("TreeConnector");
                    Renderer childRenderer = child.GameObject.transform.GetComponent<Renderer>();
                    Renderer nextChildRenderer = nextChild.GameObject.transform.GetComponent<Renderer>();
                    LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
                    lineRenderer.positionCount = 4;
                    lineRenderer.SetPosition(0, childRenderer.bounds.center);
                    lineRenderer.SetPosition(1, new Vector3(
                        (nextChildRenderer.bounds.center.x + childRenderer.bounds.center.x)/2,
                        childRenderer.bounds.center.y,
                        childRenderer.bounds.center.z
                    ));
                    lineRenderer.SetPosition(2, new Vector3(
                        (nextChildRenderer.bounds.center.x + childRenderer.bounds.center.x)/2,
                        nextChildRenderer.bounds.center.y,
                        childRenderer.bounds.center.z
                    ));
                    lineRenderer.SetPosition(3, nextChildRenderer.bounds.center);
                    lineRenderer.endWidth = lineRenderer.startWidth = 0.05f;
                    lineRenderer.material = this._lineMaterial;
                    Gradient gradient = new Gradient();
                    GradientAlphaKey alpha = new GradientAlphaKey(1.0f, 0.0f);
                    gradient.SetKeys(
                        new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                        new GradientAlphaKey[] { alpha, alpha }
                    );
                    lineRenderer.colorGradient = gradient;
                    lineRenderer.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Behind");
                }
            }
        }
    }

    void Update()
    {
    }
}
