import java.io.File;
import java.io.FileNotFoundException;

public class test {

    public static void main(String[] args) throws FileNotFoundException {
        File folder = new File("/Users/justinschaumberger/Documents/GitHub/ML/Projects/TEI_Memoirs/personography/XML/ML_personography.xml");

        CumulativeTagFrequencies everyone = new CumulativeTagFrequencies();
        everyone.scanForOccupations(folder.toString());
        everyone.scanForOffice(folder.toString());

        everyone.displayOutput("/Users/justinschaumberger/Documents/GitHub/ML/Projects/TEI_Memoirs/personography/XML/ontology.csv");
    }
}

