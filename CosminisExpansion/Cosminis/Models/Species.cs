using System;
using System.Collections.Generic;
using CustomExceptions;

namespace Models
{
    public enum ElementSpecies              
    {
        Volcanic, Glacial, Forest, Sky, Holy, Dark
    }

    public class SpeciesWithEnum
    { 
        /// <summary>
        /// Takes a ElementSpecies and return the string equivalent of the enumeration
        /// </summary>
        /// <param name="elementSpecies"></param>
        /// <returns>String of the particular element</returns>
        /// <exception cref="ElementNotFound">Occurs if no element exist matching the given input</exception>
        public string ElementToString(ElementSpecies elementSpecies)                                   
        {
            Dictionary<ElementSpecies,string> dictElement = new Dictionary<ElementSpecies, string>()
            {
                {ElementSpecies.Volcanic, "Volcanic"},
                {ElementSpecies.Glacial, "Glacial"},
                {ElementSpecies.Forest, "Forest"},
                {ElementSpecies.Sky, "Sky"},
                {ElementSpecies.Holy, "Holy"},
                {ElementSpecies.Dark, "Dark"}
            };

            if(dictElement.ContainsKey(elementSpecies))
            {
                return dictElement[elementSpecies];
            };
            throw new SpeciesNotFound();
        }

        public SpeciesWithEnum()
        {
            
        }

        public int SpeciesId { get; set; }
        public int FoodElementIdFk { get; set; }
        public string SpeciesName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? BaseStr { get; set; }
        public int? BaseDex { get; set; }
        public int? BaseInt { get; set; }
        public ElementSpecies ElementType { get; set; }
    }
    /*  This toString is just serving as an example for other places we might use it.
    public override string ToString()
    { 
        return $"UserId: {this.userId}, Legal Name: {this.legalName}, Userame: {this.userName}, Role: {RoleToString(this.role)}";
    }
    */   
}
