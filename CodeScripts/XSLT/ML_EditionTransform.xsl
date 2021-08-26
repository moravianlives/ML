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
    
    <!-- This hides "title" of the memoir  -->
    <xsl:template match="tei:div/tei:head">
        <h2>
            <xsl:apply-templates/>
        </h2>
    </xsl:template>
    
<!--    <xsl:template match="tei:div/tei:pb">
        <em>
            <xsl:text>Page: </xsl:text>
            <xsl:apply-templates/>
        </em>
    </xsl:template>-->
    
    <!-- This breaks the text into paragraphs as marked up in text (not the same as pages) -->
    <xsl:template match="tei:p">
        <p><xsl:apply-templates/></p>
    </xsl:template>
    
    <!-- This breaks the lines by lb -->
    <xsl:template match="tei:lb">
        <br/><xsl:apply-templates/>
    </xsl:template>
    
    <!-- This hides the sic spelling, leaving only the correct spelling -->
    <xsl:template match="tei:orig"/>        
    
    <!-- This renders the superscript letters in HTML <sup> tag -->
    <xsl:template match="tei:hi[@rend='superscript'] | tei:sup">
        <sup>
            <xsl:value-of select="."/>
        </sup>        
    </xsl:template>
    
    <!-- This renders underlined letters in HTML <u> tag 
    <xsl:template match="tei:hi[@rend='underline']">
        <underline>
            <xsl:value-of select="."/>
        </underline>
    </xsl:template>-->
    
    <!-- This renders letters above the line in HTML <sup> tag -->
    <xsl:template match="tei:add[@place='above'] | tei:sup">
        <sup>
            <xsl:value-of select="."/>
        </sup>        
    </xsl:template>
    
    
    <!-- This renders strikethrough in HTML -->
    <xsl:template match="tei:del">
        <del>
            <xsl:value-of select="."/>
        </del>
    </xsl:template>
    
    <!-- This hides the catchword form of note -->
    <xsl:template match="tei:note[@type='catchword']" />
    
    <!-- This adds /// at each page break -->
    <xsl:template match="tei:div[@type='page']">
        <xsl:apply-templates/>
            <xsl:text>PAGE BREAK</xsl:text>
    </xsl:template>
    
    <!-- This renders all  placeName in italics 
    <xsl:template match="tei:placeName">
        <em>
            <xsl:apply-templates/>
        </em>
    </xsl:template>-->
    
    <!-- This renders all dates in italics -->
    <xsl:template match="tei:date">
        <em>
            <xsl:apply-templates/>
        </em>
    </xsl:template>
    
<!--    <xsl:template match="tei:name[@office]">
        <em>
            <xsl:apply-templates/>
        </em>
    </xsl:template>-->

<!-- This creates a link pointer to the personography identifier -->
    <xsl:template match="tei:persName | tei:placeName">
        <a href="{@ref}.html">
            <xsl:apply-templates/>        
        </a>
    </xsl:template>
    
</xsl:stylesheet>