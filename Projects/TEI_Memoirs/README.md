# TEI Tagging Protocols and Decisions

## TEI Header
* The TEI header will be maintained on a twice-yearly basis, at which point team member participation terms will be updated and any other documentary information will be added/deleted/edited.
* The most up-to-date TEI header can be found in the GitHub repo TEI Memoirs directory, here: https://github.com/djakacki/moravianlives/blob/master/TEI_Memoirs/teiHeader_uptodate.xml 
## Capturing transcription
* Spellings are maintained from the original
* Punctuation is maintained from the original
* Words that are contracted with an apostrophe are maintained as is. Where a contraction includes a superscripted letter, that/those letters are captured with `<hi rend="superscript"></hi>`
* Words that are inserted above a line are marked as additional with `<add place="above">the</add>` or the like
* Words that have been deleted are captured with `<del></del>`; if the deleted word is legible, that word is indicated between del tags. If illegible, the word is captured with <del>xx</del>
* And signs are captured with &amp;
* Words that span two lines via line break are reunited, so that af- ter will be captured as after (in order to assist with future text analysis
* Catchwords are maintained and tagged `<note type="catchword">he</note>` (or the like) at the bottom of the page
* 
