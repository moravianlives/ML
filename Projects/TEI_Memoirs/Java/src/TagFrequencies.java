import java.io.*;
import java.util.ArrayList;
import java.util.Scanner;
import java.util.regex.Pattern;

public class TagFrequencies {

    /**
     * These are used for substringing later on (the lengths)
     */
    private final String ELEMENT_NAME="name";
    private final String ATTRIBUTE_NAME="occupation";

    /**
     * List to hold the elementNames
     */
    private ArrayList<String> elementName;

    /**
     * List to hold the attributeNames
     */
    private ArrayList<String> attributeNames;

    /**
     * List to hold the attributeValues
     */
    private ArrayList<String> attributeValues;

    /**
     * List to hold the frequencies
     */
    private ArrayList<Integer> frequencies;

    /**
     * Stores the number of tags found
     */
    private int count;
    /**
     * Stores the pattern we are working with
     */
    private Pattern p;

    /**
     *
     */
    private FileReader reader;

    /**
     *
     * Holds the path of the file
     */

    private String path;





    public TagFrequencies(String fileName) throws IOException {

        this.attributeNames = new ArrayList<>();
        this.elementName = new ArrayList<>();
        this.attributeValues = new ArrayList<>();
        this.frequencies = new ArrayList<>();
        // look for <name occupation=" and then take up until the next
        this.p = Pattern.compile("name[^>]+occupation=\"([^\"]*)\"[^>]*>([^<]*)</name>");


        // creates a fileReader object with the file
        this.reader = new FileReader(fileName);
        this.path=fileName;


    }






    /**
     * @return the location where to output the CSV files
     */
    public String getOutputLocation() {
        return ".././../../Fulneck/Tag_Frequencies/"+path.substring(path.lastIndexOf("/")+1)+".csv";
    }


    /**
     * @throws IOException Scans the code for tags matching the specified tag and prints the results to the console or a specified file
     */
    public void scanForTag() throws IOException {

        String sMatch;
        Scanner in = new Scanner(reader);
        //Scanner in2 = new Scanner(cleanWhiteSpaces(in));


        while ((sMatch = in.findWithinHorizon(p, 0)) != null) {
            sMatch = cleanUpString(sMatch);


            String eName = sMatch.substring(0,ELEMENT_NAME.length());
            String aName = sMatch.substring(ELEMENT_NAME.length()+1,ELEMENT_NAME.length()+ATTRIBUTE_NAME.length()+1);
            String aValue = sMatch.substring(3+ELEMENT_NAME.length()+ATTRIBUTE_NAME.length(),sMatch.indexOf(">")-1);

            // if the attribute already exists in the list
            if (attributeValues.contains(aValue)) {
                frequencies.set(attributeValues.indexOf(aValue), frequencies.get(attributeValues.indexOf(aValue)) + 1);

            } else {
                attributeValues.add(aValue);
                attributeNames.add(aName);
                elementName.add(eName);
                frequencies.add(1);
            }


        }
        displayOutput();


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


    /**
     * Prints the tags to the appropriate place and prints the tag frequencies if we are searching for all tags
     * @throws IOException
     *
     */
    //MAKE TO CSV OUTPUT
    //Make sure any string values are in quotes
    //Make sure dates are in standard ISO format
    public void displayOutput() throws IOException {
        String fileLocation = getOutputLocation();

        PrintWriter writer = new PrintWriter(new File(fileLocation));

        StringBuffer csvHeader = new StringBuffer("");
        StringBuffer csvData = new StringBuffer("");
        csvHeader.append("Element name,Attribute name,Attribute value, Frequency\n");

        // write header
        writer.write(csvHeader.toString());

        // write data
        for (int i = 0; i <elementName.size(); i++) {
            csvData.append(elementName.get(i));
            csvData.append(',');
            csvData.append(attributeNames.get(i));
            csvData.append(',');
            csvData.append(attributeValues.get(i));
            csvData.append(',');
            csvData.append(frequencies.get(i));
            csvData.append('\n');
        }
        writer.write(csvData.toString());
        writer.close();
    }




    /**
     * This code can loop through all the folders in the location (We would need this to be the general location if we put this on GitHub)
     */
    public void loopThroughFolders() {
        //Make sure its only xml files when running
        File folder = new File("/Users/justinschaumberger/Documents/GitHub/ML/Fulneck/SemanticXML");
        File[] listOfFiles = folder.listFiles();

        for (File file : listOfFiles) {
            if (file.isFile()) {
                System.out.println(file.getName());
            }
        }
    }


    public static void main(String[] args) throws IOException {

        File folder = new File(".././../../Fulneck/SemanticXML");

        File[] listOfFiles = folder.listFiles();


        for (File file : listOfFiles) {
            if (file.isFile()) {

                if (file.toString().substring(file.toString().lastIndexOf(".")).equals(".xml")) {
                    TagFrequencies test = new TagFrequencies(file.toString());
                    test.scanForTag();
                }

            }
        }

    }






}

