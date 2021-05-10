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



    
<!-- This hides the sic spelling, leaving only the correct spelling -->
    <xsl:template match="tei:orig"/>        
    

<!-- This renders underlined letters in HTML <u> tag -->
    
 
 <!-- This provides spacing between persons -->
    <xsl:template match="tei:listPerson">
        <p>
            <xsl:apply-templates/>
            <hr/>
        </p>

    </xsl:template>
    
 <!-- This lays out the person name: Lastname, Firstname -->
    <xsl:template match="tei:person/tei:persName">
        <strong>
            <xsl:text>Person: </xsl:text>
            <xsl:value-of select="tei:surname"/>
            <xsl:text>, </xsl:text>
            <xsl:value-of select="tei:forename"/>
        </strong>
        <br/>
    </xsl:template>   

    
<!-- This provides the person birth date (Gregorian) -->
    <xsl:template match="tei:birth/tei:date[@calendar='Gregorian']">
            <em>
                <xsl:text>Birth date: </xsl:text>
            </em>
            <xsl:apply-templates/>        
        <br/>
    </xsl:template>

<!-- This hides the person birth date (Julian) -->
    <xsl:template match="tei:birth/tei:date[@calendar='Julian']"/>
    
<!-- This hides the person birth date (other source) -->
    <xsl:template match="tei:birth/tei:date[@resp='memoir']"/>
    
<!-- This provides the person birth place -->
    <xsl:template match="tei:birth/tei:placeName">
        <em><xsl:text>Birth place: </xsl:text></em>
        <xsl:apply-templates/>        
        <br/>
    </xsl:template>
    
    <!-- This provies the person death date -->
    <xsl:template match="tei:death/tei:date">
        <em><xsl:text>Death date: </xsl:text></em>
        <xsl:apply-templates/>        
        <br/>
    </xsl:template>
    
    <!-- This provides the person death place -->
    <xsl:template match="tei:death/tei:placeName">
        <em><xsl:text>Death place: </xsl:text></em>
        <xsl:apply-templates/>        
        <br/>
    </xsl:template>
    
<!-- This structures the list of events -->
    <xsl:template match="tei:listEvent">
        <strong>
        <xsl:text>Events in Moravian Life: </xsl:text>
        </strong>
        <ul>
            <xsl:apply-templates select="tei:event"/>
        </ul>
        
    </xsl:template>   
    
    <xsl:template match="tei:event">
        <xsl:value-of select="tei:event"/>
        <li>
        <xsl:apply-templates/>
        <xsl:text> (</xsl:text>
        <xsl:value-of select="@when-iso"/>
        <xsl:text>)</xsl:text>
        </li>
    </xsl:template>
    
    <!-- Hiding residences for now -->
    <xsl:template match="tei:residence"/>
    
    
    <!-- This provides a list of occupations (if available) -->
    <xsl:template match="tei:occupation">
        <em><xsl:text>Occupation(s): </xsl:text></em>
        <xsl:apply-templates/>
        <br/>
    </xsl:template>
    
    <!-- This provides a list of offices (if available) -->
    
    <xsl:template match="tei:office">
        <em><xsl:text>Office(s): </xsl:text></em>
        <xsl:apply-templates/>
        <xsl:text>, </xsl:text>
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
        <strong>
            <xsl:text>Family Members: </xsl:text>
</strong>
        <ul>
            <xsl:apply-templates select="tei:relation"/>
        </ul>
    </xsl:template>
    
    <xsl:template match="tei:listRelation[@type='personal']/tei:relation">
        <li><xsl:apply-templates/>
            <xsl:value-of select="@passive"/>
            <xsl:value-of select="@mutual"/>
        <xsl:text> (</xsl:text>
        <xsl:value-of select="@name"/>
        <xsl:text>)</xsl:text>
        </li>
    </xsl:template>
    
<!--    This provides a list of Moravian relations (if available) -->
    <xsl:template match="tei:listRelation[@type='Moravian']">
        <strong>
            <xsl:text>Moravian relations:</xsl:text>
        </strong>
            <ul>
                <xsl:apply-templates select="tei:relation"/>
            </ul>
    </xsl:template>
    
    <xsl:template match="tei:listRelation[@type='Moravian']/tei:relation">
        <li><xsl:apply-templates/>
            <xsl:value-of select="@passive"/>
            <xsl:value-of select="@mutual"/>
            <xsl:text> (</xsl:text>
            <xsl:value-of select="@name"/>
            <xsl:text>)</xsl:text>
        </li>
    </xsl:template>
    
    
</xsl:stylesheet>