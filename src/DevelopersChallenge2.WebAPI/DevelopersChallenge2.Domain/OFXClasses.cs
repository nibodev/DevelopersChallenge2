using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DevelopersChallenge2.Domain
{
    [Serializable, XmlRoot("OFX")]
    public class OFX
    {
        [XmlElement]
        public SIGNONMSGSRSV1 SIGNONMSGSRSV1 { get; set; }
        [XmlElement]
        public BANKMSGSRSV1 BANKMSGSRSV1 { get; set; }
    }

    [Serializable]
    public class SIGNONMSGSRSV1
    {
        [XmlElement]
        public SONRS SONRS { get; set; }
    }

    [Serializable]
    public class SONRS
    {
        [XmlElement]
        public STATUS STATUS { get; set; }
        [XmlElement]
        public string DTSERVER { get; set; }
        [XmlElement]
        public string LANGUAGE { get; set; }
    }

    [Serializable]
    public class STATUS
    {
        [XmlElement]
        public string CODE { get; set; }
        [XmlElement]
        public string SEVERITY { get; set; }
    }

    [Serializable]
    public class BANKMSGSRSV1
    {
        [XmlElement]
        public STMTTRNRS STMTTRNRS { get; set; }
    }

    [Serializable]
    public class STMTTRNRS
    {
        [XmlElement]
        public string TRNUID { get; set; }
        [XmlElement]
        public STATUS STATUS { get; set; }
        [XmlElement]
        public STMTRS STMTRS { get; set; }
    }

    [Serializable]
    public class STMTRS
    {
        [XmlElement]
        public string CURDEF { get; set; }
        [XmlElement]
        public BANKACCTFROM BANKACCTFROM { get; set; }
        [XmlElement]
        public BANKTRANLIST BANKTRANLIST { get; set; }
        [XmlElement]
        public LEDGERBAL LEDGERBAL { get; set; }
    }

    [Serializable]
    public class BANKACCTFROM
    {
        [XmlElement]
        public string BANKID { get; set; }
        [XmlElement]
        public string ACCTID { get; set; }
        [XmlElement]
        public string ACCTTYPE { get; set; }
    }

    [Serializable]
    public class BANKTRANLIST : Entity
    {
        [XmlElement]
        public string DTSTART { get; set; }
        [XmlElement]
        public string DTEND { get; set; }
        [XmlArray]
        [XmlArrayItem]
        public List<STMTTRN> STMTTRN { get; set; } = new List<STMTTRN>();
    }

    [Serializable]
    public class STMTTRN
    {
        [XmlElement]
        public string TRNTYPE { get; set; }
        [XmlElement]
        public string DTPOSTED { get; set; }
        [XmlElement]
        public string TRNAMT { get; set; }
        [XmlElement]
        public string MEMO { get; set; }
    }

    [Serializable]
    public class LEDGERBAL
    {
        [XmlElement]
        public string BALAMT { get; set; }
        [XmlElement]
        public string DTASOF { get; set; }
    }
}
