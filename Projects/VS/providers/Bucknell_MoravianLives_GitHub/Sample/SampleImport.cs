using edu.bucknell.project.moravianLives.model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zen.Pebble.FlexibleData.String.Localization;
using Zen.Pebble.FlexibleData.Common.Interface;
using Zen.Pebble.FlexibleData.String.Localization.Interface;
using Zen.Pebble.FlexibleData.String.Localization.Concrete;
using Zen.Pebble.FlexibleData.Historical;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Common;
using edu.bucknell.project.moravianLives.model.Common.Resolve;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.Sample
{
    [Priority(Level = -10)]
    public class SampleImport : IMoravianLivesDataOnboarding
    {
        #region Implementation of IMoravianLivesDataOnboarding

        public void Run()
        {
            // Some Sample Person Categories

            var spouseCat = Person.Category.Fetch("Spouse");

            var h = spouseCat.FetchChild("Husband");
            h.Name.SetVariant("Ehepartner", Constants.Cultures.German);
            h.Save();

            var w = spouseCat.FetchChild("Wife");
            w.Name.SetVariant("Ehepartnerin", Constants.Cultures.German);
            w.Save();

            var parentRes = Location.Where(i => i.Name.Value == "Germany").FirstOrDefault();
            {
                if (parentRes == null)
                {
                    parentRes = new Location { Name = "Germany" };
                    parentRes.Save();
                }
            }

            var dresdenModel = new LocationResolve().Resolve(new Location() { Name = "Dresden" });

            dresdenModel.Name.SetVariant("Dresden", Constants.Cultures.German, "Dresden ist die Landeshauptstadt des Freistaates Sachsen. Mit rund 563.000 Einwohnern Ende 2019 ist Dresden, nach Leipzig, die zweitgrößte sächsische Kommune und die zwölftgrößte Stadt Deutschlands.");
            dresdenModel.Name.SetVariant("Dresden", Constants.Cultures.English_UnitedStates, "Dresden is the capital city of the German state of Saxony and its second most populous city, following only Leipzig. It is contiguous with Freital, Pirna, Radebeul, Meissen and Coswig, and its urban area has around 780,000 inhabitants, making it the largest in Saxony.");

            dresdenModel.Name
                .SetVariant("Drežďany", Constants.Cultures.English_UnitedStates, "Etymological derivation of Old Sorbian", null, "1200-01-01")
                .SetVariant("Civitas Dresdene", Constants.Cultures.English_UnitedStates, "As mentioned by Margrave Dietrich of Meissen. He chose Dresden as his interim residence in 1206", null, "1206-01-01", "1697-01-01")
                .SetVariant("Royal-Polish Residential City of Dresden", Constants.Cultures.English_UnitedStates, "Renamed after Augustus II the Strong became King of Poland in 1697", null, "1697-01-01", "1763-01-01")
                .SetVariant("Dresden", Constants.Cultures.English_UnitedStates, "Dresden’s fortifications were later dismantled as a consequence of the Seven Years’ War", null, "1763-08-26")
                ;

            dresdenModel.Parent = parentRes.Id;

            dresdenModel.Save();

            var bohmenModel = new Location
            {
                Name = new HistoricString
                {
                    Scope = Constants.Cultures.German,
                    Value = "Böhmen",
                    Variants = new Dictionary<string, VariantList<TemporalCommented<string>>>
                    {
                        {
                            Constants.Cultures.German,
                            new VariantList<TemporalCommented<string>>
                            {
                                Variants = new List<TemporalCommented<string>>
                                {
                                    new TemporalCommented<string>
                                    {
                                        Value = "Böhmen",
                                        Comments = "Böhmen (tschechisch Čechy, lateinisch Bohemia) war eines der Länder der böhmischen Krone. Als ehemaliges Königreich Böhmen bildet es mit Mähren und dem tschechischen Teil Schlesiens das Staatsgebiet des heutigen Tschechiens, ist aber keine eigenständige administrative Einheit mehr."
                                    }
                                }
                            }
                        },
                        {
                            Constants.Cultures.English_UnitedStates,
                            new VariantList<TemporalCommented<string>>
                            {
                                Variants = new List<TemporalCommented<string>>
                                {
                                    new TemporalCommented<string>
                                    {
                                        Value = "Bohemia",
                                        Comments = "Bohemia (/boʊˈhiːmiə/ boh-HEE-mee-ə;[1] Czech: Čechy;[2] German: About this soundBöhmen (help·info)) is the westernmost and largest historical region of the Czech lands in the present-day Czech Republic."
                                    },
                                    new TemporalCommented<string>
                                    {
                                        Value = "Boiohaemum",
                                        Comments = "As mentioned by Tacitus' Germania 28 (written at the end of the 1st century AD)",
                                        Period = new HistoricPeriod
                                        {
                                            Start = new HistoricDateTime
                                            {
                                                Value = DateTime.Parse("0100-01-01"),
                                                Precision = HistoricDateTime.EDatePrecision.Century
                                            },
                                            End = new HistoricDateTime
                                            {
                                                Value = DateTime.Parse("0700-01-01"),
                                                Precision = HistoricDateTime.EDatePrecision.Century
                                            }
                                        }
                                    },
                                    new TemporalCommented<string>
                                    {
                                        Value = "Čechy",
                                        Comments = "Derived from the name of the Slavic ethnic group, the Czechs, who settled in the area during the 6th or 7th century AD.",
                                        Period = new HistoricPeriod
                                        {
                                            Start = new HistoricDateTime
                                            {
                                                Value = DateTime.Parse("0700-01-01"),
                                                Precision = HistoricDateTime.EDatePrecision.Century
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                Geography = new Geography()
                {
                    Boundaries = new GeoBoundary
                    {
                        new Historic<List<Zen.Pebble.Geo.Shared.GeoPolygon>>

                        {
                            Period = new HistoricPeriod { Start = "1200-01-01" },
                            Value = new List<Zen.Pebble.Geo.Shared.GeoPolygon>()
                        }

                    }
                }
            };

            var targetBohmenModel = new LocationResolve().Resolve(new Location() { Name = "Böhmen" });

            if (targetBohmenModel != null) bohmenModel.Id = targetBohmenModel.Id;

            bohmenModel.Save();


            #endregion
        }
    }
}

