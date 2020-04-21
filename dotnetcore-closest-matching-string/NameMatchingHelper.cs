using dotnetcore_closest_matching_string.Helpers;
using dotnetcore_closest_matching_string.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnetcore_closest_matching_string
{
    public class NameMatchingHelper<T> where T : StringMatchingComparable
    {
        NameMatchingSettings _settings;

        public NameMatchingHelper(NameMatchingSettings settings)
        {
            _settings = settings;
        }

        public T GetSpecificWithName(string name, List<T> elements)
        {
            name = name.ToLower();

            // Match 1:1
            var elementByFullNameExactMatch = elements.FirstOrDefault(t => t.ComparableString.ToLower().Trim() == name.Trim());
            if (elementByFullNameExactMatch != null)
            {
                return elementByFullNameExactMatch;
            }


            // Find closest match using Levenshtein Distance algorithm based on full name
            var closestElementOnFullName = getElementClosestToName(name, elements);
            if (closestElementOnFullName != null)
            {
                return closestElementOnFullName;
            }



            string[] possibleNamesToLookFor = StringHelpers.GetAllArrayCombinations_Recursive(name.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));


            // Look for any exact matches with any combination of names
            var possibleMatches = 0;
            T matchingElement = null;

            foreach (var possibleName in possibleNamesToLookFor)
            {
                // Skip if only one name
                if (possibleName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length <= 1)
                    continue;

                foreach (var element in elements)
                {
                    // Skip if only one name
                    if (element.ComparableString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length <= 1)
                        continue;

                    var elementName = element.ComparableString.ToLower();
                    string[] possibleElementNames = StringHelpers.GetAllArrayCombinations_Recursive(elementName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

                    foreach (var matchingName in possibleElementNames)
                    {
                        if (matchingName.Equals(possibleName))
                        {
                            possibleMatches++;
                            matchingElement = element;
                            break;
                        }
                    }

                }
            }

            if (matchingElement != null && possibleMatches == 1)
            {
                return matchingElement;
            }



            // Look for any matches with any combination of names, with up to X errors
            possibleMatches = 0;
            matchingElement = null;

            foreach (var possibleName in possibleNamesToLookFor)
            {
                // Skip if only one name
                if (possibleName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length <= 1)
                    continue;

                foreach (var element in elements)
                {
                    // Skip if only one name
                    if (element.ComparableString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length <= 1)
                        continue;

                    var elementName = element.ComparableString.ToLower();
                    string[] possibleElementNames = StringHelpers.GetAllArrayCombinations_Recursive(elementName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

                    foreach (var matchingName in possibleElementNames)
                    {
                        var distance = LevenshteinDistance.Calculate(possibleName, matchingName);
                        if (distance <= _settings.AcceptedNumberOfErrors)
                        { 
                            if (matchingElement != null && matchingElement == element)
                            {
                                continue;
                            }
                            matchingElement = element;
                            possibleMatches++;
                        }
                    }

                }
            }

            if (matchingElement != null && (possibleMatches == 1 || _settings.AcceptMultipleResults))
            {
                return matchingElement;
            }

            return null;
        }

        private T getElementClosestToName(string name, List<T> elements)
        {
            T closestElement = default(T);
            int lowestDistance = 100;
            int resultsFound = 0;

            foreach (var element in elements)
            {
                var nameFinal = element.ComparableString.ToLower();

                var distance = LevenshteinDistance.Calculate(name, nameFinal);
                if (distance < lowestDistance)
                {
                    lowestDistance = distance;
                    closestElement = element;
                    if (distance <= _settings.AcceptedNumberOfErrors)
                    {
                        resultsFound++;
                    }
                }
            }

            if (lowestDistance <= _settings.AcceptedNumberOfErrors && (resultsFound == 1 || _settings.AcceptMultipleResults))
            {
                return closestElement;
            }

            return default(T);
        }
    }
}
