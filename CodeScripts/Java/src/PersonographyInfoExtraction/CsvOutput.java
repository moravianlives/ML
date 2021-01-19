package PersonographyInfoExtraction;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;

/**
 * Name: Justin Schaumberger
 * File: CsvOutput.java
 * Date: 5/21/20
 */
public class CsvOutput {

    private final StringBuffer csvHeader;
    private final StringBuffer csvData;
    private final StringBuffer append;
    /**
     * The export writer
     */
    private final PrintWriter writer;

    private final String exportPath;

    public CsvOutput(String exportPath, String header) throws FileNotFoundException {
        this.exportPath=exportPath;
        /**
         * Set up the basic stuff for the csv file
         */
        csvHeader = new StringBuffer();
        csvData = new StringBuffer();
        append = csvHeader.append(header+"\n");
        writer = new PrintWriter(new File(exportPath));
        writer.write(csvHeader.toString());
    }

    /**
     * Appends the input data to the csv
     * @param data the data to be appended
     */
    public void writeToCSV(String data) {
        csvData.append(data);
    }

    /**
     * Appends a new line character
     */
    public void nextLine() {
        csvData.append("\n");
    }

    public void finish() {
        writer.write(csvData.toString());
        writer.close();
    }
}
