import java.io.*;
import java.util.ArrayList;
import java.util.Scanner;
import java.util.regex.Pattern;

public class CumulativeFrequencies {

    /**
     * These are used for substringing later on (the lengths)
     */
    private final String ELEMENT_NAME = "name";
    private final String ATTRIBUTE_NAME1 = "occupation";
    private final String ATTRIBUTE_NAME2 = "office";

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
     * Stores the pattern we are working with for occupation tags
     */
    private Pattern pOccupation;
    /**
     * Stores the pattern we are working with for office tags
     */
    private Pattern pOffice;

    /**
     * Holds what sex the memoir is
     */
    private String sex;

    public CumulativeFrequencies(String sex) throws IOException {
        this.sex = sex;

        this.attributeNames = new ArrayList<>();
        this.elementName = new ArrayList<>();
        this.attributeValues = new ArrayList<>();
        this.frequencies = new ArrayList<>();
        // look for <name occupation=" and then take up until the next
        this.pOccupation = Pattern.compile("name[^>]+occupation=\"([^\"]*)\"[^>]*>([^<]*)</name>");
        this.pOffice = Pattern.compile("name[^>]+office=\"([^\"]*)\"[^>]*>([^<]*)</name>");


    }

    public void scanForOccupations(String file) {
        Scanner in = new Scanner(file);
        //Scanner in2 = new Scanner(cleanWhiteSpaces(in));

        String sMatch;
        while ((sMatch = in.findWithinHorizon(pOccupation, 0)) != null) {
            sMatch = cleanUpString(sMatch);


            String eName = sMatch.substring(0,ELEMENT_NAME.length());
            String aName = sMatch.substring(ELEMENT_NAME.length()+1,ELEMENT_NAME.length()+ATTRIBUTE_NAME1.length()+1);
            String aValue = sMatch.substring(3+ELEMENT_NAME.length()+ATTRIBUTE_NAME1.length(),sMatch.indexOf(">")-1);

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

    }

    public void scanForOffice(String file) {
        Scanner in = new Scanner(file);
        //Scanner in2 = new Scanner(cleanWhiteSpaces(in));

        String sMatch;
        while ((sMatch = in.findWithinHorizon(pOffice, 0)) != null) {
            sMatch = cleanUpString(sMatch);



            String eName = sMatch.substring(0,ELEMENT_NAME.length());
            String aName = sMatch.substring(ELEMENT_NAME.length()+1,ELEMENT_NAME.length()+ATTRIBUTE_NAME2.length()+1);
            String aValue = sMatch.substring(3+ELEMENT_NAME.length()+ATTRIBUTE_NAME2.length(),sMatch.indexOf(">")-1);

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

    }

    public void displayOutput() throws FileNotFoundException {


        PrintWriter writer = new PrintWriter(new File(".././../../Fulneck/Tag_Frequencies/CSV_frequencies/Collective/" + sex)+"/"+sex+".csv");

        StringBuffer csvHeader = new StringBuffer("");
        StringBuffer csvData = new StringBuffer("");
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
            csvData.append(frequencies.get(i));
            csvData.append('\n');
        }
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


    public static void main(String[] args) throws IOException {
        ArrayList<File> folders = new ArrayList<>();

        File folder = new File(".././../../Fulneck/Tag_Frequencies/Memoirs/Men");
        File folder2 = new File(".././../../Fulneck/Tag_Frequencies/Memoirs/Women");
        folders.add(folder);
        folders.add(folder2);

        for (File f : folders) {
            File[] listOfFiles = f.listFiles();
            CumulativeFrequencies freq = new CumulativeFrequencies(f.toString().substring(f.toString().lastIndexOf("/") + 1));
            for (File file : listOfFiles) {
                if (file.isFile()) {

                    if (file.toString().substring(file.toString().lastIndexOf(".")).equals(".xml")) {
                        freq.scanForOccupations(file.toString());
                        freq.scanForOffice(file.toString());
                        freq.displayOutput();

                    }
                }
            }
        }
    }

}