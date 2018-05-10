using Ooui;
using System;

namespace OouiSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the UI
            var div = new Div();
            div.AppendChild(new Heading("JaxDug Sample"));
            div.AppendChild(new Paragraph("This is a sample page rendered from C# code."));
            var button = new Button("Click me!");
            div.AppendChild(button);

            // Add some logic to it
            var count = 0;
            button.Click += (s, e) => {
                count++;
                button.Text = $"Clicked {count} times";
            };

            // Publishing makes an object available at a given URL
            // The user should be directed to http://localhost:8080/shared-button
            UI.Publish("/page1", div);
            UI.Publish("/shared-button", button);

            // Don't exit the app until someone hits return
            Console.ReadLine();
        }
    }
}
