# Спецификация форматов файлов

Техническая документация для форматов файлов ресурсов id Mobile RPG.

## Содержание

- [wtexels.bin - Данные текстур](#wtexelsbin---данные-текстур)
- [palettes.bin - Данные палитр](#palettesbin---данные-палитр)
- [Структура JAR](#структура-jar)
- [Поддержка игр](#поддержка-игр)
- [Примечания по реализации](#примечания-по-реализации)

## wtexels.bin - Данные текстур

### Структура файла

```
Смещение | Размер  | Тип    | Описание
---------|---------|--------|----------------------------------
0x0000   | 4 байта | uint32 | Общий размер данных (little-endian)
0x0004   | N байт  | данные | Данные текстур
```

### Формат текстуры

- **Кодирование:** 4-битный индексированный цвет (упакованный по полубайтам)
- **Размеры:** 64 пикселя × 64 пикселя
- **Байт на текстуру:** 2048 байт (64 × 64 ÷ 2)
- **Палитра по умолчанию:** 16-уровневая градация серого (0-255 с шагом 17)

### Хранение пикселей

Каждый байт содержит 2 пикселя:
```
Байт: [P2][P1]
      7654 3210
      
P1 = байт & 0x0F        (биты 0-3, младший полубайт)
P2 = (байт >> 4) & 0x0F (биты 4-7, старший полубайт)
```

### Порядок хранения

Пиксели хранятся в **порядке по столбцам**:

```
for x = 0 to 63:
    for y = 0 to 63 step 2:
        байт = файл[смещение++]
        пиксель[x, y]   = байт & 0x0F
        пиксель[x, y+1] = (байт >> 4) & 0x0F
```

### Палитра градаций серого по умолчанию

```
Индекс | Значение | RGB
-------|----------|----------
0      | 0        | (0,0,0)
1      | 17       | (17,17,17)
2      | 34       | (34,34,34)
...    | ...      | ...
14     | 238      | (238,238,238)
15     | 255      | (255,255,255)

Формула: RGB = индекс × 17
```

### Несколько текстур

Текстуры хранятся последовательно:

```
Количество текстур = (Размер файла - 4) / 2048

Текстура 0: байты [4...2051]
Текстура 1: байты [2052...4099]
Текстура N: байты [(N×2048)+4 ... ((N+1)×2048)+3]
```

## palettes.bin - Данные палитр

### Структура файла

```
Смещение | Размер  | Тип    | Описание
---------|---------|--------|----------------------------------
0x0000   | 4 байта | uint32 | Общий размер данных (little-endian)
0x0004   | N байт  | данные | Данные палитр
```

### Формат палитры

- **Цветов в палитре:** 16
- **Байт на палитру:** 32 (16 цветов × 2 байта)
- **Формат цвета:** BGR565 (16-бит)

### Формат цвета BGR565

16-битный цвет упакован следующим образом:

```
Бит:    15 14 13 12 11 | 10 09 08 07 06 05 | 04 03 02 01 00
        [   Синий     ] [    Зелёный     ] [   Красный   ]
        5 бит (0-31)    6 бит (0-63)       5 бит (0-31)
```

### Упаковка цвета

```
uint16 цвет = (синий << 11) | (зелёный << 5) | красный

где:
  красный = R >> 3  (преобразование 8-бит в 5-бит)
  зелёный = G >> 2  (преобразование 8-бит в 6-бит)
  синий   = B >> 3  (преобразование 8-бит в 5-бит)
```

### Распаковка цвета

```
R8 = ((цвет & 0x001F) << 3) | ((цвет & 0x001F) >> 2)
G8 = ((цвет & 0x07E0) >> 3) | ((цвет & 0x07E0) >> 9)
B8 = ((цвет & 0xF800) >> 8) | ((цвет & 0xF800) >> 13)

Упрощённо:
  красный5  = цвет & 0x1F
  зелёный6  = (цвет >> 5) & 0x3F
  синий5    = (цвет >> 11) & 0x1F
  
  R = (красный5 << 3) | (красный5 >> 2)
  G = (зелёный6 << 2) | (зелёный6 >> 4)
  B = (синий5 << 3) | (синий5 >> 2)
```

### Хранение палитр

Палитры хранятся последовательно:

```
Количество палитр = (Размер файла - 4) / 32

Палитра 0: байты [4...35]
Палитра 1: байты [36...67]
Палитра N: байты [(N×32)+4 ... ((N+1)×32)+3]

Внутри каждой палитры:
  Цвет 0:  байты [0-1]   (uint16, little-endian)
  Цвет 1:  байты [2-3]
  ...
  Цвет 15: байты [30-31]
```

## Структура JAR

### META-INF/MANIFEST.MF

Определение игры основано на записи MIDlet-1:

```
Manifest-Version: 1.0
MIDlet-1: [Название игры], [Путь к иконке], [Главный класс]
...
```

Примеры:
```
MIDlet-1: Doom RPG, /icon.png, DoomRPG
MIDlet-1: Doom RPG II, /icon.png, DoomRPGII
MIDlet-1: Orcs & Elves, /icon.png, OrcsAndElves
MIDlet-1: Orcs & Elves II, /icon.png, OrcsAndElvesII
MIDlet-1: Wolfenstein RPG, /icon.png, WolfensteinRPG
```

### Файлы ресурсов

**Doom RPG:**
- `wtexels.bin` — данные текстур
- `palettes.bin` — цветовые палитры

**Doom RPG II:**
- *(в разработке)*

**Orcs & Elves:**
- `wtexels0.bin` — первый набор текстур (суффикс `0`)
- `wtexels1.bin` — второй набор текстур (суффикс `1`)
- `palettes.bin` — цветовые палитры

**Orcs & Elves II:**
- `wtexels0.bin` — данные текстур
- `palettes.bin` — цветовые палитры

**Wolfenstein RPG:**
- *(в разработке)*

## Поддержка игр

| Игра            | Распаковка | Запаковка  |
|-----------------|------------|------------|
| Doom RPG        | ✅          | ✅          |
| Doom RPG II     | ❌          | ❌          |
| Orcs & Elves    | ✅          | ❌          |
| Orcs & Elves II | ✅          | ❌          |
| Wolfenstein RPG | ❌          | ❌          |

## Примечания по реализации

### Чтение текстур

```csharp
void ReadTexture(byte[] data, int offset, Bitmap bmp)
{
    for (int x = 0; x < 64; x++)
    {
        for (int y = 0; y < 64; y += 2)
        {
            byte packed = data[offset++];
            
            int index1 = packed & 0x0F;
            int index2 = (packed >> 4) & 0x0F;
            
            bmp.SetPixel(x, y, palette[index1]);
            bmp.SetPixel(x, y + 1, palette[index2]);
        }
    }
}
```

### Запись текстур

```csharp
void WriteTexture(Bitmap bmp, BinaryWriter writer)
{
    for (int x = 0; x < 64; x++)
    {
        for (int y = 0; y < 64; y += 2)
        {
            Color c1 = bmp.GetPixel(x, y);
            Color c2 = bmp.GetPixel(x, y + 1);
            
            int index1 = ColorToIndex(c1);
            int index2 = ColorToIndex(c2);
            
            byte packed = (byte)((index2 << 4) | index1);
            writer.Write(packed);
        }
    }
}
```

### Преобразование в градации серого

```csharp
int ColorToIndex(Color c)
{
    // Преобразование в градации серого с использованием формулы яркости
    int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
    
    // Округление до ближайшего индекса палитры
    int index = (gray + 8) / 17;
    
    // Ограничение допустимым диапазоном
    return Math.Min(15, Math.Max(0, index));
}
```

### Чтение палитр

```csharp
Color[] ReadPalette(byte[] data, int offset)
{
    Color[] palette = new Color[16];
    
    for (int i = 0; i < 16; i++)
    {
        ushort bgr565 = BitConverter.ToUInt16(data, offset + i * 2);
        palette[i] = BGR565ToColor(bgr565);
    }
    
    return palette;
}

Color BGR565ToColor(ushort bgr)
{
    int r5 = bgr & 0x1F;
    int g6 = (bgr >> 5) & 0x3F;
    int b5 = (bgr >> 11) & 0x1F;
    
    int r8 = (r5 << 3) | (r5 >> 2);
    int g8 = (g6 << 2) | (g6 >> 4);
    int b8 = (b5 << 3) | (b5 >> 2);
    
    return Color.FromArgb(r8, g8, b8);
}
```

### Запись палитр

```csharp
void WritePalette(Color[] palette, BinaryWriter writer)
{
    for (int i = 0; i < 16; i++)
    {
        ushort bgr565 = ColorToBGR565(palette[i]);
        writer.Write(bgr565);
    }
}

ushort ColorToBGR565(Color c)
{
    int r5 = c.R >> 3;
    int g6 = c.G >> 2;
    int b5 = c.B >> 3;
    
    return (ushort)((b5 << 11) | (g6 << 5) | r5);
}
```

### Упаковка текстур (PackTextures)

PNG-файлы с именами `texture_NNN.png` из папки текстур упаковываются обратно в `.bin`-файл. Файлы сортируются по имени и обрабатываются по порядку. Изображения, не соответствующие размеру 64×64, пропускаются.

```csharp
static void PackTextures(string inputDir, string outputFile)
{
    var pngFiles = Directory.GetFiles(inputDir, "texture_*.png")
                            .OrderBy(f => f)
                            .ToArray();

    int totalDataSize = pngFiles.Length * 2048;

    using (BinaryWriter writer = new BinaryWriter(File.Create(outputFile)))
    {
        writer.Write((uint)totalDataSize); // заголовок

        foreach (var file in pngFiles)
        {
            using (Bitmap bmp = new Bitmap(file))
            {
                if (bmp.Width != 64 || bmp.Height != 64) continue;

                for (int x = 0; x < 64; x++)
                    for (int y = 0; y < 64; y += 2)
                    {
                        int i1 = ColorToPaletteIndex(bmp.GetPixel(x, y));
                        int i2 = ColorToPaletteIndex(bmp.GetPixel(x, y + 1));
                        writer.Write((byte)((i2 << 4) | i1));
                    }
            }
        }
    }
}
```

### Упаковка палитр (PackPalettes)

PNG-файлы с именами `palette_NNN.png` читаются как горизонтальные полоски 16 ячеек шириной 16 пикселей каждая. Цвет берётся из центра каждой ячейки.

```csharp
static void PackPalettes(string inputDir, string outputFile)
{
    var pngFiles = Directory.GetFiles(inputDir, "palette_*.png")
                            .OrderBy(f => f)
                            .ToArray();

    int totalDataSize = pngFiles.Length * 32;

    using (BinaryWriter writer = new BinaryWriter(File.Create(outputFile)))
    {
        writer.Write((uint)totalDataSize); // заголовок

        foreach (var file in pngFiles)
        {
            Color[] palette = ExtractPaletteFromPNG(file, 16);

            for (int c = 0; c < 16; c++)
                writer.Write(ColorToBGR565(palette[c]));
        }
    }
}

// Цвет берётся из центра каждой ячейки (x = i*16+8, y = height/2)
static Color[] ExtractPaletteFromPNG(string pngPath, int colorCount)
{
    Color[] palette = new Color[colorCount];
    using (Bitmap bmp = new Bitmap(pngPath))
        for (int i = 0; i < colorCount; i++)
        {
            int x = Math.Min(i * 16 + 8, bmp.Width - 1);
            int y = Math.Min(bmp.Height / 2, bmp.Height - 1);
            palette[i] = bmp.GetPixel(x, y);
        }
    return palette;
}
```

### Запись изменений обратно в JAR (Doom RPG)

После упаковки `wtexels.bin` и `palettes.bin` файлы записываются обратно в исходный JAR-архив. Существующие записи сначала удаляются, затем добавляются новые:

```csharp
using (ZipArchive archive = ZipFile.Open(jarPath, ZipArchiveMode.Update))
{
    foreach (string fileName in new[] { "wtexels.bin", "palettes.bin" })
    {
        var existing = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith(fileName));
        existing?.Delete();
        archive.CreateEntryFromFile(Path.Combine(inputDir, fileName), fileName);
    }
}
```

## Валидация

### Валидация текстур

- Размер файла должен быть `4 + (N × 2048)` байт
- Каждая текстура должна быть точно 2048 байт
- Значение заголовка должно совпадать с общим размером данных
- При упаковке изображения, не соответствующие размеру 64×64, пропускаются

### Валидация палитр

- Размер файла должен быть `4 + (N × 32)` байт
- Каждая палитра должна быть точно 32 байта (16 цветов)
- Значение заголовка должно совпадать с общим размером данных
- PNG-палитры должны иметь ширину не менее 256 пикселей (16 × 16)

## Порядок байтов (Endianness)

Все многобайтовые значения используют порядок байтов **little-endian**:

```
uint32: [байт0][байт1][байт2][байт3]
        МЗБ                    СЗБ

uint16: [байт0][байт1]
        МЗБ     СЗБ

МЗБ = Младший значащий байт
СЗБ = Старший значащий байт
```

## Структура директорий (вывод)

### Doom RPG

```
<папка JAR>\Doom RPG\Unpacked\
├── wtexels.bin
├── palettes.bin
├── Textures\
│   ├── texture_000.png
│   ├── texture_001.png
│   └── ...
└── Palettes\
    ├── palette_000.png
    ├── palette_001.png
    └── ...
```

### Orcs & Elves

```
<папка JAR>\Orcs & Elves\Unpacked\
├── wtexels0.bin
├── wtexels1.bin
├── palettes.bin
├── Textures0\
│   └── texture_NNN.png
├── Textures1\
│   └── texture_NNN.png
└── Palettes\
    └── palette_NNN.png
```

### Orcs & Elves II

```
<папка JAR>\Orcs & Elves II\Unpacked\
├── wtexels0.bin
├── palettes.bin
├── Textures\
│   └── texture_NNN.png
└── Palettes\
    └── palette_NNN.png
```

## История версий

- **v0.1** — Первоначальная спецификация формата
  - Поддержка Doom RPG (распаковка и запаковка)
  - Поддержка извлечения Orcs & Elves (оба файла текстур: wtexels0.bin, wtexels1.bin)
  - Поддержка извлечения Orcs & Elves II
  - Обнаружение Doom RPG II и Wolfenstein RPG (поддержка в разработке)

## Справочная информация

- Оригинальная реализация: id Mobile RPG Editor от den_koter
- Целевые платформы: J2ME (Java 2 Micro Edition)
- Цветовое пространство: BGR565 (16-битный цвет)

## Часто задаваемые вопросы

### Почему используется BGR вместо RGB?

Формат BGR565 был распространён на мобильных платформах J2ME из-за особенностей аппаратного обеспечения некоторых телефонов той эпохи.

### Почему текстуры 64×64?

Это был стандартный размер для текстур в мобильных 3D-играх того времени, обеспечивающий баланс между качеством и производительностью на ограниченном оборудовании.

### Можно ли использовать полноцветные текстуры?

Формат wtexels.bin поддерживает только 4-битные индексированные текстуры (16 цветов). Для полного цвета используйте палитры.

### Как работает упаковка двух пикселей в один байт?

Младшие 4 бита (0-3) хранят индекс первого пикселя, старшие 4 бита (4-7) хранят индекс второго пикселя. Это позволяет сжать данные в 2 раза.

### Почему для Orcs & Elves два файла текстур?

В игре используется два независимых банка текстур: `wtexels0.bin` и `wtexels1.bin`. Они извлекаются в отдельные папки `Textures0` и `Textures1`.