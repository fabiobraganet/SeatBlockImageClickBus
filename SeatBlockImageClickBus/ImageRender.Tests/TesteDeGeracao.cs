using Xunit;

namespace ImageRender.Tests
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    
    public class TesteDeGeracao
    {
        private List<Seat> seats { get; set; }

        public TesteDeGeracao()
        {
            seats = new List<Seat>() 
            { 
                new Seat() { Id = "01", Available = "1", Name = "01", Position = new Position() { X =  0, Y = 0, Z = 0 } },
                new Seat() { Id = "02", Available = "1", Name = "02", Position = new Position() { X =  0, Y = 1, Z = 0 } },
                new Seat() { Id = "03", Available = "1", Name = "03", Position = new Position() { X =  0, Y = 2, Z = 0 } },
                new Seat() { Id = "04", Available = "1", Name = "04", Position = new Position() { X =  0, Y = 3, Z = 0 } },
                new Seat() { Id = "05", Available = "1", Name = "05", Position = new Position() { X =  0, Y = 4, Z = 0 } },
                new Seat() { Id = "06", Available = "1", Name = "06", Position = new Position() { X =  1, Y = 0, Z = 0 } },
                new Seat() { Id = "07", Available = "1", Name = "07", Position = new Position() { X =  1, Y = 1, Z = 0 } },
                new Seat() { Id = "08", Available = "1", Name = "08", Position = new Position() { X =  1, Y = 2, Z = 0 } },
                new Seat() { Id = "09", Available = "1", Name = "09", Position = new Position() { X =  1, Y = 3, Z = 0 } },
                new Seat() { Id = "10", Available = "1", Name = "10", Position = new Position() { X =  1, Y = 4, Z = 0 } },
                new Seat() { Id = "11", Available = "1", Name = "11", Position = new Position() { X =  2, Y = 0, Z = 0 } },
                new Seat() { Id = "12", Available = "1", Name = "12", Position = new Position() { X =  2, Y = 1, Z = 0 } },
                new Seat() { Id = "13", Available = "1", Name = "13", Position = new Position() { X =  2, Y = 2, Z = 0 } },
                new Seat() { Id = "14", Available = "1", Name = "14", Position = new Position() { X =  2, Y = 3, Z = 0 } },
                new Seat() { Id = "15", Available = "1", Name = "15", Position = new Position() { X =  2, Y = 4, Z = 0 } },
                new Seat() { Id = "16", Available = "1", Name = "16", Position = new Position() { X =  3, Y = 0, Z = 0 } },
                new Seat() { Id = "17", Available = "1", Name = "17", Position = new Position() { X =  3, Y = 1, Z = 0 } },
                new Seat() { Id = "18", Available = "1", Name = "18", Position = new Position() { X =  3, Y = 2, Z = 0 } },
                new Seat() { Id = "19", Available = "1", Name = "19", Position = new Position() { X =  3, Y = 3, Z = 0 } },
                new Seat() { Id = "20", Available = "1", Name = "20", Position = new Position() { X =  3, Y = 4, Z = 0 } },
                new Seat() { Id = "21", Available = "1", Name = "21", Position = new Position() { X =  4, Y = 0, Z = 0 } },
                new Seat() { Id = "22", Available = "1", Name = "22", Position = new Position() { X =  4, Y = 1, Z = 0 } },
                new Seat() { Id = "23", Available = "1", Name = "23", Position = new Position() { X =  4, Y = 2, Z = 0 } },
                new Seat() { Id = "24", Available = "1", Name = "24", Position = new Position() { X =  4, Y = 3, Z = 0 } },
                new Seat() { Id = "25", Available = "1", Name = "25", Position = new Position() { X =  4, Y = 4, Z = 0 } },
                new Seat() { Id = "23", Available = "1", Name = "23", Position = new Position() { X =  5, Y = 0, Z = 0 } },
                new Seat() { Id = "27", Available = "1", Name = "27", Position = new Position() { X =  5, Y = 1, Z = 0 } },
                new Seat() { Id = "28", Available = "1", Name = "28", Position = new Position() { X =  5, Y = 2, Z = 0 } },
                new Seat() { Id = "29", Available = "1", Name = "29", Position = new Position() { X =  5, Y = 3, Z = 0 } },
                new Seat() { Id = "30", Available = "1", Name = "30", Position = new Position() { X =  5, Y = 4, Z = 0 } },
                new Seat() { Id = "31", Available = "1", Name = "31", Position = new Position() { X =  6, Y = 0, Z = 0 } },
                new Seat() { Id = "32", Available = "1", Name = "32", Position = new Position() { X =  6, Y = 1, Z = 0 } },
                new Seat() { Id = "33", Available = "1", Name = "33", Position = new Position() { X =  6, Y = 2, Z = 0 } },
                new Seat() { Id = "34", Available = "1", Name = "34", Position = new Position() { X =  6, Y = 3, Z = 0 } },
                new Seat() { Id = "35", Available = "1", Name = "35", Position = new Position() { X =  6, Y = 4, Z = 0 } },
                new Seat() { Id = "36", Available = "1", Name = "36", Position = new Position() { X =  7, Y = 0, Z = 0 } },
                new Seat() { Id = "37", Available = "1", Name = "37", Position = new Position() { X =  7, Y = 1, Z = 0 } },
                new Seat() { Id = "38", Available = "1", Name = "38", Position = new Position() { X =  7, Y = 2, Z = 0 } },
                new Seat() { Id = "39", Available = "1", Name = "39", Position = new Position() { X =  7, Y = 3, Z = 0 } },
                new Seat() { Id = "40", Available = "1", Name = "40", Position = new Position() { X =  7, Y = 4, Z = 0 } },
                new Seat() { Id = "41", Available = "1", Name = "41", Position = new Position() { X =  8, Y = 0, Z = 0 } },
                new Seat() { Id = "42", Available = "1", Name = "42", Position = new Position() { X =  8, Y = 1, Z = 0 } },
                new Seat() { Id = "43", Available = "1", Name = "43", Position = new Position() { X =  8, Y = 2, Z = 0 } },
                new Seat() { Id = "44", Available = "1", Name = "44", Position = new Position() { X =  8, Y = 3, Z = 0 } },
                new Seat() { Id = "45", Available = "1", Name = "45", Position = new Position() { X =  8, Y = 4, Z = 0 } },
                new Seat() { Id = "46", Available = "1", Name = "46", Position = new Position() { X =  9, Y = 0, Z = 0 } },
                new Seat() { Id = "47", Available = "1", Name = "47", Position = new Position() { X =  9, Y = 1, Z = 0 } },
                new Seat() { Id = "48", Available = "1", Name = "48", Position = new Position() { X =  9, Y = 2, Z = 0 } },
                new Seat() { Id = "49", Available = "1", Name = "49", Position = new Position() { X =  9, Y = 3, Z = 0 } },
                new Seat() { Id = "50", Available = "1", Name = "50", Position = new Position() { X =  9, Y = 4, Z = 0 } },
                new Seat() { Id = "51", Available = "1", Name = "51", Position = new Position() { X = 10, Y = 0, Z = 0 } },
                new Seat() { Id = "52", Available = "1", Name = "52", Position = new Position() { X = 10, Y = 1, Z = 0 } },
                new Seat() { Id = "53", Available = "1", Name = "53", Position = new Position() { X = 10, Y = 2, Z = 0 } },
                new Seat() { Id = "54", Available = "1", Name = "54", Position = new Position() { X = 10, Y = 3, Z = 0 } },
                new Seat() { Id = "55", Available = "1", Name = "55", Position = new Position() { X = 10, Y = 4, Z = 0 } },
                new Seat() { Id = "56", Available = "1", Name = "56", Position = new Position() { X = 11, Y = 0, Z = 0 } }
            };
        }

        [Fact]
        public void TestarCaracterBasicasDaImagem()
        {
            var render = new Render(seats);
            var stream = render.GetStream();
            var img = Bitmap.FromStream(stream);

            img.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\assentos.png"));

            stream.Close();

            Assert.True(img.Width == 800);
            Assert.True(img.Height == 600);
            Assert.True(img.RawFormat.ToString() == ImageFormat.Png.ToString());
        }
    }
}
