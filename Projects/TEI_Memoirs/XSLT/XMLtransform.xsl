<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:tei="http://www.tei-c.org/ns/1.0"
    exclude-result-prefixes="xs tei"
    version="2.0">
    
    <xsl:output method="xml" encoding="utf-8" indent="yes"/>
    <xsl:strip-space elements="*"/>
    
    <xsl:template match="/">
        <xsl:apply-templates/>
    </xsl:template>
    
    <xsl:template match="tei:sic"/>
    
    <!-- you can add this template to run many documents at once: add this code in
        by un-commenting it, and then run this xslt to transform list.xml (in the
        forManyTexts folder). Make sure to customize list.xml so that it lists your
        document names, and make sure also that the list.xml file is in the
        same folder as your documents.
        
        <xsl:template match="list">
        <xsl:for-each select="item">
            <xsl:apply-templates select="document(@code)/tei:TEI">
                <xsl:with-param name="xpathFilename" select="@code"/>
            </xsl:apply-templates>
        </xsl:for-each>
    </xsl:template> -->
    
    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>
    
</xsl:stylesheet>