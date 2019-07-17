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
    <xsl:template match="tei:sic"/>        
    
<!-- This renders the superscript letters in HTML <sup> tag -->
    <xsl:template match="tei:hi[@rend='superscript'] | tei:sup">
        <sup xmlns="http://www.w3.org/1999/xhtml">
            <xsl:value-of select="."/>
        </sup>        
    </xsl:template>

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
</xsl:stylesheet>