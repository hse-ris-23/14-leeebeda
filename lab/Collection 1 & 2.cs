using System.Collections.Generic;

namespace ClassLib
    {
        public class ConcertParticipant
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public ConcertParticipant(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Age: {Age}";
            }
        }

        public class MusicBand
        {
            public string BandName { get; set; }
            public List<ConcertParticipant> Members { get; set; }

            public MusicBand(string bandName, List<ConcertParticipant> members)
            {
                BandName = bandName;
                Members = members;
            }

            public override string ToString()
            {
                return $"BandName: {BandName}, Members: {string.Join(", ", Members)}";
            }
        }
    }