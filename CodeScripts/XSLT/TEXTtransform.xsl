<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
    xmlns:tei="http://www.tei-c.org/ns/1.0" xmlns:xs="http://www.w3.org/2001/XMLSchema"
    exclude-result-prefixes="xs tei" 
    version="2.0">
    
    <xsl:output omit-xml-declaration="yes" method="text" encoding="UTF-8"/>
    <xsl:strip-space elements="*"/>
   
    
    <xsl:template match="/">
        <xsl:apply-templates/>
    </xsl:template>
    
    <!-- If you want to run all the files in a directory, you can add this 
    template and then run this transform on dirList.xml (in the forManyTexts folder). 
    Make sure to customize dirList.xml for your directory paths and names.
    
        <xsl:template match="list">
        <xsl:for-each select="item">
            <xsl:variable name="dir" select="@dir"/>
            <xsl:for-each select="collection(iri-to-uri(concat(@dir, '?select=*.xml')))">
                <xsl:variable name="outpath"
                    select="concat($dir, substring-before(tokenize(document-uri(.), '/')[last()], '.txt'))"/>
                <xsl:result-document href="{concat($outpath, '.txt')}">
                    <xsl:apply-templates select="tei:TEI"/>
                </xsl:result-document>
            </xsl:for-each>
        </xsl:for-each>
    </xsl:template> -->

    
    <xsl:template match="tei:teiHeader | tei:head"/>    
    <xsl:template match="*">
        <xsl:apply-templates/>
        <xsl:text></xsl:text>
    </xsl:template>
<!--
    <xsl:template match="tei:head"/>
    <xsl:template match="*">
        <xsl:apply-templates/>
    </xsl:template>
-->    
    <xsl:template match=
        "text()[not(string-length(normalize-space()))]"/>
    
    <xsl:template match=
        "text()[string-length(normalize-space()) > 0]">
        <xsl:value-of select="translate(.,'&#xA;&#xD;', '  ')"/>
    </xsl:template>



<!-- 'tokenize' removes punctuation; remove the tokenize function if you want punctuation in text
    <xsl:template match="text()">
        <xsl:value-of select="tokenize(., '\W')"/>
    </xsl:template> -->
   
</xsl:stylesheet>