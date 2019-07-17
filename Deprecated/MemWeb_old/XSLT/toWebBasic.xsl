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
                <link rel="stylesheet" type="text/css" href="../css/style2.css"/>
            </head>
            <body><xsl:apply-templates select="tei:text"/></body>
        </html>
    </xsl:template>
    
    <xsl:template match="tei:head">
        <h3><xsl:apply-templates/></h3>
    </xsl:template>
    
    <xsl:template match="tei:byline">
        <h4><xsl:apply-templates/></h4>
    </xsl:template>
    
    <xsl:template match="tei:lg">
        <p><xsl:apply-templates/></p>
    </xsl:template>
    
    <xsl:template match="tei:l">
        <xsl:apply-templates/><br />
    </xsl:template>
    
    <xsl:template match="tei:hi">
        <em><xsl:apply-templates/></em>
    </xsl:template>
    
</xsl:stylesheet>