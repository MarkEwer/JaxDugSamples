using Ooui;
using System;

namespace OouiJaxDug
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 0;
            var div = new Div();

            div.AppendChild(new Heading(1, "JaxDug OOUI Sample"));
            div.AppendChild(new Paragraph("This is a sample that shows using WASM."));
            var para = new Paragraph("");
            div.AppendChild(para);
            var button = new Button("Click Me");
            div.AppendChild(button);

            button.Click += (s, e) => {
                count++;
                para.Text = $"You have clicked the button {count} times";
            };

            UI.Publish("/", div);
        }
    }
}
