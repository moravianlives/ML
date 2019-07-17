# README - Fulneck file transformation process

Follow these steps.

1) File exported from Transkribus is considered "State 1". It is raw and we keep it as is in an exports folder.

2) "State 2" adds Moravian Lives TEI header and includes structural markup (e.g., superscript, deletion, addition, etc.) It provides the basis for a web-view - keeping line breaks intact.

3) "State 3" is the version in which we do semantic encoding. We remove the line breaks so that we can tag people, places, dates, and semantic stuff (line breaks won't allow us to do that). We use this version for data extraction, but not for web-view unless Katie wants that.

Transkribus exported TEI files are in this directory:
../TranskribusExports/Transkribus_XML (State 1)

State 2 TEI files are in this directory:
../Fulneck/StandardXML 

State 3 TEI files are in two directories:
../Fulneck/SemanticXML/Ready4Tagging/#.xml
(these have been prepped for semantic encoding and ready for Justin)

State 3 completed files are moved up one directory so ...
../Fulneck/SemanticXML/#.xml


** We have not yet taken into account the workflow for files in the Transcription Desk. 
