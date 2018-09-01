using System;

namespace ZHS.Configuration.Core
{
    public class TestConfig : IConfigModel
    {
        public String DefauleVaule { get; set; } = "Hello World";
        public Starship Starship { get; set; }
        public string Trademark { get; set; }
    }

    public class Starship
    {
        public string Name { get; set; }
        public string Registry { get; set; }
        public string Class { get; set; }
        public float Length { get; set; }
        public bool Commissioned { get; set; }
    }

}