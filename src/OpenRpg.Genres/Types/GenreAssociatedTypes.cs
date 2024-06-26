namespace OpenRpg.Genres.Types
{
    public interface GenreAssociatedTypes
    {
        public static readonly int UnknownAssociation = 0;
        
        public static readonly int ItemAssociation = 1;
        public static readonly int ClassAssociation = 2;
        public static readonly int RaceAssociation = 3;
        
        public static readonly int QuestAssociation = 10;
        
        public static readonly int NpcAssociation = 20;
        public static readonly int NpcDialogAssociation = 21;
        public static readonly int NpcDialogLinkAssociation = 22;
    }
}