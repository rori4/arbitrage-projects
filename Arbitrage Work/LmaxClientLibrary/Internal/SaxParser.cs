// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.SaxParser
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System.IO;
using System.Xml;

namespace Com.Lmax.Api.Internal
{
  public class SaxParser : IXmlParser
  {
    public void Parse(TextReader reader, ISaxContentHandler saxContentHandler)
    {
      XmlReader xmlReader = XmlReader.Create(reader, new XmlReaderSettings()
      {
        ConformanceLevel = ConformanceLevel.Fragment
      });
      while (xmlReader.Read())
      {
        if (xmlReader.HasValue)
          saxContentHandler.Content(xmlReader.Value);
        else if (xmlReader.IsEmptyElement)
        {
          saxContentHandler.StartElement(xmlReader.Name);
          saxContentHandler.EndElement(xmlReader.Name);
        }
        else if (xmlReader.IsStartElement())
          saxContentHandler.StartElement(xmlReader.Name);
        else
          saxContentHandler.EndElement(xmlReader.Name);
      }
    }
  }
}
