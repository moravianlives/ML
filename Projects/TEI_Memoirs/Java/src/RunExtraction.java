import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

public class RunExtraction {

    public static void main(String[] args) throws IOException {
        ArrayList<File> folders = new ArrayList<>();

        File folder = new File(".././../../Fulneck/Tag_Frequencies/Memoirs/Men");
        File folder2 = new File(".././../../Fulneck/Tag_Frequencies/Memoirs/Women");
        folders.add(folder);
        folders.add(folder2);
        CumulativeTagFrequencies everyone = new CumulativeTagFrequencies();
        for (File f : folders) {
            File[] listOfFiles = f.listFiles();
            CumulativeTagFrequencies cumulative = new CumulativeTagFrequencies(f.toString().substring(f.toString().lastIndexOf("/") + 1));

            for (File file : listOfFiles) {
                if (file.isFile()) {

                    if (file.toString().substring(file.toString().lastIndexOf(".")).equals(".xml")) {
                        IndividualTagFrequencies individual = new IndividualTagFrequencies(file.toString(), f.toString().substring(f.toString().lastIndexOf("/")));
                        individual.scanForOccupations(file.toString());
                        individual.scanForOffice(file.toString());
                        individual.displayOutput();
                        cumulative.scanForOccupations(file.toString());
                        cumulative.scanForOffice(file.toString());
                        cumulative.displayOutput();
                        everyone.scanForOccupations(file.toString());
                        everyone.scanForOffice(file.toString());

                    }

                }
            }
        }
        everyone.displayOutput(".././../../Fulneck/Tag_Frequencies/CSV_frequencies/Collective/Cumulative/Everyone.csv");
    }
}

