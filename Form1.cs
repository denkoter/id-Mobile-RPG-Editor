// id Mobile RPG Editor 0.2 by den_koter

using System.Drawing.Imaging;
using System.IO.Compression;

namespace id_Mobile_RPG_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JAR файл (*.jar)|*.jar";
                openFileDialog.Title = "Выберите JAR файл для работы с ресурсами";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    textBox1.Text = openFileDialog.FileName;
                    string searchKey = "MIDlet-1:";
                    using (ZipArchive archive = ZipFile.OpenRead(textBox1.Text))
                    {
                        var entry = archive.GetEntry("META-INF/MANIFEST.MF");

                        if (entry != null)
                        {
                            using (StreamReader reader = new StreamReader(entry.Open()))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (line.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase))
                                    {
                                        string value = line.Substring(searchKey.Length).Trim();

                                        if (value.Contains("Doom RPG"))
                                        {
                                            textBox2.Text = "Doom RPG";
                                        }
                                        if (value.Contains("Doom RPG II"))
                                        {
                                            textBox2.Text = "Doom RPG II";
                                        }

                                        if (value.Contains("Orcs & Elves"))
                                        {
                                            textBox2.Text = "Orcs & Elves";
                                        }

                                        if (value.Contains("Orcs & Elves II"))
                                        {
                                            textBox2.Text = "Orcs & Elves II";
                                        }

                                        if (value.Contains("Wolfenstein RPG"))
                                        {
                                            textBox2.Text = "Wolfenstein RPG";
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            textBox2.Text = "Выбран не поддерживаемый JAR файл";
                        }
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Doom RPG")
            {
                string output_dir = Path.GetDirectoryName(textBox1.Text) + @"\Doom RPG\Unpacked\";
                string output_dir_textures = Path.GetDirectoryName(textBox1.Text) + @"\Doom RPG\Unpacked\Textures\";
                string output_dir_palettes = Path.GetDirectoryName(textBox1.Text) + @"\Doom RPG\Unpacked\Palettes\";
                string wtexeles = Path.Combine(output_dir, "wtexels.bin");
                string palettes = Path.Combine(output_dir, "palettes.bin");

                using (ZipArchive archive = ZipFile.OpenRead(textBox1.Text))
                {
                    var entry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith("wtexels.bin"));

                    if (!Directory.Exists(output_dir))
                    {
                        Directory.CreateDirectory(output_dir);
                    }

                    if (entry != null)
                    {
                        string destinationPath = Path.Combine(output_dir, entry.Name);
                        entry.ExtractToFile(destinationPath, true);
                    }



                    if (!Directory.Exists(output_dir_textures))
                    {
                        Directory.CreateDirectory(output_dir_textures);
                    }
                    try
                    {
                        ExtractTextures(wtexeles, output_dir_textures);
                    }
                    catch (Exception ex)
                    {
                    }

                    entry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith("palettes.bin"));

                    if (!Directory.Exists(output_dir))
                    {
                        Directory.CreateDirectory(output_dir);
                    }

                    if (entry != null)
                    {
                        string destinationPath = Path.Combine(output_dir, entry.Name);
                        entry.ExtractToFile(destinationPath, true);
                    }

                    if (!Directory.Exists(output_dir_palettes))
                    {
                        Directory.CreateDirectory(output_dir_palettes);
                    }

                    try
                    {
                        ExtractPalettes(palettes, output_dir_palettes);
                    }
                    catch (Exception ex)
                    {
                    }
                    MessageBox.Show("Ресурсы успешно распакованы!", "id Mobile RPG Editor Распаковщик", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            if (textBox2.Text == "Orcs & Elves")
            {
                string output_dir = Path.GetDirectoryName(textBox1.Text) + @"\Orcs & Elves\Unpacked\";
                string output_dir_textures0 = Path.GetDirectoryName(textBox1.Text) + @"\Orcs & Elves\Unpacked\Textures0\";
                string output_dir_textures1 = Path.GetDirectoryName(textBox1.Text) + @"\Orcs & Elves\Unpacked\Textures1\";
                string output_dir_palettes = Path.GetDirectoryName(textBox1.Text) + @"\Orcs & Elves\Unpacked\Palettes\";
                string wtexeles0 = Path.Combine(output_dir, "wtexels0.bin");
                string wtexeles1 = Path.Combine(output_dir, "wtexels1.bin");
                string palettes = Path.Combine(output_dir, "palettes.bin");

                using (ZipArchive archive = ZipFile.OpenRead(textBox1.Text))
                {
                    var entry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith("wtexels0.bin"));

                    if (!Directory.Exists(output_dir))
                    {
                        Directory.CreateDirectory(output_dir);
                    }

                    if (entry != null)
                    {
                        string destinationPath = Path.Combine(output_dir, entry.Name);
                        entry.ExtractToFile(destinationPath, true);
                    }

                    if (!Directory.Exists(output_dir_textures0))
                    {
                        Directory.CreateDirectory(output_dir_textures0);
                    }
                    try
                    {
                        ExtractTextures(wtexeles0, output_dir_textures0);
                    }
                    catch (Exception ex)
                    {
                    }

                    entry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith("wtexels1.bin"));

                    if (!Directory.Exists(output_dir))
                    {
                        Directory.CreateDirectory(output_dir);
                    }

                    if (entry != null)
                    {
                        string destinationPath = Path.Combine(output_dir, entry.Name);
                        entry.ExtractToFile(destinationPath, true);
                    }

                    if (!Directory.Exists(output_dir_textures1))
                    {
                        Directory.CreateDirectory(output_dir_textures1);
                    }
                    try
                    {
                        ExtractTextures(wtexeles1, output_dir_textures1);
                    }
                    catch (Exception ex)
                    {
                    }




                    entry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith("palettes.bin"));

                    if (!Directory.Exists(output_dir))
                    {
                        Directory.CreateDirectory(output_dir);
                    }

                    if (entry != null)
                    {
                        string destinationPath = Path.Combine(output_dir, entry.Name);
                        entry.ExtractToFile(destinationPath, true);
                    }

                    if (!Directory.Exists(output_dir_palettes))
                    {
                        Directory.CreateDirectory(output_dir_palettes);
                    }

                    try
                    {
                        ExtractPalettes(palettes, output_dir_palettes);
                    }
                    catch (Exception ex)
                    {
                    }
                    MessageBox.Show("Ресурсы успешно извлечены!", "id Mobile RPG Editor Распаковщик", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (textBox2.Text == "Orcs & Elves II")
            {
                string output_dir = Path.GetDirectoryName(textBox1.Text) + @"\Orcs & Elves II\Unpacked\";
                string output_dir_textures = Path.GetDirectoryName(textBox1.Text) + @"\Orcs & Elves II\Unpacked\Textures\";
                string output_dir_palettes = Path.GetDirectoryName(textBox1.Text) + @"\Orcs & Elves II\Unpacked\Palettes\";
                string wtexeles = Path.Combine(output_dir, "wtexels0.bin");
                string palettes = Path.Combine(output_dir, "palettes.bin");

                using (ZipArchive archive = ZipFile.OpenRead(textBox1.Text))
                {
                    var entry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith("wtexels0.bin"));

                    if (!Directory.Exists(output_dir))
                    {
                        Directory.CreateDirectory(output_dir);
                    }

                    if (entry != null)
                    {
                        string destinationPath = Path.Combine(output_dir, entry.Name);
                        entry.ExtractToFile(destinationPath, true);
                    }
                    if (!Directory.Exists(output_dir_textures))
                    {
                        Directory.CreateDirectory(output_dir_textures);
                    }
                    try
                    {
                        ExtractTextures(wtexeles, output_dir_textures);
                    }
                    catch (Exception ex)
                    {
                    }

                    entry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith("palettes.bin"));

                    if (!Directory.Exists(output_dir))
                    {
                        Directory.CreateDirectory(output_dir);
                    }

                    if (entry != null)
                    {
                        string destinationPath = Path.Combine(output_dir, entry.Name);
                        entry.ExtractToFile(destinationPath, true);
                    }

                    if (!Directory.Exists(output_dir_palettes))
                    {
                        Directory.CreateDirectory(output_dir_palettes);
                    }

                    try
                    {
                        ExtractPalettes(palettes, output_dir_palettes);
                    }
                    catch (Exception ex)
                    {
                    }
                    MessageBox.Show("Ресурсы успешно распакованы!", "id Mobile RPG Editor Распаковщик", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (textBox2.Text == "Wolfenstein RPG")
            {
                //Coming soon...
            }
            if (textBox2.Text == "Doom RPG")
            {
                //Coming soon...
            }

            static void ExtractTextures(string filePath, string outputDir)
            {
                byte[] fileData = File.ReadAllBytes(filePath);

                int headerSize = 4;


                const int width = 64;
                const int height = 64;

                const int textureSizeBytes = 2048;

                int totalDataSize = fileData.Length - headerSize;
                int textureCount = totalDataSize / textureSizeBytes;

                Color[] grayscalePalette = new Color[16];
                for (int i = 0; i < 16; i++)
                {
                    int val = i * 17;
                    grayscalePalette[i] = Color.FromArgb(val, val, val);
                }

                int offset = headerSize;

                for (int i = 0; i < textureCount; i++)
                {

                    using (Bitmap bmp = new Bitmap(width, height))
                    {

                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y += 2)
                            {
                                if (offset >= fileData.Length) break;

                                byte b = fileData[offset++];

                                int p1Index = b & 0x0F;
                                int p2Index = (b >> 4) & 0x0F;

                                bmp.SetPixel(x, y, grayscalePalette[p1Index]);
                                bmp.SetPixel(x, y + 1, grayscalePalette[p2Index]);
                            }
                        }

                        string outPath = Path.Combine(outputDir, $"texture_{i:D3}.png");
                        bmp.Save(outPath, ImageFormat.Png);
                    }
                }

            }

            static void ExtractPalettes(string filePath, string outputDir)
            {
                byte[] fileData = File.ReadAllBytes(filePath);

                int headerSize = 4;
                uint remainingLength = BitConverter.ToUInt32(fileData, 0);

                const int paletteSize = 32;
                const int colorsPerPalette = 16;

                int totalDataSize = fileData.Length - headerSize;
                int paletteCount = totalDataSize / paletteSize;

                int offset = headerSize;

                for (int i = 0; i < paletteCount; i++)
                {
                    Color[] palette = new Color[colorsPerPalette];

                    for (int c = 0; c < colorsPerPalette; c++)
                    {
                        if (offset + 1 >= fileData.Length) break;

                        ushort color16 = BitConverter.ToUInt16(fileData, offset);
                        offset += 2;

                        palette[c] = BGR565ToColor(color16);
                    }

                    SavePaletteAsPNG(palette, outputDir, i);
                }
            }

            static Color BGR565ToColor(ushort color)
            {
                int red = color & 0x1F;
                int green = (color >> 5) & 0x3F;
                int blue = (color >> 11) & 0x1F;

                red = (red << 3) | (red >> 2);
                green = (green << 2) | (green >> 4);
                blue = (blue << 3) | (blue >> 2);

                return Color.FromArgb(red, green, blue);
            }

            static void SavePaletteAsPNG(Color[] palette, string outputDir, int paletteIndex)
            {
                const int cellWidth = 16;
                const int cellHeight = 16;
                int width = palette.Length * cellWidth;
                int height = cellHeight;

                using (Bitmap bmp = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        for (int i = 0; i < palette.Length; i++)
                        {
                            using (SolidBrush brush = new SolidBrush(palette[i]))
                            {
                                g.FillRectangle(brush, i * cellWidth, 0, cellWidth, cellHeight);
                            }
                        }
                    }

                    string outPath = Path.Combine(outputDir, $"palette_{paletteIndex:D3}.png");
                    bmp.Save(outPath, ImageFormat.Png);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Doom RPG")
            {
                string input_dir = Path.GetDirectoryName(textBox1.Text) + @"\Doom RPG\Unpacked\";
                string input_dir_textures = Path.GetDirectoryName(textBox1.Text) + @"\Doom RPG\Unpacked\Textures\";
                string input_dir_wtexeles = Path.Combine(input_dir, "wtexels.bin");

                try
                {
                    PackTextures(input_dir_textures, input_dir_wtexeles);
                }
                catch (Exception ex)
                {
                }
                string inputDir = @"C:\Users\Den\Desktop\Doom RPG\extracted_palettes";
                string outputFile = @"C:\Users\Den\Desktop\Doom RPG\palettes_new.bin";

                string input_dir_palette = Path.GetDirectoryName(textBox1.Text) + @"\Doom RPG\Unpacked\Palettes\";
                string input_dir_palettes = Path.Combine(input_dir, "palettes.bin");

                try
                {
                    PackPalettes(input_dir_palette, input_dir_palettes);

                }
                catch (Exception ex)
                {
                }

                using (ZipArchive archive = ZipFile.Open(textBox1.Text, ZipArchiveMode.Update))
                {

                    string[] filesToAdd = { "wtexels.bin", "palettes.bin" };

                    foreach (string fileName in filesToAdd)
                    {
                        string sourceFilePath = Path.Combine(input_dir, fileName);

                        if (File.Exists(sourceFilePath))
                        {

                            var existingEntry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith(fileName));
                            if (existingEntry != null)
                            {
                                existingEntry.Delete();
                            }

                            archive.CreateEntryFromFile(sourceFilePath, fileName);
                        }
                    }
                    MessageBox.Show("Ресурсы успешно запакованы!", "id Mobile RPG Editor Запаковщик", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            if (textBox2.Text == "Orcs & Elves")
            {
                //Coming soon...
            }
            if (textBox2.Text == "Orcs & Elves II")
            {
                //Coming soon...
            }
        }
        static void PackTextures(string inputDir, string outputFile)
        {
            var pngFiles = Directory.GetFiles(inputDir, "texture_*.png")
                                    .OrderBy(f => f)
                                    .ToArray();

            if (pngFiles.Length == 0)
            {
                return;
            }

            const int width = 64;
            const int height = 64;
            const int textureSizeBytes = 2048;

            int totalDataSize = pngFiles.Length * textureSizeBytes;

            using (FileStream fs = new FileStream(outputFile, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write((uint)totalDataSize);

                for (int i = 0; i < pngFiles.Length; i++)
                {

                    using (Bitmap bmp = new Bitmap(pngFiles[i]))
                    {
                        if (bmp.Width != width || bmp.Height != height)
                        {
                            continue;
                        }

                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y += 2)
                            {
                                Color c1 = bmp.GetPixel(x, y);
                                Color c2 = bmp.GetPixel(x, y + 1);

                                int p1Index = ColorToPaletteIndex(c1);
                                int p2Index = ColorToPaletteIndex(c2);

                                byte packedByte = (byte)((p2Index << 4) | p1Index);
                                writer.Write(packedByte);
                            }
                        }
                    }
                }
            }
        }

        static int ColorToPaletteIndex(Color color)
        {
            int gray = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);

            int index = (gray + 8) / 17;

            if (index > 15) index = 15;
            if (index < 0) index = 0;

            return index;
        }

        static void PackPalettes(string inputDir, string outputFile)
        {
            var pngFiles = Directory.GetFiles(inputDir, "palette_*.png")
                                    .OrderBy(f => f)
                                    .ToArray();

            if (pngFiles.Length == 0)
            {
                return;
            }

            const int paletteSize = 32;
            const int colorsPerPalette = 16;


            int totalDataSize = pngFiles.Length * paletteSize;

            using (FileStream fs = new FileStream(outputFile, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {

                writer.Write((uint)totalDataSize);

                for (int i = 0; i < pngFiles.Length; i++)
                {

                    Color[] palette = ExtractPaletteFromPNG(pngFiles[i], colorsPerPalette);

                    if (palette == null)
                    {
                        continue;
                    }

                    for (int c = 0; c < colorsPerPalette; c++)
                    {
                        ushort color16 = ColorToBGR565(palette[c]);
                        writer.Write(color16);
                    }
                }
            }

        }

        static Color[] ExtractPaletteFromPNG(string pngPath, int colorCount)
        {
            Color[] palette = new Color[colorCount];

            using (Bitmap bmp = new Bitmap(pngPath))
            {
                const int cellWidth = 16;
                int expectedWidth = colorCount * cellWidth;

                if (bmp.Width < expectedWidth)
                {
                }

                for (int i = 0; i < colorCount; i++)
                {
                    int x = i * cellWidth + cellWidth / 2;
                    int y = bmp.Height / 2;


                    if (x >= bmp.Width) x = bmp.Width - 1;
                    if (y >= bmp.Height) y = bmp.Height - 1;

                    palette[i] = bmp.GetPixel(x, y);
                }
            }

            return palette;
        }

        static ushort ColorToBGR565(Color color)
        {
            int red = color.R >> 3;
            int green = color.G >> 2;
            int blue = color.B >> 3;

            ushort bgr565 = (ushort)((blue << 11) | (green << 5) | red);

            return bgr565;
        }

    }
}