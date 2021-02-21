using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    public static class CreditTypesDataGridViewHelper
    {
        public static class CreditTypes
        {
            public const string Direction = "Direction";
            public const string Writing = "Writing";
            public const string Production = "Production";
            public const string Cinematography = "Cinematography";
            public const string FilmEditing = "Film Editing";
            public const string Music = "Music";
            public const string Sound = "Sound";
            public const string Art = "Art";
            public const string Other = "Other";
        }

        public static class CreditSubtypes
        {
            public const string Custom = "Custom";

            public static class Direction
            {
                public const string Director = "Director";
            }

            public static class Writing
            {
                public const string OriginalMaterialBy = "Original Material By";
                public const string Screenwriter = "Screenwriter";
                public const string Writer = "Writer";
                public const string OriginalCharactersBy = "Original Characters By";
                public const string CreatedBy = "Created By";
                public const string StoryBy = "Story By";
                public const string DevelopedBy = "Developed By";
            }

            public static class Production
            {
                public const string Producer = "Producer";
                public const string ExecutiveProducer = "Executive Producer";
            }

            public static class Cinematography
            {
                public const string DirectorOfPhotography = "Director of Photography";
                public const string Cinematographer = "Cinematographer";
            }

            public static class FilmEditing
            {
                public const string FilmEditor = "Film Editor";
            }

            public static class Music
            {
                public const string Composer = "Composer";
                public const string SongWriter = "Song Writer";
                public const string ThemeBy = "Theme By";
            }

            public static class Sound
            {
                public const string _Sound = "Sound";
                public const string SoundDesigner = "Sound Designer";
                public const string SupervisingSoundEditor = "Supervising Sound Editor";
                public const string SoundEditor = "Sound Editor";
                public const string SoundReRecordingMixer = "Sound Re-Recording Mixer";
                public const string ProductionSoundMixer = "Production Sound Mixer";
            }

            public static class Art
            {
                public const string ProductionDesigner = "Production Designer";
                public const string ArtDirector = "Art Director";
                public const string CostumeDesigner = "Costume Designer";
                public const string MakeUpArtist = "Make-up Artist";
                public const string VisualEffects = "Visual Effects";
                public const string MakeUpEffects = "Make-up Effects";
                public const string CreatureDesign = "Creature Design";
            }
        }

        public static void FillCreditSubtypes(string creditType, DataGridViewComboBoxCell.ObjectCollection items)
        {
            items.Clear();
            switch (creditType)
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
                        items.Add(CreditSubtypes.Writing.DevelopedBy);
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
            items.Add(CreditSubtypes.Writing.DevelopedBy);
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