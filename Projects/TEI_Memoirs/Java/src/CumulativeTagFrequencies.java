import java.io.IOException;


public class CumulativeTagFrequencies extends TagFrequencies {

    /**
     * Holds what sex the memoir is
     */
    private String sex;

    public CumulativeTagFrequencies(String sex) throws IOException {
        this.sex = sex;
        init();
    }



    @Override
    public String getOutputLocation() {
        return ".././../../Fulneck/Tag_Frequencies/CSV_frequencies/Collective/" + sex + "/" + sex + ".csv";
    }

}