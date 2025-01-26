<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:tei="http://www.tei-c.org/ns/1.0">
    <xsl:output method="text" encoding="UTF-8"/>
    
    <xsl:template match="/">
        <xsl:text>Subject,Type,Value,Attribute&#xA;</xsl:text>
        <xsl:apply-templates select="//tei:div[@type='memoir']"/>
    </xsl:template>
    
    <xsl:template match="tei:div[@type='memoir']">
        <xsl:variable name="subject" select="normalize-space(tei:head)"/>
        <xsl:apply-templates select=".//tei:persName[@ref]|.//tei:placeName[@ref]|.//tei:name[@event]|.//tei:orgName[@type]|.//tei:name[@emotion]">
            <xsl:with-param name="subject" select="$subject"/>
        </xsl:apply-templates>
    </xsl:template>
    
    <xsl:template match="tei:persName[@ref]|tei:placeName[@ref]|tei:name[@event]|tei:orgName[@type]|tei:name[@emotion]">
        <xsl:param name="subject"/>
        <xsl:value-of select="$subject"/>
        <xsl:text>,</xsl:text>
        <xsl:value-of select="local-name()"/>
        <xsl:text>,"</xsl:text>
        <xsl:value-of select="normalize-space(.)"/>
        <xsl:text>",</xsl:text>
        <xsl:value-of select="@*[local-name()='ref' or local-name()='type']"/>
        <xsl:text>&#xA;</xsl:text>
    </xsl:template>
</xsl:stylesheet>