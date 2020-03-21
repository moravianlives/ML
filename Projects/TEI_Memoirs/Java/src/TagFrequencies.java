import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Scanner;
import java.util.regex.Pattern;

public abstract class TagFrequencies {


    /**
     * The element name that we are capturing
     */
    private final String ELEMENT_NAME = "name";
    /**
     * The two attribute names that we are capturing
     */
    private final String OCCUPATION = "occupation";
    private final String OFFICE = "office";

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
    protected ArrayList<String> attributeValues;

    /**
     * List to hold the frequencies
     */
    protected ArrayList<Integer> frequencies;

    /**
     * Holds the memoir name
     */
    protected ArrayList<String> memoirNames;

    /**
     * Stores the pattern we are working with for occupation tags
     */
    protected Pattern pOccupation;
    /**
     * Stores the pattern we are working with for office tags
     */
    protected Pattern pOffice;

    /**
     * Initializes the class variables and constants necessary for the code
     */
    public void init() {
        //this.pOccupation = Pattern.compile("name[^>]+occupation=\"([^\"]*)\"[^>]*>([^<]*)</name>");
        //this.pOffice = Pattern.compile("name[^>]+office=\"([^\"]*)\"[^>]*>([^<]*)</name>");
        //this.pOccupation = Pattern.compile("<name[^>]+occupation=(\"[^\"]*\")+(.*?)");
        //this.pOffice = Pattern.compile("<name[^>]+office=(\"[^\"]*\")+(.*?)");

        this.pOccupation=Pattern.compile("<occupation>+([^\"]*)+</occupation>");
        this.pOffice=Pattern.compile("<office>+([^\"]*)+</office>");


        this.attributeNames = new ArrayList<>();
        this.elementName = new ArrayList<>();
        this.attributeValues = new ArrayList<>();
        this.frequencies = new ArrayList<>();
        this.memoirNames = new ArrayList<>();

    }



    /**
     * Scans through the file for <name office=... ></name> and a adds them to the frequency arrays
     * @param file the file that we want to scan through
     * @throws FileNotFoundException
     */
    public void scanForOffice(String file) throws FileNotFoundException {
        FileReader reader = new FileReader(file);
        Scanner in = new Scanner(reader);
        //Scanner in2 = new Scanner(cleanWhiteSpaces(in));

        String sMatch;
        while ((sMatch = in.findWithinHorizon(pOffice, 0)) != null) {
            sMatch = cleanUpString(sMatch);

            //NEED TO FIX TO REMOVE THINGS AFTER THE ATTRIBUTE VALUE

            String eName = sMatch.substring(1,ELEMENT_NAME.length()+1);
            sMatch = sMatch.substring(ELEMENT_NAME.length()+1);
            String aName = sMatch.substring(0, OFFICE.length()+1);
            sMatch = sMatch.substring(OFFICE.length()+3);
            String aValue = sMatch.substring(0,sMatch.length()-1);
            //String aName = sMatch.substring();
            //String eName = sMatch.substring(0,ELEMENT_NAME.length());
            //String aName = sMatch.substring(ELEMENT_NAME.length() + 1, ELEMENT_NAME.length() + OFFICE.length() + 1);
            //String aValue = sMatch.substring(3 + ELEMENT_NAME.length() + OFFICE.length(), sMatch.indexOf(">") - 1);

            addTag(eName, aName, aValue, file);


        }

    }

    /**
     * Add frequencies to the arrayLists if it does not already exist and otherwise adds the to the count in the arrayList
     *
     * @param eName  the element name of the tag
     * @param aName  the attribute name of the tag
     * @param aValue the attribute value of the tag
     */
    private void addTag(String eName, String aName, String aValue, String file) {

        attributeValues.add(aValue);
        attributeNames.add(aName);
        elementName.add(eName);
        memoirNames.add(file);

        /*if (attributeValues.contains(aValue)) {
            frequencies.set(attributeValues.indexOf(aValue), frequencies.get(attributeValues.indexOf(aValue)) + 1);

        } else {
            attributeValues.add(aValue);
            attributeNames.add(aName);
            elementName.add(eName);
            frequencies.add(1);
        }*/
    }

    /**
     * Scans through the file for <name occupation=... ></name> and a adds them to the frequency arrays
     * @param file the file that we want to scan through
     * @throws FileNotFoundException
     */
    public void scanForOccupations(String file) throws FileNotFoundException {
        FileReader reader = new FileReader(file);
        Scanner in = new Scanner(reader);
        //Scanner in2 = new Scanner(cleanWhiteSpaces(in));
        String sMatch;
        while ((sMatch = in.findWithinHorizon(pOccupation, 0)) != null) {
            sMatch = cleanUpString(sMatch);


            String eName = sMatch.substring(1,ELEMENT_NAME.length()+1);
            sMatch = sMatch.substring(ELEMENT_NAME.length()+1);
            String aName = sMatch.substring(0, OCCUPATION.length()+1);
            sMatch = sMatch.substring(OCCUPATION.length()+3);
            String aValue = sMatch.substring(0,sMatch.length()-1);
            /*String eName = sMatch.substring(0, ELEMENT_NAME.length());
            String aName = sMatch.substring(ELEMENT_NAME.length() + 1, ELEMENT_NAME.length() + OCCUPATION.length() + 1);
            String aValue = sMatch.substring(3 + ELEMENT_NAME.length() + OCCUPATION.length(), sMatch.indexOf(">") - 1);*/

            addTag(eName, aName, aValue, file);


        }

    }

    /**
     * displays the output to a csv file at the specified location
     * @throws FileNotFoundException
     */
    public void displayOutput() throws FileNotFoundException {
        PrintWriter writer = new PrintWriter(new File(getOutputLocation()));

        StringBuffer csvHeader = new StringBuffer();
        StringBuffer csvData = new StringBuffer();
        csvHeader.append("Element name,Attribute name,Attribute value, Frequency\n");

        // write header
        writer.write(csvHeader.toString());

        // write data
        for (int i = 0; i < elementName.size(); i++) {
            csvData.append(elementName.get(i));
            csvData.append(',');
            csvData.append(attributeNames.get(i));
            csvData.append(',');
            csvData.append(attributeValues.get(i));
            csvData.append(',');
            //csvData.append(frequencies.get(i));
            csvData.append(memoirNames.get(i));
            csvData.append('\n');
        }

        csvData.append('\n');
        csvData.append('\n');
        csvData.append('\n');
        csvData.append(',');
        csvData.append(',');
        csvData.append(',');
        csvData.append(',');
        //csvData.append("Total");
        //csvData.append(',');
        //csvData.append(getSum(frequencies));
        writer.write(csvData.toString());
        writer.close();
    }

    private int getSum(ArrayList<Integer> frequencies) {
        int sum=0;
        for (Integer num:frequencies) {
            sum+=num;
        }
        return sum;
    }

    /**
     * displays the output to a csv file at the specified location
     * @throws FileNotFoundException
     */
    public void displayOutput(String filePath) throws FileNotFoundException {


        PrintWriter writer = new PrintWriter(new File(filePath));

        StringBuffer csvHeader = new StringBuffer();
        StringBuffer csvData = new StringBuffer();
        csvHeader.append("Element name,Attribute name,Attribute value, Frequency\n");

        // write header
        writer.write(csvHeader.toString());

        // write data
        for (int i = 0; i < elementName.size(); i++) {
            csvData.append(elementName.get(i));
            csvData.append(',');
            csvData.append(attributeNames.get(i));
            csvData.append(',');
            csvData.append(attributeValues.get(i));
            csvData.append(',');
            //csvData.append(frequencies.get(i));
            csvData.append(memoirNames.get(i));
            csvData.append('\n');
        }
        csvData.append('\n');
        csvData.append('\n');
        csvData.append('\n');
        csvData.append(',');
        csvData.append(',');
        csvData.append(',');
        csvData.append(',');
        //csvData.append("Total");
        //csvData.append(',');
        //csvData.append(getSum(frequencies));

        writer.write(csvData.toString());
        writer.close();
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
     * Gets the output path where the user wants to output the csv files
     *
     * @return a String containing the path
     */
    public abstract String getOutputLocation();
}
