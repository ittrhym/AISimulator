using UnityEngine;

public class Main : MonoBehaviour
{

    private class Node
    {
        private bool Completed;
        public string Name
        {
            get;
            private set;
        }
        public Node (string name)
        {
            this.Completed = false;
            this.Name = name;
        }
    }

    public Texture2D _sprite;

    private GameObject[,] paths;

    private Node[,] progress;

    void Awake()
    {
        string[,] nodes = new string[2,3] {
            {"cooling1", "cooling2", "cooling3"},
            {"power1", "power2", "power3"}
        };
        for (int i = 0; i < nodes.Length; i++)
        {
            for (int j = 0; j < nodes[i,*].Length; j++)
            {
                this.progress[i,j] = new Node(nodes[i,j]);
            }
        }
        print(nodes);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                GameObject o = new GameObject("Node" + i + j);
                SpriteRenderer r = o.AddComponent<SpriteRenderer>();
                r.sprite = Sprite.Create(
                        this._sprite,
                        new Rect(0, 0, this._sprite.width, this._sprite.height),
                        new Vector2(0, 0)
                );
                o.transform.position = new Vector3(5, 5, 5);
                paths[i,j] = o;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
