using System;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    public static class CreditTypesDataGridViewHelper
    {
        public static class CreditTypes
        {
            public const String Direction = "Direction";
            public const String Writing = "Writing";
            public const String Production = "Production";
            public const String Cinematography = "Cinematography";
            public const String FilmEditing = "Film Editing";
            public const String Music = "Music";
            public const String Sound = "Sound";
            public const String Art = "Art";
            public const String Other = "Other";
        }

        public static class CreditSubtypes
        {
            public const String Custom = "Custom";

            public static class Direction
            {
                public const String Director = "Director";
            }

            public static class Writing
            {
                public const String OriginalMaterialBy = "Original Material By";
                public const String Screenwriter = "Screenwriter";
                public const String Writer = "Writer";
                public const String OriginalCharactersBy = "Original Characters By";
                public const String CreatedBy = "Created By";
                public const String StoryBy = "Story By";
            }

            public static class Production
            {
                public const String Producer = "Producer";
                public const String ExecutiveProducer = "Executive Producer";
            }

            public static class Cinematography
            {
                public const String DirectorOfPhotography = "Director of Photography";
                public const String Cinematographer = "Cinematographer";
            }

            public static class FilmEditing
            {
                public const String FilmEditor = "Film Editor";
            }

            public static class Music
            {
                public const String Composer = "Composer";
                public const String SongWriter = "Song Writer";
                public const String ThemeBy = "Theme By";
            }

            public static class Sound
            {
                public const String _Sound = "Sound";
                public const String SoundDesigner = "Sound Designer";
                public const String SupervisingSoundEditor = "Supervising Sound Editor";
                public const String SoundEditor = "Sound Editor";
                public const String SoundReRecordingMixer = "Sound Re-Recording Mixer";
                public const String ProductionSoundMixer = "Production Sound Mixer";
            }

            public static class Art
            {
                public const String ProductionDesigner = "Production Designer";
                public const String ArtDirector = "Art Director";
                public const String CostumeDesigner = "Costume Designer";
                public const String MakeUpArtist = "Make-up Artist";
                public const String VisualEffects = "Visual Effects";
                public const String MakeUpEffects = "Make-up Effects";
                public const String CreatureDesign = "Creature Design";
            }
        }

        public static void FillCreditSubtypes(String creditType, DataGridViewComboBoxCell.ObjectCollection items)
        {
            items.Clear();
            switch(creditType)
            {
                case (CreditTypes.Direction):
                {
                    items.Add(CreditSubtypes.Direction.Director);
                    break;
                }
                case (CreditTypes.Writing):
                {
                    items.Add(CreditSubtypes.Writing.OriginalMaterialBy);
                    items.Add(CreditSubtypes.Writing.Screenwriter);
                    items.Add(CreditSubtypes.Writing.Writer);
                    items.Add(CreditSubtypes.Writing.OriginalCharactersBy);
                    items.Add(CreditSubtypes.Writing.CreatedBy);
                    items.Add(CreditSubtypes.Writing.StoryBy);
                    break;
                }
                case (CreditTypes.Production):
                {
                    items.Add(CreditSubtypes.Production.Producer);
                    items.Add(CreditSubtypes.Production.ExecutiveProducer);
                    break;
                }
                case (CreditTypes.Cinematography):
                {
                    items.Add(CreditSubtypes.Cinematography.DirectorOfPhotography);
                    items.Add(CreditSubtypes.Cinematography.Cinematographer);
                    break;
                }
                case (CreditTypes.FilmEditing):
                {
                    items.Add(CreditSubtypes.FilmEditing.FilmEditor);
                    break;
                }
                case (CreditTypes.Music):
                {
                    items.Add(CreditSubtypes.Music.Composer);
                    items.Add(CreditSubtypes.Music.SongWriter);
                    items.Add(CreditSubtypes.Music.ThemeBy);
                    break;
                }
                case (CreditTypes.Sound):
                {
                    items.Add(CreditSubtypes.Sound._Sound);
                    items.Add(CreditSubtypes.Sound.SoundDesigner);
                    items.Add(CreditSubtypes.Sound.SupervisingSoundEditor);
                    items.Add(CreditSubtypes.Sound.SoundEditor);
                    items.Add(CreditSubtypes.Sound.SoundReRecordingMixer);
                    items.Add(CreditSubtypes.Sound.ProductionSoundMixer);
                    break;

                }
                case (CreditTypes.Art):
                {
                    items.Add(CreditSubtypes.Art.ProductionDesigner);
                    items.Add(CreditSubtypes.Art.ArtDirector);
                    items.Add(CreditSubtypes.Art.CostumeDesigner);
                    items.Add(CreditSubtypes.Art.MakeUpArtist);
                    items.Add(CreditSubtypes.Art.VisualEffects);
                    items.Add(CreditSubtypes.Art.MakeUpEffects);
                    items.Add(CreditSubtypes.Art.CreatureDesign);
                    break;
                }
                case (CreditTypes.Other):
                {
                    break;
                }
            }
            items.Add(CreditSubtypes.Custom);
        }

        public static void AllowAllCreditSubtypes(DataGridViewComboBoxCell.ObjectCollection items)
        {
            items.Clear();
            items.Add(CreditSubtypes.Direction.Director);
            items.Add(CreditSubtypes.Writing.OriginalMaterialBy);
            items.Add(CreditSubtypes.Writing.Screenwriter);
            items.Add(CreditSubtypes.Writing.Writer);
            items.Add(CreditSubtypes.Writing.OriginalCharactersBy);
            items.Add(CreditSubtypes.Writing.CreatedBy);
            items.Add(CreditSubtypes.Writing.StoryBy);
            items.Add(CreditSubtypes.Production.Producer);
            items.Add(CreditSubtypes.Production.ExecutiveProducer);
            items.Add(CreditSubtypes.Cinematography.DirectorOfPhotography);
            items.Add(CreditSubtypes.Cinematography.Cinematographer);
            items.Add(CreditSubtypes.FilmEditing.FilmEditor);
            items.Add(CreditSubtypes.Music.Composer);
            items.Add(CreditSubtypes.Music.SongWriter);
            items.Add(CreditSubtypes.Music.ThemeBy);
            items.Add(CreditSubtypes.Sound._Sound);
            items.Add(CreditSubtypes.Sound.SoundDesigner);
            items.Add(CreditSubtypes.Sound.SupervisingSoundEditor);
            items.Add(CreditSubtypes.Sound.SoundEditor);
            items.Add(CreditSubtypes.Sound.SoundReRecordingMixer);
            items.Add(CreditSubtypes.Sound.ProductionSoundMixer);
            items.Add(CreditSubtypes.Art.ProductionDesigner);
            items.Add(CreditSubtypes.Art.ArtDirector);
            items.Add(CreditSubtypes.Art.CostumeDesigner);
            items.Add(CreditSubtypes.Art.MakeUpArtist);
            items.Add(CreditSubtypes.Art.VisualEffects);
            items.Add(CreditSubtypes.Art.MakeUpEffects);
            items.Add(CreditSubtypes.Art.CreatureDesign);
            items.Add(CreditSubtypes.Custom);
        }
    }
}
