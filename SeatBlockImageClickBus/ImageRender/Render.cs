
namespace ImageRender
{
    using ImageRender.Model;
    using Svg;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Xml;

    public class Render
    {
        private readonly string _template;
        private readonly float[] _x;
        private readonly float[] _y;
        private readonly List<Seat> _seats;

        private List<Seat> _reserved { get; set; } = new List<Seat>();

        public Render(List<Seat> seats)
        {
            _template = Properties.Resources.Template202002;
            
            _x = new float[14];
            _y = new float[5];
            _seats = (seats != null) ? seats : new List<Seat>();

            SetCoordinates();
        }

        public Stream GetStream(List<Seat> reserved)
        {
            _reserved = reserved;

            MemoryStream stream = new MemoryStream();

            var doc = new XmlDocument();
            doc.LoadXml(_template);

            var svg = SvgDocument.Open(doc);

            SetSeat(svg);
            
            svg.Draw().Save(stream, ImageFormat.Png);
            svg.Draw().Dispose();
            
            return stream;
        }

        private void SetSeat(SvgDocument svg)
        {
            var query = from item in _seats
                        orderby item.Position.X, item.Position.Y
                        select item;

            var itens = query.ToList();

            foreach (var item in itens)
            {
                BuildSeat(svg, item);
            }

        }

        private void BuildSeat(SvgDocument svg, Seat item)
        {
            var seat = new Svg.SvgRectangle();

            var seatcolor = Color.FromArgb(255, 255, 255);
            var seatfree = Color.FromArgb(126, 238, 0);
            var seatselected = Color.FromArgb(255, 240, 147);
            var seatoff = Color.FromArgb(238, 238, 238);

            var query = from r in _reserved
                        where r.Name == item.Name
                        || (r.Position.X == item.Position.X
                        && r.Position.Y == item.Position.Y
                        && r.Position.Z == item.Position.Z)
                        select item;

            if (string.IsNullOrEmpty(item.Available.Trim()))
                seatcolor = seatoff;
            else if (item.Available == "1")
                seatcolor = seatfree;

            if (query.Any())
                seatcolor = seatselected;

            seat.X = new SvgUnit(SvgUnitType.Pixel, _x[item.Position.X]);
            seat.Y = new SvgUnit(SvgUnitType.Pixel, _y[item.Position.Y]);
            seat.Width = new SvgUnit(SvgUnitType.Pixel, 40);
            seat.Height = new SvgUnit(SvgUnitType.Pixel, 40);
            seat.CornerRadiusX = new SvgUnit(SvgUnitType.Pixel, 5);
            seat.CornerRadiusY = new SvgUnit(SvgUnitType.Pixel, 5);
            seat.Fill = new SvgColourServer(seatcolor);
            seat.Stroke = new SvgColourServer(Color.FromArgb(84, 124, 54));
            seat.StrokeWidth = new SvgUnit(SvgUnitType.Pixel, 1);

            var number = new Svg.SvgText(item.Name);

            number.X.Insert(0, new SvgUnit(SvgUnitType.Pixel, _x[item.Position.X] + 8));
            number.Y.Insert(0, new SvgUnit(SvgUnitType.Pixel, _y[item.Position.Y] + 28));
            number.FontFamily = "Roboto,sans-serif";
            number.FontSize = new SvgUnit(SvgUnitType.Pixel, 20);
            number.FontWeight = SvgFontWeight.Bold;
            number.Fill = new SvgColourServer(Color.FromArgb(0, 0, 0));

            svg.Children.Add(seat);
            svg.Children.Add(number);
        }

        private void SetCoordinates()
        {
            for (int px = 0; px < 14; px++)
                _x[px] = (px == 0) ? 120 : _x[px - 1] + 46;

            for (int py = 4; py > -1; py--)
                _y[py] = (py == 4) ? 190 : _y[py + 1] + 44;
        }

    }
}
