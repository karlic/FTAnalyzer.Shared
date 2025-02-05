﻿using System.Xml;

namespace FTAnalyzer
{
    public class ParentalRelationship
    {
        public enum ParentalRelationshipType { NATURAL, ADOPTED, STEP, FOSTER, RELATED, GUARDIAN, SEALED, PRIVATE, UNKNOWN };

        public Family Family { get; private set; }
        public ParentalRelationshipType FatherRelationship { get; private set; }
        public ParentalRelationshipType MotherRelationship { get; private set; }

        public ParentalRelationship(Family family, ParentalRelationshipType fatherRel, ParentalRelationshipType motherRel)
        {
            Family = family;
            FatherRelationship = fatherRel;
            MotherRelationship = motherRel;
        }

        public bool IsNaturalFather
        {
            get
            {
                return Father !=null && 
                      (FatherRelationship == ParentalRelationshipType.NATURAL ||
                       FatherRelationship == ParentalRelationshipType.UNKNOWN ||
                       FatherRelationship == ParentalRelationshipType.PRIVATE);
            }
        }

        public bool IsNaturalMother
        {
            get
            {
                return Mother is not null && 
                        (MotherRelationship == ParentalRelationshipType.NATURAL || 
                         MotherRelationship == ParentalRelationshipType.UNKNOWN ||
                         MotherRelationship == ParentalRelationshipType.PRIVATE);
            }
        }

        public Individual? Father { get { return Family?.Husband; } }
        public Individual? Mother { get { return Family?.Wife; } }

        public static ParentalRelationshipType GetRelationshipType(XmlNode node)
        {
            if (node is null)
                return ParentalRelationshipType.UNKNOWN;
            return node.InnerText.ToLower() switch
            {
                "natural" => ParentalRelationshipType.NATURAL,
                "adopted" => ParentalRelationshipType.ADOPTED,
                "step" => ParentalRelationshipType.STEP,
                "foster" => ParentalRelationshipType.FOSTER,
                "related" => ParentalRelationshipType.RELATED,
                "guardian" => ParentalRelationshipType.GUARDIAN,
                "sealed" => ParentalRelationshipType.SEALED,
                "private" => ParentalRelationshipType.PRIVATE,
                _ => ParentalRelationshipType.UNKNOWN,
            };
        }
    }
}
