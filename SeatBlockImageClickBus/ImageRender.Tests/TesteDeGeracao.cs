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
            const string FILEPATH = @"..\..\assentos.png";

            //Carga
            seats = Modelos.TodasAsPosicoesOrdemXeY();
            selelecteds = Modelos.SelecionadosParaTodasAsPosicoesOrdemXeY();

            //Definindo todos os assentos
            var render = new Render(seats);

            //Executando a renderização obtendo streams
            var stream = render.GetStream(selelecteds);
            
            //Transformando o stream em imagem
            var img = Bitmap.FromStream(stream);

            //Salvando em arquivo no diretório de binários do projeto (\bin)
            //Comentar essa linha para testes automatizados
            img.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILEPATH));

            //Apagando o renderizado da memória
            stream.Close();

            //Testes da imagem
            Assert.True(img.Width == 800);
            Assert.True(img.Height == 600);
            Assert.True(img.RawFormat.ToString() == ImageFormat.Png.ToString());

            var imgbase = Properties.Resources.TodosOsAssentosOrdemXeYComSelecionados;
            var imgtest = new Bitmap(img);

            //Comparando semelhança com uma margem pequena de erros
            Assert.True(Compare.Equals(imgbase, imgtest));
        }
    }
}
