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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using FileDecoder.MessageDecoders;
using HL7.Dotnetcore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileDecoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FileDataBox_TextChanged(object sender, RoutedEventArgs e)
        {
            string dataStart = ""; 
            if (FileDataBox.Text.Length > 2)
                dataStart = FileDataBox.Text.Substring(0, 3);
            

            if (dataStart == "MSH")
            {
                Segs.Items.Clear();
                var decoder = new Hl7Decoder();
                var segListView = decoder.Hl7TreeView(FileDataBox.Text);
                Segs.Items.Add(segListView);
            }
            else if (dataStart.StartsWith("<"))
            {
              Segs.Items.Clear();
              var decoder = new XMLDecoder();
              var xmlSegs = decoder.XmlTreeView(FileDataBox.Text); 
              Segs.Items.Add(xmlSegs);
                
                //Do nothing at the moment. 
            }
        }
    }
}
