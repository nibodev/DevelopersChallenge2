using System.Collections.Generic;

namespace API.Services
{
    internal class Node
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();

        public Node(string label, Node parent)
        {
            Label = label;
            parent?.Children.Add(this);
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public void Accept(INodeVisitor visitor)
        {
            foreach (var child in Children)
            {
                child.Accept(visitor);
            }
            visitor.Visit(this);
        }

        public override string ToString() => $"{Label} : {Value}";
    }
}