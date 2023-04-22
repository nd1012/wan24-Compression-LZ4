# wan24-Compression-LZ4

This library adopts 
[K4os.Compression.LZ4](https://github.com/MiloszKrajewski/K4os.Compression.LZ4) 
to [wan24-Compression](https://www.nuget.org/packages/wan24-Compression/) and 
extends the `wan24-Compression` library with these algorithms:

| Algorithm | ID | Name |
| --- | --- | --- |
| LZ4 | 2 | LZ4 |

## How to get it

This library is available as 
[NuGet package](https://www.nuget.org/packages/wan24-Compression-LZ4/).

## Usage

In case you don't use the `wan24-Core` bootstrapper logic, you need to 
initialize the LZ4 extension first, before you can use it:

```cs
wan24.Compression.LZ4.Bootstrapper.Boot();
```

This will register the compression algorithm to the `wan24-Compression` 
library.
