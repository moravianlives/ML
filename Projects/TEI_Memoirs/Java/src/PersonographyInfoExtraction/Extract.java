package PersonographyInfoExtraction;

import java.io.*;

import java.util.Scanner;
import java.util.regex.Pattern;
import java.util.regex.Matcher;

/**
 * Name: Justin Schaumberger
 * File: extract.java
 * Date: 5/21/20
 */
public class Extract {
    /**
     * Strings that we will need for extractions
     */
    private static final String OCCUPATIONS = "occupation";
    private static final String OFFICE = "office";
    private static final String NAME = "name";
    private static final String EVENT = "event";
    private static final String DATE = "date";
    private static final String EMOTION = "emotion";
    /**
     * Pattern used to find all instances of <name ...></name>
     */
    private static final Pattern namePattern = Pattern.compile("<name(.*)</name>");
    private static final Pattern contentsPattern = Pattern.compile("(?<=>).*?(?=<)");

    private static CsvOutput outputWriter;

    private final static String outputPath = "memoirExtraction.csv";




    public static void main(String[] args) throws IOException {

        outputWriter = new CsvOutput(outputPath,"Memoir, Tag Name, Attribute Name, Attribute Value, Tag Contents");
        File folder = new File(".././../../Fulneck/SemanticXML");


        /**
         * Loop through all the files in the folder
         */
        for (File file : folder.listFiles()) {
            /**
             * Make sure the file is a file and not a folder
             */
            if (file.isFile()) {
                /**
                 * Make sure the file is an XML file
                 */
                if (getFileExtension(file.toString()).equals("xml")) {
                    extractNameData(file.toString(),getMemoirName(file.toString()));

                }
            }
        }
        outputWriter.finish();
    }

    private static void extractNameData(String filePath, String memoirName) throws FileNotFoundException {
        FileReader reader = new FileReader(filePath);
        Scanner in = new Scanner(reader);
        String sMatch;

        while ((sMatch = in.findWithinHorizon(namePattern, 0)) != null) {
            /**
             * Skip if it is not about the subject of the memoir
             */
            if (sMatch.contains("memoirSubject") || sMatch.contains("name type=")) {
                continue;
            }

            Pattern attributePattern;
            String attributeName;

            sMatch = cleanUpString(sMatch);


            if (sMatch.contains(OFFICE)) {
                attributeName=OFFICE;
                 attributePattern = Pattern.compile("(?<=office=\\\").*?(?=\\\">)");


            } else if (sMatch.contains(OCCUPATIONS)) {
                attributeName=OCCUPATIONS;
                 attributePattern = Pattern.compile("(?<=occupation=\\\").*?(?=\\\">)");

            } else if (sMatch.contains(EVENT)) {
                attributeName=EVENT;
                attributePattern = Pattern.compile("(?<=event=\\\").*?(?=\\\">)");

            }
            else if (sMatch.contains(EMOTION)) {
                attributeName=EMOTION;
                attributePattern = Pattern.compile("(?<=emotion=\\\").*?(?=\\\">)");

            }

            else {
                continue;
            }

            outputWriter.writeToCSV(memoirName + ",");
            outputWriter.writeToCSV(NAME + ",");
            outputWriter.writeToCSV(attributeName + ",");

            Matcher attributeMatcher = attributePattern.matcher(sMatch);
            attributeMatcher.find();
            String attributeValue = attributeMatcher.group();
            Matcher contentMatcher = contentsPattern.matcher(sMatch);
            contentMatcher.find();
            String content = contentMatcher.group();
            outputWriter.writeToCSV(attributeValue + ",");
            outputWriter.writeToCSV(content + ",");

            outputWriter.nextLine();

        }
    }




    /**
     * Gets the file extension of a given file (ie: xml, txt, etc.)
     * @param fullName the name of the file
     * @return the extension of the file
     * From Files utility in Guava
     */
    private static String getFileExtension(String fullName) {
        String fileName = new File(fullName).getName();
        int dotIndex = fileName.lastIndexOf('.');
        return (dotIndex == -1) ? "" : fileName.substring(dotIndex + 1);
    }

    /**
     * Gets the memoir name
     * @param fullName the full title of the memoir (including the path)
     * @return the name of the memoir
     */
    private static String getMemoirName(String fullName) {
       return (fullName.replace("."+getFileExtension(fullName),"")).substring(fullName.lastIndexOf("/")+1);
    }



    /**
     * Cleans the String so that it no longer contains new line characters or tabs
     * @param sMatch The string that needs to be modified
     * @return a String with all "\n" removed and all tabs removed
     */
    private static String cleanUpString(String sMatch) {
        sMatch = sMatch.trim().replaceAll("[\\x09-\\x09]", "");
        sMatch = sMatch.trim().replaceAll("\n", " ");
        return sMatch;
    }



}
