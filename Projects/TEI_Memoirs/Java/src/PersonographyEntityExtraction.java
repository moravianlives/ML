import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.PrintWriter;
import java.util.Scanner;
import java.util.regex.Pattern;

public class PersonographyEntityExtraction {


    /**
     * The path to the file we are looking at
     */
    private final String filePath = ".././personography/XML/ML_personography.xml";

    private final String exportPath = "../Tag_Data";
    /**
     * The export writer
     */
    private final PrintWriter writer;

    /**
     * The tags we are looking for
     */
    private final String OFFICE = "office";
    private final String OCCUPATION = "occupation";
    private final StringBuffer append;

    /**
     * Patterns we are searching for
     */
    //private Pattern pOccupation = Pattern.compile("<occupation>+([^\"]*)+</occupation>");
    private Pattern pOccupation = Pattern.compile("<occupation(.*)</occupation>");

    //private Pattern pOffice = Pattern.compile("<office>+([^\"]*)+</office>");
    private Pattern pOffice = Pattern.compile("<office(.*)</office>");
    private final StringBuffer csvHeader;
    private final StringBuffer csvData;

    public PersonographyEntityExtraction() throws FileNotFoundException {

        /**
         * Set up the basic stuff for the csv file
         */
        csvHeader = new StringBuffer();
        csvData = new StringBuffer();
        append = csvHeader.append("Attribute name,Attribute value, Memoir\n");
        writer = new PrintWriter(new File(exportPath));
        writer.write(csvHeader.toString());
        scanForOccupations();



    }

    /**
     * Scans through the file for <occupation=... ></occupation> and adds them to the csv
     * @throws FileNotFoundException
     */
    public void scanForOccupations() throws FileNotFoundException {
        FileReader reader = new FileReader(filePath);
        Scanner in = new Scanner(reader);
        String sMatch;
        while ((sMatch = in.findWithinHorizon(pOccupation, 0)) != null) {
            sMatch = cleanUpString(sMatch);
            System.out.println(sMatch);
//            csvData.append("Occupation");
//            csvData.append(",");
//            csvData.append();
//            csvData.append(",");
//            csvData.append("Ignore column for now");
//            csvData.append('\n');




//            csvData.append(elementName.get(i));
//            csvData.append(',');
//            csvData.append(attributeNames.get(i));
//            csvData.append(',');
//            csvData.append("Will fill in later");
//            csvData.append('\n');


        }

    }
    /**
     * Cleans the String so that it no longer contains new line characters or tabs
     * @param sMatch The string that needs to be modified
     * @return a String with all "\n" removed and all tabs removed
     */
    private String cleanUpString(String sMatch) {
        sMatch = sMatch.trim().replaceAll("[\\x09-\\x09]", "");
        sMatch = sMatch.trim().replaceAll("\n", " ");
        return sMatch;
    }

    public static void main(String[] args) throws FileNotFoundException {
        PersonographyEntityExtraction extraction = new PersonographyEntityExtraction();
    }
}
