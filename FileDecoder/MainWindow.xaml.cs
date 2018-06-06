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

            if (FileDataBox.Text.Substring(0, 3) == "MSH")
            {
                Message message = new Message(FileDataBox.Text.ToString());
                message.ParseMessage();


                List<Segment> segList = message.Segments();
                for (int i = 0; i < segList.Count; i++)
                {
                    TreeViewItem segListView = new TreeViewItem();

                    segListView.Header = segList[i].Name;
                    var fieldList = segList[i].GetAllFields();
                    for (int j = 0; j < fieldList.Count; j++)
                    {
                        var fieldItem = new TreeViewItem();
                        Fields field = new Fields
                        {
                            FieldName = segList[i].Name + ":" + (j+1),
                            FieldData = fieldList[j].Value
                        };
                        if (field.FieldData != "")
                       segListView.Items.Add(segList[i].Name + ":" + (j+1) + "  --  " + fieldList[j].Value); 
                    }
                    Segs.Items.Add(segListView);
                }
            }
        }
    }
    public class Fields {
        public string FieldName { get; set; }
        public string FieldData { get; set; }

    }
    
}
