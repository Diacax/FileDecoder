using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace FileDecoder.MessageDecoders
{
    class XMLDecoder
    {
        public TreeViewItem XmlTreeView(string fileData)
        {
            var xDocument = new XmlDocument();
            xDocument.LoadXml(fileData);
            TreeViewItem xmlSegs = new TreeViewItem {Header = "XML Nodes"}; 
            foreach (XmlNode node in xDocument.ChildNodes)
            {
                if (node.HasChildNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        TreeViewItem childItem = new TreeViewItem {Header = childNode.LocalName};
                        if (childNode.HasChildNodes)
                        {
                            foreach (XmlNode nLayer in childNode.ChildNodes)
                            {
                                TreeViewItem children = new TreeViewItem {Header = nLayer.Name};
                                children.Items.Add(nLayer.InnerText);
                                childItem.Items.Add(children);
                            }
                        }
                        else
                        {
                            childItem.Items.Add(childNode.Name + " :: " + childNode.InnerText);
                        }
                        xmlSegs.Items.Add(childItem);
                    }
                }
            }

            return xmlSegs; 
        }
    }
}
