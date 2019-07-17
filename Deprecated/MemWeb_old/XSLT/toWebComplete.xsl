<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:tei="http://www.tei-c.org/ns/1.0" xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns="http://www.w3.org/1999/xhtml" exclude-result-prefixes="xs tei" version="2.0">

	<!-- Here is the document declaration necessary for an HTML (web) page -->

	<xsl:output doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN"
		doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-Strict.dtd"
		omit-xml-declaration="yes" indent="yes" encoding="UTF-8" method="xhtml"/>
	<xsl:strip-space elements="*"/>

	<!-- Make this a variable so that you can easily change what stylesheet is used for your documents. -->
	<xsl:variable name="stylesheet">../css/prose.css</xsl:variable>

	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	
	<!-- running multiple documents via a list -->

	<xsl:template match="list">
		<xsl:for-each select="item">
			<xsl:apply-templates select="document(@code)/tei:TEI">
				<xsl:with-param name="xpathFilename" select="@code"/>
			</xsl:apply-templates>
		</xsl:for-each>
	</xsl:template>

	<!--structuring the document-->

	<xsl:template match="tei:TEI">
		<xsl:param name="xpathFilename"/>
		<xsl:variable name="Filename">
			<xsl:value-of select="substring-before($xpathFilename, '.xml')"/>
		</xsl:variable>
		<xsl:result-document href="../HTML/{$Filename}.html">
			<html>
				<head>
					<link rel="stylesheet" type="text/css" href="{$stylesheet}"/>
					<xsl:apply-templates select="tei:teiHeader"/>
					<!-- the following javascript allows you to have collapsable lists -->
					<script type="text/javascript">
						function toggle(element) {
						if (element.style.display == 'none') {
						element.style.display = 'block';
						}
						else {
						element.style.display = 'none';
						}
						}
					</script>
				</head>
				<body>
					<xsl:apply-templates select="tei:text"/>
					<!-- We are placing all the notes at the end.  Template match="tei:note" will tell what to do with the notes 
						as they appear in the tei:text, but template match="note"MODE='END', as here, will tell what to do with 
						the notes as they appear at the end of the document. -->
					<xsl:apply-templates select="//tei:note" mode="end"/>
					<!-- We need all these returns so that, when a note number in the text is clicked on, the browser will take 
						you to that note AND PUT IT AT THE TOP OF THE SCREEN. Otherwise, it doesn't look as if the browswer really 
						took you to that note.  -->
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
					<br/>
				</body>
			</html>
		</xsl:result-document>
	</xsl:template>
	
	<!-- =======================================================
	 Putting the teiHeader info. into the html head element -->

	<xsl:template match="tei:teiHeader">
		<title>
			<xsl:value-of select="tei:fileDesc/tei:titleStmt/tei:title"/>
		</title>
		<style type="text/css">
			<xsl:text>@import url('</xsl:text><xsl:value-of select="$stylesheet"/><xsl:text>');
			</xsl:text>
			<xsl:apply-templates select="tei:encodingDesc/tei:tagsDecl/tei:rendition"/>
		</style>
	</xsl:template>

	<xsl:template match="tei:rendition">
		<xsl:if test="@scheme = 'css'">
			<xsl:value-of select="@xml:id"/>
			<xsl:text> {</xsl:text>
			<xsl:value-of select="."/>
			<xsl:text>}
		</xsl:text>
		</xsl:if>
	</xsl:template>
	
	<!-- =======================================================
	   front templates (plays only, but you can add things here for 
		other kinds of documents that have front matter)-->
	
	<xsl:template match="tei:front">
		<xsl:apply-templates/>
	</xsl:template>
	
	<xsl:template match="tei:castList">
		<div xmlns="http://www.w3.org/1999/xhtml" id="castList" style="display: none;">
			<h5 xmlns="http://www.w3.org/1999/xhtml"><xsl:value-of select="tei:head"/></h5>
		<ul xmlns="http://www.w3.org/1999/xhtml">
			<xsl:apply-templates select="tei:castItem | tei:castGroup"/>
		</ul>
		</div>
	</xsl:template>
	
	<xsl:template match="tei:castItem | tei:castGroup">
		<li xmlns="http://www.w3.org/1999/xhtml">
			<xsl:choose>
				<xsl:when test="tei:head">
					<xsl:value-of select="tei:head"/>
				</xsl:when>
				<xsl:when test="tei:role">
					<xsl:value-of select="tei:role"/>
				</xsl:when>
			</xsl:choose>
			<xsl:if test="tei:roleDesc">
				<xsl:text>: </xsl:text><xsl:value-of select="tei:roleDesc"/>
			</xsl:if>
		</li>
	</xsl:template>
	
	
	<!--===================================================
	         body templates used by all types of documents -->
	
	<xsl:template match="tei:text">
		<h2 xmlns="http://www.w3.org/1999/xhtml"><xsl:value-of select="ancestor-or-self::tei:TEI/tei:teiHeader/tei:fileDesc/tei:titleStmt/tei:title"/></h2>
		<h3 xmlns="http://www.w3.org/1999/xhtml"><xsl:value-of select="ancestor-or-self::tei:TEI/tei:teiHeader/tei:fileDesc/tei:titleStmt/tei:author"/></h3>
		<hr xmlns="http://www.w3.org/1999/xhtml" />
		<hr xmlns="http://www.w3.org/1999/xhtml" />
		<xsl:apply-templates select="tei:body"/>        
	</xsl:template>

	<xsl:template match="tei:head">
		<h4 xmlns="http://www.w3.org/1999/xhtml">
			<xsl:apply-templates/>
		</h4>
	</xsl:template>

	<xsl:template match="tei:byline">
		<h5 xmlns="http://www.w3.org/1999/xhtml">
			<xsl:apply-templates/>
		</h5>
	</xsl:template>

	<xsl:template match="tei:lg">
		<xsl:apply-templates/>
		<tr xmlns="http://www.w3.org/1999/xhtml">
			<td>
				<br/>
			</td>
		</tr>
	</xsl:template>

	<xsl:template match="tei:l">
		<tr xmlns="http://www.w3.org/1999/xhtml">
			<td>
				<xsl:attribute name="width">93%</xsl:attribute>
				<span>
					<xsl:attribute name="class">
						<xsl:value-of select="substring-after(@rendition, '#')"/>
					</xsl:attribute>
					<xsl:apply-templates/>
				</span>
			</td>
			<td>
				<xsl:attribute name="width">7%</xsl:attribute>
				<xsl:attribute name="align">right</xsl:attribute>
				<xsl:number from="tei:div" level="any"/>
			</td>
		</tr>
	</xsl:template>

	<xsl:template match="tei:p">
		<p xmlns="http://www.w3.org/1999/xhtml">
			<xsl:apply-templates/>
		</p>
	</xsl:template>
	
	<xsl:template match="tei:title | tei:hi[@rend='italic'] | tei:emph">
		<em xmlns="http://www.w3.org/1999/xhtml">
			<xsl:value-of select="."/>
		</em>
	</xsl:template>
	
	<xsl:template match="tei:anchor">
		<a name="{@xml:id}"/>
	</xsl:template>
	
	<xsl:template match="tei:ref">
		<a href="{@target}">
			<xsl:apply-templates/>
		</a>
	</xsl:template>
	
	<!-- =======================================================
	    body templates used by different types of documents -->	
	
	<xsl:template match="tei:stage">
		<xsl:choose>
			<xsl:when test="parent::tei:l">
				<br xmlns="http://www.w3.org/1999/xhtml"></br>
				<span xmlns="http://www.w3.org/1999/xhtml" class="stage"><xsl:apply-templates/></span>
			</xsl:when>
			<xsl:otherwise>
				<p xmlns="http://www.w3.org/1999/xhtml" class="stage"><xsl:apply-templates/></p>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template match="tei:sp">
		<p xmlns="http://www.w3.org/1999/xhtml"><span class="sp"><xsl:value-of select="tei:speaker"/></span></p>
		<table xmlns="http://www.w3.org/1999/xhtml" width="70%">
			<xsl:apply-templates select="tei:lg/tei:l | tei:l"/>
		</table>
	</xsl:template>
	
	<xsl:template match="tei:salute | tei:signed">
		<p>
			<xsl:if test="@rend">
			<xsl:attribute name="class"><xsl:value-of select="@rend"/></xsl:attribute>
			</xsl:if>
			<xsl:apply-templates/>
		</p>
	</xsl:template>
	
	
	<!-- =======================================================
	     head in different types of documents -->
	
	<xsl:template match="tei:div[@type='poeticPlay']/tei:div/tei:head">
		<h3 xmlns="http://www.w3.org/1999/xhtml">
			<a class="anchor">
				<xsl:attribute name="name"><xsl:value-of select="parent::tei:div/@xml:id"/></xsl:attribute>
				<xsl:value-of select="."/>
			</a>
		</h3>
	</xsl:template>
	
	<xsl:template match="tei:div[@type='poeticPlay']/tei:div/tei:div/tei:head">
		<h4 xmlns="http://www.w3.org/1999/xhtml">
			<a class="anchor">
				<xsl:attribute name="name"><xsl:value-of select="parent::tei:div/@xml:id"/></xsl:attribute>
				<xsl:value-of select="."/>
			</a>
		</h4>
	</xsl:template>
	
	<xsl:template match="tei:imprint">
		<xsl:text>, Vol. </xsl:text>
		<xsl:value-of select="tei:biblScope[@unit = 'volume']"/>
		<xsl:text> (</xsl:text>
		<xsl:value-of select="tei:date"/>
		<xsl:text>), </xsl:text>
		<xsl:text>pp. </xsl:text>
		<xsl:value-of select="tei:biblScope[@unit = 'page']"/>
	</xsl:template>
	
	<!-- =======================================================
	  Divs for different types of documents -->
	
	<xsl:template match="tei:div[@type = 'poem']">
		<xsl:apply-templates select="tei:head"/>
		<xsl:apply-templates select="tei:byline"/>
		<table>
			<xsl:apply-templates select="tei:lg"/>
		</table>
	</xsl:template>
	
	<xsl:template match="tei:div[@type='poeticPlay']">
		<xsl:variable name="castListID" select="ancestor-or-self::tei:TEI/tei:text/tei:front/tei:div[@type='castList']/@xml:id"/>
		<xsl:variable name="divID" select="tei:div/@xml:id"/>
		<h4 xmlns="http://www.w3.org/1999/xhtml">Table of Contents</h4>
		<ul xmlns="http://www.w3.org/1999/xhtml">
			<li><a href="#{$castListID}">Cast List</a></li>
			<xsl:apply-templates select="./tei:div" mode="toc"/>
		</ul>
		<h4 xmlns="http://www.w3.org/1999/xhtml" onclick="toggle(castList);" class="button2">
			<a class="anchor">
				<xsl:attribute name="name"><xsl:value-of select="ancestor-or-self::tei:TEI/tei:text/tei:front/tei:div/@xml:id"/></xsl:attribute>
			</a>Cast List</h4>
		<xsl:apply-templates select="ancestor-or-self::tei:TEI/tei:text/tei:front"/>
		<xsl:apply-templates/>
	</xsl:template>
	
	<xsl:template match="tei:div" mode="toc">
		<xsl:variable name="actID">
			<xsl:value-of select="@xml:id"/>
		</xsl:variable>
		<xsl:variable name="actNo" select="substring-after($actID, '-')"/>
		<li xmlns="http://www.w3.org/1999/xhtml">
			<a>
				<xsl:attribute name="href"><xsl:value-of select="concat('#', @xml:id)"/></xsl:attribute>
				<xsl:value-of select="tei:head"/>
			</a>
			<xsl:if test="tei:div"><br xmlns="http://www.w3.org/1999/xhtml" /><span onclick="toggle({$actNo});" class="button">List of Scenes</span></xsl:if>
		</li>
		<xsl:if test="tei:div">
			<li xmlns="http://www.w3.org/1999/xhtml">
				<div id="{$actNo}" style="display: none;">
					<ul><xsl:apply-templates select="./tei:div" mode="toc"/></ul>
				</div>
			</li>
		</xsl:if>
	</xsl:template>

	
	<!-- =======================================================
	   notes -->

	<xsl:template match="tei:note">
		<!-- Here is how you number notes automatically.  -->
		<xsl:variable name="noteNBR">
			<xsl:number select="." level="any"/>
		</xsl:variable>
		<!-- Now, in the body of the text, I want to create a link to an "a name" which will appear in the notes at the end -->
		<a>
			<xsl:attribute name="href">
				<xsl:text>#</xsl:text>
				<xsl:value-of select="$noteNBR"/>
			</xsl:attribute>
			<sup>
				<xsl:value-of select="$noteNBR"/>
			</sup>
		</a>
		<xsl:text> </xsl:text>
		<a>
			<xsl:attribute name="name">
				<xsl:text>N</xsl:text>
				<xsl:value-of select="$noteNBR"/>
			</xsl:attribute>
		</a>
		<!-- Notice that there is no apply-templates here at all: that is, I DON'T want the notes to appear in the body of the text, interrupting it, as it does in the TEI document itself-->
	</xsl:template>

	<!-- Using a "mode" to get the notes to appear at the end, after I have already said "do nothing" in the note template itself, is called the "pull" approach. Notice that the mode appears when I call the notes at the end in the TEI template itself.-->

	<xsl:template match="tei:note" mode="end">
		<!-- Here is how you number notes automatically.  -->
		<xsl:variable name="noteNBR">
			<xsl:number select="." level="any"/>
		</xsl:variable>
		<!--Here is the "a name" to which my links in the text refer -->
		<p><a name="{$noteNBR}"/><xsl:value-of select="$noteNBR"/>. <xsl:apply-templates/>
			<xsl:text> </xsl:text>
			<a>
				<xsl:attribute name="href"><xsl:text>#N</xsl:text><xsl:value-of select="$noteNBR"
					/></xsl:attribute>
				<xsl:text>Back</xsl:text>
			</a></p>
	</xsl:template>

</xsl:stylesheet>
