import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

public class IndividualTagFrequencies extends TagFrequencies {

    /**
     * Holds the path of the file
     */

    private String path;

    /**
     * Holds what sex the memoir is
     */
    private String sex;


    /**
     * @param sex
     * @param fileName
     * @throws IOException
     */
    public IndividualTagFrequencies(String fileName, String sex) throws IOException {
        this.sex = sex;
        init();
        this.path = fileName;


    }


    @Override
    public String getOutputLocation() {
        return (".././../../Fulneck/Tag_Frequencies/CSV_frequencies/Individual" + sex.substring(0, 1).toUpperCase() + sex.substring(1) + "/" + path.substring(path.lastIndexOf("/") + 1) + ".csv");
    }


}

