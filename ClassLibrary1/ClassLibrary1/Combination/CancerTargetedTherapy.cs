using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public class CancerTargetedTherapy
    {
        public List<string> PrintCancerTargetTherapy()
        {
            List<string> cancerTargetTherapies = new List<string>();
            List<string> incomplete = new List<string>();

            foreach (var monoclonalAntibodyCreative in Enum.GetValues(typeof(MonoclonalAntibodyCreative)))
            {
                incomplete.Add(monoclonalAntibodyCreative.ToString());
            }

            foreach (var monoclonalAntibodyTarget in Enum.GetValues(typeof(MonoclonalAntibodyTarget)))
            {
                incomplete.Add(monoclonalAntibodyTarget.ToString().Substring(0,2));
            }

            foreach (var monoclonalAntibodySubStem in Enum.GetValues(typeof(MonoclonalAntibodySubstem)))
            {
                incomplete.Add(monoclonalAntibodySubStem.ToString().Substring(0, 2));
            }

            incomplete.AddRange(
                MultiplyStem(Enum.GetValues(typeof(MonoclonalAntibodyCreative)).Length, typeof(MonoclonalAntibodyStem)));

            int back = 0;

            for (int i = 0; i < incomplete.Count; i++)
            {
                if (i < Enum.GetValues(typeof(MonoclonalAntibodyCreative)).Length)
                {
                    cancerTargetTherapies.Add("");
                }
                else if ((back + 1) % Enum.GetValues(typeof(MonoclonalAntibodyCreative)).Length != 0)
                {
                    if (back < Enum.GetValues(typeof(MonoclonalAntibodyCreative)).Length) { }
                    else { back = 0; }
                }
                cancerTargetTherapies[back] += incomplete[i];
                back++;
            }

            return cancerTargetTherapies;
        }

        public List<string> PrintCancerInhibitorTherapy()
        {
            List<string> cancerInhibitors = new List<string>();
            List<string> incomplete = new List<string>();

            foreach (var smallMoleculeInhibitorCreative in Enum.GetValues(typeof(SmallMoleculeInhibitorCreative)))
            {
                incomplete.Add(smallMoleculeInhibitorCreative.ToString());
            }

            foreach (var inhibition in Enum.GetValues(typeof(Inhibition)))
            {
                incomplete.Add(inhibition.ToString().Split('_')[0]);
            }

            incomplete.AddRange(
                MultiplyStem(Enum.GetValues(typeof(SmallMoleculeInhibitorCreative)).Length, typeof(SmallMoleculeInhibitorStem)));

            int back = 0;

            for (int i = 0; i < incomplete.Count;  i++)
            {
                if (i < Enum.GetValues(typeof(MonoclonalAntibodyCreative)).Length)
                {
                    cancerInhibitors.Add("");
                }
                else if ((back + 1) % Enum.GetValues(typeof(MonoclonalAntibodyCreative)).Length != 0)
                {
                    if (back < Enum.GetValues(typeof(MonoclonalAntibodyCreative)).Length) { }
                    else { back = 0; }
                }
                cancerInhibitors[back] += incomplete[i];
                back++;
            }

            return cancerInhibitors;
        }

        private List<string> MultiplyStem(int creativeCount, Type type)
        {
            List<string> stems = new List<string>();
            for (int i = 0; i < creativeCount; i++)
            {
                foreach (var monoclonalAntibodySubStem in Enum.GetValues(type))
                {
                    stems.Add(monoclonalAntibodySubStem.ToString());
                }
            }
            return stems;
        }
    }
}
