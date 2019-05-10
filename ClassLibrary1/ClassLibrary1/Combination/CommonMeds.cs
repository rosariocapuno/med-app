using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ClassLibrary1
{
    public class CommonMeds
    {
        public List<Type> GetTypes()
        {
            List<Type> commonTypes = new List<Type>();

            commonTypes.AddRange(Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsEnum && t.Namespace == "Common"));

            return commonTypes;
        }

        /// <summary>
        /// Filename makes the enum grouped by its purpose
        /// </summary>
        /// <returns></returns>
        private List<string> GetFileNames()
        {
            List<string> fileNames = new List<string>();
            var currentFileName = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            DirectoryInfo directoryInfo = new DirectoryInfo(currentFileName);

            foreach (var file in directoryInfo.Parent
                                              .Parent.GetDirectories()
                                              .Where(d => d.Name == "Enum")
                                              .First()
                                              .GetDirectories()
                                              .Where(d => d.Name == "Common")
                                              .First()
                                              .GetFiles())
            {
                fileNames.Add(file.Name.Replace(".cs", ""));
            }

            return fileNames;
        }

        public List<string> Print()
        {
            List<string> allCommonMeds = new List<string>();
            bool duplicateAdded = false;

            foreach(var purpose in GetFileNames())
            {
                List<string> commonMeds = new List<string>();
                List<string> incomplete = new List<string>();

                var purposeTypes = GetTypes().Where(t => t.Name.Contains(purpose)).ToList();

                if (duplicateAdded && purposeTypes.Where(t => t.Name.Contains("II")).FirstOrDefault() != null)
                {
                }
                else
                {
                    if (purposeTypes.Where(t => t.Name.Contains("II")).FirstOrDefault() == null) { }
                    else
                    {
                        duplicateAdded = true;
                        purposeTypes.RemoveAll(t => t.Name.Contains("II"));
                    }
                }

                var prefix = purposeTypes.Where(t => t.Name.Contains("Prefix")).FirstOrDefault();

                var creativeType = purposeTypes.Where(t => t.Name.Contains("Creative")).First().UnderlyingSystemType;

                var root = purposeTypes.Where(t => t.Name.Contains("Root")).FirstOrDefault();

                var substem = purposeTypes.Where(t => t.Name.Contains("Substem"));

                var stem = purposeTypes.Where(t => t.Name.Contains("Stem")).FirstOrDefault();

                var suffix = purposeTypes.Where(t => t.Name.Contains("Suffix")).FirstOrDefault();

                var creativeTypeValues = Enum.GetValues(creativeType);

                var creativeTypeValuesLength = creativeTypeValues.Length;

                if (prefix == null)
                {

                }
                else
                {
                    incomplete.AddRange(
                        MultiplyIdentifier(creativeTypeValues.Length, prefix.UnderlyingSystemType));
                }

                foreach (var creative in creativeTypeValues)
                {
                    incomplete.Add(creative.ToString());
                }

                if (root == null)
                {

                }
                else
                {
                    incomplete.AddRange(
                    MultiplyIdentifier(creativeTypeValuesLength, root.UnderlyingSystemType));
                }

                if (substem.FirstOrDefault() == null)
                {

                }
                else
                {

                }

                //introduced suffix might be a stem
                if (stem == null && suffix == null)
                {

                }
                else if (stem != null && suffix != null)
                {
                    var creativeCount = creativeTypeValuesLength;
                    foreach (var suffixTypeValues in Enum.GetValues(suffix.UnderlyingSystemType))
                    {
                        incomplete.Add(suffixTypeValues.ToString());
                        creativeCount--;
                    }

                    incomplete.AddRange(
                    MultiplyIdentifier(creativeCount, stem.UnderlyingSystemType));
                }
                else if (stem != null && suffix == null)
                {
                    incomplete.AddRange(
                    MultiplyIdentifier(creativeTypeValuesLength, stem.UnderlyingSystemType));
                }

                int back = 0;

                for (int i = 0; i < incomplete.Count; i++)
                {
                    if (i < creativeTypeValues.Length)
                    {
                        commonMeds.Add("");
                    }
                    else if ((back + 1) % creativeTypeValuesLength != 0)
                    {
                        if (back < creativeTypeValuesLength) { }
                        else { back = 0; }
                    }
                    commonMeds[back] += incomplete[i];

                    if (commonMeds[back].IndexOf("_") > -1)
                    {
                        commonMeds[back] = Exemption(commonMeds[back]);
                    }

                    back++;
                }

                allCommonMeds.AddRange(commonMeds);
            }

            return allCommonMeds;
        }

        private string Exemption(string wrongSpelling)
        {
            return wrongSpelling.Replace("_", "").Replace("f", "ph");
        }

        private List<string> MultiplyIdentifier(int creativeCount, Type type)
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
        private int CountValues(Type type)
        {
            return Enum.GetValues(type).Length;
        }
    }
}
