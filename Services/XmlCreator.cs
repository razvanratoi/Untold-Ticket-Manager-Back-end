using System.Xml;
using Assignment1.Models;
using Assignment1.Services.AbstractServices;

namespace Assignment1.Services
{
    public class XmlCreator : AbstractCreator
    {

        public XmlCreator(){}
        public void createFile(List<Ticket> tickets, Show show, Artist artist)
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            using (XmlWriter writer = XmlWriter.Create("tickets_for_" + show.Title + ".xml", settings)){
                writer.WriteStartElement("tickets");
                foreach(Ticket t in tickets)
                {
                    writer.WriteStartElement("ticket", t.Id.ToString());
                    writer.WriteElementString("cashier_id", t.CashierId.ToString());
                    writer.WriteStartElement("show", show.Id.ToString());
                    writer.WriteElementString("title", show.Title);
                    writer.WriteElementString("artist", artist.Name);
                    writer.WriteElementString("genre", artist.Genre);
                    writer.WriteEndElement();
                    writer.WriteElementString("quantity", t.Places.ToString());
                    writer.WriteEndElement();
                    writer.Flush();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }
    }
}