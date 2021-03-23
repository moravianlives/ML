using System.Collections.Generic;
using System.Xml.Serialization;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model
{
    public class Personography
    {
        /* 
         Licensed under the Apache License, Version 2.0
         
         http://www.apache.org/licenses/LICENSE-2.0
         */
        [XmlRoot(ElementName = "address", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Address
        {
            [XmlElement(ElementName = "addrLine", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<string> AddrLine { get; set; }
        }

        [XmlRoot(ElementName = "principal", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Principal
        {
            [XmlElement(ElementName = "persName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string PersName { get; set; }
            [XmlElement(ElementName = "orgName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string OrgName { get; set; }
            [XmlElement(ElementName = "address", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Address Address { get; set; }
            [XmlElement(ElementName = "email", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string Email { get; set; }
        }

        [XmlRoot(ElementName = "respStmt", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class RespStmt
        {
            [XmlElement(ElementName = "persName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string PersName { get; set; }
            [XmlElement(ElementName = "orgName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string OrgName { get; set; }
            [XmlElement(ElementName = "resp", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string Resp { get; set; }
        }

        [XmlRoot(ElementName = "titleStmt", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class TitleStmt
        {
            [XmlElement(ElementName = "title", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string Title { get; set; }
            [XmlElement(ElementName = "funder", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string Funder { get; set; }
            [XmlElement(ElementName = "principal", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Principal> Principal { get; set; }
            [XmlElement(ElementName = "respStmt", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<RespStmt> RespStmt { get; set; }
        }

        [XmlRoot(ElementName = "edition", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Edition
        {
            [XmlElement(ElementName = "orgName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string OrgName { get; set; }
            [XmlElement(ElementName = "address", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Address Address { get; set; }
        }

        [XmlRoot(ElementName = "editionStmt", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class EditionStmt
        {
            [XmlElement(ElementName = "edition", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Edition Edition { get; set; }
        }

        [XmlRoot(ElementName = "availability", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Availability
        {
            [XmlElement(ElementName = "licence", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string Licence { get; set; }
        }

        [XmlRoot(ElementName = "publicationStmt", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class PublicationStmt
        {
            [XmlElement(ElementName = "distributor", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string Distributor { get; set; }
            [XmlElement(ElementName = "availability", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Availability Availability { get; set; }
        }

        [XmlRoot(ElementName = "sourceDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class SourceDesc
        {
            [XmlElement(ElementName = "p", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string P { get; set; }
        }

        [XmlRoot(ElementName = "fileDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class FileDesc
        {
            [XmlElement(ElementName = "titleStmt", Namespace = "http://www.tei-c.org/ns/1.0")]
            public TitleStmt TitleStmt { get; set; }
            [XmlElement(ElementName = "editionStmt", Namespace = "http://www.tei-c.org/ns/1.0")]
            public EditionStmt EditionStmt { get; set; }
            [XmlElement(ElementName = "publicationStmt", Namespace = "http://www.tei-c.org/ns/1.0")]
            public PublicationStmt PublicationStmt { get; set; }
            [XmlElement(ElementName = "sourceDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
            public SourceDesc SourceDesc { get; set; }
        }

        [XmlRoot(ElementName = "category", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class CategoryBlock
        {
            [XmlElement(ElementName = "catDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string CatDesc { get; set; }
            [XmlAttribute(AttributeName = "id", Namespace = "http://www.w3.org/XML/1998/namespace")]
            public string Id { get; set; }
            [XmlElement(ElementName = "category", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<CategoryBlock> Category { get; set; }
        }

        [XmlRoot(ElementName = "taxonomy", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Taxonomy
        {
            [XmlElement(ElementName = "category", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<CategoryBlock> Category { get; set; }
        }

        [XmlRoot(ElementName = "classDecl", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class ClassDecl
        {
            [XmlElement(ElementName = "taxonomy", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Taxonomy Taxonomy { get; set; }
        }

        [XmlRoot(ElementName = "encodingDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class EncodingDesc
        {
            [XmlElement(ElementName = "classDecl", Namespace = "http://www.tei-c.org/ns/1.0")]
            public ClassDecl ClassDecl { get; set; }
        }

        [XmlRoot(ElementName = "change", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Change
        {
            [XmlAttribute(AttributeName = "who")]
            public string Who { get; set; }
            [XmlAttribute(AttributeName = "when")]
            public string When { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "revisionDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class RevisionDesc
        {
            [XmlElement(ElementName = "change", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Change> Change { get; set; }
        }

        [XmlRoot(ElementName = "teiHeader", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class TeiHeader
        {
            [XmlElement(ElementName = "fileDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
            public FileDesc FileDesc { get; set; }
            [XmlElement(ElementName = "encodingDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
            public EncodingDesc EncodingDesc { get; set; }
            [XmlElement(ElementName = "revisionDesc", Namespace = "http://www.tei-c.org/ns/1.0")]
            public RevisionDesc RevisionDesc { get; set; }
        }

        [XmlRoot(ElementName = "surname", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Surname
        {
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "resp")]
            public string Resp { get; set; }
            [XmlAttribute(AttributeName = "cert")]
            public string Cert { get; set; }
        }

        [XmlRoot(ElementName = "persName", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class PersName
        {
            [XmlElement(ElementName = "surname", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Surname> Surname { get; set; }
            [XmlElement(ElementName = "forename", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Forename> Forename { get; set; }
            [XmlAttribute(AttributeName = "ana")]
            public string Ana { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlElement(ElementName = "name", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Name> Name { get; set; }
            [XmlAttribute(AttributeName = "ref")]
            public string Ref { get; set; }
            [XmlElement(ElementName = "nameLink", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string NameLink { get; set; }
            [XmlElement(ElementName = "note", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Note Note { get; set; }
            [XmlElement(ElementName = "addName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public AddName AddName { get; set; }
        }

        [XmlRoot(ElementName = "date", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Date
        {
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "calendar")]
            public string Calendar { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlAttribute(AttributeName = "resp")]
            public string Resp { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "cert")]
            public string Cert { get; set; }
            [XmlAttribute(AttributeName = "dur")]
            public string Dur { get; set; }
        }

        [XmlRoot(ElementName = "placeName", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class PlaceName
        {
            [XmlAttribute(AttributeName = "ref")]
            public string Ref { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "resp")]
            public string Resp { get; set; }
            [XmlElement(ElementName = "date", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Date Date { get; set; }
        }

        [XmlRoot(ElementName = "birth", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Birth
        {
            [XmlElement(ElementName = "date", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Date> Date { get; set; }
            [XmlElement(ElementName = "placeName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<PlaceName> PlaceName { get; set; }
        }

        [XmlRoot(ElementName = "death", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Death
        {
            [XmlElement(ElementName = "date", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Date> Date { get; set; }
            [XmlElement(ElementName = "placeName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public PlaceName PlaceName { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlAttribute(AttributeName = "cert")]
            public string Cert { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "orgName", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class OrgName
        {
            [XmlAttribute(AttributeName = "religion")]
            public string Religion { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "en")]
            public string En { get; set; }
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
        }

        [XmlRoot(ElementName = "desc", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Desc
        {
            [XmlElement(ElementName = "orgName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<OrgName> OrgName { get; set; }
            [XmlElement(ElementName = "persName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public PersName PersName { get; set; }
            [XmlElement(ElementName = "placeName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public PlaceName PlaceName { get; set; }
        }

        [XmlRoot(ElementName = "event", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Event
        {
            [XmlElement(ElementName = "desc", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Desc Desc { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "where")]
            public string Where { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlAttribute(AttributeName = "office")]
            public string Office { get; set; }
            [XmlAttribute(AttributeName = "ref")]
            public string Ref { get; set; }
            [XmlAttribute(AttributeName = "cert")]
            public string Cert { get; set; }
            [XmlAttribute(AttributeName = "from")]
            public string From { get; set; }
            [XmlAttribute(AttributeName = "when")]
            public string When { get; set; }
            [XmlAttribute(AttributeName = "to")]
            public string To { get; set; }
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
            [XmlAttribute(AttributeName = "resp")]
            public string Resp { get; set; }
            [XmlAttribute(AttributeName = "calendar")]
            public string Calendar { get; set; }
        }

        [XmlRoot(ElementName = "occupation", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Occupation
        {
            [XmlAttribute(AttributeName = "ref")]
            public string Ref { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlElement(ElementName = "placeName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<PlaceName> PlaceName { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlElement(ElementName = "date", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Date Date { get; set; }
            [XmlAttribute(AttributeName = "from")]
            public string From { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
            [XmlAttribute(AttributeName = "to")]
            public string To { get; set; }
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlElement(ElementName = "orgName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public OrgName OrgName { get; set; }
            [XmlAttribute(AttributeName = "cert")]
            public string Cert { get; set; }
            [XmlElement(ElementName = "persName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<PersName> PersName { get; set; }
            [XmlAttribute(AttributeName = "calendar")]
            public string Calendar { get; set; }
        }

        [XmlRoot(ElementName = "affiliation", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Affiliation
        {
            [XmlElement(ElementName = "orgName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public OrgName OrgName { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlAttribute(AttributeName = "ref")]
            public string Ref { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "bibl", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Bibl
        {
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "source")]
            public string Source { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "n")]
            public string N { get; set; }
        }

        [XmlRoot(ElementName = "note", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Note
        {
            [XmlAttribute(AttributeName = "resp")]
            public string Resp { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "person", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Person
        {
            [XmlElement(ElementName = "persName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public PersName PersName { get; set; }
            [XmlElement(ElementName = "birth", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Birth Birth { get; set; }
            [XmlElement(ElementName = "death", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Death Death { get; set; }
            [XmlElement(ElementName = "event", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Event> Event { get; set; }
            [XmlElement(ElementName = "occupation", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Occupation> Occupation { get; set; }
            [XmlElement(ElementName = "affiliation", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Affiliation> Affiliation { get; set; }
            [XmlElement(ElementName = "bibl", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Bibl> Bibl { get; set; }
            [XmlElement(ElementName = "note", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Note Note { get; set; }
            [XmlAttribute(AttributeName = "id", Namespace = "http://www.w3.org/XML/1998/namespace")]
            public string Id { get; set; }
            [XmlAttribute(AttributeName = "sex")]
            public string Sex { get; set; }
            [XmlElement(ElementName = "residence", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Residence> Residence { get; set; }
            [XmlElement(ElementName = "office", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Office> Office { get; set; }
            [XmlElement(ElementName = "education", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Education> Education { get; set; }
            [XmlElement(ElementName = "faith", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Faith> Faith { get; set; }
            [XmlAttribute(AttributeName = "sameAs")]
            public string SameAs { get; set; }
        }

        [XmlRoot(ElementName = "relation", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Relation
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "passive")]
            public string Passive { get; set; }
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlAttribute(AttributeName = "mutual")]
            public string Mutual { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
            [XmlAttribute(AttributeName = "active")]
            public string Active { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "when")]
            public string When { get; set; }
        }

        [XmlRoot(ElementName = "listRelation", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class ListRelation
        {
            [XmlElement(ElementName = "relation", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<Relation> Relation { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
        }

        [XmlRoot(ElementName = "listPerson", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class ListPerson
        {
            [XmlElement(ElementName = "person", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Person Person { get; set; }
            [XmlElement(ElementName = "listRelation", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<ListRelation> ListRelation { get; set; }
        }

        [XmlRoot(ElementName = "forename", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Forename
        {
            [XmlElement(ElementName = "addName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public string AddName { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "resp")]
            public string Resp { get; set; }
        }

        [XmlRoot(ElementName = "residence", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Residence
        {
            [XmlElement(ElementName = "placeName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public PlaceName PlaceName { get; set; }
            [XmlAttribute(AttributeName = "from")]
            public string From { get; set; }
            [XmlAttribute(AttributeName = "to")]
            public string To { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlAttribute(AttributeName = "cert")]
            public string Cert { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
            [XmlAttribute(AttributeName = "calendar")]
            public string Calendar { get; set; }
            [XmlElement(ElementName = "persName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public PersName PersName { get; set; }
            [XmlElement(ElementName = "date", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Date Date { get; set; }
        }

        [XmlRoot(ElementName = "office", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Office
        {
            [XmlElement(ElementName = "placeName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<PlaceName> PlaceName { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlAttribute(AttributeName = "ref")]
            public string Ref { get; set; }
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
            [XmlAttribute(AttributeName = "from")]
            public string From { get; set; }
            [XmlAttribute(AttributeName = "to")]
            public string To { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlElement(ElementName = "orgName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public OrgName OrgName { get; set; }
            [XmlElement(ElementName = "name", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Name Name { get; set; }
            [XmlElement(ElementName = "persName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public PersName PersName { get; set; }
            [XmlElement(ElementName = "date", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Date Date { get; set; }
        }

        [XmlRoot(ElementName = "education", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Education
        {
            [XmlElement(ElementName = "placeName", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<PlaceName> PlaceName { get; set; }
            [XmlAttribute(AttributeName = "when-iso")]
            public string Wheniso { get; set; }
            [XmlAttribute(AttributeName = "from")]
            public string From { get; set; }
            [XmlAttribute(AttributeName = "to")]
            public string To { get; set; }
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
            [XmlElement(ElementName = "date", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Date Date { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "name", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Name
        {
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlAttribute(AttributeName = "resp")]
            public string Resp { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "event")]
            public string Event { get; set; }
        }

        [XmlRoot(ElementName = "faith", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Faith
        {
            [XmlAttribute(AttributeName = "notBefore")]
            public string NotBefore { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "notAfter")]
            public string NotAfter { get; set; }
        }

        [XmlRoot(ElementName = "addName", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class AddName
        {
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "body", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Body
        {
            [XmlElement(ElementName = "listPerson", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<ListPerson> ListPerson { get; set; }
            [XmlElement(ElementName = "listRelation", Namespace = "http://www.tei-c.org/ns/1.0")]
            public List<ListRelation> ListRelation { get; set; }
        }

        [XmlRoot(ElementName = "text", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class Text
        {
            [XmlElement(ElementName = "body", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Body Body { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
        }

        [XmlRoot(ElementName = "TEI", Namespace = "http://www.tei-c.org/ns/1.0")]
        public class TEI
        {
            [XmlElement(ElementName = "teiHeader", Namespace = "http://www.tei-c.org/ns/1.0")]
            public TeiHeader TeiHeader { get; set; }
            [XmlElement(ElementName = "text", Namespace = "http://www.tei-c.org/ns/1.0")]
            public Text Text { get; set; }
            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns { get; set; }
        }
    }
}