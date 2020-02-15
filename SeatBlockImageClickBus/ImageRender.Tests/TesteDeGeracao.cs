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
        private List<Seat> selelecteds { get; set; }
        
        [Fact]
        public void TestarCaracteristicasBasicasDaImagem()
        {
            //const string FILEPATH = @"..\..\assentos.png";

            //Carga
            seats = Modelos.TodasAsPosicoesOrdemXeY();
            selelecteds = Modelos.SelecionadosParaTodasAsPosicoesOrdemXeY();

            //Definindo todos os assentos
            var render = new Render(seats);

            //Executando a renderiza��o obtendo streams
            var stream = render.GetStream(selelecteds);
            
            //Transformando o stream em imagem
            var img = Bitmap.FromStream(stream);

            //Salvando em arquivo no diret�rio de bin�rios do projeto (\bin)
            //Comentar essa linha para testes automatizados
            //img.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILEPATH));

            //Apagando o renderizado da mem�ria
            stream.Close();

            //Testes da imagem
            Assert.True(img.Width == 800);
            Assert.True(img.Height == 600);
            Assert.True(img.RawFormat.ToString() == ImageFormat.Png.ToString());

            var imgbase = Properties.Resources.TodosOsAssentosOrdemXeYComOcupadosESelecionados;
            var imgtest = new Bitmap(img);

            //Comparando semelhan�a com uma margem pequena de erros
            Assert.True(Compare.Equals(imgbase, imgtest));
        }

        [Fact]
        public void TestarCaracteristicasOnibusComumVazio()
        {
            //Carga 56 posi��es X: 0..13 e Y: 0,1,3,4
            seats = Modelos.OnibusComum();
            selelecteds = new List<Seat>();

            //Definindo todos os assentos
            var render = new Render(seats);

            //Executando a renderiza��o obtendo streams
            var stream = render.GetStream(selelecteds);

            //Transformando o stream em imagem
            var img = Bitmap.FromStream(stream);

            //Apagando o renderizado da mem�ria
            stream.Close();

            //Testes da imagem
            Assert.True(img.Width == 800);
            Assert.True(img.Height == 600);
            Assert.True(img.RawFormat.ToString() == ImageFormat.Png.ToString());

            var onibus = new Bitmap(img);
            var onibusvazio = Properties.Resources.OnibusComumVazio;
            var onibuslotado = Properties.Resources.OnibusLotado;

            //Comparando semelhan�a com uma margem pequena de erros
            //Deve estar vazio
            Assert.True(Compare.Equals(onibus, onibusvazio));
            //N�o pode estar lotado
            Assert.False(Compare.Equals(onibus, onibuslotado));
        }

        [Fact]
        public void TestarCaracteristicasOnibusComum()
        {
            //Carga 56 posi��es X: 0..13 e Y: 0,1,3,4
            seats = Modelos.OnibusComum();

            //Um numero aleat�rio de ocupa��o
            seats.OcuparOnibusComum();

            //Definindo todos os assentos
            var render = new Render(seats);

            //Executando a renderiza��o obtendo streams
            var stream = render.GetStream(new List<Seat>());

            //Transformando o stream em imagem
            var img = Bitmap.FromStream(stream);

            //Apagando o renderizado da mem�ria
            stream.Close();

            //Testes da imagem
            Assert.True(img.Width == 800);
            Assert.True(img.Height == 600);
            Assert.True(img.RawFormat.ToString() == ImageFormat.Png.ToString());

            var onibus = new Bitmap(img);
            var onibusvazio = Properties.Resources.OnibusComumVazio;
            var onibuslotado = Properties.Resources.OnibusLotado;

            //Comparando semelhan�a com uma margem pequena de erros
            //N�o pode estar vazio
            Assert.False(Compare.Equals(onibus, onibusvazio));
            //N�o pode estar lotado
            Assert.False(Compare.Equals(onibus, onibuslotado));
        }
    }
}
