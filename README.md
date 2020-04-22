
# dotnetcore-kNameMatching

.NET Core library for finding single closted name in a sequence of objects

## Matching names
This plugin is primarily intended for matching a name with a list of names. This is achieved in multiple steps (all case-insensitive):

 1. See if there's a perfect match
 2. Find the closest match using the Levenshtein Distance algorithm
 3. Take every combination of names in the input string, and match with every combination of names of every name in the list of strings. Note that only combinations that still follow the same order of names are considered, i.e. in the case of 'Anne Liv Jensen', the alternatives 'Anne Liv', 'Anne Jensen' and 'Liv Jensen' are considered, but not combinations such as 'Jensen Liv' or 'Liv Anne Jensen'.
 4. For each combination, if there's no perfect match, the plugin with attempt to find the closest match using the Levenshtein Distance algorithm

## How to
The matching helper needs to know what string to compare, so your model needs to inherit the 'StringMatchingComparable' model. The `ComparableString` will be used to compare names.

To initialise the matcher, you must provide a `NameMatchingSettings` object.

    _nameMatchingHelper = new  NameMatchingHelper<MockObject>(new NameMatchingSettings
    {
		AcceptedNumberOfErrors = 2,
		AcceptMultipleResults = false
	});

To perform the matching, run the following:

    var  name = "Aina Maximilian Theresa";
    var  result = _nameMatchingHelper.GetSpecificWithName(name, _elements);

where `_elements` is the list of all possible names provided as `StringMatchingComparable` inheriting objects.

Please refer to the included test library for a full example.

## Possible use-case
Parents were provided a link from a school. However, since it's not safe to list all possible students of the school, the parents were asked to type the name of their child. While the school may have been given the child's full legal name, the parents may not want to give up the full name of their child in an external system. We therefore want to help the parent to find their child, even though we know the child by 3, 4 or more names, but the parent only provides two. However, there's a fair chance of multiple hits, the more possible combinations and errors we allow, and therefore it's important to consider this possibility when defining settings for the plugin.

# Ideas for improvements

 - Refactor NameMatchingHelper to avoid code replication (i.e. full name
   test might use the same logic as the one used by each name
   combination).
   
 - Enable swapping out distance matching algorithm with alternative
   ones.
   
 - Change StringMatchingComparable to an interface instead of class Take
   into account the length of the string, when determining how many
   spelling mistakes to accept.

# Contributors
The main contributor of this plugin is Eftertid (DANSK SKOLEFOTO ApS, Kontrafej ApS).