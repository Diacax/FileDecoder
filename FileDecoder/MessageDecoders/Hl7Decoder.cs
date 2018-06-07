using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HL7.Dotnetcore;

namespace FileDecoder.MessageDecoders
{
    public class Hl7Decoder
    {
        public TreeViewItem Hl7TreeView(string fileData)
        {
            if (fileData.Substring(0, 3) == "MSH")
            {
                Message message = new Message(fileData);
                message.ParseMessage();

                TreeViewItem segs = new TreeViewItem {Header = "Message Segments"};

                List<Segment> segList = message.Segments();
                for (int i = 0; i < segList.Count; i++)
                {
                    TreeViewItem segListView = new TreeViewItem {Header = segList[i].Name};

                    var fieldList = segList[i].GetAllFields();
                    for (int j = 0; j < fieldList.Count; j++)
                    {
                        if (fieldList[j].Value != "")
                        {
                            var fieldItem = new TreeViewItem { Header = segList[i].Name + ":" + (j + 1) + "  --  " + fieldList[j].Value };
                            if (fieldList[j].IsComponentized)
                            {
                                var fields = fieldList[j].Components();
                                for (int k = 0; k < fields.Count; k++)
                                {
                                    if (fields[k].Value != "")
                                    fieldItem.Items.Add(segList[i].Name + ":" + (j + 1) + "." + (k+1) + " --"  + fields[k].Value);
                                }
                            }
                            segListView.Items.Add(fieldItem);
                        }
                    }
                    segs.Items.Add(segListView);
                }
                return segs;
            }
            return new TreeViewItem();
        }

    }
}
