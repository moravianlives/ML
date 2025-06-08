# Moravian Lives Encoding Documentation

The Fulneck memoirs have been encoded to explore the lived experiences of Moravians in terms of relationships among them, journeys taken, life events, etc.) and also their internal/emotional experiences as described. Much of this encoding has been experimental and exploratory, and represents  chanages in encoding approaches and completeness of encoding over time.

The transcription process involved the [Transkribus](https://www.transkribus.org/) platform. Transcribed memoirs (in English and German) were then exported as searchable PDFs and standard XML files (with minimal structural description in TEI-XML). From some 150 memoirs a selection were then semantically encoded, using TEI elements and attributes as follows:
* People: `<persName>`
* Places: `<placeName>`
* Religious groups: `<orgName>` with @type of religion (list in ODD file)
* Moravian community groups: `<affiliation>` with @type of choir or congregation (list in ODD file)
* Health: `<name>` with @type="health" and @subtype, @key for more specifics
* Emotion: `<name>` with @type="emotion" and @key for described emotional experience
* Occuption: `<name>` with @type="occupation" and @key for described occupation
* Office: `<name>` with @type="office" and @key for described Moravian office.

A TEI customized schema was developed (last updated in January 2025), which is published as an [ODD file](/blob/master/CodeScripts/MoravianMemoirs.odd) and [RNG file](/blob/master/CodeScripts/out/MoravianMemoirs.rng) in the CodeScripts repository.

Most documents were tagged using the Oxygen XML editor. More recently the [LEAF-Writer](https://leaf-writer.leaf-vre.org/) semantic editor was used to capture named entities whwere authority files could be associated with information in the mmemoirs (such as Wikidata for places and the [LINCS occupation vocabulary](https://vocab.lincsproject.ca/Skosmos/occupation/en/)). These files have integrated RDF web annotations in the header (within the `<xenoData>` section), and are ready to be transformed as structured linkable data.

Early attempts to create a fully bilingual schema in English and German were not pursued (traces can be seen in the [MoravianMemoirs_deprecated.odd](https://github.com/moravianlives/ML/blob/master/Deprecated/MoravianMemoirs_deprecated.odd) file. Some attempts to create a personography of Moravians in Fulneck were [begun](/tree/master/Projects/Personography) but for the most part lives alongside the memoirs rather than integrated into them. Likewise, work from a comprehensive gazetteer of 'Moravian places' around the world has been continued throughout the life of hte project. Katherine Faull has access to a trove of geospatial data in the form of maps, databases, and spreadsheets.

*Last updated June 8, 2025 by Diane Jakacki*