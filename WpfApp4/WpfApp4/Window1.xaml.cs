using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp4.NewFolder;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.SKCharts;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Kernel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using LiveChartsCore.SkiaSharpView.WPF;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;


namespace WpfApp4
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Gr1 Gr1 { get; set; }
        public Gr2 Gr2 { get; set; }
        public MainWindow MAIN { get; set; }
        public Gr3 Gr3 { get; set; }

        List<CartesianChart> cartesianCharts = new List<CartesianChart>();

        List<byte[]> bitmaps = new List<byte[]>();

        public Window1()
        {
            InitializeComponent();
            Gr1 = new Gr1();
            Gr2 = new Gr2();
            Gr3 = new Gr3();
            MAIN = new MainWindow();

            cartesianCharts.Add(Gr1.Grafic1);
            cartesianCharts.Add(Gr2.Grafic1);
            cartesianCharts.Add(Gr3.Grafic1);
            cartesianCharts.Add(MAIN.Grafic);

            //foreach (var item in cartesianCharts)
            //{
            //    bitmaps.Add(ToBytes(item));
                
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ExportChart(cartesianCharts[0]);

        }

        public void GenerateDocument()
        {

            using (var writer = new PdfWriter("C:\\Users\\eecheto\\Desktop\\Pruebas\\prueba2\\WpfApp4\\WpfApp4\\documents.pdf"))
            {
                // Crear un documento PDF
                using (var pdf = new PdfDocument(writer))
                {
                    var doc = new Document(pdf);
                    // Crear un objeto ImageData para la imagen
                    var imageData = ImageDataFactory.Create(bitmaps[0]);

                    // Crear un objeto Image y añadirlo al documento
                    var image = new iText.Layout.Element.Image(imageData)
                        //.SetAutoScale(true) // Ajusta automáticamente el tamaño de la imagen
                        .SetWidth(300) // Ajusta el ancho de la imagen (opcional)
                        .SetHeight(350)
                        .SetMarginLeft(100); // Ajusta la altura de la imagen (opcional)
                    //nombre de muro y piso

                    doc.Add(image);
                    doc.Add(image);
                    doc.Add(image);
                    doc.Add(image);
                    doc.Add(image);
                }
            }
        }

        public void ExportChart(CartesianChart chart)
        {

            chart.Measure(new System.Windows.Size(540, 500));
            chart.Arrange(new Rect(0, 0, 540, 500));
            chart.UpdateLayout(); // Asegura que el layout está actualizado

            var bitmap = new RenderTargetBitmap(
               540,500 ,
                96, 96, PixelFormats.Pbgra32);

            bitmap.Render(chart);




            using (var fileStream = new FileStream("C:\\Users\\eecheto\\Desktop\\Pruebas\\prueba2\\WpfApp4\\WpfApp4\\WpfApp4.csprojchart.png", FileMode.Create))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(fileStream);
            }
        }


        //public byte[] ToBytes(CartesianChart chart)
        //{
        //    // Crear un RenderTargetBitmap del gráfico
        //    var bitmap = new RenderTargetBitmap(
        //        560, 750,
        //        96, 96, PixelFormats.Pbgra32);

        //    // Renderizar el gráfico al RenderTargetBitmap
        //    bitmap.Render(chart);

        //    // Convertir RenderTargetBitmap a un arreglo de bytes
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        var encoder = new PngBitmapEncoder();
        //        encoder.Frames.Add(BitmapFrame.Create(bitmap));
        //        encoder.Save(memoryStream);

        //        return memoryStream.ToArray();
        //    }
        //}

    }
}
