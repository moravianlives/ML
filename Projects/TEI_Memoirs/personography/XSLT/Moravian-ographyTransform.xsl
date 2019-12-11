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
        <html xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                <xsl:comment>This document is generated from a TEI Master--do not edit!</xsl:comment>
                <title><xsl:value-of select="tei:teiHeader/tei:fileDesc/tei:titleStmt/tei:title"/></title>
                <link rel="stylesheet" type="text/css" href="../CSS/MemStyle.css"/>
            </head>
            <body><p><xsl:apply-templates select="tei:text"/></p></body>
        </html>
    </xsl:template>

<!-- This hides "title" of the memoir -->
    <xsl:template match="tei:head" />

<!-- This adds ML unique identifier link and paragraph after person in list -->
    <xsl:template match="tei:person">
        <xsl:variable name="uri" select="concat('http://moravianlives.bucknell.edu/data/personography.html#', @xml:id)"/>
        <hr/>
        <p>
            <a>
                <xsl:attribute name="href">
                    <xsl:value-of select="$uri"/>
                </xsl:attribute>
                <xsl:value-of select="$uri"/>
            </a>
        </p>       
            <xsl:apply-templates/>        
    </xsl:template>

<!-- This shows person's first name and last name (need to work more on multiple first and last names) -->
    <xsl:template match="tei:persName">
        <b>
<xsl:text>Name: </xsl:text>
        </b>
                <xsl:apply-templates select="tei:forename"/>
        <xsl:text> </xsl:text>
                <xsl:apply-templates select="tei:surname"/>
        <br />
    </xsl:template>

<!-- This shows birth and death dates and places (need to work on old/new dating styles) -->
<xsl:template match="tei:birth/tei:date">
    <b>
        <xsl:text> Date of birth: </xsl:text>
    </b>
        <xsl:value-of select="."/> 
    <br />   
</xsl:template>
    
    <xsl:template match="tei:birth/tei:placeName">
        <b>
        <xsl:text> Place of birth: </xsl:text>
        </b>
        <xsl:value-of select="."/>
        <br/>
    </xsl:template>
    
    <xsl:template match="tei:death/tei:date">
       <b> 
           <xsl:text> Date of Death: </xsl:text>
       </b>
        <xsl:value-of select="."/>        
        <br/>
    </xsl:template>
    
    <xsl:template match="tei:death/tei:placeName">
        <b>
            <xsl:text> Place of Death: </xsl:text>
        </b>
        <xsl:value-of select="."/>
        <br/>
    </xsl:template>

<!-- This lists events included in the peronsography, and includes the type of event where provided -->
    <xsl:template match="tei:event">    
        <br />
        <b>
        <xsl:text>Event (</xsl:text>
        <xsl:value-of select="@type"/>
        <xsl:text>): </xsl:text>
        </b>
        <xsl:apply-templates/>    
    </xsl:template>

<!-- This shows the occupation(s) of the person; this pattern could also be used for other defining information (affiliation, residence, etc.) -->
<xsl:template match="tei:occupation">
    <b>
        <xsl:text>Occupation: </xsl:text>
    </b>
    <xsl:value-of select="."/>
    <br />
</xsl:template>

    <xsl:template match="tei:office">
        <b>
            <xsl:text>Office: </xsl:text>
        </b>
        <xsl:value-of select="."/>
        <br />
    </xsl:template>
    
<!-- This shows the memoir reference - for now just includes the ID from Fulneck -->
    <xsl:template match="tei:bibl">
        <br />
        <b>
            <xsl:text>Memoir Reference: </xsl:text>
        </b>
        <xsl:value-of select="."/>
        <br/>
    </xsl:template>

<!-- This hides information about person's relations -->
<xsl:template match="tei:listRelation / tei:relation"/>

<!-- This hides unstructured notes -->
    <xsl:template match="tei:note"/>

</xsl:stylesheet>