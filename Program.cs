using System.Drawing;

namespace TilePadding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileLocation;
            int tileSize, paddingSize, newTilesetWidth, newTilesetHeight;
            Bitmap imgImport, imgExport;

            Console.WriteLine("Enter file location (e.g. D:\\Files and Projects\\Sprites\\tileset.png)");
            fileLocation = Console.ReadLine();
            imgImport = new Bitmap(fileLocation, true);

            do
            {
                Console.WriteLine("Enter tile size (in pixels) (must be a multiple of 2 and less than image size)");
                tileSize = Int32.Parse(Console.ReadLine());
            }
            while (tileSize > imgImport.Width || tileSize > imgImport.Height || tileSize % 2 != 0);

            Console.WriteLine("Enter padding size (in pixels)");
            paddingSize = Int32.Parse(Console.ReadLine());


            newTilesetWidth = imgImport.Width + imgImport.Width / tileSize * paddingSize - paddingSize;
            newTilesetHeight = imgImport.Height + imgImport.Height / tileSize * paddingSize - paddingSize;

            imgExport = new Bitmap(newTilesetWidth, newTilesetHeight);
            
            
            int currentX = 0, currentY = 0;


            for (int y = 0; y < newTilesetHeight; y++)
            {
                if (currentY % tileSize == 0 && currentY != 0)
                    y += paddingSize;
                for (int x = 0; x < newTilesetWidth; x++)
                {
                    if (currentX % tileSize == 0 && currentX != 0)
                        x += paddingSize;
                    Color pixelColor = imgImport.GetPixel(currentX, currentY);
                    imgExport.SetPixel(x, y, pixelColor);
                    currentX += 1;
                }
                currentX = 0;
                currentY += 1;
            }

            imgExport.Save($"{ fileLocation.Substring(0, fileLocation.Length - 4)}_padded.png");
            Console.WriteLine("Padded Image Generated!");
        }
    }
}