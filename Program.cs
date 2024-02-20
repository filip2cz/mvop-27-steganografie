using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Šifrovat nebo Dešifrovat?");
Console.Write("S/D: ");

string volba = Console.ReadLine();


if (volba == "S" || volba == "s")
{
    Console.Write("Zadejte zprávu: ");

    string message = Console.ReadLine();

    byte[] bytes = Encoding.ASCII.GetBytes(message);

    Bitmap image;

    image = new Bitmap(@"C:\Users\fkomarek\tmp\intro_logo.jpg");

    int x;
    int y;
    int i = -1;

    if (bytes.Length > 255)
    {
        Console.WriteLine("ERROR: moc dlouhá zpráva");
        System.Environment.Exit(1);
    }
    else
    {
        Console.WriteLine("Šifrovaná zpráva: ");
        Console.WriteLine(Encoding.ASCII.GetString(bytes));
    }
    
    for (x = 0; x < image.Width; x++)
    {
        for (y = 0; y < image.Height; y++)
        {
            if (i == -1)
            {
                Color pixelColor = image.GetPixel(x, y);
                Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, bytes.Length);
                image.SetPixel(x, y, newColor);
                Console.WriteLine($"pixel: {x}, {y}");
                Console.WriteLine($"i: {i}");
                Console.WriteLine($"newColor.B: {newColor.B}");
                Console.WriteLine("----------------------------");
            }
            else if (i < bytes.Length)
            {
                Color pixelColor = image.GetPixel(x, y);
                Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, bytes[i]);
                image.SetPixel(x, y, newColor);
                Console.WriteLine($"pixel: {x}, {y}");
                Console.WriteLine($"i: {i}");
                Console.WriteLine($"newColor.B: {bytes[i]}");
                Console.WriteLine("----------------------------");
            }
            i++;
        }
    }

    image.Save("output.jpg");
}
else if (volba == "D" || volba == "d")
{
    Bitmap image;

    image = new Bitmap("output.jpg");

    Color pixelColorLength = image.GetPixel(0, 0);

    int messageLength = pixelColorLength.B;

    Console.WriteLine($"pixel: {0}, {0}");
    Console.WriteLine($"i: -1");
    Console.WriteLine($"pixelColor.B: {pixelColorLength.B}");
    Console.WriteLine("----------------------------");

    byte[] bytes = new byte[messageLength];

    int x;
    int y;
    int i = 0;
    for (x = 0; x < image.Width; x++)
    {
        for (y = 1; y < image.Height; y++)
        {
            if (i < messageLength)
            {
                Color pixelColor = image.GetPixel(x, y);
                bytes[i] = pixelColor.B;
                Console.WriteLine($"pixel: {x}, {y}");
                Console.WriteLine($"i: {i}");
                Console.WriteLine($"pixelColor.B: {pixelColor.B}");
                Console.WriteLine("----------------------------");
                i++;
            }
        }
    }

    Console.Write("Dešifrovaná zpráva: ");
    Console.WriteLine(Encoding.ASCII.GetString(bytes));
}
else
{
    Console.WriteLine("Neplatná volba!");
}