<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:tei="http://www.tei-c.org/ns/1.0"
    xmlns:xhtml="http://www.w3.org/1999/xhtml"
    exclude-result-prefixes="xs tei xhtml"
    version="2.0">
    
    <xsl:output doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN"
        doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"
        method="xhtml" omit-xml-declaration="yes" indent="yes" encoding="UTF-8"/>
    <xsl:strip-space elements="*"/>
    
    <xsl:template match="/">
        <xsl:apply-templates/>
    </xsl:template>
    
    <xsl:template match="tei:TEI">
        <html>
            <head>
                <xsl:comment>This document is generated from a TEI Master--do not edit!</xsl:comment>
                <title><xsl:value-of select="tei:teiHeader/tei:fileDesc/tei:titleStmt/tei:title"/></title>
                <link rel="stylesheet" type="text/css" href="../CSS/MemStyle.css"/>
            </head>
            <body><xsl:apply-templates select="tei:text"/></body>
        </html>
    </xsl:template>

<!-- This hides "title" of the memoir -->
    <xsl:template match="tei:head" />

    
<!-- This breaks the text into paragraphs as marked up in text (not the same as pages) -->
    <xsl:template match="tei:p">
        <p><xsl:apply-templates/></p>
    </xsl:template>

    
<!-- This hides the sic spelling, leaving only the correct spelling -->
    <xsl:template match="tei:orig"/>        
    
<!-- This renders the superscript letters in HTML <sup> tag -->
    <xsl:template match="tei:hi[@rend='superscript'] | tei:sup">
        <sup xmlns="http://www.w3.org/1999/xhtml">
            <xsl:value-of select="."/>
        </sup>        
    </xsl:template>

<!-- This renders underlined letters in HTML <u> tag -->
    
    
<!-- This renders strikethrough in HTML -->
    <xsl:template match="tei:del">
        <del xmlns="http://www.w3.org/1999/xhtml">
            <xsl:value-of select="."/>
        </del>
    </xsl:template>
    
<!-- This hides the catchword form of note -->
    <xsl:template match="tei:note[@type='catchword']" />

<!-- This adds /// at each page break -->
    <xsl:template match="tei:div[@type='page']">
        <p><xsl:apply-templates/>
        <xsl:text>///</xsl:text>
        </p>
    </xsl:template>
 
 <!-- This provides spacing between persons -->
    <xsl:template match="tei:listPerson">
        <p>
            <xsl:apply-templates/>
            <hr/>
        </p>

    </xsl:template>
    
 <!-- This lays out the person name 
    <xsl:template match="tei:person/tei:persName">
        <strong>
            <xsl:text>Person: </xsl:text>
            <xsl:apply-templates/>
        </strong>
        <br/>
    </xsl:template>  -->  
    
    <xsl:template match="tei:surname">
        <xsl:text> </xsl:text>
        <xsl:apply-templates/>
    <br/>
    </xsl:template>
    
<!-- This provides the person birth date (Gregorian) -->
    <xsl:template match="tei:birth/tei:date[@calendar='Gregorian']">
            <xsl:text>Birth date: </xsl:text>
            <xsl:apply-templates/>        
        <br/>
    </xsl:template>

<!-- This hides the person birth date (Julian) -->
    <xsl:template match="tei:birth/tei:date[@calendar='Julian']"/>
    
<!-- This hides the person birth date (other source) -->
    <xsl:template match="tei:birth/tei:date[@resp='memoir']"/>
    
<!-- This provides the person birth place -->
    <xsl:template match="tei:birth/tei:placeName">
        <xsl:text>Birth place: </xsl:text>
        <xsl:apply-templates/>        
        <br/>
    </xsl:template>
    
    <!-- This provies the person death date -->
    <xsl:template match="tei:death/tei:date">
        <xsl:text>Death date: </xsl:text>
        <xsl:apply-templates/>        
        <br/>
    </xsl:template>
    
    <!-- This provides the person death place -->
    <xsl:template match="tei:death/tei:placeName">
        <xsl:text>Death place: </xsl:text>
        <xsl:apply-templates/>        
        <br/>
    </xsl:template>
    
<!-- This structures the list of events -->
    <xsl:template match="tei:listEvent">
        <h1>
        <xsl:text>Events in Moravian Life: </xsl:text>
        </h1>
        <ul>
            <xsl:apply-templates select="tei:event"/>
        </ul>
        

    </xsl:template>

    <!-- This provides a list of events (if available) -->
    <xsl:template match="tei:event">
        <li>
        <xsl:apply-templates select="tei:event"/>
        </li>
    </xsl:template>    
    
    <!-- This provides a list of occupations (if available) -->
    <xsl:template match="tei:occupation">
        <xsl:text>Occupation(s): </xsl:text>
        <xsl:apply-templates/>
        <br/>
    </xsl:template>
    
    
    <!-- This provides a list of affiliations (if available) -->
    <xsl:template match="tei:affiliation">
        <xsl:text>Affiliation(s): </xsl:text>
        <xsl:apply-templates/>
        <br/>
    </xsl:template>
    
    <!-- This hides the bibliographical information -->
    <xsl:template match="tei:bibl"/>
    
    <!-- This hides the note -->
    <xsl:template match="tei:note"/>
    
    <!--This provides a list of relations (if available) -->
    <xsl:template match="tei:listRelation[@type='personal']">
        <p>List of Relations: </p>
        <xsl:apply-templates/>        
    </xsl:template>
    
    <xsl:template match="tei:relation[@name='child']">
        <xsl:apply-templates/>
        <xsl:text> (child) </xsl:text>
        <br/>
    </xsl:template>


</xsl:stylesheet>