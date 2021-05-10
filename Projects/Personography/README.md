The Moravian Lives personography is a data resource designed to compile and document life-information about Moravian persons who have one or more of the following: 1) are the subject of a memoir; 2) are the subject of a Gehm...?; 3) are referred to in a Moravian Lives-adjacent document (e.g., the Yorkshire Families document).

A person in the Moravian Lives personography must be disambiguatable by a name (first and/or last) that can be tied explicitly and uniquely to a date (birth, death, floruit). 

Once disambiguation is confirmed (this should be determined by Moravian scholars and specialists), an entry is created.

Each person is given a unique identifier (xml:id) in sequential order.

A stub entry in the personography must include this information in this valid TEI-XML format:

<listPerson>
	<person xml:id="mlper000000" sex="">
		<persName>
			<forename>First name</forename>
			<surname type="?">Surname</surname> /*Types of surnames include @birth, @variant, @married */
		</persName>
		<birth>
			<date type="birth" when-iso="0000-00-00">Human readable date</date> /* Use the ISO dating system; date @calendar can be Gregorian or Julian */
			<placeName ref="mlpla000000">Human readable place name</placeName> /* the @ref refers to the ML unique identifier for place in the gazetteer (under construction) */
		</birth>
		